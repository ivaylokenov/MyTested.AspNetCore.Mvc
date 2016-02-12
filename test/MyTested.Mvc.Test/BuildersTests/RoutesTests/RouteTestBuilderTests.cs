namespace MyTested.Mvc.Tests.BuildersTests.RoutesTests
{
    using Setups.Routes;
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
        public void ToShouldResolverCorrectControllerAndActionWithNoModel()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Normal/ActionWithMultipleParameters/1")
                .To<NormalController>(c => c.ActionWithMultipleParameters(1, With.No<string>(), With.No<RequestModel>()));
        }
    }
}
