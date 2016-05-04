namespace MyTested.Mvc.Builders.Contracts.ActionResults.Challenge
{
    using System;
    using System.Collections.Generic;
    using Authentication;
    using Base;
    using Microsoft.AspNetCore.Http.Authentication;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ChallengeResult"/>.
    /// </summary>
    public interface IChallengeTestBuilder : IBaseTestBuilderWithActionResult<ChallengeResult>
    {
        /// <summary>
        /// Tests whether <see cref="ChallengeResult"/> contains specific authentication scheme provided by string.
        /// </summary>
        /// <param name="authenticationScheme">Expected authentication scheme as string.</param>
        /// <returns>The same <see cref="IAndChallengeTestBuilder"/>.</returns>
        IAndChallengeTestBuilder ContainingAuthenticationScheme(string authenticationScheme);

        /// <summary>
        /// Tests whether <see cref="ChallengeResult"/> has the provided enumerable of authentication schemes.
        /// </summary>
        /// <param name="authenticationSchemes">Expected authentication schemes as enumerable.</param>
        /// <returns>The same <see cref="IAndChallengeTestBuilder"/>.</returns>
        IAndChallengeTestBuilder ContainingAuthenticationSchemes(IEnumerable<string> authenticationSchemes);

        /// <summary>
        /// Tests whether <see cref="ChallengeResult"/> has the provided parameters of authentication schemes.
        /// </summary>
        /// <param name="authenticationSchemes">Expected authentication schemes as string parameters.</param>
        /// <returns>The same <see cref="IAndChallengeTestBuilder"/>.</returns>
        IAndChallengeTestBuilder ContainingAuthenticationSchemes(params string[] authenticationSchemes);

        /// <summary>
        /// Tests whether <see cref="ChallengeResult"/> has the provided <see cref="AuthenticationProperties"/>.
        /// </summary>
        /// <param name="properties">Expected <see cref="AuthenticationProperties"/>.</param>
        /// <returns>The same <see cref="IAndChallengeTestBuilder"/>.</returns>
        IAndChallengeTestBuilder WithAuthenticationProperties(AuthenticationProperties properties);

        /// <summary>
        /// Tests whether <see cref="ChallengeResult"/> has the given <see cref="AuthenticationProperties"/> provided as builder.
        /// </summary>
        /// <param name="authenticationPropertiesBuilder">Expected <see cref="AuthenticationProperties"/> builder.</param>
        /// <returns>The same <see cref="IAndChallengeTestBuilder"/>.</returns>
        IAndChallengeTestBuilder WithAuthenticationProperties(Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder);
    }
}
