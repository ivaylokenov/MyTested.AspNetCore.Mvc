namespace MusicStore.Test.Routing
{
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using System.Threading;
    using Xunit;

    public class ShoppingCartRouteTest
    {
        [Fact]
        public void IndexShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routing()
                .ShouldMap("/ShoppingCart/Index")
                .To<ShoppingCartController>(c => c.Index());
        }
        
        [Fact]
        public void AddToCartShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routing()
                .ShouldMap("/ShoppingCart/AddToCart/1")
                .To<ShoppingCartController>(c => c.AddToCart(1, With.Any<CancellationToken>()));
        }

        [Fact]
        public void RemoveFromCartShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routing()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/ShoppingCart/RemoveFromCart/1"))
                .To<ShoppingCartController>(c => c.RemoveFromCart(1, With.Any<CancellationToken>()));
        }
    }
}
