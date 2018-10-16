using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IMailService
    {
        Task SendMsgToEmail(string email, string subject, string msg);
    }
}
