namespace MusicStore.Test.Controllers
{
    using System.Threading;
    using Models;
    using MusicStore.Controllers;
    using MyTested.Mvc;
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
        public void CompleteShouldReturnViewWithCorrectIdWithCorrectOrder()
        {
            MyMvc
                .Controller<CheckoutController>()
                .WithAuthenticatedUser(user => user.WithUsername("TestUser"))
                .WithServiceSetupFor<MusicStoreContext>(ms =>
                {
                    ms.Orders.Add(new Order
                    {
                        OrderId = 1,
                        Username = "TestUser"
                    });

                    ms.SaveChanges();
                })
                .Calling(c => c.Complete(From.Services<MusicStoreContext>(), 1))
                .ShouldReturn()
                .View(1);
        }
        
        [Fact]
        public void CompleteShouldReturnViewWithErrorWithIncorrectOrder()
        {
            MyMvc
                .Controller<CheckoutController>()
                .WithAuthenticatedUser(user => user.WithUsername("TestUser"))
                .WithServiceSetupFor<MusicStoreContext>(ms =>
                {
                    ms.Orders.Add(new Order
                    {
                        OrderId = 1,
                        Username = "AnotherUser"
                    });

                    ms.SaveChanges();
                })
                .Calling(c => c.Complete(From.Services<MusicStoreContext>(), 1))
                .ShouldReturn()
                .View("Error");
        }
    }
}
