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
                .View()
                .WithModel(With.Default<Order>());
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
                .View()
                .WithModel(With.Default<Order>());
        }
    }
}
