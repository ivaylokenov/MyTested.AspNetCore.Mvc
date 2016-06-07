namespace MyTested.Mvc.Test.BuildersTests.ControllerTests
{
    using System;
    using Internal.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ControllerBuilderTests
    {

        [Fact]
        public void WithSessionShouldPopulateSessionCorrectly()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyMvc
                .Controller<MvcController>()
                .WithSession(session =>
                {
                    session.WithEntry("test", "value");
                })
                .Calling(c => c.SessionAction())
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithSessionShouldThrowExceptionIfSessionIsNotSet()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .WithSession(session =>
                       {
                           session.WithEntry("test", "value");
                       })
                       .Calling(c => c.SessionAction())
                       .ShouldReturn()
                       .Ok();
                },
                "Session has not been configured for this application or request.");
        }
        
        [Fact]
        public void WithSessionShouldPopulateSessionCorrectlyForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                    services.AddHttpContextAccessor();
                });

            MyMvc
                .Controller<PocoController>()
                .WithSession(session =>
                {
                    session.WithEntry("test", "value");
                })
                .Calling(c => c.SessionAction())
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithSessionShouldThrowExceptionIfSessionIsNotSetForPocoController()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyMvc
                       .Controller<PocoController>()
                       .WithSession(session =>
                       {
                           session.WithEntry("test", "value");
                       })
                       .Calling(c => c.SessionAction())
                       .ShouldReturn()
                       .Ok();
                },
                "Session has not been configured for this application or request.");
        }

        [Fact]
        public void WithHttpContextShouldPopulateCustomHttpContextForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                    services.AddHttpContextAccessor();
                });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "Custom";
            httpContext.Response.StatusCode = 404;
            httpContext.Response.ContentType = ContentType.ApplicationJson;
            httpContext.Response.ContentLength = 100;

            MyMvc
                .Controller<PocoController>()
                .WithHttpContext(httpContext)
                .ShouldPassFor()
                .TheHttpContext(setHttpContext =>
                {
                    Assert.Equal("Custom", setHttpContext.Request.Scheme);
                    Assert.IsAssignableFrom<MockedHttpResponse>(setHttpContext.Response);
                    Assert.Same(httpContext.Response.Body, setHttpContext.Response.Body);
                    Assert.Equal(httpContext.Response.ContentLength, setHttpContext.Response.ContentLength);
                    Assert.Equal(httpContext.Response.ContentType, setHttpContext.Response.ContentType);
                    Assert.Equal(httpContext.Response.StatusCode, setHttpContext.Response.StatusCode);
                    Assert.Same(httpContext.Items, setHttpContext.Items);
                    Assert.Same(httpContext.Features, setHttpContext.Features);
                    Assert.Same(httpContext.RequestServices, setHttpContext.RequestServices);
                    Assert.Same(httpContext.Session, setHttpContext.Session);
                    Assert.Same(httpContext.TraceIdentifier, setHttpContext.TraceIdentifier);
                    Assert.Same(httpContext.User, setHttpContext.User);
                });

            MyMvc
                .Controller<PocoController>()
                .WithHttpContext(httpContext)
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.Equal("Custom", controller.CustomHttpContext.Request.Scheme);
                    Assert.IsAssignableFrom<MockedHttpResponse>(controller.CustomHttpContext.Response);
                    Assert.Same(httpContext.Response.Body, controller.CustomHttpContext.Response.Body);
                    Assert.Equal(httpContext.Response.ContentLength, controller.CustomHttpContext.Response.ContentLength);
                    Assert.Equal(httpContext.Response.ContentType, controller.CustomHttpContext.Response.ContentType);
                    Assert.Equal(httpContext.Response.StatusCode, controller.CustomHttpContext.Response.StatusCode);
                    Assert.Same(httpContext.Items, controller.CustomHttpContext.Items);
                    Assert.Same(httpContext.Features, controller.CustomHttpContext.Features);
                    Assert.Same(httpContext.RequestServices, controller.CustomHttpContext.RequestServices);
                    Assert.Same(httpContext.Session, controller.CustomHttpContext.Session);
                    Assert.Same(httpContext.TraceIdentifier, controller.CustomHttpContext.TraceIdentifier);
                    Assert.Same(httpContext.User, controller.CustomHttpContext.User);
                });

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithHttpContextShouldPopulateCustomHttpContext()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "Custom";
            httpContext.Response.StatusCode = 404;
            httpContext.Response.ContentType = ContentType.ApplicationJson;
            httpContext.Response.ContentLength = 100;

            MyMvc
                .Controller<MvcController>()
                .WithHttpContext(httpContext)
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.Equal("Custom", controller.HttpContext.Request.Scheme);
                    Assert.IsAssignableFrom<MockedHttpResponse>(controller.HttpContext.Response);
                    Assert.Same(httpContext.Response.Body, controller.HttpContext.Response.Body);
                    Assert.Equal(httpContext.Response.ContentLength, controller.HttpContext.Response.ContentLength);
                    Assert.Equal(httpContext.Response.ContentType, controller.HttpContext.Response.ContentType);
                    Assert.Equal(httpContext.Response.StatusCode, controller.HttpContext.Response.StatusCode);
                    Assert.Same(httpContext.Items, controller.HttpContext.Items);
                    Assert.Same(httpContext.Features, controller.HttpContext.Features);
                    Assert.Same(httpContext.RequestServices, controller.HttpContext.RequestServices);
                    Assert.Same(httpContext.Session, controller.HttpContext.Session);
                    Assert.Same(httpContext.TraceIdentifier, controller.HttpContext.TraceIdentifier);
                    Assert.Same(httpContext.User, controller.HttpContext.User);
                })
                .TheHttpContext(setHttpContext =>
                {
                    Assert.Equal("Custom", setHttpContext.Request.Scheme);
                    Assert.IsAssignableFrom<MockedHttpResponse>(setHttpContext.Response);
                    Assert.Same(httpContext.Response.Body, setHttpContext.Response.Body);
                    Assert.Equal(httpContext.Response.ContentLength, setHttpContext.Response.ContentLength);
                    Assert.Equal(httpContext.Response.ContentType, setHttpContext.Response.ContentType);
                    Assert.Equal(httpContext.Response.StatusCode, setHttpContext.Response.StatusCode);
                    Assert.Same(httpContext.Items, setHttpContext.Items);
                    Assert.Same(httpContext.Features, setHttpContext.Features);
                    Assert.Same(httpContext.RequestServices, setHttpContext.RequestServices);
                    Assert.Same(httpContext.Session, setHttpContext.Session);
                    Assert.Same(httpContext.TraceIdentifier, setHttpContext.TraceIdentifier);
                    Assert.Same(httpContext.User, setHttpContext.User);
                });

            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
