namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AttributesTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ControllerAttributesTestBuilderTests
    {
        [Fact]
        public void ValidatingAntiForgeryTokenShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.ValidatingAntiForgeryToken());
        }

        [Fact]
        public void ValidatingAntiForgeryTokenShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<AttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.ValidatingAntiForgeryToken());
                },
                "When testing AttributesController was expected to have ValidateAntiForgeryTokenAttribute, but in fact such was not found.");
        }
        
        [Fact]
        public void IgnoringAntiForgeryTokenShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.IgnoringAntiForgeryToken());
        }

        [Fact]
        public void IgnoringAntiForgeryTokenShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.IgnoringAntiForgeryToken());
                },
                "When testing MvcController was expected to have IgnoreAntiforgeryTokenAttribute, but in fact such was not found.");
        }
        
        [Fact]
        public void SkippingStatusCodePagesShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<AttributesController>
                .Instance()
                .ShouldHave()
                .Attributes(attributes => attributes.SkippingStatusCodePages());
        }

        [Fact]
        public void SkippingStatusCodePagesThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(attributes => attributes.SkippingStatusCodePages());
                },
                "When testing MvcController was expected to have SkipStatusCodePagesAttribute, but in fact such was not found.");
        }
    }
}
