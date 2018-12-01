using System.Collections.Generic;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.Articles;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IArticleService
    {
        FilteredModel<ArticleModeratePreviewModel> GetModerateArticles(
            ArticlesFilteringModel articlesFilteringModel);

        bool DeleteArticle(int articleId);
        bool CreateArticle(ArticleCreatingModel newArticle);
        SendingArticleEditingModel GetArticleForEditing(int id);
        bool UpdateArticle(ArticleUpdateModel updateModel);
        IEnumerable<int> ConvertStringIdsToInt(string[] ids);
    }
}