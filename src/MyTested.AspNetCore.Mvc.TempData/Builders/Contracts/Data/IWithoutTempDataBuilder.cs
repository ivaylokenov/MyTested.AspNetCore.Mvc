namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Used for building <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
    /// </summary>
    public interface IWithoutTempDataBuilder
    {
        /// <summary>
        /// Remove temp data entry by providing its key to the built <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
        /// </summary>
        /// <param name="key">Key of the temp data entry.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.</returns>
        IAndWithoutTempDataBuilder WithoutEntry(string key);

        /// <summary>
        /// Remove temp data entries by providing their keys to the built <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
        /// </summary>
        /// <param name="keys">Keys of the temp data entries to be deleted.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.</returns>
        IAndWithoutTempDataBuilder WithoutEntries(IEnumerable<string> keys);

        /// <summary>
        /// Remove temp data entries by providing their keys as params to the built <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
        /// </summary>
        /// <param name="keys">Keys of the temp data entries to be deleted.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.</returns>
        IAndWithoutTempDataBuilder WithoutEntries(params string[] keys);

        /// <summary>
        /// Clear all entities from the built <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
        /// </summary>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.</returns>
        IAndWithoutTempDataBuilder WithoutEntries();
    }
}
