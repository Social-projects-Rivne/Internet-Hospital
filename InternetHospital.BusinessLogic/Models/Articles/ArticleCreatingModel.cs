using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace InternetHospital.BusinessLogic.Models.Articles
{
    public class ArticleCreatingModel
    {
        [Required]
        public int AuthorId { get; set; } = 1;
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MinLength(1)]
        public IEnumerable<int> TypeIds { get; set; }
        [Required]
        [MinLength(20)]
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        [Required]
        public string Text { get; set; }
        public IFormFile[] ArticleAttachments { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public IFormFile[] ArticlePreviewAttachments { get; set; }
    }
}
