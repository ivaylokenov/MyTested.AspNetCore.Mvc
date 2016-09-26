namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AttributesTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Common;
    using Setups.ViewComponents;
    using System;
    using Xunit;

    public class ViewComponentAttributesTestBuilderTests
    {
        [Fact]
        public void ContainingAttributeOfTypeShouldNotThrowExceptionWithViewComponentWithTheAttribute()
        {
            MyViewComponent<AttributesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .Attributes(attributes => attributes.ContainingAttributeOfType<CustomAttribute>());
        }

        public void ContainingAttributeOfTypeShouldThrowExceptionWithViewComponentWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Attributes(attributes => attributes.ContainingAttributeOfType<HttpPatchAttribute>());
                },
                "When testing AttributesComponent expected to have HttpPatchAttribute, but in fact such was not found.");
        }

        [Fact]
        public void ChangingViewComponentNameToShouldNotThrowExceptionWithViewComponentWithTheAttribute()
        {
            MyViewComponent<AttributesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingViewComponentNameTo("Test"));
        }

        [Fact]
        public void ChangingViewComponentNameToShouldThrowExceptionWithViewComponentWithTheAttributeAndWrongName()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Attributes(attributes => attributes.ChangingViewComponentNameTo("Another"));
                },
                "When testing AttributesComponent was expected to have ViewComponentAttribute with 'Another' name, but in fact found 'Test'.");
        }

        [Fact]
        public void ChangingViewComponentNameToShouldThrowExceptionWithViewComponentWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Attributes(attributes => attributes.ChangingViewComponentNameTo("Normal"));
                },
                "When testing NormalComponent was expected to have at least 1 attribute, but in fact none was found.");
        }

        [Fact]
        public void IndicatingControllerShouldNotThrowExceptionWithTheAttribute()
        {
            MyViewComponent<AttributesComponent>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes
                    .IndicatingViewComponentExplicitly());
        }

        [Fact]
        public void DisablingActionCallShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<ComponentWithCustomAttribute>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .IndicatingViewComponentExplicitly());
                },
                "When testing ComponentWithCustomAttribute was expected to have ViewComponentAttribute, but in fact such was not found.");
        }

        [Fact]
        public void PassingForShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyViewComponent<AttributesComponent>
                .Instance()
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
                        .Instance()
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
                        .Instance()
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
                .Instance()
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
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<ViewComponentAttribute>(route => route.Name == "Invalid"));
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
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .PassingFor<ActionNameAttribute>(authorize => authorize.Name == "Admin"));
                },
                "When testing AttributesComponent was expected to have ActionNameAttribute, but in fact such was not found.");
        }
    }
}
