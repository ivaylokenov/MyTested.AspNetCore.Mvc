namespace MyTested.AspNetCore.Mvc.Test.UtilitiesTests.ValidatorsTests
{
    using Exceptions;
    using Setups;
    using System;
    using System.Collections.Generic;
    using Utilities.Validators;
    using Xunit;

    public class ActionValidatorTests
    {
        [Fact]
        public void CheckForExceptionShouldNotThrowIfExceptionNull()
        {
            ActionValidator.CheckForException(null);
        }

        [Fact]
        public void CheckForExceptionShouldThrowIfExceptionNotNullWithEmptyMessage()
        {
            Test.AssertException<ActionCallAssertionException>(
                () =>
                {
                    ActionValidator.CheckForException(new NullReferenceException(string.Empty));
                },
                "NullReferenceException was thrown but was not caught or expected.");
        }

        [Fact]
        public void CheckForExceptionShouldThrowIfExceptionNotNullWithMessage()
        {
            Test.AssertException<ActionCallAssertionException>(
                () =>
                {
                    ActionValidator.CheckForException(new NullReferenceException("Test"));
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
                    ActionValidator.CheckForException(aggregateException);
                },
                "AggregateException (containing NullReferenceException with 'Null test' message, InvalidCastException with 'Cast test' message, InvalidOperationException with 'Operation test' message) was thrown but was not caught or expected.");
        }
    }
}
