namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentsTests
{
    using Exceptions;
    using Internal.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Setups;
    using Setups.ViewComponents;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class ViewComponentBuilderTests
    {
        [Fact]
        public void WithHttpContextSessionShouldThrowExceptionIfSessionIsNotRegistered()
        {
            var httpContext = new DefaultHttpContext();

            MyViewComponent<NormalComponent>
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

            MyViewComponent<NormalComponent>
                .Instance()
                .WithHttpContext(httpContext)
                .ShouldPassForThe<NormalComponent>(controller =>
                {
                    Assert.Equal("Custom", controller.HttpContext.Request.Scheme);
                    Assert.IsAssignableFrom<HttpResponseMock>(controller.HttpContext.Response);
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
                    Assert.IsAssignableFrom<HttpResponseMock>(setHttpContext.Response);
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
            MyViewComponent<NormalComponent>
                .Instance()
                .WithHttpContext(httpContext =>
                {
                    httpContext.Request.ContentType = ContentType.ApplicationOctetStream;
                })
                .ShouldPassForThe<NormalComponent>(controller =>
                {
                    Assert.Equal(ContentType.ApplicationOctetStream, controller.HttpContext.Request.ContentType);
                });
        }

        [Fact]
        public void WithRequestShouldNotWorkWithDefaultRequestAction()
        {
            MyViewComponent<HttpRequestComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content();
        }

        [Fact]
        public void WithRequestShouldWorkWithSetRequestAction()
        {
            MyViewComponent<HttpRequestComponent>
                .Instance()
                .WithHttpRequest(req => req.WithFormField("Test", "TestValue"))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithRequestAsObjectShouldWorkWithSetRequestAction()
        {
            var httpContext = new HttpContextMock();
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues> { ["Test"] = "TestValue" });

            MyViewComponent<HttpRequestComponent>
                .Instance()
                .WithHttpRequest(httpContext.Request)
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void HttpResponseAssertionsShouldWorkCorrectly()
        {
            MyViewComponent<NormalComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content()
                .ShouldPassForThe<HttpResponse>(response =>
                {
                    Assert.NotNull(response);
                });
        }

        [Fact]
        public void HttpResponsePredicateShouldWorkCorrectly()
        {
            MyViewComponent<NormalComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldPassForThe<HttpResponse>(response => response != null);
        }

        [Fact]
        public void HttpResponsePredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldPassForThe<HttpResponse>(response => response == null);
                },
                "Expected HttpResponse to pass the given predicate but it failed.");
        }
    }
}
