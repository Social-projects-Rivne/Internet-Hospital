using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Validation;

namespace InternetHospital.BusinessLogic.Services
{
    public class UploadingService : IUploadingFiles
    {
        private IHostingEnvironment _env;
        private ApplicationContext _context;
        private UserManager<User> _userManager;

        public UploadingService(IHostingEnvironment env, ApplicationContext context, UserManager<User> userManager)
        {
            _env = env;
            _context = context;
            _userManager = userManager;

        }

        public async Task<User> UploadAvatar(IFormFile image, User user)
        {
            const int MIN_HEIGHT = 150;
            const int MAX_HEIGHT = 3000;
            const int MIN_WIDTH = 150;
            const int MAX_WIDTH = 3000;
            const int IMAGE_MAX_LENGTH = 20;

            bool isValiImage = ImageValidation.IsValidImageFile(image, MIN_HEIGHT, MAX_HEIGHT, MIN_WIDTH, MAX_WIDTH) 
                && ImageValidation.IsImage(image);

            if (!isValiImage)
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

            int fileNameLength = user.UserName.Length < IMAGE_MAX_LENGTH ? user.UserName.Length : IMAGE_MAX_LENGTH;
            var fileExtesion = Path.GetExtension(image.FileName);
            var fileName = Guid.NewGuid().ToString() + fileExtesion;
            var fileFullPath = Path.Combine(fileDestDir, fileName);

            using (var stream = new FileStream(fileFullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream); 
            }

            string pathFile = $"/{folderName}/{user.UserName}/{avatarFolder}/{fileName}";

            user.AvatarURL = pathFile;
            var result = await _userManager.UpdateAsync(user);

            return user;
        }
    }
}
