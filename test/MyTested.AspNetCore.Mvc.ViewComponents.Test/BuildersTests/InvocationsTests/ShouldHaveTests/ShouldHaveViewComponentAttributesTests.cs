namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldHaveTests
{
    using Exceptions;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;

    public class ShouldHaveViewComponentAttributesTests
    {
        [Fact]
        public void NoAttributesShouldNotThrowExceptionWithActionContainingNoAttributes()
        {
            MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .NoAttributes();
        }
        
        [Fact]
        public void NoAttributesShouldThrowExceptionWithActionContainingAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .NoAttributes();
                },
                "When testing AttributesComponent was expected to not have any attributes, but it had some.");
        }
        
        [Fact]
        public void AttributesShouldNotThrowEceptionWithActionContainingAttributes()
        {
            MyViewComponent<AttributesComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .Attributes();
        }

        [Fact]
        public void AttributesShouldThrowEceptionWithActionContainingNoAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Attributes();
                },
                "When testing NormalComponent was expected to have at least 1 attribute, but in fact none was found.");
        }
        
        [Fact]
        public void AttributesShouldNotThrowEceptionWithActionContainingNumberOfAttributes()
        {
            MyViewComponent<AttributesComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .Attributes(withTotalNumberOf: 2);
        }

        [Fact]
        public void AttributesShouldThrowEceptionWithActionContainingNumberOfAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Attributes(withTotalNumberOf: 10);
                },
                "When testing AttributesComponent was expected to have 10 attributes, but in fact found 2.");
        }

        [Fact]
        public void AttributesShouldThrowEceptionWithActionContainingNumberOfAttributesTestingWithOne()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Attributes(withTotalNumberOf: 1);
                },
                "When testing AttributesComponent was expected to have 1 attribute, but in fact found 2.");
        }
    }
}
