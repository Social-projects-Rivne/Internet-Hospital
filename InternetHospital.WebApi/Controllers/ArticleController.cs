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

        // GET: api/Article
        [HttpGet]
        public IActionResult GetFilteredArticles([FromQuery] ArticlesFilteringModel filter)
        {
            var articles = _articleService.GetShortModerateShortArticles(filter);
            return Ok(articles);
        }

        // GET: api/Article/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Article
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        public IActionResult Post(IFormFile[] articlePreviewAttachment, IFormFile[] articleAttachment)
        {
            var articleModel = new ArticleCreatingModel
            {
                Title = Request.Form["Title"],
                ShortDescription = Request.Form["ShortDescription"],
                TypeIds = _articleService.ConvertStringIdsToInt(Request.Form["TypeIds"]),
                Text = Request.Form["Article"],
                ArticlePreviewAttachments = articlePreviewAttachment,
                ArticleAttachments = articleAttachment
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

        [HttpPost("type")]
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

        // PUT: api/Article/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
