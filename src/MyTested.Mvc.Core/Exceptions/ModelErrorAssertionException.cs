namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
    /// </summary>
    public class ModelErrorAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelErrorAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ModelErrorAssertionException(string message)
            : base(message)
        {
        }
    }
}
