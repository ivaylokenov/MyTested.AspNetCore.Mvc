namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;
    
    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.SignInResult"/>.
    /// </summary>
    public class SignInResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignInResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SignInResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
