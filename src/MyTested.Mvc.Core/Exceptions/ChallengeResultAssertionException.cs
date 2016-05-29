namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.ChallengeResult"/>.
    /// </summary>
    public class ChallengeResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ChallengeResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
