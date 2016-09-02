namespace MyTested.AspNetCore.Mvc.Exceptions
{
    using Internal;
    using System;

    /// <summary>
    /// <see cref="Exception"/> for invalid <see cref="Microsoft.AspNetCore.Mvc.ViewResult"/>, <see cref="Microsoft.AspNetCore.Mvc.PartialViewResult"/> or <see cref="Microsoft.AspNetCore.Mvc.ViewComponentResult"/>.
    /// </summary>
    public class ViewViewComponentResultAssertionException : Exception
    {
        public static ViewViewComponentResultAssertionException ForNameEquality(
            string messagePrefix,
            string expectedViewName,
            string actualViewName)
            => new ViewViewComponentResultAssertionException(string.Format(
                "{0} view result to be {1}, but instead received {2}.",
                messagePrefix,
                TestHelper.GetFriendlyName(expectedViewName),
                TestHelper.GetFriendlyName(actualViewName)));

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewViewComponentResultAssertionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ViewViewComponentResultAssertionException(string message)
            : base(message)
        {
        }
    }
}
