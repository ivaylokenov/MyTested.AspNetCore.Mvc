namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Base;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Validators;

    /// <summary>
    /// Base class for all data provider test builder.
    /// </summary>
    public abstract class BaseDataProviderTestBuilder : BaseTestBuilderWithComponent
    {
        private IDictionary<string, object> dataProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDataProviderTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="dataProviderName">Name of the data provider.</param>
        protected BaseDataProviderTestBuilder(ComponentTestContext testContext, string dataProviderName)
            : base(testContext)
        {
            CommonValidator.CheckForNotWhiteSpaceString(dataProviderName);
            this.DataProviderName = dataProviderName;
        }

        /// <summary>
        /// Gets or sets the data provider name.
        /// </summary>
        /// <value>Name of the data provider as string.</value>
        protected string DataProviderName { get; }

        /// <summary>
        /// Gets the data provider as <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <value>Data provider as <see cref="IDictionary{TKey,TValue}"/>.</value>
        protected IDictionary<string, object> DataProvider
        {
            get
            {
                if (this.dataProvider == null)
                {
                    this.dataProvider = this.GetDataProvider();
                    CommonValidator.CheckForNullReference(this.dataProvider, nameof(this.DataProvider));
                }

                return this.dataProvider;
            }
        }
        
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
            => DictionaryValidator.ValidateStringKey(
                this.DataProviderName,
                this.DataProvider,
                key,
                this.ThrowNewDataProviderAssertionException);

        /// <summary>
        /// Validates whether the data provider contains entry with the given value.
        /// </summary>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="value">Value to validate.</param>
        protected void ValidateContainingEntryWithValue<TValue>(TValue value) 
            => DictionaryValidator.ValidateValue(
                this.DataProviderName,
                this.DataProvider,
                value,
                this.ThrowNewDataProviderAssertionException);

        /// <summary>
        /// Validates whether the data provider contains entry value with the given type.
        /// </summary>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        protected void ValidateContainingEntryOfType<TValue>() 
            => DictionaryValidator.ValidateValueOfType<TValue>(
                this.DataProviderName,
                this.DataProvider,
                this.ThrowNewDataProviderAssertionException);

        /// <summary>
        /// Validates whether the data provider contains entry value with the given type and corresponding key.
        /// </summary>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="key">Key to validate.</param>
        protected void ValidateContainingEntryOfType<TValue>(string key) 
            => DictionaryValidator.ValidateStringKeyAndValueOfType<TValue>(
                this.DataProviderName,
                this.DataProvider,
                key,
                this.ThrowNewDataProviderAssertionException);

        /// <summary>
        /// Validates whether the data provider contains entry with the given key and corresponding value.
        /// </summary>
        /// <param name="key">Key to validate.</param>
        /// <param name="value">Value to validate.</param>
        protected void ValidateContainingEntry(string key, object value) 
            => DictionaryValidator.ValidateStringKeyAndValue(
                this.DataProviderName,
                this.DataProvider,
                key,
                value,
                this.ThrowNewDataProviderAssertionException);

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
            => DictionaryValidator.ValidateValues(
                this.DataProviderName,
                this.DataProvider,
                entries,
                this.ThrowNewDataProviderAssertionException);

        /// <summary>
        /// Throws new <see cref="DataProviderAssertionException"/> with the provided details.
        /// </summary>
        /// <param name="propertyName">Name of the property that failed.</param>
        /// <param name="expectedValue">Expected value.</param>
        /// <param name="actualValue">Actual value.</param>
        protected void ThrowNewDataProviderAssertionException(string propertyName, string expectedValue, string actualValue) 
            => throw new DataProviderAssertionException(string.Format(
                "{0} {1} {2}, but {3}.",
                this.TestContext.ExceptionMessagePrefix,
                propertyName,
                expectedValue,
                actualValue));
    }
}
