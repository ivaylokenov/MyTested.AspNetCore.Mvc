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
        public void WithEntitiesShouldSetupDbContext()
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
                .WithData(data => data
                    .WithEntities<CustomDbContext>(db => db
                        .Models.Add(new CustomModel
                        {
                            Id = 1,
                            Name = "Test"
                        })))
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CustomModel>()
                    .Passing(m => m.Name == "Test"));

            MyViewComponent<FindDataComponent>
                .Instance()
                .WithData(data => data
                    .WithEntities(db => db.Add(new CustomModel
                    {
                        Id = 1,
                        Name = "Test"
                    })))
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CustomModel>()
                    .Passing(m => m.Name == "Test"));

            MyViewComponent<FindDataComponent>
                .Instance()
                .WithData(data => data
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
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .Content("Invalid");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntitiesShouldSetupDbContextThroughInterface()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<ICustomDbContext, CustomDbContext>(options =>
                        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));
                });

            MyViewComponent<FindDataComponent>
                .Instance()
                .WithData(data => data
                    .WithEntities<ICustomDbContext>(db => db
                        .Models.Add(new CustomModel
                        {
                            Id = 1,
                            Name = "Test"
                        })))
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CustomModel>()
                    .Passing(m => m.Name == "Test"));

            MyViewComponent<FindDataComponent>
                .Instance()
                .WithData(data => data
                    .WithEntities(db => db.Add(new CustomModel
                    {
                        Id = 1,
                        Name = "Test"
                    })))
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CustomModel>()
                    .Passing(m => m.Name == "Test"));

            MyViewComponent<FindDataComponent>
                .Instance()
                .WithData(data => data
                    .WithEntities<ICustomDbContext>(db => db
                        .Models.Add(new CustomModel
                        {
                            Id = 2,
                            Name = "Test"
                        })))
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .Content("Invalid");

            MyViewComponent<FindDataComponent>
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
                .WithData(data => data
                    .WithSet<CustomDbContext, CustomModel>(set => set
                        .Add(new CustomModel
                        {
                            Id = 1,
                            Name = "Test"
                        })))
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CustomModel>()
                    .Passing(m => m.Name == "Test"));

            MyViewComponent<FindDataComponent>
                .Instance()
                .WithData(data => data
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
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .Content("Invalid");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithSetShouldSetupDbContextThroughInterface()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<ICustomDbContext, CustomDbContext>(options =>
                        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));
                });

            MyViewComponent<FindDataComponent>
                .Instance()
                .WithData(data => data
                    .WithSet<ICustomDbContext, CustomModel>(set => set
                        .Add(new CustomModel
                        {
                            Id = 1,
                            Name = "Test"
                        })))
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CustomModel>()
                    .Passing(m => m.Name == "Test"));

            MyViewComponent<FindDataComponent>
                .Instance()
                .WithData(data => data
                    .WithSet<ICustomDbContext, CustomModel>(set => set
                        .Add(new CustomModel
                        {
                            Id = 2,
                            Name = "Test"
                        })))
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .Content("Invalid");

            MyViewComponent<FindDataComponent>
                .InvokedWith(c => c.Invoke(1))
                .ShouldReturn()
                .Content("Invalid");

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
