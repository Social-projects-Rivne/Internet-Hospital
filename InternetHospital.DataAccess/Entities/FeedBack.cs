using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.Entities 
{
    public class FeedBack 
    {
        public int Id { set; get; }
        public string Text { set; get; }
        public FeedBackType Type { set; get; }
        public User User { set; get; }
        public DateTime DateTime { set; get; }
    }
}
