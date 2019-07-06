namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Attribute"/>.
    /// </summary>
    public class AttributeAssertionException : Exception
    {
        public const string DefaultFormat = "When testing {0} was expected to have {1} with {2}, but {3}.";

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
