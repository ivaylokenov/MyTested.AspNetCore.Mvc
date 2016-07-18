namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.RoutesTests
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Setups;
    using Setups.Routing;
    using Setups.Startups;
    using Xunit;

    public class RouteTestBuilderTests
    {
        [Fact]
        public void ToActionShouldNotThrowExceptionWithCorrectAction()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/")
                .ToAction("Index");
        }
        
        [Fact]
        public void ToActionShouldNotThrowExceptionWithCorrectFullAction()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Home/Index")
                .To<HomeController>(c => c.Index());
        }

        [Fact]
        public void ToActionShouldThrowExceptionWithIncorrectAction()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/")
                        .ToAction("Home");
                },
                "Expected route '/' to match Home action but in fact matched Index.");
        }

        [Fact]
        public void ToControllerShouldNotThrowExceptionWithCorrectController()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/")
                .ToController("Home");
        }

        [Fact]
        public void ToControllerAndActionShouldNotThrowExceptionWithCorrectActionAndController()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/")
                .To("Index", "Home");
        }

        [Fact]
        public void ToControllerWithGenericShouldNotThrowExceptionWithCorrectController()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>();
        }

        [Fact]
        public void ToControllerShouldThrowExceptionWithIncorrectControllerType()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/")
                        .To<RouteController>();
                },
                "Expected route '/' to match RouteController but in fact matched HomeController.");
        }

        [Fact]
        public void ToControllerShouldThrowExceptionWithIncorrectAction()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/")
                        .ToController("Index");
                },
                "Expected route '/' to match Index controller but in fact matched Home.");
        }

        [Fact]
        public void ToRouteValueShouldNotThrowExceptionWithCorrectRouteValueKey()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .ToRouteValue("id");
        }

        [Fact]
        public void ToRouteValueShouldThrowExceptionWithIncorrectRouteValueKey()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/Home/Contact/1")
                        .ToRouteValue("name");
                },
                "Expected route '/Home/Contact/1' to contain route value with 'name' key but such was not found.");
        }

        [Fact]
        public void ToRouteValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .ToRouteValue("id", 1);
        }

        [Fact]
        public void ToRouteValueShouldThrowExceptionWithIncorrectRouteValue()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/Home/Contact/1")
                        .ToRouteValue("id", 2);
                },
                "Expected route '/Home/Contact/1' to contain route value with 'id' key and the provided value but the value was different.");
        }

        [Fact]
        public void ToRouteValuesShouldNotThrowExceptionWithCorrectRouteValues()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .ToRouteValues(new { controller = "Home", action = "Contact", id = 1 });
        }

        [Fact]
        public void ToRouteValuesShouldNotMakeCountCheckWithProvidedLambda()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .To<HomeController>(c => c.Contact(1))
                .AndAlso()
                .ToRouteValues(new { id = 1 });
        }

        [Fact]
        public void ToRouteValuesShouldNotThrowExceptionWithCorrectRouteValuesAsDictionary()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .ToRouteValues(new Dictionary<string, object>
                {
                    ["controller"] = "Home",
                    ["action"] = "Contact",
                    ["id"] = 1
                });
        }

        [Fact]
        public void ToRouteValuesShouldThrowExceptionWithIncorrectRouteValues()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/Home/Contact/1")
                        .ToRouteValues(new { controller = "Home", action = "Index", id = 1 });
                },
                "Expected route '/Home/Contact/1' to contain route value with 'action' key and the provided value but the value was different.");
        }

        [Fact]
        public void ToRouteValuesShouldThrowExceptionWithIncorrectRouteValuesWithSingleCountError()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/Home/Contact/1")
                        .ToRouteValues(new { id = 1 });
                },
                "Expected route '/Home/Contact/1' to contain 1 route value but in fact found 3.");
        }

        [Fact]
        public void ToRouteValuesShouldThrowExceptionWithIncorrectRouteValuesWithMultipleCountError()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/Home/Contact/1")
                        .ToRouteValues(new { id = 1, query = "invalid", another = "another", fourth = "test" });
                },
                "Expected route '/Home/Contact/1' to contain 4 route values but in fact found 3.");
        }

        [Fact]
        public void ToDataTokenShouldNotThrowExceptionWithCorrectDataTokenKey()
        {
            MyApplication.StartsFrom<RoutesStartup>();

            MyRoutes
                .Configuration()
                .ShouldMap("/Test")
                .ToDataToken("random");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToDataTokenShouldThrowExceptionWithIncorrectDataTokenKey()
        {
            MyApplication.StartsFrom<RoutesStartup>();

            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/Test")
                        .ToDataToken("name");
                },
                "Expected route '/Test' to contain data token with 'name' key but such was not found.");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToDataTokenShouldNotThrowExceptionWithCorrectDataToken()
        {
            MyApplication.StartsFrom<RoutesStartup>();

            MyRoutes
                .Configuration()
                .ShouldMap("/Test")
                .ToDataToken("random", "value");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToDataTokenShouldThrowExceptionWithIncorrectDataToken()
        {
            MyApplication.StartsFrom<RoutesStartup>();

            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/Test")
                        .ToDataToken("random", 2);
                },
                "Expected route '/Test' to contain data token with 'random' key and the provided value but the value was different.");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToDataTokensShouldNotThrowExceptionWithCorrectDataTokens()
        {
            MyApplication.StartsFrom<RoutesStartup>();

            MyRoutes
                .Configuration()
                .ShouldMap("/Test")
                .ToDataTokens(new { random = "value", another = "token" });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToDataTokensShouldNotThrowExceptionWithCorrectDataTokensAsDictionary()
        {
            MyApplication.StartsFrom<RoutesStartup>();

            MyRoutes
                .Configuration()
                .ShouldMap("/Test")
                .ToDataTokens(new Dictionary<string, object>
                {
                    ["random"] = "value",
                    ["another"] = "token"
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToDataTokensShouldThrowExceptionWithIncorrectDataTokens()
        {
            MyApplication.StartsFrom<RoutesStartup>();

            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/Test")
                        .ToDataTokens(new { random = "value", another = "invalid" });
                },
                "Expected route '/Test' to contain data token with 'another' key and the provided value but the value was different.");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToDataTokenShouldThrowExceptionWithIncorrectDataTokensWithSingleCountError()
        {
            MyApplication.StartsFrom<RoutesStartup>();

            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/Test")
                        .ToDataTokens(new { id = 1 });
                },
                "Expected route '/Test' to contain 1 data token but in fact found 2.");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToDataTokensShouldThrowExceptionWithIncorrectDataTokensWithMultipleCountError()
        {
            MyApplication.StartsFrom<RoutesStartup>();

            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/Test")
                        .ToDataTokens(new { id = 1, query = "invalid", another = "another", fourth = "test" });
                },
                "Expected route '/Test' to contain 4 data tokens but in fact found 2.");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithEmptyPath()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndAction()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Home/AsyncMethod")
                .To<HomeController>(c => c.AsyncMethod());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionIfNoLocationIsSet()
        {
            MyRoutes
                .Configuration()
                .ShouldMap(request => request.WithMethod(HttpMethod.Post))
                .To<HomeController>(c => c.Index());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithPartialPath()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Home")
                .To<HomeController>(c => c.Index());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithNormalPath()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Home/Index")
                .To<HomeController>(c => c.Index());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithNormalPathAndRouteParameter()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .To<HomeController>(c => c.Contact(1));
        }
        
        [Fact]
        public void ToShouldResolveCorrectlyWithIgnoredParameter()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .To<HomeController>(c => c.Contact(With.Any<int>()));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithNoModel()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Normal/ActionWithMultipleParameters/1")
                .To<NormalController>(c => c.ActionWithMultipleParameters(1, With.No<string>(), With.No<RequestModel>()));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithQueryString()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Normal/ActionWithMultipleParameters/1?text=test")
                .To<NormalController>(c => c.ActionWithMultipleParameters(1, "test", With.No<RequestModel>()));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithRequestModelAsString()
        {
            MyRoutes
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Normal/ActionWithMultipleParameters/1")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(@"{""Integer"":1,""String"":""Text""}"))
                .To<NormalController>(c => c.ActionWithMultipleParameters(
                    1,
                    With.No<string>(),
                    new RequestModel
                    {
                        Integer = 1,
                        String = "Text"
                    }));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithRequestModelAsInstance()
        {
            MyRoutes
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Normal/ActionWithMultipleParameters/1")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(new RequestModel
                    {
                        Integer = 1,
                        String = "Text"
                    }))
                .To<NormalController>(c => c.ActionWithMultipleParameters(
                    1,
                    With.No<string>(),
                    new RequestModel
                    {
                        Integer = 1,
                        String = "Text"
                    }));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithRequestModelAsObject()
        {
            MyRoutes
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Normal/ActionWithMultipleParameters/1")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(new
                    {
                        Integer = 1,
                        String = "Text"
                    }))
                .To<NormalController>(c => c.ActionWithMultipleParameters(
                    1,
                    With.No<string>(),
                    new RequestModel
                    {
                        Integer = 1,
                        String = "Text"
                    }));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithActionNameAttribute()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Normal/AnotherName")
                .To<NormalController>(c => c.ActionWithChangedName());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithRouteAttributes()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/AttributeController/AttributeAction")
                .To<RouteController>(c => c.Index());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithRouteAttributesWithParameter()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/AttributeController/Action/1")
                .To<RouteController>(c => c.Action(1));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithPocoController()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Poco/Action/1")
                .To<PocoController>(c => c.Action(1));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithEmptyArea()
        {
            MyApplication.StartsFrom<RoutesStartup>();

            MyRoutes
                .Configuration()
                .ShouldMap("/Files")
                .To<DefaultController>(c => c.Test("None"));

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithNonEmptyArea()
        {
            MyApplication.StartsFrom<RoutesStartup>();

            MyRoutes
                .Configuration()
                .ShouldMap("/Files/Default/Download/Test")
                .To<DefaultController>(c => c.Download("Test"));

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithDefaultValues()
        {
            MyApplication.StartsFrom<RoutesStartup>();

            MyRoutes
                .Configuration()
                .ShouldMap("/CustomRoute")
                .To<NormalController>(c => c.FromRouteAction(new RequestModel { Integer = 1, String = "test" }));

            MyApplication.IsUsingDefaultConfiguration();
        }
        
        // MVC has bug - uncomment when resolved
        //[Fact]
        //public void ToShouldResolveCorrectControllerAndActionWithRouteConstraints()
        //{
        //    MyRoutes
        //        .Configuration()
        //        .ShouldMap("/Normal/ActionWithConstraint/5")
        //        .To<NormalController>(c => c.ActionWithConstraint(5));
        //}

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithConventions()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/ChangedController/ChangedAction?ChangedParameter=1")
                .To<ConventionsController>(c => c.ConventionsAction(1));
        }

        [Fact]
        public void ToShouldThrowExceptionWithInvalidRoute()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/Normal/ActionWithModel/1")
                        .To<HomeController>(c => c.Index());
                },
                "Expected route '/Normal/ActionWithModel/1' to match Index action in HomeController but action could not be matched.");
        }

        [Fact]
        public void ToShouldThrowExceptionWithDifferentAction()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/")
                        .To<HomeController>(c => c.Contact(1));
                },
                "Expected route '/' to match Contact action in HomeController but instead matched Index action.");
        }

        [Fact]
        public void ToShouldThrowExceptionWithDifferentController()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/")
                        .To<RouteController>(c => c.Index());
                },
                "Expected route '/' to match Index action in RouteController but instead matched HomeController.");
        }

        [Fact]
        public void ToShouldResolveNonExistingRouteWithInvalidGetMethod()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Normal/ActionWithModel/1")
                .ToNonExistingRoute();
        }

        [Fact]
        public void ToNonExistingRouteShouldThrowExceptionWithValidOne()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRoutes
                        .Configuration()
                        .ShouldMap("/")
                        .ToNonExistingRoute();
                },
                "Expected route '/' to be non-existing but in fact it was resolved successfully.");
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithCorrectHttpMethod()
        {
            MyRoutes
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/Normal/ActionWithModel/1"))
                .To<NormalController>(c => c.ActionWithModel(1, With.No<RequestModel>()));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithFromRouteAction()
        {
            MyApplication.StartsFrom<RoutesStartup>();

            MyRoutes
                .Configuration()
                .ShouldMap("/CustomRoute")
                .To<NormalController>(c => c.FromRouteAction(new RequestModel
                {
                    Integer = 1,
                    String = "test"
                }));

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithFromQueryAction()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Normal/FromQueryAction?Integer=1&String=test")
                .To<NormalController>(c => c.FromQueryAction(new RequestModel
                {
                    Integer = 1,
                    String = "test"
                }));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithFromFormAction()
        {
            MyRoutes
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Normal/FromFormAction")
                    .WithFormField("Integer", "1")
                    .WithFormField("String", "test"))
                .To<NormalController>(c => c.FromFormAction(new RequestModel
                {
                    Integer = 1,
                    String = "test"
                }));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithFromHeaderAction()
        {
            MyRoutes
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Normal/FromHeaderAction")
                    .WithHeader("MyHeader", "MyHeaderValue"))
                .To<NormalController>(c => c.FromHeaderAction("MyHeaderValue"));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithFromServicesAction()
        {
            MyRoutes
                .Configuration()
                .ShouldMap("/Normal/FromServicesAction")
                .To<NormalController>(c => c.FromServicesAction(From.Services<IActionSelector>()));
        }

        [Fact]
        public void ShouldMapWithRequestShouldWorkCorrectly()
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());
            request.Path = "/Normal/FromServicesAction";

            MyRoutes
                .Configuration()
                .ShouldMap(request)
                .To<NormalController>(c => c.FromServicesAction(From.Services<IActionSelector>()));
        }

        [Fact]
        public void ShouldMapWithUriShouldWorkCorrectly()
        {
            MyRoutes
                .Configuration()
                .ShouldMap(new Uri("/Normal/FromServicesAction", UriKind.Relative))
                .To<NormalController>(c => c.FromServicesAction(From.Services<IActionSelector>()));
        }

        [Fact]
        public void UltimateCrazyModelBindingTest()
        {
            MyRoutes
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Normal/UltimateModelBinding/100?myQuery=Test")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(new
                    {
                        Integer = 1,
                        String = "MyBodyValue"
                    })
                    .WithFormField("MyField", "MyFieldValue")
                    .WithHeader("MyHeader", "MyHeaderValue"))
                .To<NormalController>(c => c.UltimateModelBinding(
                    new ModelBindingModel
                    {
                        Body = new RequestModel { Integer = 1, String = "MyBodyValue" },
                        Form = "MyFieldValue",
                        Route = 100,
                        Query = "Test",
                        Header = "MyHeaderValue"
                    },
                    From.Services<IUrlHelperFactory>()));
        }
    }
}
