namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using System.Collections.Generic;
    using Contracts.Data;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Extensions;

    /// <summary>
    /// Base class for all test builders with data provider containing string as key.
    /// </summary>
    /// <typeparam name="TDataProviderTestBuilder">Type of data provider.</typeparam>
    public abstract class BaseDataProviderWithStringKeyTestBuilder<TDataProviderTestBuilder> : BaseDataProviderTestBuilder
        where TDataProviderTestBuilder : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDataProviderTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="dataProviderName">Name of the data provider.</param>
        protected BaseDataProviderWithStringKeyTestBuilder(ComponentTestContext testContext, string dataProviderName)
            : base(testContext, dataProviderName)
        {
        }

        /// <summary>
        /// Gets the data provider test builder.
        /// </summary>
        /// <value>Test builder for the concrete data provider.</value>
        protected abstract TDataProviderTestBuilder DataProviderTestBuilder { get; }
        
        /// <inheritdoc />
        public TDataProviderTestBuilder ContainingEntryWithKey(string key)
        {
            this.ValidateContainingEntryWithKey(key);
            return this.DataProviderTestBuilder;
        }

        /// <inheritdoc />
        public TDataProviderTestBuilder ContainingEntryWithValue<TValue>(TValue value)
        {
            this.ValidateContainingEntryWithValue(value);
            return this.DataProviderTestBuilder;
        }

        /// <inheritdoc />
        public TDataProviderTestBuilder ContainingEntryOfType<TValue>()
        {
            this.ContainingEntryOfType(typeof(TValue));
            return this.DataProviderTestBuilder;
        }

        /// <inheritdoc />
        public TDataProviderTestBuilder ContainingEntryOfType(Type valueType)
        {
            this.ValidateContainingEntryOfType(valueType);
            return this.DataProviderTestBuilder;
        }

        /// <inheritdoc />
        public TDataProviderTestBuilder ContainingEntryOfType<TValue>(string key)
        {
            this.ContainingEntryOfType(key,typeof(TValue));
            return this.DataProviderTestBuilder;
        }

        /// <inheritdoc />
        public TDataProviderTestBuilder ContainingEntryOfType(string key, Type valueType)
        {
            this.ValidateContainingEntryOfType(key, valueType);
            return this.DataProviderTestBuilder;
        }

        /// <inheritdoc />
        public TDataProviderTestBuilder ContainingEntry(string key, object value)
        {
            this.ValidateContainingEntry(key, value);
            return this.DataProviderTestBuilder;
        }

        /// <inheritdoc />
        public TDataProviderTestBuilder ContainingEntry(Action<IDataProviderEntryKeyTestBuilder> dataProviderEntryTestBuilder)
        {
            var newDataProviderEntryBuilder = new DataProviderEntryTestBuilder(this.TestContext, this.DataProviderName);
            dataProviderEntryTestBuilder(newDataProviderEntryBuilder);

            var key = newDataProviderEntryBuilder.GetDataProviderEntryKey();
            this.ContainingEntryWithKey(key);

            var actualEntry = this.DataProvider[key];

            newDataProviderEntryBuilder
                .GetDataProviderEntryValidations()
                .ForEach(v => v(actualEntry));
            
            return this.DataProviderTestBuilder;
        }

        /// <inheritdoc />
        public TDataProviderTestBuilder ContainingEntries(object entries)
            => this.ContainingEntries(new RouteValueDictionary(entries));

        /// <inheritdoc />
        public TDataProviderTestBuilder ContainingEntries(IDictionary<string, object> entries)
        {
            this.ValidateContainingEntries(entries);
            return this.DataProviderTestBuilder;
        }
    }
}
