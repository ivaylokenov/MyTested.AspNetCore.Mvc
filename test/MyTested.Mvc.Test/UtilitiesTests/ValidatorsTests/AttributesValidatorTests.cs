namespace MyTested.Mvc.Test.UtilitiesTests.ValidatorsTests
{
    using System;
    using Builders;
    using Builders.Attributes;
    using Setups;
    using Setups.Controllers;
    using Utilities;
    using Utilities.Validators;
    using Xunit;
    
    public class AttributesValidatorTests
    {
        // TODO: ?
        //[Fact]
        //public void ValidateNoAttributesShouldNotFailWithNoAttributes()
        //{
        //    var attributes = Reflection.GetCustomAttributes(new UserBuilder());

        //    AttributesValidator.ValidateNoAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres());
        //}

        [Fact]
        public void ValidateNoAttributesShouldFailWithAttributes()
        {
            var attributes = Reflection.GetCustomAttributes(new MvcController());

            var exception = Assert.Throws<NullReferenceException>(() => AttributesValidator.ValidateNoAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres()));
            Assert.Equal("not have any attributes it had some", exception.Message);
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

        // TODO: ?
        //[Fact]
        //[ExpectedException(
        //    typeof(NullReferenceException),
        //    ExpectedMessage = "have at least 1 attribute in fact none was found")]
        //public void ValidateAnyNumberOfAttributesShouldFailWithNoAttributes()
        //{
        //    var attributes = Reflection.GetCustomAttributes(new UserBuilder());

        //    AttributesValidator.ValidateNumberOfAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres());
        //}

        [Fact]
        public void ValidateAnyNumberOfAttributesShouldFailWithIncorrectExpectedNumberOfAttributes()
        {
            var attributes = Reflection.GetCustomAttributes(new MvcController());

            var exception = Assert.Throws<NullReferenceException>(() => AttributesValidator.ValidateNumberOfAttributes(attributes, TestObjectFactory.GetFailingValidationActionWithTwoParameteres(), 3));
            Assert.Equal("have 3 attributes in fact found 2", exception.Message);
        }

        // TODO: fix?
        //[Fact]
        //public void ValidateAttributesShouldWorkCorrectly()
        //{
        //    var attributes = Reflection.GetCustomAttributes(new MvcController());

        //    AttributesValidator.ValidateAttributes(
        //        attributes,
        //        new ActionAttributesTestBuilder(new MvcController(), "Test"),
        //        TestObjectFactory.GetFailingValidationActionWithTwoParameteres());
        //}
    }
}
