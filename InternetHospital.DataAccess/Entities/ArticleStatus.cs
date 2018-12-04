using System.Collections.Generic;

namespace InternetHospital.DataAccess.Entities
{
    public class ArticleStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
