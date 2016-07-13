namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Common;
    using Xunit;

    public class ControllerBuilderTests
    {
        [Fact]
        public void WithEntitesShouldSetupDbContext()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .NotFound();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithSetShouldSetupDbContext()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
