namespace MyTested.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> tests.
    /// </summary>
    public interface IAndViewDataTestBuilder : IViewDataTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/>.
        /// </summary>
        /// <returns>The same <see cref="IViewDataTestBuilder"/>.</returns>
        IViewDataTestBuilder AndAlso();
    }
}
