using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.Entities 
{
    public class Feedback 
    {
        public int Id { set; get; }
        public int TypeId { set; get; }
        public int UserId { set; get; }
        public string Text { set; get; }
        public string Reply { set; get; }
        public bool IsViewed { set; get; }
        public DateTime DateTime { set; get; }

        public virtual FeedbackType Type { set; get; }
        public virtual User User { set; get; }
    }
}
