namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AttributesTests
{
    using System;
    using Exceptions;
    using Microsoft.AspNetCore.Authorization;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    using Microsoft.AspNetCore.Mvc;

    public class ControllerAttributesTestBuilderTests
    {
        [Fact]
        public void ContainingAttributeOfTypeShouldNotThrowExceptionWithControllerWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.ContainingAttributeOfType<AuthorizeAttribute>());
        }

        [Fact]
        public void ContainingAttributeOfTypeShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.ContainingAttributeOfType<AllowAnonymousAttribute>());
                },
                "When testing MvcController was expected to have AllowAnonymousAttribute, but in fact such was not found.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttribute()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test"));
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttributeAndCaseDifference()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/Test"));
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithControllerWithTheAttributeAndWrongTemplate()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.ChangingRouteTo("api/another"));
                },
                "When testing AttributesController was expected to have RouteAttribute with 'api/another' template, but in fact found 'api/test'.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttributeAndCorrectName()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test", withName: "TestRouteAttributes"));
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithTheAttributeAndWrongName()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.ChangingRouteTo("api/test", withName: "AnotherRoute"));
                },
                "When testing AttributesController was expected to have RouteAttribute with 'AnotherRoute' name, but in fact found 'TestRouteAttributes'.");
        }

        [Fact]
        public void ChangingRouteToShouldNotThrowExceptionWithActionWithTheAttributeAndCorrectOrder()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test", withOrder: 1));
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithTheAttributeAndWrongOrder()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.ChangingRouteTo("api/test", withOrder: 2));
                },
                "When testing AttributesController was expected to have RouteAttribute with order of 2, but in fact found 1.");
        }

        [Fact]
        public void ChangingRouteToShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AreaController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.ChangingRouteTo("api/test"));
                },
                "When testing AreaController was expected to have RouteAttribute, but in fact such was not found.");
        }

        [Fact]
        public void SpecifyingAreaShouldNotThrowExceptionWithActionWithTheAttribute()
        {
            MyController<AreaController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.SpecifyingArea("CustomArea"));
        }

        [Fact]
        public void SpecifyingAreaShouldThrowExceptionWithActionWithTheAttributeAndWrongName()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AreaController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingArea("AnotherArea"));
                },
                "When testing AreaController was expected to have 'AnotherArea' area, but in fact found 'CustomArea'.");
        }

        [Fact]
        public void SpecifyingAreaToShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SpecifyingArea("CustomArea"));
                },
                "When testing MvcController was expected to have AreaAttribute, but in fact such was not found.");
        }

        [Fact]
        public void AllowingAnonymousRequestsShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.AllowingAnonymousRequests());
        }

        [Fact]
        public void AllowingAnonymousRequestsShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.AllowingAnonymousRequests());
                },
                "When testing MvcController was expected to have AllowAnonymousAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.RestrictingForAuthorizedRequests());
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.RestrictingForAuthorizedRequests());
                },
                "When testing AttributesController was expected to have AuthorizeAttribute, but in fact such was not found.");
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttributeWithCorrectRoles()
        {
            MyController<MvcController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedRoles: "Admin,Moderator"));
        }

        [Fact]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithControllerWithoutTheAttributeWithIncorrectRoles()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedRoles: "Admin"));
                },
                "When testing MvcController was expected to have AuthorizeAttribute with allowed 'Admin' roles, but in fact found 'Admin,Moderator'.");
        }

        [Fact]
        public void PassingForShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyController<MvcController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .PassingFor<AuthorizeAttribute>(authorize => Assert.Equal("Admin,Moderator", authorize.Roles)));
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectAssertions()
        {
            Assert.ThrowsAny<Exception>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<AuthorizeAttribute>(authorize => Assert.Equal("Admin", authorize.Roles)));
                });
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<ActionNameAttribute>(authorize => Assert.Equal("Admin", authorize.Name)));
                },
                "When testing MvcController was expected to have ActionNameAttribute, but in fact such was not found.");
        }
        
        [Fact]
        public void PassingForShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyController<MvcController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .PassingFor<AuthorizeAttribute>(authorize => authorize.Roles == "Admin,Moderator"));
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectPredicate()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<AuthorizeAttribute>(authorize => authorize.Roles == "Admin"));
                },
                "When testing MvcController was expected to have AuthorizeAttribute passing the given predicate, but it failed.");
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectAttributeWithPredicate()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<AuthorizeAttribute>(authorize => authorize.Roles == "Admin,Moderator")
                            .AndAlso()
                            .PassingFor<ActionNameAttribute>(authorize => authorize.Name == "Admin"));
                },
                "When testing MvcController was expected to have ActionNameAttribute, but in fact such was not found.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes
                    => attributes
                        .AllowingAnonymousRequests()
                        .AndAlso()
                        .ChangingRouteTo("api/test"));
        }
    }
}
