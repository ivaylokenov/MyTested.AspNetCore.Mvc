namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Authentication
{
    /// <summary>
    /// Used for adding AndAlso() method to
    /// the <see cref="Microsoft.AspNetCore.Mvc.SignInResult"/> tests.
    /// </summary>
    public interface IAndSignInTestBuilder : ISignInTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when
        /// chaining <see cref="Microsoft.AspNetCore.Mvc.SignInResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="ISignInTestBuilder"/>.</returns>
        ISignInTestBuilder AndAlso();
    }
}
