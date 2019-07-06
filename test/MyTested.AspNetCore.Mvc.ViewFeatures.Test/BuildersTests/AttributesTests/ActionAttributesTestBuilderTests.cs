namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AttributesTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ActionAttributesTestBuilderTests
    {
        [Fact]
        public void ValidatingAntiForgeryTokenShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AntiForgeryToken())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.ValidatingAntiForgeryToken());
        }

        [Fact]
        public void ValidatingAntiForgeryTokenShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.ValidatingAntiForgeryToken());
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have ValidateAntiForgeryTokenAttribute, but in fact such was not found.");
        }

        [Fact]
        public void IgnoringAntiForgeryTokenShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IgnoreAntiForgeryToken())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.IgnoringAntiForgeryToken());
        }

        [Fact]
        public void IgnoringAntiForgeryTokenShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.IgnoringAntiForgeryToken());
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have IgnoreAntiforgeryTokenAttribute, but in fact such was not found.");
        }
        
        [Fact]
        public void SkippingStatusCodePagesShouldNotThrowExceptionWithTheAttribute()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.SkippingStatusCodePages());
        }

        [Fact]
        public void SkippingStatusCodePagesShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(attributes => attributes.SkippingStatusCodePages());
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to have SkipStatusCodePagesAttribute, but in fact such was not found.");
        }
    }
}
