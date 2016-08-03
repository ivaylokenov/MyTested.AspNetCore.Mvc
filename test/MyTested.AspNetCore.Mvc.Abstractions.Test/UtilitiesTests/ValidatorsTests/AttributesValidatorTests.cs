namespace MyTested.AspNetCore.Mvc.Test.UtilitiesTests.ValidatorsTests
{
    using System;
    using Setups;
    using Setups.Controllers;
    using Utilities;
    using Utilities.Validators;
    using Xunit;

    public class AttributesValidatorTests
    {
        [Fact]
        public void ValidateNoAttributesShouldNotFailWithNoAttributes()
        {
            var attributes = Reflection.GetCustomAttributes(new AttributesValidatorTests());

            AttributesValidator.ValidateNoAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres());
        }

        [Fact]
        public void ValidateNoAttributesShouldFailWithAttributes()
        {
            var attributes = Reflection.GetCustomAttributes(new MvcController());

            Test.AssertException<NullReferenceException>(
                () =>
                {
                    AttributesValidator.ValidateNoAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres());
                },
                "not have any attributes it had some");
        }

        [Fact]
        public void ValidateAnyNumberOfAttributesShouldNotFailWithCorrectAttributes()
        {
            var attributes = Reflection.GetCustomAttributes(new MvcController());

            AttributesValidator.ValidateNumberOfAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres());
        }

        [Fact]
        public void ValidateAnyNumberOfAttributesShouldNotFailWithExpectedNumberOfAttributes()
        {
            var attributes = Reflection.GetCustomAttributes(new MvcController());

            AttributesValidator.ValidateNumberOfAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres(), 2);
        }

        [Fact]
        public void ValidateAnyNumberOfAttributesShouldFailWithNoAttributes()
        {
            Test.AssertException<NullReferenceException>(
                () =>
                {
                    var attributes = Reflection.GetCustomAttributes(new AttributesValidatorTests());

                    AttributesValidator.ValidateNumberOfAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres());
                },
                "have at least 1 attribute in fact none was found");
        }

        [Fact]
        public void ValidateAnyNumberOfAttributesShouldFailWithIncorrectExpectedNumberOfAttributes()
        {
            var attributes = Reflection.GetCustomAttributes(new MvcController());

            Test.AssertException<NullReferenceException>(
                () =>
                {
                    AttributesValidator.ValidateNumberOfAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres(), 3);
                },
                "have 3 attributes in fact found 2");
        }
    }
}
