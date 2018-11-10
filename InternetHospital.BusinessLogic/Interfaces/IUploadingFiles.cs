using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IUploadingFiles
    {
        Task<User> UploadAvatar(IFormFile image, User user);
    }
}
