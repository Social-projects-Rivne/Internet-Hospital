using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IFilesService
    {
        Task<User> UploadAvatar(IFormFile image, User user);
        Task<User> UploadPassport(IFormFileCollection images, User user);
    }
}
