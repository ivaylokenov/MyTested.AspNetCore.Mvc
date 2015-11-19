namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldHave
{
    using Exceptions;
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldHaveActionAttributesTests
    {
        [Fact]
        public void NoActionAttributesShouldNotThrowExceptionWithActionContainingNoAttributes()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultAction())
                .ShouldHave()
                .NoActionAttributes();
        }

        [Fact]
        public void NoActionAttributesShouldNotThrowExceptionWithVoidActionContainingNoAttributes()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.EmptyAction())
                .ShouldHave()
                .NoActionAttributes();
        }

        [Fact]
        public void NoActionAttributesShouldThrowExceptionWithActionContainingAttributes()
        {
            var exception = Assert.Throws<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.NormalActionWithAttributes())
                    .ShouldHave()
                    .NoActionAttributes();
            });

            Assert.Equal("When calling NormalActionWithAttributes action in MvcController expected action to not have any attributes, but it had some.", exception.Message);
        }

        [Fact]
        public void NoActionAttributesShouldThrowExceptionWithVoidActionContainingAttributes()
        {
            var exception = Assert.Throws<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.EmptyActionWithAttributes())
                    .ShouldHave()
                    .NoActionAttributes();
            });

            Assert.Equal("When calling EmptyActionWithAttributes action in MvcController expected action to not have any attributes, but it had some.", exception.Message);
        }

        [Fact]
        public void ActionAttributesShouldNotThrowEceptionWithActionContainingAttributes()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes();
        }

        [Fact]
        public void ActionAttributesShouldThrowEceptionWithActionContainingNoAttributes()
        {
            var exception = Assert.Throws<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.OkResultAction())
                    .ShouldHave()
                    .ActionAttributes();
            });

            Assert.Equal("When calling OkResultAction action in MvcController expected action to have at least 1 attribute, but in fact none was found.", exception.Message);
        }

        [Fact]
        public void ActionAttributesShouldNotThrowEceptionWithVoidActionContainingAttributes()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.EmptyActionWithAttributes())
                .ShouldHave()
                .ActionAttributes();
        }

        [Fact]
        public void ActionAttributesShouldThrowEceptionWithVoidActionContainingNoAttributes()
        {
            var exception = Assert.Throws<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.EmptyAction())
                    .ShouldHave()
                    .ActionAttributes();
            });

            Assert.Equal("When calling EmptyAction action in MvcController expected action to have at least 1 attribute, but in fact none was found.", exception.Message);
        }

        [Fact]
        public void ActionAttributesShouldNotThrowEceptionWithActionContainingNumberOfAttributes()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(withTotalNumberOf: 3);
        }

        [Fact]
        public void ActionAttributesShouldThrowEceptionWithActionContainingNumberOfAttributes()
        {
            var exception = Assert.Throws<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.NormalActionWithAttributes())
                    .ShouldHave()
                    .ActionAttributes(withTotalNumberOf: 10);
            });

            Assert.Equal("When calling NormalActionWithAttributes action in MvcController expected action to have 10 attributes, but in fact found 3.", exception.Message);
        }

        [Fact]
        public void ActionAttributesShouldThrowEceptionWithActionContainingNumberOfAttributesTestingWithOne()
        {
            var exception = Assert.Throws<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.NormalActionWithAttributes())
                    .ShouldHave()
                    .ActionAttributes(withTotalNumberOf: 1);
            });

            Assert.Equal("When calling NormalActionWithAttributes action in MvcController expected action to have 1 attribute, but in fact found 3.", exception.Message);
        }
    }
}
