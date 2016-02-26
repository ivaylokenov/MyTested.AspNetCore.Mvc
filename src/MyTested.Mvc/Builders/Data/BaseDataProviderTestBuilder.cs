namespace MyTested.Mvc.Builders.Data
{
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Routing;
    using MyTested.Mvc.Builders.Base;
    using System.Collections.Generic;
    using Utilities.Extensions;
    using Utilities.Validators;

    public abstract class BaseDataProviderTestBuilder : BaseTestBuilderWithInvokedAction
    {
        protected BaseDataProviderTestBuilder(ControllerTestContext testContext, string dataProviderName)
            : base(testContext)
        {
            CommonValidator.CheckForNotWhiteSpaceString(dataProviderName);
            this.DataProviderName = dataProviderName;
            this.DataProvider = this.GetDataProvider();
            CommonValidator.CheckForNullReference(this.DataProvider);
        }

        protected string DataProviderName { get; set; }

        protected IDictionary<string, object> DataProvider { get; private set; }

        protected abstract IDictionary<string, object> GetDataProvider();

        protected void ValidateContainingEntry(string key)
        {
            DictionaryValidator.ValidateStringKey(
                this.DataProviderName,
                this.DataProvider,
                key,
                this.ThrowNewDataProviderAssertionException);
        }

        protected void ValidateContainingEntry<TEntry>(TEntry value)
        {
            DictionaryValidator.ValidateValue(
                this.DataProviderName,
                this.DataProvider,
                value,
                this.ThrowNewDataProviderAssertionException);
        }

        protected void ValidateContainingEntryOfType<TEntry>()
        {
            DictionaryValidator.ValidateValueOfType<TEntry>(
                this.DataProviderName,
                this.DataProvider,
                this.ThrowNewDataProviderAssertionException);
        }

        protected void ValidateContainingEntryOfType<TEntry>(string key)
        {
            DictionaryValidator.ValidateStringKeyAndValueOfType<TEntry>(
                this.DataProviderName,
                this.DataProvider,
                key,
                this.ThrowNewDataProviderAssertionException);
        }

        protected void ValidateContainingEntry(string key, object value)
        {
            DictionaryValidator.ValidateStringKeyAndValue(
                this.DataProviderName,
                this.DataProvider,
                key,
                value,
                this.ThrowNewDataProviderAssertionException);
        }

        protected void ValidateContainingEntries(object entries)
            => this.ValidateContainingEntries(new RouteValueDictionary(entries));

        protected void ValidateContainingEntries(IDictionary<string, object> entries)
        {
            DictionaryValidator.ValidateValues(
                this.DataProviderName,
                this.DataProvider,
                entries,
                this.ThrowNewDataProviderAssertionException);
        }

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
