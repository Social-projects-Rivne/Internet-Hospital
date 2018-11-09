﻿using System;

namespace InternetHospital.BusinessLogic.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserSecondName { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}