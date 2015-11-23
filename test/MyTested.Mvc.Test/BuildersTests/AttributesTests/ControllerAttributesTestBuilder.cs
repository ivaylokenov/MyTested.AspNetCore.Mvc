namespace MyTested.Mvc.Tests.BuildersTests.AttributesTests
{
    using Exceptions;
    using Microsoft.AspNet.Authorization;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ControllerAttributesTestBuilderTests
    {
        [Fact]
        public void ContainingAttributeOfTypeShouldNotThrowExceptionWithControllerWithTheAttribute()
        {
            MyMvc
                .Controller<MvcController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ContainingAttributeOfType<AuthorizeAttribute>());
        }

        [Fact]
        public void ContainingAttributeOfTypeShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .ShouldHave()
                    .Attributes(attributes => attributes.ContainingAttributeOfType<AllowAnonymousAttribute>());
            }, "When testing MvcController was expected to have AllowAnonymousAttribute, but in fact such was not found.");

        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttribute()
        {
            MyMvc
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test"));
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttributeAndCaseDifference()
        {
            MyMvc
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/Test"));
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithControllerWithTheAttributeAndWrongTemplate()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<AttributesController>()
                    .ShouldHave()
                    .Attributes(attributes => attributes.ChangingRouteTo("api/another"));
            }, "When testing AttributesController was expected to have RouteAttribute with 'api/another' template, but in fact found 'api/test'.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttributeAndCorrectName()
        {
            MyMvc
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test", withName: "TestRouteAttributes"));
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithTheAttributeAndWrongName()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<AttributesController>()
                    .ShouldHave()
                    .Attributes(attributes => attributes.ChangingRouteTo("api/test", withName: "AnotherRoute"));
            }, "When testing AttributesController was expected to have RouteAttribute with 'AnotherRoute' name, but in fact found 'TestRouteAttributes'.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithActionWithTheAttributeAndCorrectOrder()
        {
            MyMvc
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test", withOrder: 1));
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithTheAttributeAndWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<AttributesController>()
                    .ShouldHave()
                    .Attributes(attributes => attributes.ChangingRouteTo("api/test", withOrder: 2));
            }, "When testing AttributesController was expected to have RouteAttribute with order of 2, but in fact found 1.");
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<AreaController>()
                    .ShouldHave()
                    .Attributes(attributes => attributes.ChangingRouteTo("api/test"));
            }, "When testing AreaController was expected to have RouteAttribute, but in fact such was not found.");
        }

        // TODO: route prefix?
        //[Fact]
        //public void ChangingRoutePrefixToShouldNotThrowExceptionWithCorrectTheAttribute()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .ShouldHave()
        //        .Attributes(attributes => attributes.ChangingRoutePrefixTo("/api/test"));
        //}

        //[Fact]
        //public void ChangingRoutePrefixToShouldNotThrowExceptionWithCorrectTheAttributeAndCaseDifference()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .ShouldHave()
        //        .Attributes(attributes => attributes.ChangingRoutePrefixTo("/api/Test"));
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(AttributeAssertionException),
        //    ExpectedMessage = "When testing MvcController was expected to have RoutePrefixAttribute with '/api/another' prefix, but in fact found '/api/test'.")]
        //public void ChangingRoutePrefixToShouldThrowExceptionWithControllerWithTheAttributeAndWrongPrefix()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .ShouldHave()
        //        .Attributes(attributes => attributes.ChangingRoutePrefixTo("/api/another"));
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(AttributeAssertionException),
        //    ExpectedMessage = "When testing AttributesController was expected to have RoutePrefixAttribute, but in fact such was not found.")]
        //public void ChangingActionNameToShouldThrowExceptionWithActionWithoutTheAttribute()
        //{
        //    MyMvc
        //        .Controller<AttributesController>()
        //        .ShouldHave()
        //        .Attributes(attributes => attributes.ChangingRoutePrefixTo("/api/test"));
        //}

        [Fact]
        public void AllowingAnonymousRequestsShouldNotThrowExceptionWithTheAttribute()
        {
            MyMvc
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.AllowingAnonymousRequests());
        }

        [Fact]
        public void AllowingAnonymousRequestsShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .ShouldHave()
                    .Attributes(attributes => attributes.AllowingAnonymousRequests());
            }, "When testing MvcController was expected to have AllowAnonymousAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttribute()
        {
            MyMvc
                .Controller<MvcController>()
                .ShouldHave()
                .Attributes(attributes => attributes.RestrictingForAuthorizedRequests());
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<AttributesController>()
                    .ShouldHave()
                    .Attributes(attributes => attributes.RestrictingForAuthorizedRequests());
            }, "When testing AttributesController was expected to have AuthorizeAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttributeWithCorrectRoles()
        {
            MyMvc
                .Controller<MvcController>()
                .ShouldHave()
                .Attributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedRoles: "Admin,Moderator"));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithControllerWithoutTheAttributeWithIncorrectRoles()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .ShouldHave()
                    .Attributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedRoles: "Admin"));
            }, "When testing MvcController was expected to have AuthorizeAttribute with allowed 'Admin' roles, but in fact found 'Admin,Moderator'.");
        }

        // TODO: with allowed users?
        //[Fact]
        //public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttributeWithCorrectUsers()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .ShouldHave()
        //        .Attributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedUsers: "John,George"));
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(AttributeAssertionException),
        //    ExpectedMessage = "When testing MvcController was expected to have AuthorizeAttribute with allowed 'John' users, but in fact found 'John,George'.")]
        //public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithControllerWithoutTheAttributeWithIncorrectUsers()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .ShouldHave()
        //        .Attributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedUsers: "John"));
        //}

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes
                    => attributes
                        .AllowingAnonymousRequests()
                        .AndAlso()
                        .ChangingRouteTo("api/test"));
        }
    }
}
