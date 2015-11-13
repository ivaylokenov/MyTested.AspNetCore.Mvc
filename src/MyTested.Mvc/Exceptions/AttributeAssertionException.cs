namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid attributes.
    /// </summary>
    public class AttributeAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the AttributeAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public AttributeAssertionException(string message)
            : base(message)
        {
        }
    }
}
