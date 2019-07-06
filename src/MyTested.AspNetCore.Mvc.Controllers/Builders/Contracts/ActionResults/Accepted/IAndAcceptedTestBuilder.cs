namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Accepted
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.AcceptedResult"/>,
    /// <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtActionResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtRouteResult"/> tests.
    /// </summary>
    public interface IAndAcceptedTestBuilder : IAcceptedTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when
        /// chaining <see cref="Microsoft.AspNetCore.Mvc.AcceptedResult"/>,
        /// <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtActionResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.AcceptedAtRouteResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IAcceptedTestBuilder"/>.</returns>
        IAcceptedTestBuilder AndAlso();
    }
}
