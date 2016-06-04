namespace MyTested.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.Controller.ViewBag"/> tests.
    /// </summary>
    public interface IAndViewBagTestBuilder : IViewBagTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.AspNetCore.Mvc.Controller.ViewBag"/>.
        /// </summary>
        /// <returns>The same <see cref="IViewBagTestBuilder"/>.</returns>
        IViewBagTestBuilder AndAlso();
    }
}
