using System;
using System.Collections.Generic;

namespace InternetHospital.DataAccess.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public int ArticleStatusId { get; set; }

        public virtual User Author { get; set; }
        public virtual ArticleStatus ArticleStatus { get; set; }
        public virtual ICollection<ArticleEdition> ArticleEditions { get; set; }
        public virtual ICollection<ArticleTypeArticle> Types { get; set; }
        public virtual ICollection<ArticleAttachment> Attachments { get; set; }
    }
}
