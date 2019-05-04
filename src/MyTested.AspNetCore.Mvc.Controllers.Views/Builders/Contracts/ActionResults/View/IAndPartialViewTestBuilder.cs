namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.View
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/> tests.
    /// </summary>
    public interface IAndPartialViewTestBuilder : IPartialViewTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IPartialViewTestBuilder"/>.</returns>
        IPartialViewTestBuilder AndAlso();
    }
}
