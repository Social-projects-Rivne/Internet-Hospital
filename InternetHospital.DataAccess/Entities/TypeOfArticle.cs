using System.Collections.Generic;

namespace InternetHospital.DataAccess.Entities
{
    public class ArticleType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ArticleTypeArticle> Articles { get; set; }
    }
}
