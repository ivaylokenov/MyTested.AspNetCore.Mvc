namespace MyTested.Mvc.Builders.Contracts.ActionResults.Forbid
{
    using System;
    using System.Collections.Generic;
    using Authentication;
    using Base;
    using Microsoft.AspNetCore.Http.Authentication;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing forbid result.
    /// </summary>
    public interface IForbidTestBuilder : IBaseTestBuilderWithActionResult<ForbidResult>
    {
        /// <summary>
        /// Tests whether forbid result contains specific authentication scheme provided by string.
        /// </summary>
        /// <param name="authenticationScheme">Expected authentication scheme as string.</param>
        /// <returns>The same forbid test builder.</returns>
        IAndForbidTestBuilder ContainingAuthenticationScheme(string authenticationScheme);

        /// <summary>
        /// Tests whether forbid result has the provided enumerable of authentication schemes.
        /// </summary>
        /// <param name="authenticationSchemes">Expected authentication schemes as enumerable.</param>
        /// <returns>The same forbid test builder.</returns>
        IAndForbidTestBuilder ContainingAuthenticationSchemes(IEnumerable<string> authenticationSchemes);

        /// <summary>
        /// Tests whether forbid result has the provided parameters of authentication schemes.
        /// </summary>
        /// <param name="authenticationSchemes">Expected authentication schemes as string parameters.</param>
        /// <returns>The same forbid test builder.</returns>
        IAndForbidTestBuilder ContainingAuthenticationSchemes(params string[] authenticationSchemes);

        /// <summary>
        /// Tests whether forbid result has the provided authentication properties.
        /// </summary>
        /// <param name="properties">Expected authentication properties.</param>
        /// <returns>The same forbid test builder.</returns>
        IAndForbidTestBuilder WithAuthenticationProperties(AuthenticationProperties properties);

        /// <summary>
        /// Tests whether forbid result has the provided authentication properties provided as builder.
        /// </summary>
        /// <param name="authenticationPropertiesBuilder">Expected authentication properties.</param>
        /// <returns>The same forbid test builder.</returns>
        IAndForbidTestBuilder WithAuthenticationProperties(Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder);
    }
}
