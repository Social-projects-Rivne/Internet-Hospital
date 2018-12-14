using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int RecepientId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime Date { get; set; }

        public virtual User Recepient { get; set; }
    }
}
