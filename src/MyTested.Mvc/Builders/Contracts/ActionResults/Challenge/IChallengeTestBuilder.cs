namespace MyTested.Mvc.Builders.Contracts.ActionResults.Challenge
{
    using System;
    using System.Collections.Generic;
    using Authentication;
    using Base;
    using Microsoft.AspNet.Http.Authentication;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Used for testing challenge result.
    /// </summary>
    public interface IChallengeTestBuilder : IBaseTestBuilderWithActionResult<ChallengeResult>
    {
        /// <summary>
        /// Tests whether challenge result contains specific authentication scheme provided by string.
        /// </summary>
        /// <param name="authenticationScheme">Expected authentication scheme as string.</param>
        /// <returns>The same challenge test builder.</returns>
        IAndChallengeTestBuilder ContainingAuthenticationScheme(string authenticationScheme);
        
        /// <summary>
        /// Tests whether challenge result has the provided enumerable of authentication schemes.
        /// </summary>
        /// <param name="authenticationSchemes">Expected authentication schemes as enumerable.</param>
        /// <returns>The same challenge test builder.</returns>
        IAndChallengeTestBuilder ContainingAuthenticationSchemes(IEnumerable<string> authenticationSchemes);

        /// <summary>
        /// Tests whether challenge result has the provided parameters of authentication schemes.
        /// </summary>
        /// <param name="authenticationSchemes">Expected authentication schemes as string parameters.</param>
        /// <returns>The same challenge test builder.</returns>
        IAndChallengeTestBuilder ContainingAuthenticationSchemes(params string[] authenticationSchemes);

        /// <summary>
        /// Tests whether challenge result has the provided authentication properties.
        /// </summary>
        /// <param name="properties">Expected authentication properties.</param>
        /// <returns>The same challenge test builder.</returns>
        IAndChallengeTestBuilder WithAuthenticationProperties(AuthenticationProperties properties);

        /// <summary>
        /// Tests whether challenge result has the provided authentication properties provided as builder.
        /// </summary>
        /// <param name="authenticationPropertiesBuilder">Expected authentication properties.</param>
        /// <returns>The same challenge test builder.</returns>
        IAndChallengeTestBuilder WithAuthenticationProperties(Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder);
    }
}
