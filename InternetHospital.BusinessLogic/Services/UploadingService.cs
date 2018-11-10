using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Validation;
using Newtonsoft.Json;

namespace InternetHospital.BusinessLogic.Services
{
    public class UploadingService : IUploadingFiles
    {
        private readonly IHostingEnvironment _env;
        private readonly ApplicationContext _context;

        const int MIN_HEIGHT = 150;
        const int MAX_HEIGHT = 3000;
        const int MIN_WIDTH = 150;
        const int MAX_WIDTH = 3000;
        const int IMAGE_MAX_LENGTH = 20;

        public UploadingService(IHostingEnvironment env, ApplicationContext context)
        {
            _env = env;
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

            int fileNameLength = user.UserName.Length < IMAGE_MAX_LENGTH ? user.UserName.Length : IMAGE_MAX_LENGTH;
            var fileExtesion = Path.GetExtension(image.FileName);
            var fileName = user.UserName.Substring(0, fileNameLength) + fileExtesion;
            var fileFullPath = Path.Combine(fileDestDir, fileName);

            using (var stream = new FileStream(fileFullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream); 
            }

            string pathFile = $"/{folderName}/{user.UserName}/{avatarFolder}/{fileName}";
            _context.Users.Update(user);
            user.AvatarURL = pathFile; 
            await _context.SaveChangesAsync();

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
