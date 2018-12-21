using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models.Articles
{
    public class HomePageArticlesModel
    {
        public ICollection<HomePageArticleModel> Articles { get; set; }
        public int LastArticleId { get; set; }
        public bool IsLast { get; set; }
    }
}
