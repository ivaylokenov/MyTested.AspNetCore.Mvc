namespace MyTested.AspNetCore.Mvc.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Internal.Application;
    using Internal.Formatters;
    using Internal.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Setups.Common;
    using Setups.Controllers;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void CustomConfigureOptionsShouldNotOverrideTheDefaultTestOnes()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.Configure<MvcOptions>(options =>
                    {
                        options.MaxModelValidationErrors = 120;
                    });
                });

            var builtOptions = TestApplication.Services.GetRequiredService<IOptions<MvcOptions>>();

            Assert.Equal(120, builtOptions.Value.MaxModelValidationErrors);
            Assert.Contains(typeof(StringInputFormatter), builtOptions.Value.InputFormatters.Select(f => f.GetType()));
            Assert.Equal(1, builtOptions.Value.Conventions.Count);

            MyApplication.IsUsingDefaultConfiguration();
        }


        [Fact]
        public void IHttpContextAccessorShouldWorkCorrectlySynchronously()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.ReplaceTransient<IHttpContextFactory, CustomHttpContextFactory>();
                    services.AddHttpContextAccessor();
                });

            HttpContext firstContext = null;
            HttpContext secondContext = null;

            MyController<HttpContextController>
                .Instance()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    firstContext = controller.Context;
                });

            MyController<HttpContextController>
                .Instance()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    secondContext = controller.Context;
                });

            Assert.NotNull(firstContext);
            Assert.NotNull(secondContext);
            Assert.IsAssignableFrom<MockedHttpContext>(firstContext);
            Assert.IsAssignableFrom<MockedHttpContext>(secondContext);
            Assert.NotSame(firstContext, secondContext);
            Assert.Equal(ContentType.AudioVorbis, firstContext.Request.ContentType);
            Assert.Equal(ContentType.AudioVorbis, secondContext.Request.ContentType);

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void IHttpContextAccessorShouldWorkCorrectlyAsynchronously()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            Task
                .Run(async () =>
                {
                    HttpContext firstContextAsync = null;
                    HttpContext secondContextAsync = null;
                    HttpContext thirdContextAsync = null;
                    HttpContext fourthContextAsync = null;
                    HttpContext fifthContextAsync = null;

                    var tasks = new List<Task>
                    {
                        Task.Run(() =>
                        {
                            MyController<HttpContextController>
                                .Instance()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    firstContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<HttpContextController>
                                .Instance()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    secondContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<HttpContextController>
                                .Instance()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    thirdContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<HttpContextController>
                                .Instance()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    fourthContextAsync = controller.Context;
                                });
                        }),
                        Task.Run(() =>
                        {
                            MyController<HttpContextController>
                                .Instance()
                                .ShouldPassFor()
                                .TheController(controller =>
                                {
                                    fifthContextAsync = controller.Context;
                                });
                        })
                    };

                    await Task.WhenAll(tasks);

                    Assert.NotNull(firstContextAsync);
                    Assert.NotNull(secondContextAsync);
                    Assert.NotNull(thirdContextAsync);
                    Assert.NotNull(fourthContextAsync);
                    Assert.NotNull(fifthContextAsync);
                    Assert.IsAssignableFrom<MockedHttpContext>(firstContextAsync);
                    Assert.IsAssignableFrom<MockedHttpContext>(secondContextAsync);
                    Assert.IsAssignableFrom<MockedHttpContext>(thirdContextAsync);
                    Assert.IsAssignableFrom<MockedHttpContext>(fourthContextAsync);
                    Assert.IsAssignableFrom<MockedHttpContext>(fifthContextAsync);
                    Assert.NotSame(firstContextAsync, secondContextAsync);
                    Assert.NotSame(firstContextAsync, thirdContextAsync);
                    Assert.NotSame(secondContextAsync, thirdContextAsync);
                    Assert.NotSame(thirdContextAsync, fourthContextAsync);
                    Assert.NotSame(fourthContextAsync, fifthContextAsync);
                    Assert.NotSame(thirdContextAsync, fifthContextAsync);
                })
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithCustomHttpContextShouldSetItToAccessor()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.ContentType = ContentType.AudioVorbis;

            MyController<HttpContextController>
                .Instance()
                .WithHttpContext(httpContext)
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.HttpContext);
                    Assert.NotNull(controller.Context);
                    Assert.Equal(ContentType.AudioVorbis, controller.HttpContext.Request.ContentType);
                    Assert.Equal(ContentType.AudioVorbis, controller.Context.Request.ContentType);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
