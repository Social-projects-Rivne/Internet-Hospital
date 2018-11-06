using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.Entities 
{
    public class FeedBackType 
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public virtual ICollection<FeedBack> FeedBacks { set; get; }
    }
}
