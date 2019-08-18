namespace MusicStore.Test.Components
{
    using Models;
    using MusicStore.Components;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class CartSummaryComponentTest
    {
        [Fact]
        public void ComponentShouldReturnCartedItems()
        {
            var cartId = "CartId_A";
            var albumName = "AlbumA";
            var totalCartItems = 10;

            MyMvc
                .ViewComponent<CartSummaryComponent>()
                .WithSession(session => session.WithEntry("Session", cartId))
                .WithData(data => data
                    .WithEntities(entities => entities
                        .AddRange(GetCartItems(cartId, albumName, totalCartItems))))
                .InvokedWith(vc => vc.InvokeAsync())
                .ShouldHave()
                .ViewBag(viewBag => viewBag
                    .ContainingEntry("CartCount", totalCartItems)
                    .ContainingEntry("CartSummary", albumName))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        private static IEnumerable<CartItem> GetCartItems(string cartId, string albumTitle, int itemCount)
        {
            var album = new Album()
            {
                AlbumId = 1,
                Title = albumTitle,
            };

            return Enumerable
                .Range(1, itemCount)
                .Select(n => new CartItem
                {
                    AlbumId = 1,
                    Album = album,
                    Count = 1,
                    CartId = cartId,
                })
                .ToArray();
        }
    }
}
