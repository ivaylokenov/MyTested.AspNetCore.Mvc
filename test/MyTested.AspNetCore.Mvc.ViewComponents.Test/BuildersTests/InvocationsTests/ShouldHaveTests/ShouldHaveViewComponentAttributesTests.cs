namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldHaveTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using MyTested.AspNetCore.Mvc.Test.Setups.Common;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;
    using Xunit.Sdk;

    public class ShouldHaveViewComponentAttributesTests
    {
        [Fact]
        public void NoAttributesShouldNotThrowExceptionWithActionContainingNoAttributes()
        {
            MyViewComponent<NormalComponent>
                .Instance()
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
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .NoAttributes();
                },
                "When testing AttributesComponent was expected to not have any attributes, but it had some.");
        }
        
        [Fact]
        public void AttributesShouldNotThrowExceptionWithActionContainingAttributes()
        {
            MyViewComponent<AttributesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .Attributes();
        }

        [Fact]
        public void AttributesShouldNotThrowExceptionWithActionContainingAttributesOfType()
        {
            MyViewComponent<AttributesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .Attributes(attributes => 
                {
                    attributes.ContainingAttributeOfType<CustomAttribute>();
                });
        }

        [Fact]
        public void AttributesShouldThrowExceptionWithActionContainingAttributesOfTypeWithIncorrectValue()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Attributes(attributes =>
                        {
                            attributes.ContainingAttributeOfType<AreaAttribute>();
                        });
                },
                "When testing AttributesComponent was expected to have AreaAttribute, but in fact such was not found.");
        }

        [Fact]
        public void AttributesShouldThrowExceptionWithActionContainingNoAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Attributes();
                },
                "When testing NormalComponent was expected to have at least 1 attribute, but in fact none was found.");
        }

        [Fact]
        public void AttributesShouldThrowExceptionWithActionContainingZeroAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Attributes(withTotalNumberOf: 0);
                },
                "When testing NormalComponent was expected to have at least 1 attribute, but in fact none was found.");
        }

        [Fact]
        public void AttributesShouldNotThrowExceptionWithActionContainingNumberOfAttributes()
        {
            MyViewComponent<AttributesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .Attributes(withTotalNumberOf: 2);
        }

        [Fact]
        public void AttributesShouldThrowExceptionWithActionContainingNumberOfAttributes()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Attributes(withTotalNumberOf: 10);
                },
                "When testing AttributesComponent was expected to have 10 attributes, but in fact found 2.");
        }

        [Fact]
        public void AttributesShouldThrowExceptionWithActionContainingNumberOfAttributesTestingWithOne()
        {
            Test.AssertException<AttributeAssertionException>(
                () =>
                {
                    MyViewComponent<AttributesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Attributes(withTotalNumberOf: 1);
                },
                "When testing AttributesComponent was expected to have 1 attribute, but in fact found 2.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyViewComponent<PocoViewComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .NoAttributes()
                .AndAlso()
                .ShouldPassForThe<PocoViewComponent>((viewComponent) =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.Empty(viewComponent.TempData);
                });
        }

        [Fact]
        public void AndAlsoShouldThrowExceptionWithIncorrectAssertions()
        {
            Assert.Throws<TrueException>(
                () =>
                {
                    MyViewComponent<PocoViewComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .NoAttributes()
                        .AndAlso()
                        .ShouldPassForThe<PocoViewComponent>((viewComponent) =>
                        {
                            Assert.True(viewComponent.TempData == null);
                        });
                });
        }
    }
}
