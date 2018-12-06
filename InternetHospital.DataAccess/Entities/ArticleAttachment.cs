namespace InternetHospital.DataAccess.Entities
{
    public class ArticleAttachment
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsOnPreview { get; set; }
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
