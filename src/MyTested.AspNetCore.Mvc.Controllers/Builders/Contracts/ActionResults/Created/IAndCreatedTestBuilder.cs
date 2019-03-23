namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Created
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>,
    /// <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.CreatedAtRouteResult"/> tests.
    /// </summary>
    public interface IAndCreatedTestBuilder : ICreatedTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when
        /// chaining <see cref="Microsoft.AspNetCore.Mvc.CreatedResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.CreatedAtActionResult"/> or <see cref="Microsoft.AspNetCore.Mvc.CreatedAtRouteResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="ICreatedTestBuilder"/>.</returns>
        ICreatedTestBuilder AndAlso();
    }
}
