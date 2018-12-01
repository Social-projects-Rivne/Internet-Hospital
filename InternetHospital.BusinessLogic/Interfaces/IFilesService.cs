using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IFilesService
    {
        Task<User> UploadAvatar(IFormFile image, User user);
        Task<User> UploadPassport(IFormFileCollection images, User user, DateTime addedTime);
        Task<User> UploadDiploma(IFormFileCollection images, User user, DateTime addedTime);
        Task<User> UploadLicense(IFormFileCollection images, User user, DateTime addedTime);

        void UploadArticleAttachments(IFormFile[] previewImages, IFormFile[] articleImages, int articleId);
        IEnumerable<string> GetBase64StringsFromAttachments(IEnumerable<string> attachmentPaths);
        void RemoveArticleAttachment(string[] attachmentPaths);
    }
}
