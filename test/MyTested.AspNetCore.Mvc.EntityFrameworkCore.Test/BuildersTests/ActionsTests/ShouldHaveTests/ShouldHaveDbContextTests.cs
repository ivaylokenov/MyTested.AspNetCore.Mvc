namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
{
    using Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using Setups;
    using Setups.Common;
    using Xunit;

    public class ShouldHaveDbContextTests
    {
        [Fact]
        public void DbContextShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase());
                });

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .DbContext(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db =>
                    {
                        Assert.NotNull(db.Models.FirstOrDefaultAsync(m => m.Id == 1));
                    }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void DbContextShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase());
                });

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .DbContext(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DbContextShouldThrowExceptionWithIncorrectPredicate()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase());
                });

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .DbContext(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();
            
            Test.AssertException<DataProviderAssertionException>(() =>
            {
                MyController<DbContextController>
                    .Instance()
                    .Calling(c => c.Create(new CustomModel { Id = 2, Name = "Test" }))
                    .ShouldHave()
                    .DbContext(dbContext => dbContext
                        .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) == null))
                    .AndAlso()
                    .ShouldReturn()
                    .Ok();
            },
            "When calling Create action in DbContextController expected the CustomDbContext entities to pass the given predicate, but it failed.");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DbContextWithSetShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase());
                });

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .DbContext(dbContext => dbContext
                    .WithSet<CustomDbContext, CustomModel>(set =>
                    {
                        Assert.NotNull(set.FirstOrDefaultAsync(m => m.Id == 1));
                    }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DbContextWithSetShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase());
                });

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .DbContext(dbContext => dbContext
                    .WithSet<CustomDbContext, CustomModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DbContextWithSetShouldThrowExceptionWithIncorrectPredicate()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase());
                });

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .DbContext(dbContext => dbContext
                    .WithSet<CustomDbContext, CustomModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            Test.AssertException<DataProviderAssertionException>(() =>
            {
                MyController<DbContextController>
                    .Instance()
                    .Calling(c => c.Create(new CustomModel { Id = 2, Name = "Test" }))
                    .ShouldHave()
                    .DbContext(dbContext => dbContext
                        .WithSet<CustomDbContext, CustomModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) == null))
                    .AndAlso()
                    .ShouldReturn()
                    .Ok();
            },
            "When calling Create action in DbContextController expected the CustomDbContext set of CustomModel to pass the given predicate, but it failed.");

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
