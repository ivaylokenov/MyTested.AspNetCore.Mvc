namespace MyTested.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> tests.
    /// </summary>
    public interface IAndTempDataTestBuilder : ITempDataTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
        /// </summary>
        /// <returns>The same <see cref="ITempDataTestBuilder"/>.</returns>
        ITempDataTestBuilder AndAlso();
    }
}
