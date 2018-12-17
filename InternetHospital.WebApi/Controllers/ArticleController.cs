using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetHospital.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IArticleTypeService _articleTypeService;

        public ArticleController(IArticleService articleService, IArticleTypeService articleTypeService)
        {
            _articleService = articleService;
            _articleTypeService = articleTypeService;
        }

        [HttpGet("moderate")]
        public IActionResult GetFilteredModerateArticles([FromQuery] ArticlesFilteringModel filter)
        {
            var articles = _articleService.GetModerateArticles(filter);
            return Ok(articles);
        }

        [HttpGet("moderate/{id}")]
        public IActionResult Get(int id)
        {
            var result = _articleService.GetArticleForEditing(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost("moderate")]
        public IActionResult Post(IFormFile[] articlePreviewAttachments, IFormFile[] articleAttachments)
        {
            var articleModel = new ArticleCreatingModel
            {
                Title = Request.Form["Title"],
                ShortDescription = Request.Form["ShortDescription"],
                TypeIds = _articleService.ConvertStringIdsToInt(Request.Form["TypeIds"]),
                Text = Request.Form["Article"],
                ArticlePreviewAttachments = articlePreviewAttachments,
                ArticleAttachments = articleAttachments
            };

            if (int.TryParse(User.Identity.Name, out int userId))
            {
                articleModel.AuthorId = userId;
            }
            else
            {
                return BadRequest(new { message = "Wrong claims!" });
            }

            if (TryValidateModel(articleModel))
            {
                _articleService.CreateArticle(articleModel);
                return Ok(new {message = "Article created successfully"});
            }
            return BadRequest(new {message = "Model is invalid!"});
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPut("moderate")]
        public IActionResult Put(IFormFile[] articlePreviewAttachments, IFormFile[] articleAttachments)
        {
            var articleModel = new ArticleUpdateModel
            {
                Title = Request.Form["Title"],
                ShortDescription = Request.Form["ShortDescription"],
                TypeIds = _articleService.ConvertStringIdsToInt(Request.Form["TypeIds"]),
                Text = Request.Form["Article"],
                DeletedArticleAttachmentPaths = Request.Form["DeletedArticleAttachmentPaths"],
                DeletedPreviewAttachmentPaths = Request.Form["DeletedPreviewAttachmentPaths"],
                ArticlePreviewAttachments = articlePreviewAttachments,
                ArticleAttachments = articleAttachments
            };

            if (int.TryParse(Request.Form["Id"], out int id))
            {
                articleModel.Id = id;
            }
            else
            {
                return BadRequest(new { message = "Wrong article!" });
            }


            if (int.TryParse(User.Identity.Name, out int userId))
            {
                articleModel.EditorId = userId;
            }
            else
            {
                return BadRequest(new { message = "Wrong claims!" });
            }

            if (TryValidateModel(articleModel))
            {
                _articleService.UpdateArticle(articleModel);
                return Ok(new { message = "Article updated successfully" });
            }
            return BadRequest(new { message = "Model is invalid!" });
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost("moderate/type")]
        public IActionResult PostArticleType([FromBody]string articleType)
        {
            bool result = _articleTypeService.CreateArticleType(articleType);
            return Ok(result);

        }

        [HttpGet("type")]
        public IActionResult GetArticleTypes()
        {
            var articleTypes = _articleTypeService.GetArticleType();
            return Ok(articleTypes);
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpDelete("moderate/{id}")]
        public IActionResult Delete(int id)
        {
            var res = _articleService.DeleteArticle(id);
            if (res)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
