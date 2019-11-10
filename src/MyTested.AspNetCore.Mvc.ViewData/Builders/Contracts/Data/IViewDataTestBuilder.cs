namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/>.
    /// </summary>
    public interface IViewDataTestBuilder
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> contains entry with the provided key.
        /// </summary>
        /// <param name="key">Key of the view data entry.</param>
        /// <returns>The same <see cref="IAndViewDataTestBuilder"/>.</returns>
        IAndViewDataTestBuilder ContainingEntryWithKey(string key);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> contains entry with the provided value.
        /// </summary>
        /// <typeparam name="TValue">Type of the view data entry value.</typeparam>
        /// <param name="value">Value of the view data entry.</param>
        /// <returns>The same <see cref="IAndViewDataTestBuilder"/>.</returns>
        IAndViewDataTestBuilder ContainingEntryWithValue<TValue>(TValue value);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> contains entry with value of the provided type.
        /// </summary>
        /// <typeparam name="TValue">Type of the view data entry value.</typeparam>
        /// <returns>The same <see cref="IAndViewDataTestBuilder"/>.</returns>
        IAndViewDataTestBuilder ContainingEntryOfType<TValue>();

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> contains entry with value of the provided type.
        /// </summary>
        /// <returns>The same <see cref="IAndViewDataTestBuilder"/>.</returns>
        IAndViewDataTestBuilder ContainingEntryOfType(Type valueType);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> contains entry with value of the provided type and the given key.
        /// </summary>
        /// <typeparam name="TValue">Type of the view data entry value.</typeparam>
        /// <param name="key">Key of the view data entry.</param>
        /// <returns>The same <see cref="IAndViewDataTestBuilder"/>.</returns>
        IAndViewDataTestBuilder ContainingEntryOfType<TValue>(string key);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> contains entry with value of the provided type and the given key.
        /// </summary>
        /// <param name="key">Key of the view data entry.</param>
        /// <param name="valueType"></param>
        /// <returns>The same <see cref="IAndViewDataTestBuilder"/>.</returns>
        IAndViewDataTestBuilder ContainingEntryOfType(string key,Type valueType);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> contains entry with the provided key and corresponding value.
        /// </summary>
        /// <param name="key">Key of the view data entry.</param>
        /// <param name="value">Value of the view data entry.</param>
        /// <returns>The same <see cref="IAndViewDataTestBuilder"/>.</returns>
        IAndViewDataTestBuilder ContainingEntry(string key, object value);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> contains specific entry by using a builder. 
        /// </summary>
        /// <param name="viewDataEntryTestBuilder">Builder for setting specific <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> entry tests.</param>
        /// <returns>The same <see cref="IAndViewDataTestBuilder"/>.</returns>
        IAndViewDataTestBuilder ContainingEntry(Action<IDataProviderEntryKeyTestBuilder> viewDataEntryTestBuilder);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> contains the provided entries. 
        /// </summary>
        /// <param name="entries">Anonymous object of view data entries.</param>
        /// <returns>The same <see cref="IAndViewDataTestBuilder"/>.</returns>
        IAndViewDataTestBuilder ContainingEntries(object entries);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> contains the provided entries. 
        /// </summary>
        /// <param name="entries">Dictionary of view data entries.</param>
        /// <returns>The same <see cref="IAndViewDataTestBuilder"/>.</returns>
        IAndViewDataTestBuilder ContainingEntries(IDictionary<string, object> entries);
    }
}
