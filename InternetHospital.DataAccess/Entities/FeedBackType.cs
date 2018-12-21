using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.Entities 
{
    public class FeedbackType 
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public virtual ICollection<Feedback> Feedbacks { set; get; }
    }
}
