﻿namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Authentication
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Authentication.AuthenticationProperties"/> tests.
    /// </summary>
    public interface IAndAuthenticationPropertiesTestBuilder : IAuthenticationPropertiesTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when testing <see cref="Microsoft.AspNetCore.Authentication.AuthenticationProperties"/>.
        /// </summary>
        /// <returns>The same <see cref="IAuthenticationPropertiesTestBuilder"/>.</returns>
        IAuthenticationPropertiesTestBuilder AndAlso();
    }
}
