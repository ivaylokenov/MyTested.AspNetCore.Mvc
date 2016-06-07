namespace MusicStore.Test.Routes
{
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeRouteTest
    {
        [Fact]
        public void ErrorShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
        }
    }
}
