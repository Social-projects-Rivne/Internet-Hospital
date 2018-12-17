namespace InternetHospital.DataAccess.Entities
{
    public class ArticleTypeArticle
    {
        public int TypeId { get; set; }
        public int ArticleId { get; set; }

        public virtual ArticleType Type { get; set; }
        public virtual Article Article { get; set; }
       
    }
}
