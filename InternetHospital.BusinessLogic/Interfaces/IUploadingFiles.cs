using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IUploadingFiles
    {
        Task<User> UploadAvatar(IFormFile image, User user);
    }
}
