namespace MyTested.AspNetCore.Mvc.Test.InternalTests.RoutingTests
{
    using System.IO;
    using System.Reflection;
    using Internal.Application;
    using Internal.Http;
    using Internal.Routing;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Setups;
    using Setups.Routing;
    using Xunit;

    public class MvcRouteResolverTests
    {
        public MvcRouteResolverTests() 
            => MyApplication.StartsFrom<TestStartup>();

        [Fact]
        public void ResolveShouldResolveCorrectControllerAndActionWithDefaultRoute()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/"));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(HomeController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Home", routeInfo.ControllerName);
            Assert.Equal("Index", routeInfo.Action);
            Assert.Equal(0, routeInfo.ActionArguments.Count);
            Assert.True(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectControllerAndActionWithActionNameAttribute()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/AnotherName"));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("AnotherName", routeInfo.Action);
            Assert.Equal(0, routeInfo.ActionArguments.Count);
            Assert.True(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectlyWithRouteAttribute()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/AttributeController/AttributeAction"));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(RouteController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Route", routeInfo.ControllerName);
            Assert.Equal("Index", routeInfo.Action);
            Assert.Equal(0, routeInfo.ActionArguments.Count);
            Assert.True(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectlyWithParameter()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/ActionWithParameters/5"));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("ActionWithParameters", routeInfo.Action);
            Assert.Equal(1, routeInfo.ActionArguments.Count);
            Assert.Equal(5, routeInfo.ActionArguments["id"].Value);
            Assert.True(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectlyWithParameterOfDifferentType()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/ActionWithStringParameters/Test"));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("ActionWithStringParameters", routeInfo.Action);
            Assert.Equal(1, routeInfo.ActionArguments.Count);
            Assert.Equal("Test", routeInfo.ActionArguments["id"].Value);
            Assert.True(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectlyWithNonMatchingParameterOfDifferentType()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/ActionWithParameters/Test"));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("ActionWithParameters", routeInfo.Action);
            Assert.Equal(0, routeInfo.ActionArguments.Count);
            Assert.False(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectlyWithParameterAndQueryString()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/ActionWithMultipleParameters/5", queryString: "?text=test", contentType: ContentType.ApplicationJson));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("ActionWithMultipleParameters", routeInfo.Action);
            Assert.Equal(2, routeInfo.ActionArguments.Count);
            Assert.Equal(5, routeInfo.ActionArguments["id"].Value);
            Assert.Equal("test", routeInfo.ActionArguments["text"].Value);
            Assert.False(routeInfo.ActionArguments.ContainsKey("model"));
            Assert.False(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectlyWithSpecificMethod()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/GetMethod"));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("GetMethod", routeInfo.Action);
            Assert.Equal(0, routeInfo.ActionArguments.Count);
            Assert.True(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectlyWithWrongSpecificMethod()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/GetMethod", "POST"));

            Assert.False(routeInfo.IsResolved);
            Assert.Equal("action could not be matched", routeInfo.UnresolvedError);
            Assert.Null(routeInfo.ControllerType);
            Assert.Null(routeInfo.ControllerName);
            Assert.Null(routeInfo.Action);
            Assert.Null(routeInfo.ActionArguments);
            Assert.Null(routeInfo.ModelState);
        }

        [Fact]
        public void ResolveShouldResolveCorrectlyWithFullQueryString()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/QueryString", "POST", "?first=test&second=5"));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("QueryString", routeInfo.Action);
            Assert.Equal(2, routeInfo.ActionArguments.Count);
            Assert.Equal("test", routeInfo.ActionArguments["first"].Value);
            Assert.Equal(5, routeInfo.ActionArguments["second"].Value);
            Assert.True(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectlyWithPartialQueryString()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/QueryString", "POST", "?first=test"));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("QueryString", routeInfo.Action);
            Assert.Equal(1, routeInfo.ActionArguments.Count);
            Assert.Equal("test", routeInfo.ActionArguments["first"].Value);
            Assert.True(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectlyWithMissingQueryString()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/QueryString", "POST"));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("QueryString", routeInfo.Action);
            Assert.Equal(0, routeInfo.ActionArguments.Count);
            Assert.True(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectlyJsonContentBody()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/ActionWithModel/5", "POST", body: @"{""Integer"":5,""String"":""Test""}", contentType: ContentType.ApplicationJson));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("ActionWithModel", routeInfo.Action);
            Assert.Equal(2, routeInfo.ActionArguments.Count);
            Assert.Equal(5, routeInfo.ActionArguments["id"].Value);
            Assert.True(routeInfo.ActionArguments.ContainsKey("model"));

            var model = routeInfo.ActionArguments["model"].Value as RequestModel;

            Assert.NotNull(model);
            Assert.Equal(5, model.Integer);
            Assert.Equal("Test", model.String);

            Assert.True(routeInfo.ModelState.IsValid);
        }
        
        [Fact]
        public void ResolveShouldResolveCorrectlyWithEmptyJsonContentBody()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/ActionWithModel/5", "POST", contentType: ContentType.ApplicationJson));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("ActionWithModel", routeInfo.Action);
            Assert.Equal(1, routeInfo.ActionArguments.Count);
            Assert.Equal(5, routeInfo.ActionArguments["id"].Value);
            Assert.False(routeInfo.ActionArguments.ContainsKey("model"));
            Assert.False(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldResolveCorrectlyWithJsonContentBodyAndQueryString()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/ActionWithMultipleParameters/5", "POST", body: @"{""Integer"":5,""String"":""Test""}", queryString: "?text=test", contentType: ContentType.ApplicationJson));

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("ActionWithMultipleParameters", routeInfo.Action);
            Assert.Equal(3, routeInfo.ActionArguments.Count);
            Assert.Equal(5, routeInfo.ActionArguments["id"].Value);
            Assert.Equal("test", routeInfo.ActionArguments["text"].Value);
            Assert.True(routeInfo.ActionArguments.ContainsKey("model"));

            var model = routeInfo.ActionArguments["model"].Value as RequestModel;

            Assert.NotNull(model);
            Assert.Equal(5, model.Integer);
            Assert.Equal("Test", model.String);

            Assert.True(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldReturnProperErrorWhenTwoActionsAreMatched()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/ActionWithOverloads"));

            Assert.False(routeInfo.IsResolved);
            Assert.Equal(
                "exception was thrown when trying to select an action: 'Multiple actions matched. The following actions matched route data and had all constraints satisfied:\r\n\r\nMyTested.AspNetCore.Mvc.Test.Setups.Routing.NormalController.ActionWithOverloads (MyTested.AspNetCore.Mvc.Test.Setups)\r\nMyTested.AspNetCore.Mvc.Test.Setups.Routing.NormalController.ActionWithOverloads (MyTested.AspNetCore.Mvc.Test.Setups)'",
                routeInfo.UnresolvedError);
            Assert.Null(routeInfo.ControllerType);
            Assert.Null(routeInfo.ControllerName);
            Assert.Null(routeInfo.Action);
            Assert.Null(routeInfo.ActionArguments);
            Assert.Null(routeInfo.ModelState);
        }

        [Fact]
        public void ResolveShouldNotCallTheActualActionCode()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Home/FailingAction"));

            Assert.True(routeInfo.IsResolved);
        }

        [Fact]
        public void ResolveShouldNotReturnErrorWhenRequestFiltersArePresentWithoutFullExecutionAndRequestIsNotSetup()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/FiltersAction"),
                false);

            Assert.True(routeInfo.IsResolved);
            Assert.Null(routeInfo.UnresolvedError);
            Assert.Equal(typeof(NormalController).GetTypeInfo(), routeInfo.ControllerType);
            Assert.Equal("Normal", routeInfo.ControllerName);
            Assert.Equal("FiltersAction", routeInfo.Action);
            Assert.Equal(0, routeInfo.ActionArguments.Count);
            Assert.True(routeInfo.ModelState.IsValid);
        }

        [Fact]
        public void ResolveShouldReturnProperErrorWhenRequestFiltersArePresentWithFullExecutionAndRequestIsNotSetup()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                TestApplication.RoutingServices,
                TestApplication.Router,
                this.GetRouteContext("/Normal/FiltersAction"),
                true);

            Assert.False(routeInfo.IsResolved);
            Assert.Equal(
                "action could not be invoked because of the declared filters. You must set the request properties so that they will pass through the pipeline",
                routeInfo.UnresolvedError);
            Assert.Null(routeInfo.ControllerType);
            Assert.Null(routeInfo.ControllerName);
            Assert.Null(routeInfo.Action);
            Assert.Null(routeInfo.ActionArguments);
            Assert.Null(routeInfo.ModelState);
        }

        [Fact]
        public void ResolveShouldNotCallTheActualActionWithFullExecution()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                   TestApplication.RoutingServices,
                   TestApplication.Router,
                   this.GetRouteContext("/Normal/ThrowableAction"),
                   true);

            Assert.True(routeInfo.IsResolved);
        }

        [Fact]
        public void ResolveShouldNotCallTheActualActionWithoutFullExecution()
        {
            var routeInfo = MvcRouteResolver.Resolve(
                   TestApplication.RoutingServices,
                   TestApplication.Router,
                   this.GetRouteContext("/Normal/ThrowableAction"),
                   false);

            Assert.True(routeInfo.IsResolved);
        }

        private RouteContext GetRouteContext(
            string url, 
            string method = "GET", 
            string queryString = null, 
            string body = null, 
            string contentType = null)
        {
            MyApplication.StartsFrom<DefaultStartup>();

            var httpContext = new HttpContextMock();
            httpContext.Request.Path = new PathString(url);
            httpContext.Request.QueryString = new QueryString(queryString);
            httpContext.Request.Method = method;
            httpContext.Request.ContentType = contentType;

            if (body != null)
            {
                httpContext.Request.Body = new MemoryStream();
                var streamWriter = new StreamWriter(httpContext.Request.Body);
                streamWriter.Write(body);
                streamWriter.Flush();
                httpContext.Request.Body.Position = 0;
            }

            return new RouteContext(httpContext);
        }
    }
}
