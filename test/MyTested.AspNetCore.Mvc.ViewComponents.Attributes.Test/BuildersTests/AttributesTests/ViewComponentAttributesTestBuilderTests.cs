namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AttributesTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewComponentAttributesTestBuilderTests
    {
        [Fact]
        public void ChangingViewComponentNameToShouldNotThrowExceptionWithViewComponentWithTheAttribute()
        {
            MyViewComponent<AttributesComponent>
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
                        .ShouldHave()
                        .Attributes(attributes => attributes
                            .IndicatingViewComponentExplicitly());
                },
                "When testing ComponentWithCustomAttribute was expected to have ViewComponentAttribute, but in fact such was not found.");
        }

        [Fact]
        public void IncludingInheritedShouldIncludeAllCustomInheritedAttributesFromBaseViewComponentAndNotThrowException()
        {
            MyViewComponent<InheritViewComponent>
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
                    MyViewComponent<InheritViewComponent>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes//.IncludingInherited()
                            .ContainingAttributeOfType<ValidateAntiForgeryTokenAttribute>()
                            .ContainingAttributeOfType<ResponseCacheAttribute>());
                }, $"When testing {nameof(InheritViewComponent)} was expected to have ValidateAntiForgeryTokenAttribute, but in fact such was not found.");
        }
    }
}
