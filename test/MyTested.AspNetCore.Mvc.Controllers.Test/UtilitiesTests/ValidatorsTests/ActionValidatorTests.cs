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
            InvocationValidator.CheckForException(null, string.Empty);
        }

        [Fact]
        public void CheckForExceptionShouldThrowIfExceptionNotNullWithEmptyMessage()
        {
            Test.AssertException<InvocationAssertionException>(
                () =>
                {
                    InvocationValidator.CheckForException(new NullReferenceException(string.Empty), "Test");
                },
                "Test no exception but NullReferenceException was thrown without being caught.");
        }

        [Fact]
        public void CheckForExceptionShouldThrowIfExceptionNotNullWithMessage()
        {
            Test.AssertException<InvocationAssertionException>(
                () =>
                {
                    InvocationValidator.CheckForException(new NullReferenceException("Test"), "Test");
                },
                "Test no exception but NullReferenceException with 'Test' message was thrown without being caught.");
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

            Test.AssertException<InvocationAssertionException>(
                () =>
                {
                    InvocationValidator.CheckForException(aggregateException, "Test");
                },
                "Test no exception but AggregateException (containing NullReferenceException with 'Null test' message, InvalidCastException with 'Cast test' message, InvalidOperationException with 'Operation test' message) was thrown without being caught.");
        }
    }
}
