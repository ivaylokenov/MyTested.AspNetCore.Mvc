namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Http.HttpResponse"/>.
    /// </summary>
    public class HttpResponseAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public HttpResponseAssertionException(string message)
            : base(message)
        {
        }
    }
}
