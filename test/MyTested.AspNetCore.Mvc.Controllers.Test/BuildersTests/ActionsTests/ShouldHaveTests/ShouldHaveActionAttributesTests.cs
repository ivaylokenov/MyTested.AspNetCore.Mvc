namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldHaveActionAttributesTests
    {
        [Fact]
        public void NoActionAttributesShouldNotThrowExceptionWithActionContainingNoAttributes()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultAction())
                .ShouldHave()
                .NoActionAttributes();
        }

        [Fact]
        public void NoActionAttributesShouldNotThrowExceptionWithVoidActionContainingNoAttributes()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.EmptyAction())
                .ShouldHave()
                .NoActionAttributes();
        }

        [Fact]
        public void NoActionAttributesShouldThrowExceptionWithActionContainingAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .NoActionAttributes();
                },
                "When calling NormalActionWithAttributes action in MvcController expected action to not have any attributes, but it had some.");
        }

        [Fact]
        public void NoActionAttributesShouldThrowExceptionWithVoidActionContainingAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.EmptyActionWithAttributes())
                        .ShouldHave()
                        .NoActionAttributes();
                }, 
                "When calling EmptyActionWithAttributes action in MvcController expected action to not have any attributes, but it had some.");
        }

        [Fact]
        public void ActionAttributesShouldNotThrowExceptionWithActionContainingAttributes()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes();
        }

        [Fact]
        public void ActionAttributesShouldThrowExceptionWithActionContainingNoAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultAction())
                        .ShouldHave()
                        .ActionAttributes();
                }, 
                "When calling OkResultAction action in MvcController expected action to have at least 1 attribute, but in fact none was found.");
        }

        [Fact]
        public void ActionAttributesShouldNotThrowExceptionWithVoidActionContainingAttributes()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.EmptyActionWithAttributes())
                .ShouldHave()
                .ActionAttributes();
        }

        [Fact]
        public void ActionAttributesShouldThrowExceptionWithVoidActionContainingNoAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.EmptyAction())
                        .ShouldHave()
                        .ActionAttributes();
                }, 
                "When calling EmptyAction action in MvcController expected action to have at least 1 attribute, but in fact none was found.");
        }

        [Fact]
        public void ActionAttributesShouldNotThrowExceptionWithActionContainingNumberOfAttributes()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(withTotalNumberOf: 3);
        }

        [Fact]
        public void ActionAttributesShouldThrowExceptionWithActionContainingNumberOfAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(withTotalNumberOf: 10);
                }, 
                "When calling NormalActionWithAttributes action in MvcController expected action to have 10 attributes, but in fact found 3.");
        }

        [Fact]
        public void ActionAttributesShouldThrowExceptionWithActionContainingNumberOfAttributesTestingWithOne()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldHave()
                        .ActionAttributes(withTotalNumberOf: 1);
                }, 
                "When calling NormalActionWithAttributes action in MvcController expected action to have 1 attribute, but in fact found 3.");
        }
    }
}
