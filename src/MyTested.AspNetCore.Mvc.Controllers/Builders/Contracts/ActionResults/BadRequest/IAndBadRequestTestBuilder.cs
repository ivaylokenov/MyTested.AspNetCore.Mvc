namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.BadRequest
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.BadRequestResult"/>
    /// and <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/> tests.
    /// </summary>
    public interface IAndBadRequestTestBuilder : IBadRequestTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.BadRequestResult"/>
        /// and <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IBadRequestTestBuilder"/>.</returns>
        IBadRequestTestBuilder AndAlso();
    }
}
