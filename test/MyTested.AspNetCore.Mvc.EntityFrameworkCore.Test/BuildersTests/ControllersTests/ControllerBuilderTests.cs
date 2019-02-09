namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Setups.Controllers;
    using Setups.Common;
    using Xunit;
    using Setups;
    using EntityFrameworkCore.Test;

    public class ControllerBuilderTests
    {
        [Fact]
        public void WithDataShouldSetupDbContext()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options =>
                        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));
                });

            MyController<DbContextController>
                .Instance()
                .WithData(new CustomModel
                {
                    Id = 1,
                    Name = "Test"
                })
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .Ok()
                .WithModelOfType<CustomModel>()
                .Passing(m => m.Name == "Test");
        }

        [Fact]
        public void WithEntitesShouldSetupDbContext()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options =>
                        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));
                });

            MyController<DbContextController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db => db
                        .Models.Add(new CustomModel
                        {
                            Id = 1, Name = "Test"
                        })))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .Ok()
                .WithModelOfType<CustomModel>()
                .Passing(m => m.Name == "Test");

            MyController<DbContextController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(db => db.Add(new CustomModel
                        {
                            Id = 1,
                            Name = "Test"
                        })))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .Ok()
                .WithModelOfType<CustomModel>()
                .Passing(m => m.Name == "Test");

            MyController<DbContextController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(
                        new CustomModel
                        {
                            Id = 1,
                            Name = "Test 1"
                        },
                        new CustomModel
                        {
                            Id = 2,
                            Name = "Test 2"
                        }))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .Ok()
                .WithModelOfType<CustomModel>()
                .Passing(m => m.Name == "Test 1");

            MyController<DbContextController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db => db
                        .Models.Add(new CustomModel
                        {
                            Id = 2,
                            Name = "Test"
                        })))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .NotFound();

            MyController<DbContextController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities<CustomDbContext>(
                        new CustomModel
                        {
                            Id = 2,
                            Name = "Test 2"
                        },
                        new CustomModel
                        {
                            Id = 3,
                            Name = "Test 3"
                        }))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .NotFound();

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .NotFound();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithSetShouldSetupDbContext()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options =>
                        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));
                });

            MyController<DbContextController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithSet<CustomDbContext, CustomModel>(set => set
                        .Add(new CustomModel
                        {
                            Id = 1,
                            Name = "Test"
                        })))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .Ok()
                .WithModelOfType<CustomModel>()
                .Passing(m => m.Name == "Test");

            MyController<DbContextController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithSet<CustomDbContext, CustomModel>(set => set
                        .Add(new CustomModel
                        {
                            Id = 2,
                            Name = "Test"
                        })))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .NotFound();

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .NotFound();

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
