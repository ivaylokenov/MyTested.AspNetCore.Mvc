namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using System;
    using Internal;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/>,
    /// <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/> or
    /// <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/>.
    /// </summary>
    public class ViewResultAssertionException : Exception
    {
        public static ViewResultAssertionException ForNameEquality(
            string messagePrefix,
            string viewType,
            string expectedViewName,
            string actualViewName)
            => new ViewResultAssertionException(string.Format(
                "{0} {1} result to be {2}, but instead received {3}.",
                messagePrefix,
                viewType,
                TestHelper.GetFriendlyName(expectedViewName),
                TestHelper.GetFriendlyName(actualViewName)));

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ViewResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
