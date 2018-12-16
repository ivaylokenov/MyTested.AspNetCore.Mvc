namespace Blog.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ArticleService : IArticleService
    {
        private readonly BlogDbContext db;
        private readonly IMapper mapper;

        public ArticleService(BlogDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ArticleListingServiceModel>> All(int page = 1, int count = 10)
            => await this.db
                .Articles
                .Where(a => a.IsPublic)
                .OrderByDescending(a => a.PublishedOn)
                .Skip((page - 1) * count)
                .Take(count)
                .ProjectTo<ArticleListingServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<IEnumerable<ArticleForUserListingServiceModel>> ByUser(string userId)
            => await this.db
                .Articles
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.PublishedOn)
                .ProjectTo<ArticleForUserListingServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<bool> IsByUser(int id, string userId)
            => await this.db
                .Articles
                .AnyAsync(a => a.Id == id && a.UserId == userId);

        public async Task<ArticleDetailsServiceModel> Details(int id)
            => await this.db
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<ArticleDetailsServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<int> Total()
            => await this.db
                .Articles
                .Where(a => a.IsPublic)
                .CountAsync();

        public async Task<int> Add(string title, string content, string userId)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                UserId = userId
            };

            this.db.Articles.Add(article);

            await this.db.SaveChangesAsync();

            return article.Id;
        }
        
        public async Task Edit(int id, string title, string content)
        {
            var article = await this.db.Articles.FindAsync(id);

            if (article == null)
            {
                return;
            }

            article.Title = title;
            article.Content = content;
            article.IsPublic = false;

            await this.db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var article = await this.db.Articles.FindAsync(id);
            this.db.Articles.Remove(article);

            await this.db.SaveChangesAsync();
        }
    }
}
