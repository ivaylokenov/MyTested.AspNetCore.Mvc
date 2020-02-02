namespace MusicStore.Test.Controllers
{
    using System.Linq;
    using System.Threading;
    using Models;
    using Microsoft.AspNetCore.Http;
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using ViewModels;
    using Xunit;

    public class ShoppingCartControllerTest
    {
        [Fact]
        public void IndexShouldReturnNoCartItemsWhenSessionIsEmpty()
        {
            MyMvc
                .Controller<ShoppingCartController>()
                .Calling(c => c.Index())
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
        public void IndexShouldReturnNoCartItemsWhenNoCartItemsInCart()
        {
            MyMvc
                .Controller<ShoppingCartController>()
                .WithSession(session => session.WithEntry("Session", "CartId_A"))
                .Calling(c => c.Index())
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
        public void IndexShouldReturnCartItemsWhenItemsInCart()
        {
            var cartId = "CartId_A";

            MyMvc
                .Controller<ShoppingCartController>()
                .WithSession(session => session.WithEntry("Session", cartId))
                .WithData(db => db
                    .WithEntities(entities => 
                    {
                        var cartItems = CreateTestCartItems(
                            cartId,
                            itemPrice: 10,
                            numberOfItem: 5);

                        entities.AddRange(cartItems.Select(n => n.Album).Distinct());
                        entities.AddRange(cartItems);
                    }))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ShoppingCartViewModel>()
                    .Passing(model =>
                    {
                        Assert.Equal(5, model.CartItems.Count);
                        Assert.Equal(5 * 10, model.CartTotal);
                    }));
        }

        [Fact]
        public void AddToCartShouldAddItemsToCart()
        {
            int albumId = 3;

            MyMvc
                .Controller<ShoppingCartController>()
                .WithSession(session => session.WithEntry("Session", "CartId_A"))
                .WithData(db => db
                    .WithEntities(entities => entities
                        .AddRange(CreateTestAlbums(itemPrice: 10))))
                .Calling(c => c.AddToCart(albumId, CancellationToken.None))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ShoppingCartController>(c => c.Index())
                    .AndAlso()
                    .ShouldPassForThe<HttpContext>(async httpContext =>
                    {
                        var cart = ShoppingCart.GetCart(From.Services<MusicStoreContext>(), httpContext);
                        Assert.Single(await cart.GetCartItems());
                        Assert.Equal(albumId, (await cart.GetCartItems()).Single().AlbumId);
                    }));
        }

        [Fact]
        public void RemoveFromCartShouldRemoveItemFromCart()
        {
            var cartId = "CartId_А";
            var cartItemId = 3;
            var numberOfItem = 5;
            var unitPrice = 10;

            MyMvc
                .Controller<ShoppingCartController>()
                .WithSession(session => session.WithEntry("Session", cartId))
                .WithData(db => db
                    .WithEntities(entities =>
                    {
                        var cartItems = CreateTestCartItems(cartId, unitPrice, numberOfItem);
                        entities.AddRange(cartItems.Select(n => n.Album).Distinct());
                        entities.AddRange(cartItems);
                    }))
                .Calling(c => c.RemoveFromCart(cartItemId, CancellationToken.None))
                .ShouldReturn()
                .Json(json => json
                    .WithModelOfType<ShoppingCartRemoveViewModel>()
                    .Passing(model =>
                    {
                        Assert.Equal(numberOfItem - 1, model.CartCount);
                        Assert.Equal((numberOfItem - 1) * 10, model.CartTotal);
                        Assert.Equal(" has been removed from your shopping cart.", model.Message);
                    }))
                .AndAlso()
                .ShouldPassForThe<HttpContext>(async httpContext =>
                {
                    var cart = ShoppingCart.GetCart(From.Services<MusicStoreContext>(), httpContext);
                    Assert.DoesNotContain(await cart.GetCartItems(), c => c.CartItemId == cartItemId);
                });
        }

        [Fact]
        public void RemoveFromCartShouldReturnNoItemsWhenWrongCartIdIsPassed()
        {
            var cartId = "CartId_А";
            var wrongCartItemId = -1;
            var numberOfItem = 3;
            var unitPrice = 5;

            MyMvc
                .Controller<ShoppingCartController>()
                .WithSession(session => session.WithEntry("Session", cartId))
                .WithData(db => db
                    .WithEntities(entities =>
                    {
                        var cartItems = CreateTestCartItems(cartId, unitPrice, numberOfItem);
                        entities.AddRange(cartItems.Select(n => n.Album).Distinct());
                        entities.AddRange(cartItems);
                    }))
                .Calling(c => c.RemoveFromCart(wrongCartItemId, CancellationToken.None))
                .ShouldReturn()
                .Json(json => json
                    .WithModelOfType<ShoppingCartRemoveViewModel>()
                    .Passing(model =>
                    {
                        Assert.Equal(numberOfItem, model.CartCount);
                        Assert.Equal((numberOfItem) * unitPrice, model.CartTotal);
                        Assert.Equal(0, model.ItemCount);
                        Assert.Equal("Could not find this item, nothing has been removed from your shopping cart.", model.Message);
                    }));
        }

        private static CartItem[] CreateTestCartItems(string cartId, decimal itemPrice, int numberOfItem)
        {
            var albums = CreateTestAlbums(itemPrice);

            var cartItems = Enumerable.Range(1, numberOfItem).Select(n =>
                new CartItem
                {
                    Count = 1,
                    CartId = cartId,
                    AlbumId = n % albums.Length,
                    Album = albums[n % albums.Length],
                }).ToArray();

            return cartItems;
        }

        private static Album[] CreateTestAlbums(decimal itemPrice)
        {
            return Enumerable.Range(1, 10).Select(n =>
                new Album
                {
                    AlbumId = n,
                    Price = itemPrice,
                }).ToArray();
        }
    }
}
