namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AttributesTests
{
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
    }
}
