namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
                .Calling(c => c.Get(1))
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
                            Id = 1,
                            Name = "Test"
                        })))
                .Calling(c => c.Get(1))
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
                .Calling(c => c.Get(1))
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
                .Calling(c => c.Get(1))
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
                .Calling(c => c.Get(1))
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
                .Calling(c => c.Get(1))
                .ShouldReturn()
                .NotFound();

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Get(1))
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
                .Calling(c => c.Get(1))
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
                .Calling(c => c.Get(1))
                .ShouldReturn()
                .NotFound();

            MyController<DbContextController>
                .Instance()
                .Calling(c => c.Get(1))
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
                .Calling(c => c.Get(1))
                .ShouldReturn()
                .NotFound();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithDataShouldCleanUpChangeTrackerBeforeTestExecution()
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
                .Calling(c => c.Update(1))
                .ShouldReturn()
                .Ok();

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

        [Fact]
        public void WithoutDataParamsReturnsCorrectResultCodeWhenAllProvidedDataIsDelete()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services.AddDbContext<CustomDbContext>());

            var model1 = new CustomModel
            {
                Id = 1,
                Name = "Test1"
            };

            var model2 = new CustomModel
            {
                Id = 2,
                Name = "Test2"
            };

            MyController<DbContextController>
                .Instance()
                .WithData(model1)
                .AndAlso()
                .WithData(model2)
                .WithoutData(model1, model2)
                .Calling(c => c.Get(model1.Id))
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void WithoutDataWithSingleEntityReturnsCorrectResultWhenAllProvidedDataIsDeleted()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services.AddDbContext<CustomDbContext>());

            var model = new CustomModel
            {
                Id = 1,
                Name = "Test"
            };

            MyController<DbContextController>
                .Instance()
                .WithData(model)
                .WithoutData(model)
                .Calling(c => c.Get(model.Id))
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void WithoutDataDeletingWholeDatabaseReturnsCorrectData()
        {
            MyApplication
                  .StartsFrom<TestStartup>()
                  .WithServices(services => services.AddDbContext<CustomDbContext>());

            var models = new List<CustomModel> { new CustomModel
            {
                Id = 1,
                Name = "Test"
            }};

            MyController<DbContextController>
                .Instance()
                .WithData(models)
                .WithoutData()
                .Calling(c => c.Get(models.First().Id))
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void WithoutDataThrowsExceptionWhenNullIsProvided()
        {
            Test.AssertException<ArgumentNullException>(
                () =>
                {
                    MyApplication
                    .StartsFrom<TestStartup>()
                    .WithServices(services => services.AddDbContext<CustomDbContext>());

                    var model = new CustomModel
                    {
                        Id = 1,
                        Name = "Test"
                    };

                    MyController<DbContextController>
                        .Instance()
                        .WithData(model)
                        .WithoutData(default(List<object>))
                        .Calling(c => c.Get(model.Id))
                        .ShouldReturn()
                        .NotFound();
                }, 
                "Value cannot be null. (Parameter 'entities')");
        }

        [Fact]
        public void WithoutDataDoesNotDeleteAnythingIfEmptyCollectionIsProvided()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services.AddDbContext<CustomDbContext>());

            var models = new List<CustomModel> {
                new CustomModel
                {
                    Id = 1,
                    Name = "Test"
                },
            new CustomModel
                {
                    Id = 2,
                    Name = "Test2"
                }
            };

            MyController<DbContextController>
                .Instance()
                .WithData(models)
                .WithoutData(new List<object>())
                .Calling(c => c.Get(models.Last().Id))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<CustomModel>()
                    .Passing(cm => cm.Name.Equals(models.Last().Name)));
        }

        [Fact]
        public void WithoutDataWithProvidedOnlyPartialDataForDeletionReturnsCorrectResult()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services.AddDbContext<CustomDbContext>());

            var models = new List<CustomModel> {
                new CustomModel
                {
                    Id = 1,
                    Name = "Test"
                },
                new CustomModel
                {
                    Id = 2,
                    Name = "Test2"
                },
                new CustomModel
                {
                    Id = 3,
                    Name = "Test3"
                }
            };

            MyController<DbContextController>
                .Instance()
                .WithData(models)
                .WithoutData(new Func<IEnumerable<object>>(() =>
                    {
                        models.RemoveAt(1);
                        return models;
                    })
                    .Invoke())
                .Calling(c => c.GetAll())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<List<CustomModel>>()
                    .Passing(model => model.Count == 1));
        }

        [Fact]
        public void WithoutDataReturnsCorrectDataWhenAllTableEntitiesAreDeleted()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services.AddDbContext<CustomDbContext>());

            var models = new List<CustomModel> {
                new CustomModel
                {
                    Id = 1,
                    Name = "Test"
                },
                new CustomModel
                {
                    Id = 2,
                    Name = "Test2"
                }};

            MyController<DbContextController>
                .Instance()
                .WithData(models)
                .WithoutData(models)
                .Calling(c => c.GetAll())
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void WithoutDataReturnsCorrectDataWhenDeletingNonExistingObjects()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services.AddDbContext<CustomDbContext>());

            var models = new List<CustomModel> {
                new CustomModel
                {
                    Id = 1,
                    Name = "Test"
                }};

            MyController<DbContextController>
                .Instance()
                .WithoutData(models)
                .Calling(c => c.GetAll())
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void WithoutSetReturnsCorrectDataWhenWholeRangeIsRemoved()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services.AddDbContext<CustomDbContext>());

            var models = new List<CustomModel> {
                new CustomModel
                {
                    Id = 1,
                    Name = "Test"
                }};

            MyController<DbContextController>
                .Instance()
                .WithData(models)
                .WithoutData(data =>
                    data.WithoutSet<CustomModel>(
                        cm => cm.RemoveRange(models)))
                .Calling(c => c.GetAll())
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void WithoutSetReturnsCorrectDataWhenPartialEntitiesAreRemoved()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services.AddDbContext<CustomDbContext>());

            var models = new List<CustomModel> {
                new CustomModel
                {
                    Id = 1,
                    Name = "Test1"
                },
                new CustomModel
                {
                    Id = 2,
                    Name = "Test2"
                }};

            MyController<DbContextController>
                .Instance()
                .WithData(models)
                .WithoutData(data =>
                    data.WithoutSet<CustomModel>(
                        cm => cm.Remove(models.Last())))
                .Calling(c => c.GetAll())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<List<CustomModel>>()
                    .Passing(mdls => mdls.Count == 1));
        }

        [Fact]
        public void WithoutSetReturnsCorrectDataWhenDeletingNonExistingObjects()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services.AddDbContext<CustomDbContext>());

            MyController<DbContextController>
                .Instance()
                .WithoutData(data => data.WithoutSet<CustomModel>(cm => cm.Remove(new CustomModel())))
                .Calling(c => c.GetAll())
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void WithoutDataByProvidingModelAndKeyInBuilderShouldRemoveTheCorrectObject()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services.AddDbContext<CustomDbContext>());

            var model = new CustomModel
            {
                Id = 1,
                Name = "Test"
            };

            MyController<DbContextController>
                .Instance()
                .WithData(model)
                .WithoutData(data => data.WithoutEntityByKey<CustomModel>(model.Id))
                .Calling(c => c.Get(model.Id))
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void WithoutDataByProvidingNonExistingModelAndKeyInBuilderShouldRemoveTheCorrectObject()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services.AddDbContext<CustomDbContext>());

            var model = new CustomModel
            {
                Id = 1,
                Name = "Test"
            };

            var keyToRemove = int.MaxValue;
            MyController<DbContextController>
                .Instance()
                .WithData(model)
                .WithoutData(data => data.WithoutEntityByKey<CustomModel>(keyToRemove))
                .Calling(c => c.Get(model.Id))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<CustomModel>()
                    .Passing(cm => cm.Name.Equals(model.Name)));
        }

        [Fact]
        public void WithoutDataByProvidingModelAndMultpleKeysInBuilderShouldRemoveTheCorrectObjects()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services.AddDbContext<CustomDbContext>());

            var models = new List<CustomModel>
            {
                new CustomModel
                {
                    Id = 1,
                    Name = "Test1"
                },
                new CustomModel
                {
                    Id = 2,
                    Name = "Test2"
                },
            };

            var keys = models.Select(x => x.Id as object).ToList();
            MyController<DbContextController>
                .Instance()
                .WithData(models)
                .WithoutData(data => data.WithoutEntitiesByKeys<CustomModel>(keys))
                .Calling(c => c.Get(models[0].Id))
                .ShouldReturn()
                .NotFound();
        }

        [Fact]
        public void WithoutDataByProvidingNonExistingModelAndMultpleKeysInBuilderShouldRemoveTheCorrectObjects()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithServices(services => services.AddDbContext<CustomDbContext>());

            var models = new List<CustomModel>
            {
                new CustomModel
                {
                    Id = 1,
                    Name = "Test1"
                }
            };

            var keys = new List<object> { int.MaxValue };
            MyController<DbContextController>
                .Instance()
                .WithData(models)
                .WithoutData(data => data.WithoutEntitiesByKeys<CustomModel>(keys))
                .Calling(c => c.Get(models[0].Id))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<CustomModel>()
                    .Passing(cm => cm.Name.Equals(models[0].Name)));
        }
    }
}
