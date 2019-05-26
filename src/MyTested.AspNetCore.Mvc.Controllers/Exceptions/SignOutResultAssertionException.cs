namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.SignOutResult"/>.
    /// </summary>
    public class SignOutResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignOutResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SignOutResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
