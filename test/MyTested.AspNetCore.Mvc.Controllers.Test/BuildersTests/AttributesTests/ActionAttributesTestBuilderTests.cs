namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AttributesTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    using System;
    using Microsoft.AspNetCore.Authorization;

    public class ActionAttributesTestBuilderTests
    {
        [Fact]
        public void ContainingAttributeOfTypeShouldNotThrowExceptionWithActionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.ContainingAttributeOfType<HttpGetAttribute>());
        }

        [Fact]
        public void ContainingAttributeOfTypeShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.ContainingAttributeOfType<HttpPatchAttribute>());
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have HttpPatchAttribute, but in fact such was not found.");
        }

        [Fact]
        public void PassingForShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .PassingFor<RouteAttribute>(route => Assert.Equal("/api/test", route.Template)));
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectAssertions()
        {
            Assert.ThrowsAny<Exception>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .PassingFor<RouteAttribute>(route => Assert.Equal("/invalid", route.Template)));
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
                        .Calling(c => c.OtherAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .PassingFor<ActionNameAttribute>(authorize => Assert.Equal("Admin", authorize.Name)));
                },
                "When calling OtherAttributes action in MvcController expected action to have ActionNameAttribute, but in fact such was not found.");
        }

        [Fact]
        public void PassingForShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .PassingFor<RouteAttribute>(route => route.Template == "/api/test"));
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectPredicate()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.VariousAttributesAction())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .PassingFor<RouteAttribute>(route => route.Template == "/invalid"));
                },
                "When calling VariousAttributesAction action in MvcController expected action to have RouteAttribute passing the given predicate, but it failed.");
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectAttributeWithPredicate()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OtherAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes
                            .PassingFor<ActionNameAttribute>(authorize => authorize.Name == "Admin"));
                },
                "When calling OtherAttributes action in MvcController expected action to have ActionNameAttribute, but in fact such was not found.");
        }

        [Fact]
        public void IncludingInheritedShouldIncludeAllCustomInheritedAttributesFromBaseMethodsAndNotThrowException()
        {
            MyController<InheritControllerAttributes>
                .Instance()
                .Calling(c=> c.MethodA())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.IncludingInherited()
                    .ContainingAttributeOfType<HttpPostAttribute>()
                    .ContainingAttributeOfType<AuthorizeAttribute>());
        }
    }
}
