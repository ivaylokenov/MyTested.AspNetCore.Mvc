namespace MusicStore.Test.Routes
{
    using System.Threading;
    using Models;
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class CheckoutRouteTest
    {
        [Fact]
        public void GetAddressAndPaymentShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Checkout/AddressAndPayment")
                .To<CheckoutController>(c => c.AddressAndPayment());
        }

        [Fact]
        public void PostAddressAndPaymentShouldBeRoutedCorrectly()
        {
            var firstName = "FirstNameTest";
            var lastName = "LastNameTest";
            var address = "AddressTest";
            var city = "CityTest";
            var state = "StateTest";
            var postalCode = "PostalTest";
            var country = "CountryTest";
            var phone = "PhoneTest";
            var email = "test@email.com";

            MyMvc
                .Routes()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/Checkout/AddressAndPayment")
                    .WithFormFields(new
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Address = address,
                        City = city,
                        State = state,
                        PostalCode = postalCode,
                        Country = country,
                        Phone = phone,
                        Email = email,
                    })
                    .WithAntiForgeryToken())
                .To<CheckoutController>(c => c.AddressAndPayment(
                    With.Any<MusicStoreContext>(),
                    new Order
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Address = address,
                        City = city,
                        State = state,
                        PostalCode = postalCode,
                        Country = country,
                        Phone = phone,
                        Email = email
                    },
                    With.Any<CancellationToken>()))
                .AndAlso()
                .ToValidModelState();
        }

        [Fact]
        public void GetCompleteShouldBeRoutedCorrectly()
        {
            MyMvc
                .Routes()
                .ShouldMap("/Checkout/Complete/1")
                .To<CheckoutController>(c => c.Complete(With.Any<MusicStoreContext>(), 1));
        }
    }
}
