using System.Collections.Generic;

namespace InternetHospital.BusinessLogic.Models.Articles
{
    public class SendingArticleEditingModel
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Text { get; set; }
        public IEnumerable<int> TypeIds { get; set; }
        public IEnumerable<string> PreviewAttachmentsBase64 { get; set; }
        public IEnumerable<string> PreviewAttachmentPaths { get; set; }
        public IEnumerable<string> ArticleAttachmentPaths { get; set; }
    }
}
