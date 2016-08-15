namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Contracts.Data;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for building mocked <see cref="ITempDataDictionary"/>.
    /// </summary>
    public class TempDataBuilder : IAndTempDataBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TempDataBuilder"/> class.
        /// </summary>
        /// <param name="tempData"><see cref="ITempDataDictionary"/> to built.</param>
        public TempDataBuilder(ITempDataDictionary tempData)
        {
            CommonValidator.CheckForNullReference(tempData, nameof(ITempDataDictionary));
            this.TempData = tempData;
        }

        /// <summary>
        /// Gets the mocked <see cref="ITempDataDictionary"/>.
        /// </summary>
        /// <value>Built <see cref="ITempDataDictionary"/>.</value>
        protected ITempDataDictionary TempData { get; private set; }

        /// <inheritdoc />
        public IAndTempDataBuilder WithEntry(string key, object value)
        {
            this.TempData.Add(key, value);
            return this;
        }

        /// <inheritdoc />
        public IAndTempDataBuilder WithEntries(IDictionary<string, object> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndTempDataBuilder WithEntries(object entries)
            => this.WithEntries(new RouteValueDictionary(entries));

        /// <inheritdoc />
        public ITempDataBuilder AndAlso() => this;
    }
}
