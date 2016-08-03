namespace MyTested.AspNetCore.Mvc.Test.UtilitiesTests.ValidatorsTests
{
    using System;
    using System.Collections.Generic;
    using Setups;
    using Utilities.Validators;
    using Xunit;

    public class CommonValidatorTests
    {
        [Fact]
        public void CheckForNullReferenceShouldThrowArgumentNullExceptionWithNullObject()
        {
            Assert.Throws<NullReferenceException>(() => CommonValidator.CheckForNullReference(null));
        }

        [Fact]
        public void CheckForNullReferenceShouldNotThrowExceptionWithNotNullObject()
        {
            CommonValidator.CheckForNullReference(new object());
        }

        [Fact]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithNullString()
        {
            Assert.Throws<NullReferenceException>(() => CommonValidator.CheckForNotWhiteSpaceString(null));
        }

        [Fact]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithEmptyString()
        {
            Assert.Throws<NullReferenceException>(() => CommonValidator.CheckForNotWhiteSpaceString(string.Empty));
        }

        [Fact]
        public void CheckForNotEmptyStringShouldThrowArgumentNullExceptionWithWhiteSpace()
        {
            Assert.Throws<NullReferenceException>(() => CommonValidator.CheckForNotWhiteSpaceString("      "));
        }

        [Fact]
        public void CheckForNotEmptyStringShouldNotThrowExceptionWithNormalString()
        {
            CommonValidator.CheckForNotWhiteSpaceString(new string('a', 10));
        }

        [Fact]
        public void CheckForDefaultValueShouldReturnTrueIfValueIsDefaultForClass()
        {
            object obj = TestObjectFactory.GetNullRequestModel();
            var result = CommonValidator.CheckForDefaultValue(obj);

            Assert.True(result);
        }

        [Fact]
        public void CheckForDefaultValueShouldReturnTrueIfValueIsDefaultForStruct()
        {
            var result = CommonValidator.CheckForDefaultValue(0);

            Assert.True(result);
        }

        [Fact]
        public void CheckForDefaultValueShouldReturnTrueIfValueIsDefaultForNullableType()
        {
            var result = CommonValidator.CheckForDefaultValue<int?>(null);

            Assert.True(result);
        }

        [Fact]
        public void CheckForDefaultValueShouldReturnFalseIfValueIsNotDefaultForClass()
        {
            object obj = TestObjectFactory.GetValidRequestModel();
            var result = CommonValidator.CheckForDefaultValue(obj);

            Assert.False(result);
        }

        [Fact]
        public void CheckForDefaultValueShouldReturnFalseIfValueIsNotDefaultForStruct()
        {
            var result = CommonValidator.CheckForDefaultValue(1);

            Assert.False(result);
        }

        [Fact]
        public void CheckIfTypeCanBeNullShouldNotThrowExceptionWithClass()
        {
            CommonValidator.CheckIfTypeCanBeNull(typeof(object));
        }

        [Fact]
        public void CheckIfTypeCanBeNullShouldNotThrowExceptionWithNullableType()
        {
            CommonValidator.CheckIfTypeCanBeNull(typeof(int?));
        }

        [Fact]
        public void CheckIfTypeCanBeNullShouldThrowExceptionWithStruct()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    CommonValidator.CheckIfTypeCanBeNull(typeof(int));
                },
                "Int32 cannot be null.");
        }
    }
}
