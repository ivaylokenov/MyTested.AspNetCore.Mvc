namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for controller unresolved dependencies.
    /// </summary>
    public class UnresolvedDependenciesException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnresolvedDependenciesException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnresolvedDependenciesException(string message)
            : base(message)
        {
        }
    }
}
