namespace Blog.Controllers.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ArticleFormModel
    {
        [Required]
        [MaxLength(40)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
