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
        public const string ID_KEY = "HomePage/%$#@_article_id_@$#%/Attachments/";

        public ArticleService(ApplicationContext context, IFilesService filesService)
        {
            _context = context;
            _filesService = filesService;
        }

        public FilteredModel<ArticleModeratePreviewModel> GetModerateArticles(ArticlesFilteringModel articlesFilteringModel)
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

            var result = new FilteredModel<ArticleModeratePreviewModel>();
            result.Amount = articles.Count();

            articles = PaginationHelper<Article>.GetPageValues(articles, articlesFilteringModel.PageSize,
                articlesFilteringModel.Page);

            result.Results = articles.Select(a => new ArticleModeratePreviewModel
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

        public SendingArticleEditingModel GetArticleForEditing(int id)
        {
            var article = _context.Articles.Include(a => a.Author)
                                            .Include(a => a.Types)
                                            .Include(a => a.Attachments).FirstOrDefault(a => a.Id == id);
            SendingArticleEditingModel result = null;
            if (article != null)
            {
                result = Mapper.Map<Article, SendingArticleEditingModel>(article, cnf =>
                    cnf.AfterMap((src, dest) =>
                    {
                        dest.Author = $"{src.Author.FirstName} {src.Author.SecondName} {src.Author.ThirdName}";
                        dest.TypeIds = src.Types.Select(t => t.TypeId).ToList();
                        dest.ArticleAttachmentPaths =
                            src.Attachments.Where(a => !a.IsOnPreview).Select(a => a.Url).ToList();
                        dest.PreviewAttachmentPaths =
                            src.Attachments.Where(a => a.IsOnPreview).Select(a => a.Url).ToList();
                    }));
                var prevAtach = article.Attachments.Where(a => a.IsOnPreview).Select(a => a.Url).ToList();
                result.PreviewAttachmentsBase64 = _filesService.GetBase64StringsFromAttachments(prevAtach);
            }

            return result;
        }

        public bool CreateArticle(ArticleCreatingModel newArticle)
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

            _filesService.UploadArticleAttachments(newArticle.ArticlePreviewAttachments, newArticle.ArticleAttachments,
                article.Id);
            var articalAttachments = article.Attachments.Where(a => !a.IsOnPreview).ToList();
            for (var i = 0; i < articalAttachments.Count; ++i)
            {
                article.Text = article.Text.Replace($"{ID_KEY}{i + 1}", articalAttachments[i].Url);
            }       
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

        public bool UpdateArticle(ArticleUpdateModel updateModel)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == updateModel.Id);
            if (article != null)
            {
                article.Title = updateModel.Title;
                article.ShortDescription = updateModel.ShortDescription;

                var articleTypesForRemoving = _context.ArticleTypeArticles
                                    .Where(ata => ata.ArticleId == article.Id && !updateModel.TypeIds
                                                                                            .Contains(ata.TypeId));
                _context.ArticleTypeArticles.RemoveRange(articleTypesForRemoving);
                var typeIdsForAdding = updateModel.TypeIds
                    .Where(t => !_context.ArticleTypeArticles.Any(ata => ata.ArticleId == article.Id 
                                                                        && ata.TypeId == t)).Select(
                                                                                t => new ArticleTypeArticle
                                                                                {
                                                                                    ArticleId = article.Id,
                                                                                    TypeId = t
                                                                                });
                _context.ArticleTypeArticles.AddRange(typeIdsForAdding);
                _filesService.RemoveArticleAttachment(updateModel.DeletedAttachmentsPaths);
                _filesService.UploadArticleAttachments(updateModel.ArticlePreviewAttachments, updateModel.ArticleAttachments, article.Id);

                var articalAttachments = article.Attachments.Where(a => !a.IsOnPreview).ToList();
                for (var i = 0; i < articalAttachments.Count; ++i)
                {
                    article.Text = article.Text.Replace($"{ID_KEY}{i + 1}", articalAttachments[i].Url);
                }
                _context.ArticleEditions.Add(new ArticleEdition
                {
                    ArticleId = article.Id,
                    AuthorId = updateModel.EditorId,
                    Time = DateTime.UtcNow
                });
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
