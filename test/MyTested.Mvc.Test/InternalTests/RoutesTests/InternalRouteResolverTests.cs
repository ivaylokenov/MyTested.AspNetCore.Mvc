namespace MyTested.Mvc.Tests.InternalTests.RoutesTests
{
    using Internal.Application;
    using Internal.Http;
    using Internal.Logging;
    using Internal.Routes;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Builder.Internal;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using Setups.Routes;
    using System.Threading.Tasks;
    using Xunit;

    public class InternalRouteResolverTests
    {
        [Fact]
        public void ResolveShouldResolveCorrectControllerAndActionWithDefaultRoute()
        {
            var routeInfo = InternalRouteResolver.Resolve(
                TestApplication.RouteServices,
                TestApplication.Router, 
                GetRouteContext("/"));

            Assert.True(routeInfo.IsResolved);
            Assert.False(routeInfo.MethodIsNotAllowed);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(HomeController), routeInfo.ControllerType);
            Assert.Equal("Home", routeInfo.ControllerName);
            Assert.Equal("Index", routeInfo.Action);
            Assert.Equal(0, routeInfo.ActionArguments.Count);
            Assert.True(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectControllerAndActionWithActionNameAttribute()
        {
            var routeInfo = InternalRouteResolver.Resolve(
                TestApplication.RouteServices,
                TestApplication.Router,
                GetRouteContext("/Normal/AnotherName"));
            
            Assert.True(routeInfo.IsResolved);
            Assert.False(routeInfo.MethodIsNotAllowed);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("AnotherName", routeInfo.Action);
            Assert.Equal(0, routeInfo.ActionArguments.Count);
            Assert.True(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectlyWithRouteAttribute()
        {
            var routeInfo = InternalRouteResolver.Resolve(
                TestApplication.RouteServices,
                TestApplication.Router,
                GetRouteContext("/AttributeController/AttributeAction"));

            Assert.True(routeInfo.IsResolved);
            Assert.False(routeInfo.MethodIsNotAllowed);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(RouteController), routeInfo.ControllerType);
            Assert.Equal("Route", routeInfo.ControllerName);
            Assert.Equal("Index", routeInfo.Action);
            Assert.Equal(0, routeInfo.ActionArguments.Count);
            Assert.True(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectlyWithParameter()
        {
            var routeInfo = InternalRouteResolver.Resolve(
                TestApplication.RouteServices,
                TestApplication.Router,
                GetRouteContext("/Normal/ActionWithParameters/5"));

            Assert.True(routeInfo.IsResolved);
            Assert.False(routeInfo.MethodIsNotAllowed);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("ActionWithParameters", routeInfo.Action);
            Assert.Equal(1, routeInfo.ActionArguments.Count);
            Assert.Equal(5, routeInfo.ActionArguments["id"].Value);
            Assert.True(routeInfo.ModelState.IsValid);
        }
        
        private RouteContext GetRouteContext(string url, string method = "GET")
        {
            var httpContext = new MockedHttpContext();
            httpContext.Request.Path = new PathString(url);
            httpContext.Request.Method = method;

            return new RouteContext(httpContext);
        }
    }
}
