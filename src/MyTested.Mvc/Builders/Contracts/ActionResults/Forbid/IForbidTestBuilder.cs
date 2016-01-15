namespace MyTested.Mvc.Builders.Contracts.ActionResults.Forbid
{
    using System;
    using System.Collections.Generic;
    using Authentication;
    using Microsoft.AspNet.Http.Authentication;

    /// <summary>
    /// Used for testing forbid result.
    /// </summary>
    public interface IForbidTestBuilder
    {
        IAndForbidTestBuilder ContainingAuthenticationScheme(string authenticationScheme);

        IAndForbidTestBuilder ContainingAuthenticationSchemes(IEnumerable<string> authenticationSchemes);

        IAndForbidTestBuilder ContainingAuthenticationSchemes(params string[] authenticationSchemes);

        IAndForbidTestBuilder WithAuthenticationProperties(AuthenticationProperties properties);

        IAndForbidTestBuilder WithAuthenticationProperties(Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder);
    }
}
