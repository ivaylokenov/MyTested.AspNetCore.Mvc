namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.RoutingTests
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
    using Microsoft.AspNetCore.Http;
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
            MyRouting
                .Configuration()
                .ShouldMap("/")
                .ToAction("Index");
        }
        
        [Fact]
        public void ToActionShouldNotThrowExceptionWithCorrectFullAction()
        {
            MyRouting
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
                    MyRouting
                        .Configuration()
                        .ShouldMap("/")
                        .ToAction("Home");
                },
                "Expected route '/' to match Home action but in fact matched Index.");
        }

        [Fact]
        public void ToControllerShouldNotThrowExceptionWithCorrectController()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/")
                .ToController("Home");
        }

        [Fact]
        public void ToControllerAndActionShouldNotThrowExceptionWithCorrectActionAndController()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/")
                .To("Index", "Home");
        }

        [Fact]
        public void ToControllerWithGenericShouldNotThrowExceptionWithCorrectController()
        {
            MyRouting
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
                    MyRouting
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
                    MyRouting
                        .Configuration()
                        .ShouldMap("/")
                        .ToController("Index");
                },
                "Expected route '/' to match Index controller but in fact matched Home.");
        }

        [Fact]
        public void ToRouteValueShouldNotThrowExceptionWithCorrectRouteValueKey()
        {
            MyRouting
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
                    MyRouting
                        .Configuration()
                        .ShouldMap("/Home/Contact/1")
                        .ToRouteValue("name");
                },
                "Expected route '/Home/Contact/1' to contain route value with 'name' key but such was not found.");
        }

        [Fact]
        public void ToRouteValueShouldNotThrowExceptionWithCorrectRouteValue()
        {
            MyRouting
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
                    MyRouting
                        .Configuration()
                        .ShouldMap("/Home/Contact/1")
                        .ToRouteValue("id", 2);
                },
                "Expected route '/Home/Contact/1' to contain route value with 'id' key and the provided value but the value was different. Expected a value of '2', but in fact it was '1'.");
        }

        [Fact]
        public void ToRouteValuesShouldNotThrowExceptionWithCorrectRouteValues()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .ToRouteValues(new { controller = "Home", action = "Contact", id = 1 });
        }

        [Fact]
        public void ToRouteValuesShouldNotMakeCountCheckWithProvidedLambda()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .To<HomeController>(c => c.Contact(1))
                .AndAlso()
                .ToRouteValues(new { id = 1 });
        }

        [Fact]
        public void ToRouteValuesShouldNotThrowExceptionWithCorrectRouteValuesAsDictionary()
        {
            MyRouting
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
                    MyRouting
                        .Configuration()
                        .ShouldMap("/Home/Contact/1")
                        .ToRouteValues(new { controller = "Home", action = "Index", id = 1 });
                },
                "Expected route '/Home/Contact/1' to contain route value with 'action' key and the provided value but the value was different - Contact.");
        }

        [Fact]
        public void ToRouteValuesShouldThrowExceptionWithIncorrectRouteValuesWithSingleCountError()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRouting
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
                    MyRouting
                        .Configuration()
                        .ShouldMap("/Home/Contact/1")
                        .ToRouteValues(new { id = 1, query = "invalid", another = "another", fourth = "test" });
                },
                "Expected route '/Home/Contact/1' to contain 4 route values but in fact found 3.");
        }

        [Fact]
        public void ToDataTokenShouldNotThrowExceptionWithCorrectDataTokenKey()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("/Test")
                .ToDataToken("random");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToDataTokenShouldThrowExceptionWithIncorrectDataTokenKey()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRouting
                        .Configuration()
                        .ShouldMap("/Test")
                        .ToDataToken("name");
                },
                "Expected route '/Test' to contain data token with 'name' key but such was not found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToDataTokenShouldNotThrowExceptionWithCorrectDataToken()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("/Test")
                .ToDataToken("random", "value");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToDataTokenShouldThrowExceptionWithIncorrectDataToken()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRouting
                        .Configuration()
                        .ShouldMap("/Test")
                        .ToDataToken("random", 2);
                },
                "Expected route '/Test' to contain data token with 'random' key and the provided value but the value was different. Expected a value of Int32 type, but in fact it was String.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToDataTokensShouldNotThrowExceptionWithCorrectDataTokens()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("/Test")
                .ToDataTokens(new { random = "value", another = "token" });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToDataTokensShouldNotThrowExceptionWithCorrectDataTokensAsDictionary()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("/Test")
                .ToDataTokens(new Dictionary<string, object>
                {
                    ["random"] = "value",
                    ["another"] = "token"
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToDataTokensShouldThrowExceptionWithIncorrectDataTokens()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRouting
                        .Configuration()
                        .ShouldMap("/Test")
                        .ToDataTokens(new { random = "value", another = "invalid" });
                },
                "Expected route '/Test' to contain data token with 'another' key and the provided value but the value was different. Expected a value of 'invalid', but in fact it was 'token'.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToDataTokenShouldThrowExceptionWithIncorrectDataTokensWithSingleCountError()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRouting
                        .Configuration()
                        .ShouldMap("/Test")
                        .ToDataTokens(new { id = 1 });
                },
                "Expected route '/Test' to contain 1 data token but in fact found 2.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToDataTokensShouldThrowExceptionWithIncorrectDataTokensWithMultipleCountError()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRouting
                        .Configuration()
                        .ShouldMap("/Test")
                        .ToDataTokens(new { id = 1, query = "invalid", another = "another", fourth = "test" });
                },
                "Expected route '/Test' to contain 4 data tokens but in fact found 2.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithEmptyPath()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndAction()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home/AsyncMethod")
                .To<HomeController>(c => c.AsyncMethod());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionIfNoLocationIsSet()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request.WithMethod(HttpMethod.Post))
                .To<HomeController>(c => c.Index());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithPartialPath()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home")
                .To<HomeController>(c => c.Index());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithNormalPath()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home/Index")
                .To<HomeController>(c => c.Index());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithNormalPathAndRouteParameter()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .To<HomeController>(c => c.Contact(1));
        }
        
        [Fact]
        public void ToShouldResolveCorrectlyWithIgnoredParameter()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .To<HomeController>(c => c.Contact(With.Any<int>()));
        }

        [Fact]
        public void ToShouldResolveCorrectlyWithIgnoredParameterButActualValue()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .To<HomeController>(c => c.Contact(With.Value(2)));
        }

        [Fact]
        public void ToShouldResolveCorrectlyWithIgnoredParameterButActualValueLongName()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .To<HomeController>(c => c.Contact(With.IgnoredRouteValue(2)));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithNoModel()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Normal/ActionWithMultipleParameters/1")
                .To<NormalController>(c => c.ActionWithMultipleParameters(1, With.No<string>(), With.No<RequestModel>()));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithQueryString()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Normal/ActionWithMultipleParameters/1?text=test")
                .To<NormalController>(c => c.ActionWithMultipleParameters(1, "test", With.No<RequestModel>()));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithRequestModelAsString()
        {
            MyRouting
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
            MyRouting
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
            MyRouting
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
            MyRouting
                .Configuration()
                .ShouldMap("/Normal/AnotherName")
                .To<NormalController>(c => c.ActionWithChangedName());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithRouteAttributes()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/AttributeController/AttributeAction")
                .To<RouteController>(c => c.Index());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithRouteAttributesWithParameter()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/AttributeController/Action/1")
                .To<RouteController>(c => c.Action(1));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithPocoController()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Poco/Action/1")
                .To<PocoController>(c => c.Action(1));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithEmptyArea()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("/Files")
                .To<DefaultController>(c => c.Test("None"));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithNonEmptyArea()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("/Files/Default/Download/Test")
                .To<DefaultController>(c => c.Download("Test"));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithDefaultValues()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("/CustomRoute")
                .To<NormalController>(c => c.FromRouteAction(new RequestModel { Integer = 1, String = "test" }));

            MyApplication.StartsFrom<DefaultStartup>();
        }
        
        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithConventions()
        {
            MyRouting
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
                    MyRouting
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
                    MyRouting
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
                    MyRouting
                        .Configuration()
                        .ShouldMap("/")
                        .To<RouteController>(c => c.Index());
                },
                "Expected route '/' to match Index action in RouteController but instead matched HomeController.");
        }

        [Fact]
        public void ToShouldResolveNonExistingRouteWithInvalidGetMethod()
        {
            MyRouting
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
                    MyRouting
                        .Configuration()
                        .ShouldMap("/")
                        .ToNonExistingRoute();
                },
                "Expected route '/' to be non-existing but in fact it was resolved successfully.");
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithCorrectHttpMethod()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/Normal/ActionWithModel/1"))
                .To<NormalController>(c => c.ActionWithModel(1, With.No<RequestModel>()));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithFromRouteAction()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("/CustomRoute")
                .To<NormalController>(c => c.FromRouteAction(new RequestModel
                {
                    Integer = 1,
                    String = "test"
                }));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithFromQueryAction()
        {
            MyRouting
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
            MyRouting
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
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Normal/FromHeaderAction")
                    .WithHeader("MyHeader", "MyHeaderValue"))
                .To<NormalController>(c => c.FromHeaderAction("MyHeaderValue"));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithFromServicesAction()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Normal/FromServicesAction")
                .To<NormalController>(c => c.FromServicesAction(From.Services<IActionSelector>()));
        }

        [Fact]
        public void ShouldMapWithRequestShouldWorkCorrectly()
        {
            var context = new DefaultHttpContext();
            context.Request.Path = "/Normal/FromServicesAction";

            MyRouting
                .Configuration()
                .ShouldMap(context.Request)
                .To<NormalController>(c => c.FromServicesAction(From.Services<IActionSelector>()));
        }

        [Fact]
        public void ShouldMapWithUriShouldWorkCorrectly()
        {
            MyRouting
                .Configuration()
                .ShouldMap(new Uri("/Normal/FromServicesAction", UriKind.Relative))
                .To<NormalController>(c => c.FromServicesAction(From.Services<IActionSelector>()));
        }

        [Fact]
        public void ShouldMapShouldNotExecuteAuthorizationFiltersAndShouldValidateJustRoutes()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Normal/AuthorizedAction")
                .To<NormalController>(c => c.AuthorizedAction());
        }

        [Fact]
        public void ShouldMapShouldNotExecuteActionFiltersAndShouldValidateJustRoutes()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Normal/FiltersAction")
                .To<NormalController>(c => c.FiltersAction());
        }

        [Fact]
        public void ShouldMapShouldNotExecuteCustomActionFiltersAndShouldValidateJustRoutes()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Normal/CustomFiltersAction?throw=true")
                .To<NormalController>(c => c.CustomFiltersAction());
        }

        [Fact]
        public void ShouldMapShouldNotExecuteActionFiltersAndShouldValidateJustRoutesAndModelBinding()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Normal/FiltersActionWithModelBinding/1")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(new
                    {
                        Integer = 1,
                        String = "Text"
                    }))
                .To<NormalController>(c => c.FiltersActionWithModelBinding(
                    1,
                    new RequestModel
                    {
                        Integer = 1,
                        String = "Text"
                    }));
        }

        [Fact]
        public void UltimateCrazyModelBindingTest()
        {
            MyRouting
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
