namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Base;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all data provider test builder.
    /// </summary>
    public abstract class BaseDataProviderTestBuilder : BaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDataProviderTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="dataProviderName">Name of the data provider.</param>
        protected BaseDataProviderTestBuilder(ControllerTestContext testContext, string dataProviderName)
            : base(testContext)
        {
            ActionValidator.CheckForNotWhiteSpaceString(dataProviderName);
            this.DataProviderName = dataProviderName;
            this.DataProvider = this.GetDataProvider();
            ActionValidator.CheckForNullReference(this.DataProvider);
        }

        /// <summary>
        /// Gets or sets the data provider name.
        /// </summary>
        /// <value>Name of the data provider as string.</value>
        protected string DataProviderName { get; set; }

        /// <summary>
        /// Gets the data provider as <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <value>Data provider as <see cref="IDictionary{TKey,TValue}"/>.</value>
        protected IDictionary<string, object> DataProvider { get; private set; }

        /// <summary>
        /// When overridden in derived class provides a way to built the data provider as <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <returns>Data provider as <see cref="IDictionary{TKey,TValue}"/></returns>
        protected abstract IDictionary<string, object> GetDataProvider();

        /// <summary>
        /// Validates whether the data provider contains entry with the given key.
        /// </summary>
        /// <param name="key">Key to validate.</param>
        protected void ValidateContainingEntryWithKey(string key)
        {
            DictionaryValidator.ValidateStringKey(
                this.DataProviderName,
                this.DataProvider,
                key,
                this.ThrowNewDataProviderAssertionException);
        }

        /// <summary>
        /// Validates whether the data provider contains entry with the given value.
        /// </summary>
        /// <typeparam name="TEntry">Type of the value.</typeparam>
        /// <param name="value">Value to validate.</param>
        protected void ValidateContainingEntryWithValue<TEntry>(TEntry value)
        {
            DictionaryValidator.ValidateValue(
                this.DataProviderName,
                this.DataProvider,
                value,
                this.ThrowNewDataProviderAssertionException);
        }

        /// <summary>
        /// Validates whether the data provider contains entry value with the given type.
        /// </summary>
        /// <typeparam name="TEntry">Type of the value.</typeparam>
        protected void ValidateContainingEntryOfType<TEntry>()
        {
            DictionaryValidator.ValidateValueOfType<TEntry>(
                this.DataProviderName,
                this.DataProvider,
                this.ThrowNewDataProviderAssertionException);
        }

        /// <summary>
        /// Validates whether the data provider contains entry value with the given type and corresponding key.
        /// </summary>
        /// <typeparam name="TEntry">Type of the value.</typeparam>
        /// <param name="key">Key to validate.</param>
        protected void ValidateContainingEntryOfType<TEntry>(string key)
        {
            DictionaryValidator.ValidateStringKeyAndValueOfType<TEntry>(
                this.DataProviderName,
                this.DataProvider,
                key,
                this.ThrowNewDataProviderAssertionException);
        }

        /// <summary>
        /// Validates whether the data provider contains entry with the given key and corresponding value.
        /// </summary>
        /// <param name="key">Key to validate.</param>
        /// <param name="value">Value to validate.</param>
        protected void ValidateContainingEntry(string key, object value)
        {
            DictionaryValidator.ValidateStringKeyAndValue(
                this.DataProviderName,
                this.DataProvider,
                key,
                value,
                this.ThrowNewDataProviderAssertionException);
        }

        /// <summary>
        /// Validates whether the data provider contains entry with the given entries.
        /// </summary>
        /// <param name="entries">Anonymous object of entries.</param>
        protected void ValidateContainingEntries(object entries)
            => this.ValidateContainingEntries(new RouteValueDictionary(entries));

        /// <summary>
        /// Validates whether the data provider contains entry with the given entries.
        /// </summary>
        /// <param name="entries">Dictionary of entries.</param>
        protected void ValidateContainingEntries(IDictionary<string, object> entries)
        {
            DictionaryValidator.ValidateValues(
                this.DataProviderName,
                this.DataProvider,
                entries,
                this.ThrowNewDataProviderAssertionException);
        }

        /// <summary>
        /// Throws new <see cref="DataProviderAssertionException"/> with the provided details.
        /// </summary>
        /// <param name="propertyName">Name of the property that failed.</param>
        /// <param name="expectedValue">Expected value.</param>
        /// <param name="actualValue">Actual value.</param>
        protected void ThrowNewDataProviderAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new DataProviderAssertionException(string.Format(
                    "When calling {0} action in {1} expected {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
