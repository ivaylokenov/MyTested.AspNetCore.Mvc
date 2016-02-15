namespace MyTested.Mvc.Tests.BuildersTests.RoutesTests
{
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Setups.Routes;
    using Setups.Startups;
    using Xunit;

    public class RouteTestBuilderTests
    {
        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithEmptyPath()
        {
            MyMvc
                .Routes()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());
        }
        
        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithPartialPath()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Home")
                .To<HomeController>(c => c.Index());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithNormalPath()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Home/Index")
                .To<HomeController>(c => c.Index());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithNormalPathAndRouteParameter()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Home/Contact/1")
                .To<HomeController>(c => c.Contact(1));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithNoModel()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Normal/ActionWithMultipleParameters/1")
                .To<NormalController>(c => c.ActionWithMultipleParameters(1, With.No<string>(), With.No<RequestModel>()));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithQueryString()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Normal/ActionWithMultipleParameters/1?text=test")
                .To<NormalController>(c => c.ActionWithMultipleParameters(1, "test", With.No<RequestModel>()));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithRequestModelAsString()
        {
            MyMvc
                .Routes()
                .ShouldMap(request => request
                    .WithLocation("/Normal/ActionWithMultipleParameters/1")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(@"{""Integer"":1,""String"":""Text""}"))
                .To<NormalController>(c => c.ActionWithMultipleParameters(1, With.No<string>(), new RequestModel
                {
                    Integer = 1,
                    String = "Text"
                }));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithRequestModelAsInstance()
        {
            MyMvc
                .Routes()
                .ShouldMap(request => request
                    .WithLocation("/Normal/ActionWithMultipleParameters/1")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(new RequestModel { Integer = 1, String = "Text" }))
                .To<NormalController>(c => c.ActionWithMultipleParameters(1, With.No<string>(), new RequestModel
                {
                    Integer = 1,
                    String = "Text"
                }));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithRequestModelAsObject()
        {
            MyMvc
                .Routes()
                .ShouldMap(request => request
                    .WithLocation("/Normal/ActionWithMultipleParameters/1")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(new { Integer = 1, String = "Text" }))
                .To<NormalController>(c => c.ActionWithMultipleParameters(1, With.No<string>(), new RequestModel
                {
                    Integer = 1,
                    String = "Text"
                }));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithActionNameAttribute()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Normal/AnotherName")
                .To<NormalController>(c => c.ActionWithChangedName());
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithRouteAttributes()
        {
            MyMvc
                .Routes()
                .ShouldMap("/AttributeController/AttributeAction")
                .To<RouteController>(c => c.Index());
        }
        
        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithRouteAttributesWithParameter()
        {
            MyMvc
                .Routes()
                .ShouldMap("/AttributeController/Action/1")
                .To<RouteController>(c => c.Action(1));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithPocoController()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Poco/Action/1")
                .To<PocoController>(c => c.Action(1));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithEmptyArea()
        {
            MyMvc.StartsFrom<RoutesStartup>();

            MyMvc
                .Routes()
                .ShouldMap("/Files")
                .To<DefaultController>(c => c.Test("None"));

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithNonEmptyArea()
        {
            MyMvc.StartsFrom<RoutesStartup>();

            MyMvc
                .Routes()
                .ShouldMap("/Files/Default/Download/Test")
                .To<DefaultController>(c => c.Download("Test"));

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithRouteConstraints()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Normal/ActionWithConstraint/5")
                .To<NormalController>(c => c.ActionWithConstraint(5));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithConventions()
        {
            MyMvc
                .Routes()
                .ShouldMap("/ChangedController/ChangedAction?ChangedParameter=1")
                .To<ConventionsController>(c => c.ConventionsAction(1));
        }

        [Fact]
        public void ToShouldResolveNonExistingRouteWithInvalidGetMethod()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Normal/ActionWithModel/1")
                .ToNonExistingRoute();
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithCorrectHttpMethod()
        {
            MyMvc
                .Routes()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/Normal/ActionWithModel/1"))
                .To<NormalController>(c => c.ActionWithModel(1, With.No<RequestModel>()));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithFromRouteAction()
        {
            MyMvc.StartsFrom<RoutesStartup>();

            MyMvc
                .Routes()
                .ShouldMap("/CustomRoute")
                .To<NormalController>(c => c.FromRouteAction(new RequestModel
                {
                    Integer = 1,
                    String = "test"
                }));

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithFromQueryAction()
        {
            MyMvc
                .Routes()
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
            MyMvc
                .Routes()
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
            MyMvc
                .Routes()
                .ShouldMap(request => request
                    .WithLocation("/Normal/FromHeaderAction")
                    .WithHeader("MyHeader", "MyHeaderValue"))
                .To<NormalController>(c => c.FromHeaderAction("MyHeaderValue"));
        }

        [Fact]
        public void ToShouldResolveCorrectControllerAndActionWithFromServicesAction()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Normal/FromServicesAction")
                .To<NormalController>(c => c.FromServicesAction(From.Services<IActionSelector>()));
        }

        [Fact]
        public void UltimateCrazyModelBindingTest()
        {
            MyMvc
                .Routes()
                .ShouldMap(request => request
                    .WithLocation("/Normal/UltimateModelBinding/100?myQuery=Test")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(new { Integer = 1, String = "MyBodyValue" })
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
