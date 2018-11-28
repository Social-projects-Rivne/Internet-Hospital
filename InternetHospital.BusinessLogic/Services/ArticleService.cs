using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InternetHospital.BusinessLogic.Helpers;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.BusinessLogic.Models.Articles;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace InternetHospital.BusinessLogic.Services
{
    public class ArticleService: IArticleService
    {
        private readonly ApplicationContext _context;
        private readonly IFilesService _filesService;

        private const string ACTIVE_ARTICLE = "Active";
        private const string DELETED__ARTICLE = "Deleted";
        public const string ID_KEY = "%$#@_article_id_@$#%";

        public ArticleService(ApplicationContext context, IFilesService filesService)
        {
            _context = context;
            _filesService = filesService;
        }

        public FilteredModel<ArticleModerateShortModel> GetShortModerateShortArticles(ArticlesFilteringModel articlesFilteringModel)
        {
            var articles = _context.Articles.AsQueryable();
            if (!articlesFilteringModel.IncludeAll)
            {
                articles = articles.Where(a => a.ArticleStatus.Name == ACTIVE_ARTICLE);
            }
            if (!string.IsNullOrWhiteSpace(articlesFilteringModel.Search))
            {
                var search = articlesFilteringModel.Search.ToLower();
                articles = articles.Where(a => 
                    a.Author.FirstName.ToLower().Contains(search)
                    || a.Author.SecondName.ToLower().Contains(search)
                    || a.Author.ThirdName.ToLower().Contains(search)
                    || a.Title.ToLower().Contains(search));
            }

            if (articlesFilteringModel.TypeIds != null && articlesFilteringModel.TypeIds.Length > 0)
            {
                articles = articles.Where(
                    a => a.Types.Any(artT => articlesFilteringModel.TypeIds.Contains(artT.TypeId)));
            }

            if (articlesFilteringModel.From != null)
            {
                articles = articles.Where(a => a.TimeOfCreation > articlesFilteringModel.From);
            }

            if (articlesFilteringModel.To != null)
            {
                articles = articles.Where(a => a.TimeOfCreation < articlesFilteringModel.To);
            }

            var result = new FilteredModel<ArticleModerateShortModel>();
            result.Amount = articles.Count();

            articles = PaginationHelper<Article>.GetPageValues(articles, articlesFilteringModel.PageSize,
                articlesFilteringModel.Page);

            result.Results = articles.Select(a => new ArticleModerateShortModel
            {
                Author = $"{a.Author.FirstName} {a.Author.SecondName} {a.Author.ThirdName}",
                Id = a.Id,
                Title = a.Title,
                ShortDescription = a.ShortDescription,
                DateOfCreation = a.TimeOfCreation,
                Editions = a.ArticleEditions.Select(edition => new ArticleEditingModel
                {
                    Author = $"{edition.Author.FirstName} {edition.Author.SecondName} {edition.Author.ThirdName}",
                    DateOfEdit = edition.Time
                }).ToList(),
                PreviewImageUrls = a.Attachments.Where(att => att.IsOnPreview).Select(att => att.Url).ToList(),
                Types = a.Types.Select(t => t.Type.Name).ToList(),
                Status = a.ArticleStatus.Name
            }).ToList();
            return result;
        }

        public ArticleModerateModel GetModelForEditing(int id)
        {
            var article = _context.Articles.Include(a => a.Attachments)
                                                .Include(a => a.Types).ThenInclude(t => t.Type)
                                                                        .FirstOrDefault(a => a.Id == id);
            if (article != null)
            {
                var editedArticle = new ArticleModerateModel
                {
                    Title = article.Title,
                    AttachmentLinks = article.Attachments.Select(att => Mapper.Map<ArticleAttachment, ArticleAttachmentModel>(att)).ToArray(),
                    ShortDescription = article.ShortDescription,
                    Text = article.Text,
                    TypeIds = article.Types.Select(t => Mapper.Map<ArticleType, ArticleTypeModel>(t.Type)).ToList()
                };
                return editedArticle;
            }

            return null;

        }
        // public UpdateModel()
        public bool DeleteArticle(int articleId)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == articleId);
            if (article != null)
            {
                article.ArticleStatusId = _context.ArticleStatuses.FirstOrDefault(s => s.Name == DELETED__ARTICLE).Id;
                _context.SaveChanges();
                return true;
            }

            return false;
        }
        // public GetArticlesForPreview()
        // public GetArticle(int id)



        public async Task<bool> CreateArticle(ArticleCreatingModel newArticle)
        {
            var article = Mapper.Map<ArticleCreatingModel, Article>(newArticle, cnf => cnf.AfterMap((src, dest) =>
                {
                    dest.ArticleStatusId = _context.ArticleStatuses.FirstOrDefault(st => st.Name == ACTIVE_ARTICLE).Id;
                    dest.TimeOfCreation = DateTime.UtcNow;
                }));
            _context.Add(article);
            _context.SaveChanges();
            foreach (int typeId in newArticle.TypeIds)
            {
                {
                    if (_context.ArticleTypes.Any(at => at.Id == typeId))
                        _context.Add(new ArticleTypeArticle
                        {
                            ArticleId = article.Id,
                            TypeId = typeId
                        });
                }
            }

            _filesService.UploadArticlePhotos(newArticle.ArticlePreviewAttachments, newArticle.ArticleAttachments,
                article.Id);
            article.Text = article.Text.Replace(ID_KEY, article.Id.ToString());
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<int> ConvertStringIdsToInt(string[] ids)
        {
            var typeIds = new List<int>();
            foreach (var id in ids)
            {
                if (int.TryParse(id, out int intId))
                {
                    typeIds.Add(intId);
                }
            }
            return typeIds;
        }
    }
}
