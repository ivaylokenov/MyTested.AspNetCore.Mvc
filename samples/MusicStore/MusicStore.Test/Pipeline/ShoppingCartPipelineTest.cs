namespace MusicStore.Test.Pipeline
{
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using ViewModels;
    using Xunit;

    public class ShoppingCartPipelineTest
    {
        [Fact]
        public void GetIndexShouldReturnNoCartItemsWhenSessionIsEmpty()
        {
            MyMvc
                .Pipeline()
                .ShouldMap("/ShoppingCart/Index")
                .To<ShoppingCartController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ShoppingCartViewModel>()
                    .Passing(model =>
                    {
                        Assert.Empty(model.CartItems);
                        Assert.Equal(0, model.CartTotal);
                    }));
        }

        [Fact]
        public void GetIndexShouldReturnNoCartItemsWhenNoCartItemsInCart()
        {
            MyMvc
                .Pipeline()
                .ShouldMap("/ShoppingCart/Index")
                .To<ShoppingCartController>(c => c.Index())
                .Which()
                .WithSession(session => session.WithEntry("Session", "CartId_A"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ShoppingCartViewModel>()
                    .Passing(model =>
                    {
                        Assert.Empty(model.CartItems);
                        Assert.Equal(0, model.CartTotal);
                    }));
        }
    }
}
