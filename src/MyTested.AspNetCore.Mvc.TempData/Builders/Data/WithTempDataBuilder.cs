namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Contracts.Data;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Extensions;

    /// <summary>
    /// Used for building mocked <see cref="ITempDataDictionary"/>.
    /// </summary>
    public class WithTempDataBuilder : BaseTempDataBuilder, IAndWithTempDataBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithTempDataBuilder"/> class.
        /// </summary>
        /// <param name="tempData"><see cref="ITempDataDictionary"/> to built.</param>
        public WithTempDataBuilder(ITempDataDictionary tempData)
            : base(tempData)
        {
        }

        /// <inheritdoc />
        public IAndWithTempDataBuilder WithEntry(string key, object value)
        {
            this.TempData.Add(key, value);
            return this;
        }

        /// <inheritdoc />
        public IAndWithTempDataBuilder WithEntries(IDictionary<string, object> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndWithTempDataBuilder WithEntries(object entries)
            => this.WithEntries(new RouteValueDictionary(entries));

        /// <inheritdoc />
        public IWithTempDataBuilder AndAlso() => this;
    }
}
