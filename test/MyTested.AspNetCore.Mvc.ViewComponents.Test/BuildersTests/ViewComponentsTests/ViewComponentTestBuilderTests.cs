namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentsTests
{
    using Exceptions;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewComponentTestBuilderTests
    {
        [Fact]
        public void NoAttributesShouldNotThrowExceptionWithControllerContainingNoAttributes()
        {
            MyViewComponent<NormalComponent>
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
                    MyViewComponent<AttributesComponent>
                        .Instance()
                        .ShouldHave()
                        .NoAttributes();
                },
                "When testing AttributesComponent was expected to not have any attributes, but it had some.");
        }

        [Fact]
        public void AttributesShouldNotThrowEceptionWithControllerContainingAttributes()
        {
            MyViewComponent<AttributesComponent>
                .Instance()
                .ShouldHave()
                .Attributes();
        }

        [Fact]
        public void AttributesShouldThrowEceptionWithControllerContainingNoAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .ShouldHave()
                        .Attributes();
                },
                "When testing NormalComponent was expected to have at least 1 attribute, but in fact none was found.");
        }

        [Fact]
        public void AttributesShouldNotThrowEceptionWithControllerContainingNumberOfAttributes()
        {
            MyViewComponent<AttributesComponent>
                .Instance()
                .ShouldHave()
                .Attributes(withTotalNumberOf: 2);
        }

        [Fact]
        public void AttributesShouldThrowEceptionWithControllerContainingNumberOfAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .Instance()
                        .ShouldHave()
                        .Attributes(withTotalNumberOf: 10);
                },
                "When testing AttributesComponent was expected to have 10 attributes, but in fact found 2.");
        }

        [Fact]
        public void AttributesShouldThrowEceptionWithControllerContainingNumberOfAttributesTestingWithOne()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .Instance()
                        .ShouldHave()
                        .Attributes(withTotalNumberOf: 1);
                },
                "When testing AttributesComponent was expected to have 1 attribute, but in fact found 2.");
        }
    }
}
