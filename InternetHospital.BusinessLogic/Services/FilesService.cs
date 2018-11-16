﻿using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Validation;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System;

namespace InternetHospital.BusinessLogic.Services
{
    public class FilesService : IFilesService
    {
        private readonly IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;

        const int MIN_HEIGHT = 150;
        const int MAX_HEIGHT = 3000;
        const int MIN_WIDTH = 150;
        const int MAX_WIDTH = 3000;
        const int IMAGE_MAX_LENGTH = 20;

        public FilesService(IHostingEnvironment env, UserManager<User> userManager)
        {
            _env = env;
            _userManager = userManager;
        }

        public async Task<User> UploadAvatar(IFormFile image, User user)
        {
            bool isValidImage = ImageValidation.IsValidImageFile(image, MIN_HEIGHT, MAX_HEIGHT, MIN_WIDTH, MAX_WIDTH) 
                && ImageValidation.IsImage(image);

            if (!isValidImage)
            {
                return null;
            }

            string webRootPath = _env.WebRootPath;
            string folderName = "Images";
            string avatarFolder = "Avatar";
            var fileDestDir = Path.Combine(webRootPath, folderName, user.UserName, avatarFolder);

            if (!Directory.Exists(fileDestDir))
            {
                Directory.CreateDirectory(fileDestDir);
            }
            
            var fileExtesion = Path.GetExtension(image.FileName);
            var fileName = Guid.NewGuid().ToString() + fileExtesion;
            var fileFullPath = Path.Combine(fileDestDir, fileName);

            using (var stream = new FileStream(fileFullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream); 
            }

            string pathFile = $"/{folderName}/{user.UserName}/{avatarFolder}/{fileName}";

            user.AvatarURL = pathFile;
            await _userManager.UpdateAsync(user);

            return user;
        }

        public async Task<User> UploadPassport(IFormFileCollection images, User user)
        {
            bool isValidImage = false;
            foreach (var image in images)
            {
                isValidImage = ImageValidation.IsValidImageFile(image, MIN_HEIGHT, MAX_HEIGHT, MIN_WIDTH, MAX_WIDTH)
                    && ImageValidation.IsImage(image);

                if (!isValidImage)
                {
                    return null;
                }
            }

            string webRootPath = _env.WebRootPath;
            string folderName = "Images";
            string passportFolder = "Passport";
            var fileDestDir = Path.Combine(webRootPath, folderName, user.UserName, passportFolder);

            if (!Directory.Exists(fileDestDir))
            {
                Directory.CreateDirectory(fileDestDir);
            }

            string[] pathFile = new string[images.Count];
            for (int i = 0; i < images.Count; i++)
            {
                var fileExtesion = Path.GetExtension(images[i].FileName);
                var fileName = $"Passport_{i + 1}" + fileExtesion;
                var fileFullPath = Path.Combine(fileDestDir, fileName);

                using (var stream = new FileStream(fileFullPath, FileMode.Create))
                {
                    await images[i].CopyToAsync(stream);
                }
                pathFile[i] = $"/{folderName}/{user.UserName}/{passportFolder}/{fileName}";
            }
            string json = JsonConvert.SerializeObject(pathFile);
            user.PassportURL = json;

            return user;
        }
    }
}