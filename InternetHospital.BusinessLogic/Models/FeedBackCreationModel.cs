using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models
{
    public class FeedbackCreationModel
    {
        public string Text { get; set; }
        public int TypeId { set; get; }
        public int UserId { set; get; }
    }
}
