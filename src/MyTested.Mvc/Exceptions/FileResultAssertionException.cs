namespace MyTested.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid file result.
    /// </summary>
    public class FileResultAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the FileResultAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public FileResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
