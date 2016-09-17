namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Contracts.Data;
    using Internal.TestContexts;
    using Utilities;
    using Utilities.Validators;
    using Exceptions;

    /// <summary>
    /// Used for testing data provider entry.
    /// </summary>
    public class DataProviderEntryTestBuilder : BaseTestBuilderWithComponent,
        IDataProviderEntryKeyTestBuilder, IAndDataProviderEntryTestBuilder
    {
        private readonly ICollection<Action<object>> validations;
        private readonly string dataProviderName;

        private string entryKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataProviderEntryTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="dataProviderName">Data provider name.</param>
        public DataProviderEntryTestBuilder(
            ComponentTestContext testContext,
            string dataProviderName)
            : base(testContext)
        {
            CommonValidator.CheckForNotWhiteSpaceString(dataProviderName);
            this.dataProviderName = dataProviderName;

            this.validations = new List<Action<object>>();
        }
        
        /// <inheritdoc />
        public IAndDataProviderEntryTestBuilder WithKey(string key)
        {
            this.entryKey = key;
            return this;
        }

        /// <inheritdoc />
        public IAndDataProviderEntryTestBuilder WithValue(object value)
        {
            this.validations.Add((actual) =>
            {
                if (Reflection.AreNotDeeplyEqual(value, actual))
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have entry with '{this.entryKey}' key and the given value",
                        "the value was different");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndDataProviderEntryDetailsTestBuilder<TValue> WithValueOfType<TValue>()
        {
            this.validations.Add((actual) =>
            {
                var expectedType = typeof(TValue);
                var actualType = actual.GetType();

                if (Reflection.AreDifferentTypes(expectedType, actualType))
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have entry with '{this.entryKey}' key and value of {expectedType.ToFriendlyTypeName()} type",
                        $"in fact found {actualType.ToFriendlyTypeName()}");
                }
            });

            return new DataProviderEntryDetailsTestBuilder<TValue>(this);
        }

        /// <inheritdoc />
        public IDataProviderEntryTestBuilder AndAlso() => this;

        internal string GetDataProviderEntryKey()
        {
            if (this.entryKey == null)
            {
                throw new InvalidOperationException($"{this.dataProviderName} entry key must be provided. 'WithKey' method must be called on the entry test builder in order to run this test case successfully.");
            }

            return this.entryKey;
        }

        internal ICollection<Action<object>> GetDataProviderEntryValidations()
            => this.validations;

        internal void ThrowNewDataProviderAssertionException(string expectedValue, string actualValue)
        {
            throw new DataProviderAssertionException(string.Format(
                "{0} {1} {2}, but {3}.",
                this.TestContext.ExceptionMessagePrefix,
                this.dataProviderName,
                expectedValue,
                actualValue));
        }
    }
}
