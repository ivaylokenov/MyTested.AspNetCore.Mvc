namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AttributesTests
{
    using System;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Common;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewComponentAttributesTestBuilderTests
    {
        [Fact]
        public void ContainingAttributeOfTypeShouldNotThrowExceptionWithViewComponentWithTheAttribute()
        {
            MyViewComponent<AttributesComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .Attributes(attributes => attributes.ContainingAttributeOfType<CustomAttribute>());
        }

        [Fact]
        public void ContainingAttributeOfTypeShouldThrowExceptionWithViewComponentWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Attributes(attributes => attributes.ContainingAttributeOfType<HttpPatchAttribute>());
                },
                "When testing AttributesComponent was expected to have HttpPatchAttribute, but in fact such was not found.");
        }

        [Fact]
        public void PassingForShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyViewComponent<AttributesComponent>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .PassingFor<ViewComponentAttribute>(vc => Assert.Equal("Test", vc.Name)));
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectAssertions()
        {
            Assert.ThrowsAny<Exception>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<ViewComponentAttribute>(route => Assert.Equal("Invalid", route.Name)));
                });
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<ActionNameAttribute>(authorize => Assert.Equal("Admin", authorize.Name)));
                },
                "When testing AttributesComponent was expected to have ActionNameAttribute, but in fact such was not found.");
        }

        [Fact]
        public void PassingForShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyViewComponent<AttributesComponent>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .PassingFor<ViewComponentAttribute>(route => route.Name == "Test"));
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectPredicate()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<ViewComponentAttribute>(vc => vc.Name == "Invalid"));
                },
                "When testing AttributesComponent was expected to have ViewComponentAttribute passing the given predicate, but it failed.");
        }

        [Fact]
        public void PassingForShouldThrowExceptionWithIncorrectAttributeWithPredicate()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<ActionNameAttribute>(vc => vc.Name == "Admin"));
                },
                "When testing AttributesComponent was expected to have ActionNameAttribute, but in fact such was not found.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyViewComponent<AttributesComponent>
                .ShouldHave()
                .Attributes(attributes => attributes
                    .PassingFor<ViewComponentAttribute>(viewComponent => viewComponent.Name == "Test")
                    .AndAlso()
                    .ContainingAttributeOfType<ViewComponentAttribute>());
        }
    }
}
