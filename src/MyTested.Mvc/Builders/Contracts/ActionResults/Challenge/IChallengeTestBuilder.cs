namespace MyTested.Mvc.Builders.Contracts.ActionResults.Challenge
{
    using Authentication;
    using Microsoft.AspNet.Http.Authentication;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used for testing challenge result.
    /// </summary>
    public interface IChallengeTestBuilder
    {
        IAndChallengeTestBuilder ContainingAuthenticationScheme(string authenticationScheme);

        IAndChallengeTestBuilder ContainingAuthenticationSchemes(IEnumerable<string> authenticationSchemes);

        IAndChallengeTestBuilder ContainingAuthenticationSchemes(params string[] authenticationSchemes);

        IAndChallengeTestBuilder WithAuthenticationProperties(AuthenticationProperties properties);

        IAndChallengeTestBuilder WithAuthenticationProperties(Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder);
    }
}
