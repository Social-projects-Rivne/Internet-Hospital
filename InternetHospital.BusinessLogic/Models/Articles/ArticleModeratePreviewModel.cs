using System;
using System.Collections.Generic;

namespace InternetHospital.BusinessLogic.Models.Articles
{
    public class ArticleModeratePreviewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Author { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Status { get; set; }
        public ICollection<ArticleEditingModel> Editions { get; set; }
        public ICollection<string> Types { get; set; }
        public ICollection<string> PreviewImageUrls { get; set; }
    }
}
