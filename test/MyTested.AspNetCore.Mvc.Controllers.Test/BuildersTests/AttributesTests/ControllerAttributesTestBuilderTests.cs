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
                        .ShouldHave()
                        .Attributes(attributes => attributes.ContainingAttributeOfType<AllowAnonymousAttribute>());
                },
                "When testing MvcController was expected to have AllowAnonymousAttribute, but in fact such was not found.");
        }
        
        [Fact]
        public void PassingForShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyController<MvcController>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<AuthorizeAttribute>(authorize => authorize.Roles == "Admin,Moderator")
                            .AndAlso()
                            .PassingFor<ActionNameAttribute>(authorize => authorize.Name == "Admin"));
                },
                "When testing MvcController was expected to have ActionNameAttribute, but in fact such was not found.");
        }



        [Fact]
        public void IncludingInheritedShouldIncludeAllCustomInheritedAttributesFromBaseClassesAndNotThrowException()
        {
            MyController<InheritControllerAttributes>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.IncludingInherited()
                    .ContainingAttributeOfType<ValidateAntiForgeryTokenAttribute>()
                    .ContainingAttributeOfType<AllowAnonymousAttribute>()
                    .ContainingAttributeOfType<ResponseCacheAttribute>());
        }

        [Fact]
        public void TryingToAssertInheritedAttributesWithoutIncludingInheritedShouldThrowException()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<InheritControllerAttributes>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes//.IncludingInherited()
                            .ContainingAttributeOfType<ValidateAntiForgeryTokenAttribute>()
                            .ContainingAttributeOfType<AllowAnonymousAttribute>()
                            .ContainingAttributeOfType<ResponseCacheAttribute>());
                }, "When testing InheritAttributesController was expected to have ValidateAntiForgeryTokenAttribute, but in fact such was not found.");
        }
    }
}
