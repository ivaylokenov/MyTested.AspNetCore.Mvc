namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Unauthorized
{
    /// <summary>	
    /// Used for adding AndAlso() method to	
    /// the <see cref="Microsoft.AspNetCore.Mvc.UnauthorizedResult"/>	
    /// or <see cref="Microsoft.AspNetCore.Mvc.UnauthorizedObjectResult"/> tests.	
    /// </summary>	
    public interface IAndUnauthorizedTestBuilder : IUnauthorizedTestBuilder
    {
        /// <summary>	
        /// AndAlso method for better readability when	
        /// chaining <see cref="Microsoft.AspNetCore.Mvc.UnauthorizedResult"/>	
        /// or <see cref="Microsoft.AspNetCore.Mvc.UnauthorizedObjectResult"/> tests.	
        /// </summary>	
        /// <returns>The same <see cref="IUnauthorizedTestBuilder"/>.</returns>	
        IUnauthorizedTestBuilder AndAlso();
    }
}