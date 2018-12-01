using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace InternetHospital.BusinessLogic.Models.Articles
{
    public class ArticleUpdateModel
    {
        [Required]
        public int Id { get; set; }
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
        [Required]
        public int EditorId { get; set; }
        public IFormFile[] ArticleAttachments { get; set; }
        public IFormFile[] ArticlePreviewAttachments { get; set; }
        public string[] DeletedAttachmentsPaths { get; set; }
    }
}
