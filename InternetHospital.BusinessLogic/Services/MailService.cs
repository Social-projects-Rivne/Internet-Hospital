using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

using InternetHospital.BusinessLogic.Interfaces;

namespace InternetHospital.BusinessLogic.services
{
    public class MailService : IMailService
    {
        public async Task SendMsgToEmail(string email, string subject, string msg)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Internet Hospital", "internet.hospital.sender@gmail.com"));
            message.To.Add(new MailboxAddress(email));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = msg
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("internet.hospital.sender@gmail.com", "1234Passw@rd");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

        }
    }
}
