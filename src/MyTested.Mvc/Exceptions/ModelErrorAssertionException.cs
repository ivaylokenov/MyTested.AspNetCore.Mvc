namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for model with errors.
    /// </summary>
    public class ModelErrorAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ModelErrorAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public ModelErrorAssertionException(string message)
            : base(message)
        {
        }
    }
}
