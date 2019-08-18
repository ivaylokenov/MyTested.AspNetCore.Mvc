namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.StatusCode
{
    /// <summary>
    /// Used for adding AndAlso() method to
    /// the <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/> tests.
    /// </summary>
    public interface IAndStatusCodeTestBuilder : IStatusCodeTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when
        /// chaining <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IStatusCodeTestBuilder"/>.</returns>
        IStatusCodeTestBuilder AndAlso();
    }
}
