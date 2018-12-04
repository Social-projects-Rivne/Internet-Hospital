using System.Collections.Generic;
using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class ArticleStatusConfiguration : IEntityTypeConfiguration<ArticleStatus>
    {
        public void Configure(EntityTypeBuilder<ArticleStatus> builder)
        {
            int id = 1;

            var initialArticleStatuses = new List<ArticleStatus>
            {
                new ArticleStatus
                {
                    Id = id++,
                    Name = "Active"
                },
                new ArticleStatus
                {
                    Id = id++,
                    Name = "Deleted"
                }
            };

            builder.HasData(initialArticleStatuses.ToArray());
        }
    }
}
