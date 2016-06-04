namespace MyTested.Mvc.Builders.Contracts.ActionResults.Json
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.JsonResult"/> tests.
    /// </summary>
    public interface IAndJsonTestBuilder : IJsonTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.JsonResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IJsonTestBuilder"/>.</returns>
        IJsonTestBuilder AndAlso();
    }
}
