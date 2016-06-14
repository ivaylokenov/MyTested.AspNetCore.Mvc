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
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase());
                });

            MyMvc
                .Controller<DbContextController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void DbContextShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase());
                });

            MyMvc
                .Controller<DbContextController>()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .DbContext(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DbContextShouldThrowExceptionWithIncorrectPredicate()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase());
                });

            MyMvc
                .Controller<DbContextController>()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .DbContext(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();
            
            Test.AssertException<DataProviderAssertionException>(() =>
            {
                MyMvc
                    .Controller<DbContextController>()
                    .Calling(c => c.Create(new CustomModel { Id = 2, Name = "Test" }))
                    .ShouldHave()
                    .DbContext(dbContext => dbContext
                        .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) == null))
                    .AndAlso()
                    .ShouldReturn()
                    .Ok();
            },
            "When calling Create action in DbContextController expected the CustomDbContext entities to pass the given predicate, but it failed.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DbContextWithSetShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase());
                });

            MyMvc
                .Controller<DbContextController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DbContextWithSetShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase());
                });

            MyMvc
                .Controller<DbContextController>()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .DbContext(dbContext => dbContext
                    .WithSet<CustomDbContext, CustomModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DbContextWithSetShouldThrowExceptionWithIncorrectPredicate()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase());
                });

            MyMvc
                .Controller<DbContextController>()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .DbContext(dbContext => dbContext
                    .WithSet<CustomDbContext, CustomModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            Test.AssertException<DataProviderAssertionException>(() =>
            {
                MyMvc
                    .Controller<DbContextController>()
                    .Calling(c => c.Create(new CustomModel { Id = 2, Name = "Test" }))
                    .ShouldHave()
                    .DbContext(dbContext => dbContext
                        .WithSet<CustomDbContext, CustomModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) == null))
                    .AndAlso()
                    .ShouldReturn()
                    .Ok();
            },
            "When calling Create action in DbContextController expected the CustomDbContext set of CustomModel to pass the given predicate, but it failed.");

            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
