using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.BusinessLogic.Models.Articles
{
    public class HomePageArticleModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Author { get; set; }
        public DateTime DateOfCreation { get; set; }
        public ICollection<string> Types { get; set; }
        public ICollection<string> PreviewImageUrls { get; set; }
    }
}
