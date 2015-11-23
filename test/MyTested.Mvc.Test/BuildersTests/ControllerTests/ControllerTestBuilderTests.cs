namespace MyTested.Mvc.Tests.BuildersTests.ControllerTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ControllerTestBuilderTests
    {
        [Fact]
        public void NoAttributesShouldNotThrowExceptionWithControllerContainingNoAttributes()
        {
            MyMvc
                .Controller<NoAttributesController>()
                .ShouldHave()
                .NoAttributes();
        }

        [Fact]
        public void NoAttributesShouldThrowExceptionWithControllerContainingAttributes()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .ShouldHave()
                    .NoAttributes();
            }, "When testing MvcController was expected to not have any attributes, but it had some.");
        }

        [Fact]
        public void AttributesShouldNotThrowEceptionWithControllerContainingAttributes()
        {
            MyMvc
                .Controller<MvcController>()
                .ShouldHave()
                .Attributes();
        }

        [Fact]
        public void AttributesShouldThrowEceptionWithControllerContainingNoAttributes()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<NoAttributesController>()
                    .ShouldHave()
                    .Attributes();
            }, "When testing NoAttributesController was expected to have at least 1 attribute, but in fact none was found.");
        }

        [Fact]
        public void AttributesShouldNotThrowEceptionWithControllerContainingNumberOfAttributes()
        {
            MyMvc
                .Controller<MvcController>()
                .ShouldHave()
                .Attributes(withTotalNumberOf: 2);
        }

        [Fact]
        public void AttributesShouldThrowEceptionWithControllerContainingNumberOfAttributes()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .ShouldHave()
                    .Attributes(withTotalNumberOf: 10);
            }, "When testing MvcController was expected to have 10 attributes, but in fact found 2.");
        }

        [Fact]
        public void AttributesShouldThrowEceptionWithControllerContainingNumberOfAttributesTestingWithOne()
        {
            Test.AssertException<AttributeAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .ShouldHave()
                    .Attributes(withTotalNumberOf: 1);
            }, "When testing MvcController was expected to have 1 attribute, but in fact found 2.");
        }
    }
}
