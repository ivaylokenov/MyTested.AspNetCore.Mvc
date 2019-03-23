namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Base
{
    using System;
    using System.Collections.Generic;
    using Authentication;
    using Contracts.Base;
    using Microsoft.AspNetCore.Authentication;

    /// <summary>
    /// Base interface for all test builders with authentication <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>.
    /// </summary>
    /// <typeparam name="TAuthenticationResultTestBuilder">Type of authentication result test builder to use as a return type for common methods.</typeparam>
    public interface IBaseTestBuilderWithAuthenticationResult<TAuthenticationResultTestBuilder> : IBaseTestBuilderWithActionResult
        where TAuthenticationResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// contains specific authentication scheme provided by string.
        /// </summary>
        /// <param name="authenticationScheme">Expected authentication scheme as string.</param>
        /// <returns>The same authentication <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TAuthenticationResultTestBuilder ContainingAuthenticationScheme(string authenticationScheme);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// has the provided collection of authentication schemes.
        /// </summary>
        /// <param name="authenticationSchemes">Expected authentication schemes as collection.</param>
        /// <returns>The same authentication <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TAuthenticationResultTestBuilder ContainingAuthenticationSchemes(IEnumerable<string> authenticationSchemes);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// has the provided parameters of authentication schemes.
        /// </summary>
        /// <param name="authenticationSchemes">Expected authentication schemes as string parameters.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TAuthenticationResultTestBuilder ContainingAuthenticationSchemes(params string[] authenticationSchemes);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// has the provided <see cref="AuthenticationProperties"/>.
        /// </summary>
        /// <param name="properties">Expected <see cref="AuthenticationProperties"/>.</param>
        /// <returns>The same authentication <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TAuthenticationResultTestBuilder WithAuthenticationProperties(AuthenticationProperties properties);

        /// <summary>
        /// Tests whether the authentication <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> has
        /// the given <see cref="AuthenticationProperties"/> provided as builder.
        /// </summary>
        /// <param name="authenticationPropertiesBuilder">Expected <see cref="AuthenticationProperties"/> builder.</param>
        /// <returns>The same authentication <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        TAuthenticationResultTestBuilder WithAuthenticationProperties(
            Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder);
    }
}
