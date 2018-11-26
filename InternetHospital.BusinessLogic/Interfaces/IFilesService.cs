using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IFilesService
    {
        Task<User> UploadAvatar(IFormFile image, User user);
        Task<User> UploadPassport(IFormFileCollection images, User user, DateTime addedTime);
        Task<User> UploadDiploma(IFormFileCollection images, User user, DateTime addedTime);
        Task<User> UploadLicense(IFormFileCollection images, User user, DateTime addedTime);

        Task<bool> UploadArticlePhotosAsync(IFormFile[] previewImages, IFormFile[] articleImages, int articleId);
    }
}
