namespace Blog.Data.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
