namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Data;

    /// <summary>
    /// Used for building mocked <see cref="ITempDataDictionary"/>.
    /// </summary>
    public class WithoutTempDataBuilder : BaseTempDataBuilder, IAndWithoutTempDataBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithoutTempDataBuilder"/> class.
        /// </summary>
        /// <param name="tempData"><see cref="ITempDataDictionary"/> to built.</param>
        public WithoutTempDataBuilder(ITempDataDictionary tempData)
            : base(tempData)
        {
        }

        /// <inheritdoc />
        public IAndWithoutTempDataBuilder WithoutEntries(IEnumerable<string> entriesKeys)
        {
            foreach (var key in entriesKeys)
            {
                this.WithoutEntry(key);
            }

            return this;
        }

        /// <inheritdoc />
        public IAndWithoutTempDataBuilder WithoutEntry(string key)
        {
            if (this.TempData.ContainsKey(key))
                this.TempData.Remove(key);

            return this;
        }

        /// <inheritdoc />
        public IAndWithoutTempDataBuilder WithoutAllEntries()
        {
            this.TempData.Clear();
            return this;
        }

        /// <inheritdoc />
        public IWithoutTempDataBuilder AndAlso()
            => this;
    }
}
