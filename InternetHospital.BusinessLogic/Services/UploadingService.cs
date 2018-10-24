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

namespace InternetHospital.BusinessLogic.services
{
    public class UploadingService : IUploadingFiles
    {
        private IHostingEnvironment _env;
        private ApplicationContext _context;
        private UserManager<User> _userManager;

        public UploadingService(IHostingEnvironment env, ApplicationContext context, 
            UserManager<User> userManager)
        {
            _env = env;
            _context = context;
            _userManager = userManager;
        }

        public async Task<User> UploadAvatar(IFormFile image, User user)
        {
            var isValiImage = ImageValidation.IsValidImageFile(image, 150, 3000, 150, 3000);

            if (!isValiImage)
            {
                return null;
            }

            string webRootPath = _env.WebRootPath;
            string folderName = "Images";
            var fileDestDir = Path.Combine(webRootPath, folderName, user.UserName, "Avatar");

            if (!Directory.Exists(fileDestDir))
            {
                Directory.CreateDirectory(fileDestDir);
            }

            var fileFullPath = Path.Combine(fileDestDir, image.FileName);

            using (var stream = new FileStream(fileFullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream); 
            }

            _context.Users.Update(user);
            user.AvatarURL = fileFullPath; 
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
