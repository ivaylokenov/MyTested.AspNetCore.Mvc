namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentsTests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Common;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewComponentBuilderTests
    {
        [Fact]
        public void WithEntitesShouldSetupDbContext()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options =>
                        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));
                });

            MyViewComponent<FindDataComponent>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db => db
                        .Models.Add(new CustomModel
                        {
                            Id = 1,
                            Name = "Test"
                        })))
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .View()
                .WithModelOfType<CustomModel>()
                .Passing(m => m.Name == "Test");

            MyViewComponent<FindDataComponent>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(db => db.Add(new CustomModel
                    {
                        Id = 1,
                        Name = "Test"
                    })))
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .View()
                .WithModelOfType<CustomModel>()
                .Passing(m => m.Name == "Test");

            MyViewComponent<FindDataComponent>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities<CustomDbContext>(db => db
                        .Models.Add(new CustomModel
                        {
                            Id = 2,
                            Name = "Test"
                        })))
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .Content("Invalid");

            MyViewComponent<FindDataComponent>
                .Instance()
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .Content("Invalid");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithSetShouldSetupDbContext()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options =>
                        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));
                });

            MyViewComponent<FindDataComponent>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithSet<CustomDbContext, CustomModel>(set => set
                        .Add(new CustomModel
                        {
                            Id = 1,
                            Name = "Test"
                        })))
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .View()
                .WithModelOfType<CustomModel>()
                .Passing(m => m.Name == "Test");

            MyViewComponent<FindDataComponent>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithSet<CustomDbContext, CustomModel>(set => set
                        .Add(new CustomModel
                        {
                            Id = 2,
                            Name = "Test"
                        })))
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .Content("Invalid");

            MyViewComponent<FindDataComponent>
                .Instance()
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .Content("Invalid");

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
