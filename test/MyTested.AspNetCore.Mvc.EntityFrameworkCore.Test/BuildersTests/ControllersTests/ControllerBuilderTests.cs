namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Setups.Controllers;
    using Setups.Common;
    using Xunit;
    using Setups;

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
                .Ok(ok => ok
                    .WithModelOfType<CustomModel>()
                    .Passing(m => m.Name == "Test"));
            
            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntitiesShouldSetupDbContext()
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
                .WithData(data => data
                    .WithEntities<CustomDbContext>(db => db
                        .Models.Add(new CustomModel
                        {
                            Id = 1, Name = "Test"
                        })))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<CustomModel>()
                    .Passing(m => m.Name == "Test"));

            MyController<DbContextController>
                .Instance()
                .WithData(data => data
                    .WithEntities(db => db.Add(new CustomModel
                        {
                            Id = 1,
                            Name = "Test"
                        })))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<CustomModel>()
                    .Passing(m => m.Name == "Test"));

            MyController<DbContextController>
                .Instance()
                .WithData(data => data
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
                .Ok(ok => ok
                    .WithModelOfType<CustomModel>()
                    .Passing(m => m.Name == "Test 1"));

            MyController<DbContextController>
                .Instance()
                .WithData(data => data
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
                .WithData(data => data
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
                .WithData(data => data
                    .WithSet<CustomDbContext, CustomModel>(set => set
                        .Add(new CustomModel
                        {
                            Id = 1,
                            Name = "Test"
                        })))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<CustomModel>()
                    .Passing(m => m.Name == "Test"));

            MyController<DbContextController>
                .Instance()
                .WithData(data => data
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
        
        [Fact]
        public void WithEntitiesShouldSetupMultipleDbContext()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options =>
                        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));
                    
                    services.AddDbContext<AnotherDbContext>(options =>
                        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AnotherTestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));
                });

            var modelName = "Test";
            var anotherModelName = "Another Test";

            MyController<MultipleDbContextController>
                .Instance()
                .WithData(data => data
                    .WithEntities<CustomDbContext>(db => db
                        .Models.Add(new CustomModel
                        {
                            Id = 1,
                            Name = modelName
                        }))
                    .WithEntities<AnotherDbContext>(db => db
                        .OtherModels.Add(new AnotherModel
                        {
                            Id = 1,
                            FullName = anotherModelName
                        })))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new
                    {
                        Model = modelName,
                        AnotherModel = anotherModelName
                    }));
            
            MyController<MultipleDbContextController>
                .Instance()
                .WithData(data => data
                    .WithEntities<CustomDbContext>(
                        new CustomModel
                        {
                            Id = 1,
                            Name = modelName
                        },
                        new CustomModel
                        {
                            Id = 2,
                            Name = "Test 2"
                        })
                    .WithEntities<AnotherDbContext>(
                        new AnotherModel
                        {
                            Id = 1,
                            FullName = anotherModelName
                        },
                        new AnotherModel
                        {
                            Id = 2,
                            FullName = "Test 2"
                        }))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new
                    {
                        Model = modelName,
                        AnotherModel = anotherModelName
                    }));

            MyController<MultipleDbContextController>
                .Instance()
                .WithData(data => data
                    .WithEntities<CustomDbContext>(db => db
                        .Models.Add(new CustomModel
                        {
                            Id = 2,
                            Name = "Test"
                        }))
                    .WithEntities<AnotherDbContext>(db => db
                        .OtherModels.Add(new AnotherModel
                        {
                            Id = 2,
                            FullName = "Test"
                        })))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .NotFound();
            
            MyController<MultipleDbContextController>
                .Instance()
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .NotFound();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithSetShouldSetupMultipleDbContext()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options =>
                        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));
                    
                    services.AddDbContext<AnotherDbContext>(options =>
                        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AnotherTestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));
                });

            var modelName = "Test";
            var anotherModelName = "Another Test";

            MyController<MultipleDbContextController>
                .Instance()
                .WithData(data => data
                    .WithSet<CustomDbContext, CustomModel>(set => set
                        .Add(new CustomModel
                        {
                            Id = 1,
                            Name = modelName
                        }))
                    .WithSet<AnotherDbContext, AnotherModel>(set => set
                        .Add(new AnotherModel
                        {
                            Id = 1,
                            FullName = anotherModelName
                        })))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new
                    {
                        Model = modelName,
                        AnotherModel = anotherModelName
                    }));

            MyController<MultipleDbContextController>
                .Instance()
                .WithData(data => data
                    .WithSet<CustomDbContext, CustomModel>(set => set
                        .Add(new CustomModel
                        {
                            Id = 2,
                            Name = modelName
                        })))
                .Calling(c => c.Find(1))
                .ShouldReturn()
                .NotFound();
            
            MyController<MultipleDbContextController>
                .Instance()
                .WithData(data => data
                    .WithSet<AnotherDbContext, AnotherModel>(set => set
                        .Add(new AnotherModel
                        {
                            Id = 2,
                            FullName = anotherModelName
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

        [Fact]
        public void WithDataThrowCorrectExceptionWhenMultipleDbContextsAreRegisteredAndDbContextIsNotSpecified()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services =>
                {
                    services.AddDbContext<CustomDbContext>(options =>
                        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));

                    services.AddDbContext<AnotherDbContext>(options =>
                        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AnotherTestDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=30;"));
                });

            Test.AssertException<InvalidOperationException>(() =>
                {
                    MyController<MultipleDbContextController>
                        .Instance()
                        .WithData(new CustomModel { Id = 1, Name = "Test" })
                        .Calling(c => c.Find(1))
                        .ShouldReturn()
                        .NotFound();
                },
                "Multiple services of type DbContext are registered in the test service provider. You should specify the DbContext class explicitly by calling '.WithData(data => data.WithEntities<TDbContext>(dbContextSetupAction)'.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithDataThrowCorrectExceptionWhenNoDbContextIsRegistered()
        {
            MyApplication.StartsFrom<TestStartup>();

            Test.AssertException<InvalidOperationException>(() =>
                {
                    MyController<MultipleDbContextController>
                        .Instance()
                        .WithData(new CustomModel { Id = 1, Name = "Test" })
                        .Calling(c => c.Find(1))
                        .ShouldReturn()
                        .NotFound();
                },
                "DbContext is not registered in the test service provider.");

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
