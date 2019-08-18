namespace Blog.Test.Routing
{
    using Blog.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeRouteTest
    {
        [Fact]
        public void GetIndexShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());
        
        [Fact]
        public void GetPrivacyShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Privacy")
                .To<HomeController>(c => c.Privacy());
    }
}
