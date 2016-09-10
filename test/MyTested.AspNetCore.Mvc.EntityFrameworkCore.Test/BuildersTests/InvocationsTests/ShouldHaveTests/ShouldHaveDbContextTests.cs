namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldHaveTests
{
    using Exceptions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Common;
    using Setups.ViewComponents;
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

            MyViewComponent<CreateDataComponent>
                .Instance()
                .InvokedWith(c => c.Invoke(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .DbContext(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db =>
                    {
                        Assert.NotNull(db.Models.FirstOrDefaultAsync(m => m.Id == 1));
                    }))
                .AndAlso()
                .ShouldReturn()
                .View();

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

            MyViewComponent<CreateDataComponent>
                .Instance()
                .InvokedWith(c => c.Invoke(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .DbContext(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .View();

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

            MyViewComponent<CreateDataComponent>
                .Instance()
                .InvokedWith(c => c.Invoke(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .DbContext(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .View();

            Test.AssertException<DataProviderAssertionException>(() =>
            {
                MyViewComponent<CreateDataComponent>
                    .Instance()
                    .InvokedWith(c => c.Invoke(new CustomModel { Id = 2, Name = "Test" }))
                    .ShouldHave()
                    .DbContext(dbContext => dbContext
                        .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) == null))
                    .AndAlso()
                    .ShouldReturn()
                    .View();
            },
            "When invoking CreateDataComponent expected the CustomDbContext entities to pass the given predicate, but it failed.");

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
