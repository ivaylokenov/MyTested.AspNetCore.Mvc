namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.UnprocessableEntity
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.UnprocessableEntityResult"/>
    /// and <see cref="Microsoft.AspNetCore.Mvc.UnprocessableEntityObjectResult"/> tests.
    /// </summary>
    public interface IAndUnprocessableEntityTestBuilder : IUnprocessableEntityTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.UnprocessableEntityResult"/>
        /// and <see cref="Microsoft.AspNetCore.Mvc.UnprocessableEntityObjectResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IUnprocessableEntityTestBuilder"/>.</returns>
        IUnprocessableEntityTestBuilder AndAlso();
    }
}
