namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid challenge result.
    /// </summary>
    public class ChallengeResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ContentResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public ChallengeResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
