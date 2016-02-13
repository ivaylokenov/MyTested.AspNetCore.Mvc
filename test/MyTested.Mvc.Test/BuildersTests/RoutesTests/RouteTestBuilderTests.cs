namespace MyTested.Mvc.Tests.BuildersTests.RoutesTests
{
    using Setups.Routes;
    using Setups.Startups;
    using System.Text;
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
        public void ToShouldResolveCorrectControllerAndActionWithRequestModelAsObject()
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
    }
}
