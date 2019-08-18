namespace MusicStore.Test.Controllers
{
    using System.Threading;
    using System.Linq;
    using Models;
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using HttpMethod = System.Net.Http.HttpMethod;

    public class CheckoutControllerTest
    {
        [Fact]
        public void CheckoutControllerShouldBeOnlyForAuthorizedUsers()
        {
            MyMvc
                .Controller<CheckoutController>()
                .ShouldHave()
                .Attributes(attrs => attrs
                    .RestrictingForAuthorizedRequests());
        }

        [Fact]
        public void GetAddressAndPaymentShouldReturnDefaultView()
        {
            MyMvc
                .Controller<CheckoutController>()
                .Calling(c => c.AddressAndPayment())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void PostAddressAndPaymentShouldHaveValidAttributes()
        {
            MyMvc
                .Controller<CheckoutController>()
                .Calling(c => c.AddressAndPayment(
                    With.No<MusicStoreContext>(),
                    With.Default<Order>(),
                    CancellationToken.None))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .ValidatingAntiForgeryToken())
                .AndAlso()
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<Order>());
        }

        [Fact]
        public void PostAddressAndPaymentShouldRedirectToCompleteWhenSuccessful()
        {
            int orderId = 10;
            string cartId = "CartId_A";

            MyMvc
                .Controller<CheckoutController>()
                .WithHttpRequest(request => request
                    .WithFormField("PromoCode", "FREE"))
                .WithSession(session => session
                    .WithEntry("Session", cartId))
                .WithUser()
                .WithRouteData()
                .WithData(db => db
                    .WithEntities(entities =>
                    {
                        var cartItems = CreateTestCartItems(
                            cartId,
                            itemPrice: 10,
                            numberOfItem: 1);

                        entities.AddRange(cartItems.Select(n => n.Album).Distinct());
                        entities.AddRange(cartItems);
                    }))
                .WithoutValidation()
                .Calling(c => c.AddressAndPayment(
                    From.Services<MusicStoreContext>(),
                    new Order { OrderId = orderId },
                    CancellationToken.None))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<CheckoutController>(c => c.Complete(With.No<MusicStoreContext>(), orderId)));
        }

        [Fact]
        public void PostAddressAndPaymentShouldReturnViewWithInvalidPromoCode()
        {
            MyMvc
                .Controller<CheckoutController>()
                .WithHttpRequest(request => request
                    .WithFormField("PromoCode", "Invalid"))
                .Calling(c => c.AddressAndPayment(
                    With.No<MusicStoreContext>(),
                    With.Default<Order>(),
                    CancellationToken.None))
                .ShouldReturn()
                .View(With.Default<Order>());
        }

        [Fact]
        public void PostAddressAndPaymentShouldReturnViewWithTheOrderIfRequestIsCancelled()
        {
            MyMvc
                .Controller<CheckoutController>()
                .Calling(c => c.AddressAndPayment(
                    From.Services<MusicStoreContext>(),
                    With.Default<Order>(),
                    new CancellationToken(true)))
                .ShouldReturn()
                .View(With.Default<Order>());
        }

        [Fact]
        public void CompleteShouldReturnViewWithCorrectIdWithCorrectOrder()
        {
            MyMvc
                .Controller<CheckoutController>()
                .WithUser(user => user.WithUsername("TestUser"))
                .WithData(dbContext =>
                    dbContext.WithSet<Order>(o => o.Add(new Order
                    {
                        OrderId = 1,
                        Username = "TestUser"
                    })))
                .Calling(c => c.Complete(From.Services<MusicStoreContext>(), 1))
                .ShouldReturn()
                .View(1);
        }

        [Fact]
        public void CompleteShouldReturnViewWithErrorWithIncorrectOrder()
        {
            MyMvc
                .Controller<CheckoutController>()
                .WithUser(user => user.WithUsername("TestUser"))
                .WithData(dbContext =>
                    dbContext.WithSet<Order>(o => o.Add(new Order
                    {
                        OrderId = 1,
                        Username = "AnotherUser"
                    })))
                .Calling(c => c.Complete(From.Services<MusicStoreContext>(), 1))
                .ShouldReturn()
                .View("Error");
        }

        private static CartItem[] CreateTestCartItems(string cartId, decimal itemPrice, int numberOfItem)
        {
            var albums = Enumerable.Range(1, 10).Select(n =>
                new Album
                {
                    AlbumId = n,
                    Price = itemPrice,
                }).ToArray();

            var cartItems = Enumerable.Range(1, numberOfItem).Select(n =>
                new CartItem
                {
                    Count = 1,
                    CartId = cartId,
                    AlbumId = n % 10,
                    Album = albums[n % 10],
                }).ToArray();

            return cartItems;
        }
    }
}
