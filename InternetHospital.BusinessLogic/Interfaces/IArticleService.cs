using System.Collections.Generic;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Models.Articles;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IArticleService
    {
        IEnumerable<ArticleModerateShortModel> GetShortModerateShortArticles(
            ArticlesFilteringModel articlesFilteringModel);

        ArticleModerateModel GetModelForEditing(int id);
        bool DeleteArticle(int articleId);
        Task<bool> CreateArticle(ArticleCreatingModel newArticle);
        IEnumerable<int> ConvertStringIdsToInt(string[] ids);
    }
}