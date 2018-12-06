using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    class ArticleTypeArticleConfiguration: IEntityTypeConfiguration<ArticleTypeArticle>
    {
        public void Configure(EntityTypeBuilder<ArticleTypeArticle> builder)
        {
            builder.HasKey(artType => new {artType.ArticleId, artType.TypeId});
        }
    }
}