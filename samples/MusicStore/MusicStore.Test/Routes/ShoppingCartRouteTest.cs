namespace MusicStore.Test.Routes
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
                .Routes()
                .ShouldMap("/ShoppingCart/Index")
                .To<ShoppingCartController>(c => c.Index());
        }
        
        [Fact]
        public void AddToCartShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routes()
                .ShouldMap("/ShoppingCart/AddToCart/1")
                .To<ShoppingCartController>(c => c.AddToCart(1));
        }

        [Fact]
        public void RemoveFromCartShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routes()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/ShoppingCart/RemoveFromCart/1")
                    .WithAntiForgeryToken())
                .To<ShoppingCartController>(c => c.RemoveFromCart(1, With.Any<CancellationToken>()));
        }
    }
}
