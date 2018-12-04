using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace InternetHospital.BusinessLogic.Services
{
    public class FilesService : IFilesService
    {
        private readonly IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;

        const int MIN_HEIGHT = 150;
        const int MAX_HEIGHT = 3000;
        const int MIN_WIDTH = 150;
        const int MAX_WIDTH = 3000;
        const int IMAGE_MAX_LENGTH = 20;
        const string PASSPORT = "Passport";
        const string DIPLOMA = "Diploma";
        const string LICENSE = "License";

        const string ARTICLE_ATTACHMENTS_FOLDER_NAME = "Attachments";
        const string HOME_PAGE = "HomePage";
        private const int PREVIEW_MIN_WIDTH = 300;
        private const int PREVIEW_MAX_WIDTH = 3000;
        private const int PREVIEW_MIN_HEIGHT = 300;
        private const int PREVIEW_MAX_HEIGHT = 3000;

        public FilesService(IHostingEnvironment env, UserManager<User> userManager, ApplicationContext context)
        {
            _env = env;
            _userManager = userManager;
            _context = context;
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

        public async Task<User> UploadPassport(IFormFileCollection images, User user, DateTime addedTime)
        {
            return images.Count == 0 ? user : await UploadFiles(images, user, addedTime, PASSPORT);
        }

        public async Task<User> UploadDiploma(IFormFileCollection images, User user, DateTime addedTime)
        {
            return images.Count == 0 ? user : await UploadFiles(images, user, addedTime, DIPLOMA);
        }

        public async Task<User> UploadLicense(IFormFileCollection images, User user, DateTime addedTime)
        {
            return images.Count == 0 ? user : await UploadFiles(images, user, addedTime, LICENSE);
        }

        public async Task<User> UploadFiles(IFormFileCollection images, User user, DateTime addedTime, string fileTypeFolder)
        {
            bool isValidImage;
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
            string addedTimeFolder = addedTime.ToString().Replace(':', '-');
            var fileDestDir = Path.Combine(webRootPath, folderName, user.UserName, fileTypeFolder, addedTimeFolder);

            if (!Directory.Exists(fileDestDir))
            {
                Directory.CreateDirectory(fileDestDir);
            }

            for (int i = 0; i < images.Count; i++)
            {
                var fileExtesion = Path.GetExtension(images[i].FileName);
                var fileName = $"{Guid.NewGuid().ToString()}_{i + 1}" + fileExtesion;
                var fileFullPath = Path.Combine(fileDestDir, fileName);
                var dbURL = $"/{folderName}/{user.UserName}/{fileTypeFolder}/{addedTimeFolder}/{fileName}";

                using (var stream = new FileStream(fileFullPath, FileMode.Create))
                {
                    await images[i].CopyToAsync(stream);
                }
                if (fileTypeFolder == PASSPORT)
                {
                    var passportPhoto = new Passport
                    {
                        UserId = user.Id,
                        PassportURL = dbURL,
                        AddedTime = addedTime
                    };
                    _context.Add(passportPhoto);
                }
                else if (fileTypeFolder == DIPLOMA)
                {
                    var diplomaPhoto = new Diploma
                    {
                        DoctorId = user.Id,
                        DiplomaURL = dbURL,
                        AddedTime = addedTime
                    };
                    _context.Add(diplomaPhoto);
                }
                else if (fileTypeFolder == LICENSE)
                {
                    user.Doctor.LicenseURL = dbURL;
                }
            }
            return user;
        }

        public IEnumerable<string> UploadArticleAttachment(IFormFile[] attachments, int articleId, bool isOnPreview)
        {
            var createdFileNames = new List<string>();
            if (attachments.Length > 0)
            {
                var fileDestDir = Path.Combine(_env.WebRootPath, HOME_PAGE, articleId.ToString(),
                    ARTICLE_ATTACHMENTS_FOLDER_NAME);

                if (!Directory.Exists(fileDestDir))
                {
                    Directory.CreateDirectory(fileDestDir);
                }

                for (int i = 0; i < attachments.Length; i++)
                {
                    if (IsValidArticleImageAttachment(attachments[i], isOnPreview))
                    {
                        string extension = attachments[i].FileName.Substring(attachments[i].FileName.LastIndexOf('.'));
                        var fileName = $"{Guid.NewGuid()}{extension}";
                        var fileFullPath = Path.Combine(fileDestDir, fileName);
                        using (var stream = new FileStream(fileFullPath, FileMode.Create))
                        {
                            attachments[i].CopyTo(stream);
                        }
                        createdFileNames.Add(fileName);
                    }
                }
            }

            return createdFileNames;
        }

        public IEnumerable<string> RemoveArticleAttachment(IEnumerable<string> attachmentPaths)
        {
            var removedUrls = new List<string>();
            foreach (var attachmentPath in attachmentPaths)
            {
                var fileFullPath = _env.WebRootPath + attachmentPath.Replace("/", "\\");
                if (File.Exists(fileFullPath))
                {
                    File.Delete(fileFullPath);
                    removedUrls.Add(attachmentPath);
                }
            }

            return removedUrls;
        }

        public IEnumerable<string> GetBase64StringsFromAttachments(IEnumerable<string> attachmentPaths)
        {
            var base64Imgs = new List<string>();
            foreach (var attachmentPath in attachmentPaths)
            {
                var filePath = $"{_env.WebRootPath}/{attachmentPath}";
                var byteArr = File.ReadAllBytes(filePath);
                var format = $"data:image/{filePath.Substring(filePath.LastIndexOf('.') + 1)}";
                var base64 = $"{format};base64,{Convert.ToBase64String(byteArr)}";
                base64Imgs.Add(base64);
            }

            return base64Imgs;
        }

        private bool IsValidArticleImageAttachment(IFormFile file, bool isOnPreview)
        {
            bool isValidFile;
            if (isOnPreview)
            {
                isValidFile = ImageValidation.IsValidImageFile(file, PREVIEW_MIN_HEIGHT,
                    PREVIEW_MAX_HEIGHT, PREVIEW_MIN_WIDTH, PREVIEW_MAX_WIDTH);
            }
            else
            {
                isValidFile = ImageValidation.IsImage(file);
            }

            return isValidFile;
        }
    }
}
