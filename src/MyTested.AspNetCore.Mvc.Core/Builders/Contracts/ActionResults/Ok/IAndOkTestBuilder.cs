namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Ok
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.OkResult"/> or <see cref="Microsoft.AspNetCore.Mvc.OkObjectResult"/> tests.
    /// </summary>
    public interface IAndOkTestBuilder : IOkTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.OkResult"/> or <see cref="Microsoft.AspNetCore.Mvc.OkObjectResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IOkTestBuilder"/>.</returns>
        IOkTestBuilder AndAlso();
    }
}
