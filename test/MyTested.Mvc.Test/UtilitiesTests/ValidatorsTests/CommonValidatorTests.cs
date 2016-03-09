namespace MyTested.Mvc.Test.UtilitiesTests.ValidatorsTests
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
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
        public void CheckForExceptionShouldNotThrowIfExceptionNull()
        {
            CommonValidator.CheckForException(null);
        }

        [Fact]
        public void CheckForExceptionShouldThrowIfExceptionNotNullWithEmptyMessage()
        {
            Test.AssertException<ActionCallAssertionException>(
                () =>
                {
                    CommonValidator.CheckForException(new NullReferenceException(string.Empty));
                }, 
                "NullReferenceException was thrown but was not caught or expected.");
        }

        [Fact]
        public void CheckForExceptionShouldThrowIfExceptionNotNullWithMessage()
        {
            Test.AssertException<ActionCallAssertionException>(
                () =>
                {
                    CommonValidator.CheckForException(new NullReferenceException("Test"));
                }, 
                "NullReferenceException with 'Test' message was thrown but was not caught or expected.");
        }

        [Fact]
        public void CheckForExceptionShouldThrowWithProperMessageIfExceptionIsAggregateException()
        {
            var aggregateException = new AggregateException(new List<Exception>
                    {
                        new NullReferenceException("Null test"),
                        new InvalidCastException("Cast test"),
                        new InvalidOperationException("Operation test")
                    });

            Test.AssertException<ActionCallAssertionException>(
                () =>
                {
                    CommonValidator.CheckForException(aggregateException);
                }, 
                "AggregateException (containing NullReferenceException with 'Null test' message, InvalidCastException with 'Cast test' message, InvalidOperationException with 'Operation test' message) was thrown but was not caught or expected.");
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
            Test.AssertException<ActionCallAssertionException>(
                () =>
                {
                    CommonValidator.CheckIfTypeCanBeNull(typeof(int));
                },
                "Int32 cannot be null.");
        }
    }
}
