namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Attribute"/>.
    /// </summary>
    public class AttributeAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AttributeAssertionException(string message)
            : base(message)
        {
        }
    }
}
