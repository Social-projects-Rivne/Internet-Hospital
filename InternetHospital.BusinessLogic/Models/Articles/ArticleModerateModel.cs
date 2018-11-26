using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace InternetHospital.BusinessLogic.Models.Articles
{
    public class ArticleModerateModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public ICollection<ArticleTypeModel> TypeIds { get; set; }
        public string Text { get; set; }
        public ICollection<ArticleAttachmentModel> AttachmentLinks { get; set; }
    }
}
