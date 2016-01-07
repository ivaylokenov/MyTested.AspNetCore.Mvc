namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid authentication properties call.
    /// </summary>
    public class AuthenticationPropertiesAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the AuthenticationPropertiesAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public AuthenticationPropertiesAssertionException(string message)
            : base(message)
        {
        }
    }
}
