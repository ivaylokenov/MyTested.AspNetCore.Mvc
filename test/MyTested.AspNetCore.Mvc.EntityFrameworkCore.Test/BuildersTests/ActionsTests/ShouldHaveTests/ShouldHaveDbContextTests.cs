namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
{
    using System;
    using Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Xunit;

    public class ShouldHaveDbContextTests
    {
        [Fact]
        public void DbContextShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services
                    .AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName)));

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db =>
                    {
                        Assert.NotNull(db.Models.FirstOrDefaultAsync(m => m.Id == 1));
                    }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextShouldNotThrowExceptionWithCorrectAssertionsThroughInterface()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services
                    .AddDbContext<ICustomDbContext, CustomDbContext>(options => options
                        .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName)));

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
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
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services
                    .AddDbContext<CustomDbContext>(options => options
                        .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName)));

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextShouldNotThrowExceptionWithCorrectPredicateThroughInterface()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services
                    .AddDbContext<ICustomDbContext, CustomDbContext>(options => options
                        .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName)));

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
                    .WithEntities<ICustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null)
                    .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null)
                    .WithEntities<DbContext>(db => db.Set<CustomModel>().FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextShouldThrowExceptionWithCorrectPredicateThroughInvalidInterface()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services
                        .AddDbContext<ICustomDbContext, CustomDbContext>(options => options
                            .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));

                    services.AddScoped<IInvalidDbContext, InvalidDbContext>();
                });

            Test.AssertException<InvalidOperationException>(() =>
            {
                MyController<DbContextController>
                    .Instance()
                    .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                    .ShouldHave()
                    .Data(dbContext => dbContext
                        .WithEntities<ICustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null)
                        .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null)
                        .WithEntities<DbContext>(db => db.Set<CustomModel>().FirstOrDefaultAsync(m => m.Id == 1) != null)
                        .WithEntities<IInvalidDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null))
                    .AndAlso()
                    .ShouldReturn()
                    .Ok();
            },
            "The provided service IInvalidDbContext is not an instance of DbContext. The resolved implementation is InvalidDbContext.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextShouldThrowExceptionWithIncorrectPredicate()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services
                    .AddDbContext<CustomDbContext>(options => options
                        .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName)));

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
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
                    .Data(dbContext => dbContext
                        .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) == null))
                    .AndAlso()
                    .ShouldReturn()
                    .Ok();
            },
            "When calling Create action in DbContextController expected the CustomDbContext entities to pass the given predicate, but it failed.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextWithSetShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services
                    .AddDbContext<CustomDbContext>(options => options
                        .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName)));

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
                    .WithSet<CustomDbContext, CustomModel>(set =>
                    {
                        Assert.NotNull(set.FirstOrDefaultAsync(m => m.Id == 1));
                    }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextWithSetShouldNotThrowExceptionWithCorrectAssertionsThroughInterface()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services
                    .AddDbContext<ICustomDbContext, CustomDbContext>(options => options
                        .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName)));

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
                    .WithSet<ICustomDbContext, CustomModel>(set =>
                    {
                        Assert.NotNull(set.FirstOrDefaultAsync(m => m.Id == 1));
                    }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextWithSetShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services
                    .AddDbContext<CustomDbContext>(options => options
                        .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName)));

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
                    .WithSet<CustomDbContext, CustomModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextWithSetShouldNotThrowExceptionWithCorrectPredicateThroughInterface()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services
                    .AddDbContext<ICustomDbContext, CustomDbContext>(options => options
                        .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName)));

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
                    .WithSet<ICustomDbContext, CustomModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void DbContextWithSetShouldThrowExceptionWithIncorrectPredicate()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services
                    .AddDbContext<CustomDbContext>(options => options
                        .UseInMemoryDatabase(TestObjectFactory.TestDatabaseName)));

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Create(new CustomModel { Id = 1, Name = "Test" }))
                .ShouldHave()
                .Data(dbContext => dbContext
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
                    .Data(dbContext => dbContext
                        .WithSet<CustomDbContext, CustomModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) == null))
                    .AndAlso()
                    .ShouldReturn()
                    .Ok();
            },
            "When calling Create action in DbContextController expected the CustomDbContext set of CustomModel to pass the given predicate, but it failed.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MultipleDbContextShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                    services.AddDbContext<AnotherDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                });

            MyController<MultipleDbContextController>
                .Instance()
                .Calling(c => c.Create(
                    new CustomModel { Id = 1, Name = "Test" },
                    new AnotherModel { Id = 1, FullName = "Another Test" }))
                .ShouldHave()
                .Data(data => data
                    .WithEntities<CustomDbContext>(db =>
                    {
                        Assert.NotNull(db.Models.FirstOrDefaultAsync(m => m.Id == 1));
                    })
                    .WithEntities<AnotherDbContext>(db =>
                    {
                        Assert.NotNull(db.OtherModels.FirstOrDefaultAsync(m => m.Id == 1));
                    }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MultipleDbContextShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                    services.AddDbContext<AnotherDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                });

            MyController<MultipleDbContextController>
                .Instance()
                .Calling(c => c.Create(
                    new CustomModel { Id = 1, Name = "Test" },
                    new AnotherModel { Id = 1, FullName = "Another Test" }))
                .ShouldHave()
                .Data(data => data
                    .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null)
                    .WithEntities<AnotherDbContext>(db => db.OtherModels.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MultipleDbContextShouldThrowExceptionWithIncorrectPredicate()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                    services.AddDbContext<AnotherDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                });

            MyController<MultipleDbContextController>
                .Instance()
                .Calling(c => c.Create(
                    new CustomModel { Id = 1, Name = "Test" },
                    new AnotherModel { Id = 1, FullName = "Another Test" }))
                .ShouldHave()
                .Data(data => data
                    .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null)
                    .WithEntities<AnotherDbContext>(db => db.OtherModels.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            Test.AssertException<DataProviderAssertionException>(() =>
            {
                MyController<MultipleDbContextController>
                    .Instance()
                    .Calling(c => c.Create(
                        new CustomModel { Id = 1, Name = "Test" },
                        new AnotherModel { Id = 1, FullName = "Another Test" }))
                    .ShouldHave()
                    .Data(data => data
                        .WithEntities<CustomDbContext>(db => db.Models.FirstOrDefaultAsync(m => m.Id == 1) != null)
                        .WithEntities<AnotherDbContext>(db => db.OtherModels.FirstOrDefaultAsync(m => m.Id == 1) == null))
                    .AndAlso()
                    .ShouldReturn()
                    .Ok();
            },
            "When calling Create action in MultipleDbContextController expected the AnotherDbContext entities to pass the given predicate, but it failed.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MultipleDbContextWithSetShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                    services.AddDbContext<AnotherDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                });

            MyController<MultipleDbContextController>
                .Instance()
                .Calling(c => c.Create(
                    new CustomModel { Id = 1, Name = "Test" },
                    new AnotherModel { Id = 1, FullName = "Another Test" }))
                .ShouldHave()
                .Data(data => data
                    .WithSet<CustomDbContext, CustomModel>(set =>
                    {
                        Assert.NotNull(set.FirstOrDefaultAsync(m => m.Id == 1));
                    })
                    .WithSet<AnotherDbContext, AnotherModel>(set =>
                    {
                        Assert.NotNull(set.FirstOrDefaultAsync(m => m.Id == 1));
                    }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MultipleDbContextWithSetShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                    services.AddDbContext<AnotherDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                });

            MyController<MultipleDbContextController>
                .Instance()
                .Calling(c => c.Create(
                    new CustomModel { Id = 1, Name = "Test" },
                    new AnotherModel { Id = 1, FullName = "Another Test" }))
                .ShouldHave()
                .Data(data => data
                    .WithSet<CustomDbContext, CustomModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) != null)
                    .WithSet<AnotherDbContext, AnotherModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void MultipleDbContextWithSetShouldThrowExceptionWithIncorrectPredicate()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                    services.AddDbContext<AnotherDbContext>(options => options.UseInMemoryDatabase(TestObjectFactory.TestDatabaseName));
                });

            MyController<MultipleDbContextController>
                .Instance()
                .Calling(c => c.Create(
                    new CustomModel { Id = 1, Name = "Test" },
                    new AnotherModel { Id = 1, FullName = "Another Test" }))
                .ShouldHave()
                .Data(data => data
                    .WithSet<CustomDbContext, CustomModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) != null)
                    .WithSet<AnotherDbContext, AnotherModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) != null))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            Test.AssertException<DataProviderAssertionException>(() =>
            {
                MyController<MultipleDbContextController>
                    .Instance()
                    .Calling(c => c.Create(
                        new CustomModel { Id = 1, Name = "Test" },
                        new AnotherModel { Id = 1, FullName = "Another Test" }))
                    .ShouldHave()
                    .Data(data => data
                        .WithSet<CustomDbContext, CustomModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) != null)
                        .WithSet<AnotherDbContext, AnotherModel>(set => set.FirstOrDefaultAsync(m => m.Id == 1) == null))
                    .AndAlso()
                    .ShouldReturn()
                    .Ok();
            },
            "When calling Create action in MultipleDbContextController expected the AnotherDbContext set of AnotherModel to pass the given predicate, but it failed.");

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
