namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
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
            MyController<NoAttributesController>
                .Instance()
                .ShouldHave()
                .NoAttributes();
        }

        [Fact]
        public void NoAttributesShouldThrowExceptionWithControllerContainingAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .NoAttributes();
                }, 
                "When testing MvcController was expected to not have any attributes, but it had some.");
        }

        [Fact]
        public void AttributesShouldNotThrowExceptionWithControllerContainingAttributes()
        {
            MyController<MvcController>
                .Instance()
                .ShouldHave()
                .Attributes();
        }

        [Fact]
        public void AttributesShouldThrowExceptionWithControllerContainingNoAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<NoAttributesController>
                        .Instance()
                        .ShouldHave()
                        .Attributes();
                }, 
                "When testing NoAttributesController was expected to have at least 1 attribute, but in fact none was found.");
        }

        [Fact]
        public void AttributesShouldNotThrowExceptionWithControllerContainingNumberOfAttributes()
        {
            MyController<MvcController>
                .Instance()
                .ShouldHave()
                .Attributes(withTotalNumberOf: 4);
        }

        [Fact]
        public void AttributesShouldThrowExceptionWithControllerContainingNumberOfAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(withTotalNumberOf: 10);
                }, 
                "When testing MvcController was expected to have 10 attributes, but in fact found 4.");
        }

        [Fact]
        public void AttributesShouldThrowExceptionWithControllerContainingNumberOfAttributesTestingWithOne()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .ShouldHave()
                        .Attributes(withTotalNumberOf: 1);
                },
                "When testing MvcController was expected to have 1 attribute, but in fact found 4.");
        }
    }
}
