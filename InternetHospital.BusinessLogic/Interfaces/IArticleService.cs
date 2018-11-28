using System.Collections.Generic;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.Articles;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IArticleService
    {
        FilteredModel<ArticleModerateShortModel> GetShortModerateShortArticles(
            ArticlesFilteringModel articlesFilteringModel);

        ArticleModerateModel GetModelForEditing(int id);
        bool DeleteArticle(int articleId);
        Task<bool> CreateArticle(ArticleCreatingModel newArticle);
        IEnumerable<int> ConvertStringIdsToInt(string[] ids);
    }
}