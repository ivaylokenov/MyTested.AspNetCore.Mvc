namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for controller unresolved service dependencies.
    /// </summary>
    public class UnresolvedServicesException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnresolvedServicesException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnresolvedServicesException(string message)
            : base(message)
        {
        }
    }
}
