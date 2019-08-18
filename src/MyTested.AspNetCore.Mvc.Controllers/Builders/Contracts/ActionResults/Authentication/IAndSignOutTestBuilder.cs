namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Authentication
{
    /// <summary>
    /// Used for adding AndAlso() method to
    /// the <see cref="Microsoft.AspNetCore.Mvc.SignOutResult"/> tests.
    /// </summary>
    public interface IAndSignOutTestBuilder : ISignOutTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when
        /// chaining <see cref="Microsoft.AspNetCore.Mvc.SignOutResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="ISignOutTestBuilder"/>.</returns>
        ISignOutTestBuilder AndAlso();
    }
}
