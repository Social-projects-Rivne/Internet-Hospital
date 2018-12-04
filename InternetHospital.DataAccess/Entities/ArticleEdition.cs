using System;

namespace InternetHospital.DataAccess.Entities
{
    public class ArticleEdition
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int ArticleId { get; set; }
        public DateTime Time { get; set; }

        public virtual User Author { get; set; }
        public virtual Article Article { get; set; }
    }
}
