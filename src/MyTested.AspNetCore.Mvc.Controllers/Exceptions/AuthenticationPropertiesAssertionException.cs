namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Authentication.AuthenticationProperties"/> assertion.
    /// </summary>
    public class AuthenticationPropertiesAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationPropertiesAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AuthenticationPropertiesAssertionException(string message)
            : base(message)
        {
        }
    }
}
