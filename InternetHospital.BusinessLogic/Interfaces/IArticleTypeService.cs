using System.Collections.Generic;
using InternetHospital.BusinessLogic.Models;

namespace InternetHospital.BusinessLogic.Interfaces
{
    public interface IArticleTypeService
    {
        IEnumerable<ArticleTypeModel> GetArticleType();
        bool CreateArticleType(string articleTypeName);
    }
}