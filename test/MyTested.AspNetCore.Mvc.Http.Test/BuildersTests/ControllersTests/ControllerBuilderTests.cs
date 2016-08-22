namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Internal.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Setups.Controllers;

    public class ControllerBuilderTests
    {
        [Fact]
        public void WithHttpContextSessionShouldThrowExceptionIfSessionIsNotRegistered()
        {
            var httpContext = new DefaultHttpContext();

            MyController<MvcController>
                .Instance()
                .WithHttpContext(httpContext)
                .ShouldPassForThe<HttpContext>(context =>
                {
                    Assert.Throws<InvalidOperationException>(() => context.Session);
                });
        }

        [Fact]
        public void WithHttpContextShouldPopulateCustomHttpContext()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "Custom";
            httpContext.Response.StatusCode = 404;
            httpContext.Response.ContentType = ContentType.ApplicationJson;
            httpContext.Response.ContentLength = 100;

            MyController<MvcController>
                .Instance()
                .WithHttpContext(httpContext)
                .ShouldPassForThe<MvcController>(controller =>
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
                    Assert.Same(httpContext.TraceIdentifier, controller.HttpContext.TraceIdentifier);
                    Assert.Same(httpContext.User, controller.HttpContext.User);
                })
                .ShouldPassForThe<HttpContext>(setHttpContext =>
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
                    Assert.Same(httpContext.TraceIdentifier, setHttpContext.TraceIdentifier);
                    Assert.Same(httpContext.User, setHttpContext.User);
                });
        }

        [Fact]
        public void WithHttpContextSetupShouldPopulateContextProperties()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpContext(httpContext =>
                {
                    httpContext.Request.ContentType = ContentType.ApplicationOctetStream;
                })
                .ShouldPassForThe<MvcController>(controller =>
                {
                    Assert.Equal(ContentType.ApplicationOctetStream, controller.HttpContext.Request.ContentType);
                });
        }

        [Fact]
        public void WithRequestShouldNotWorkWithDefaultRequestAction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void WithRequestShouldWorkWithSetRequestAction()
        {
            MyController<MvcController>
                .Instance()
                .WithHttpRequest(req => req.WithFormField("Test", "TestValue"))
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithRequestAsObjectShouldWorkWithSetRequestAction()
        {
            var httpContext = new MockedHttpContext();
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues> { ["Test"] = "TestValue" });

            MyController<MvcController>
                .Instance()
                .WithHttpRequest(httpContext.Request)
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .Ok();
        }

    }
}
