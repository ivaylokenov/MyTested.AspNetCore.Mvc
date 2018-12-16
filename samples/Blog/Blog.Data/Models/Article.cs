namespace Blog.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsPublic { get; set; }

        public DateTime? PublishedOn { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
