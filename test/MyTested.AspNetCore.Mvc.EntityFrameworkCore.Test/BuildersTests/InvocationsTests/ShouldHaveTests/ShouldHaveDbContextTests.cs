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
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services
                        .AddDbContext<CustomDbContext>(options => options
                            .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                });

            MyViewComponent<CreateDataComponent>
                .InvokedWith(c => c.Invoke(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db =>
                    {
                        Assert.NotNull(db.Models.FirstOrDefaultAsync(m => m.Id == 1));
                    }))
                .AndAlso()
                .ShouldReturn()
                .View();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextShouldNotThrowExceptionWithCorrectAssertionsThroughInterface()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services
                        .AddDbContext<ICustomDbContext, CustomDbContext>(options => options
                            .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                });

            MyViewComponent<CreateDataComponent>
                .InvokedWith(c => c.Invoke(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
                    .WithEntities<ICustomDbContext>(db =>
                    {
                        Assert.NotNull(db.Models.FirstOrDefaultAsync(m => m.Id == 1));
                    })
                    .WithEntities<CustomDbContext>(db =>
                    {
                        Assert.NotNull(db.Models.FirstOrDefaultAsync(m => m.Id == 1));
                    })
                    .WithEntities<DbContext>(db =>
                    {
                        Assert.NotNull(db.Set<CustomModel>().FirstOrDefaultAsync(m => m.Id == 1));
                    }))
                .AndAlso()
                .ShouldReturn()
                .View();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services
                        .AddDbContext<CustomDbContext>(options => options
                            .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                });

            MyViewComponent<CreateDataComponent>
                .InvokedWith(c => c.Invoke(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .View();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextShouldNotThrowExceptionWithCorrectPredicateThroughInterface()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services
                        .AddDbContext<ICustomDbContext, CustomDbContext>(options => options
                            .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                });

            MyViewComponent<CreateDataComponent>
                .InvokedWith(c => c.Invoke(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
                    .WithEntities<ICustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null)
                    .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null)
                    .WithEntities<DbContext>(db => db.Set<CustomModel>().FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .View();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextShouldThrowExceptionWithIncorrectPredicate()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services
                        .AddDbContext<CustomDbContext>(options => options
                            .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                });

            MyViewComponent<CreateDataComponent>
                .InvokedWith(c => c.Invoke(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .View();

            Test.AssertException<DataProviderAssertionException>(() =>
            {
                MyViewComponent<CreateDataComponent>
                    .InvokedWith(c => c.Invoke(new CustomModel { Id = 2, Name = "Test" }))
                    .ShouldHave()
                    .Data(dbContext => dbContext
                        .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) == null))
                    .AndAlso()
                    .ShouldReturn()
                    .View();
            },
            "When invoking CreateDataComponent expected the CustomDbContext entities to pass the given predicate, but it failed.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextShouldThrowExceptionWithIncorrectPredicateThroughInterface()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services
                        .AddDbContext<ICustomDbContext, CustomDbContext>(options => options
                            .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                });

            MyViewComponent<CreateDataComponent>
                .InvokedWith(c => c.Invoke(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
                    .WithEntities<ICustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null)
                    .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null)
                    .WithEntities<DbContext>(db => db.Set<CustomModel>().FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .View();

            Test.AssertException<DataProviderAssertionException>(() =>
            {
                MyViewComponent<CreateDataComponent>
                    .InvokedWith(c => c.Invoke(new CustomModel { Id = 2, Name = "Test" }))
                    .ShouldHave()
                    .Data(dbContext => dbContext
                        .WithEntities<ICustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) == null)
                        .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) == null)
                        .WithEntities<DbContext>(db => db.Set<CustomModel>().FirstOrDefaultAsync(m => m.Id == 1) == null))
                    .AndAlso()
                    .ShouldReturn()
                    .View();
            },
            "When invoking CreateDataComponent expected the ICustomDbContext entities to pass the given predicate, but it failed.");

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
