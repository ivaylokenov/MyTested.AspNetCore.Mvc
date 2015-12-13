namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for controller unresolved dependencies.
    /// </summary>
    public class UnresolvedDependenciesException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the UnresolvedDependenciesException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public UnresolvedDependenciesException(string message)
            : base(message)
        {
        }
    }
}
