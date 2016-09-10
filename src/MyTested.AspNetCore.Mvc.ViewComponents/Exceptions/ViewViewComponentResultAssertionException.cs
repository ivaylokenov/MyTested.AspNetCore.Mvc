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

        public static ViewViewComponentResultAssertionException ForViewEngineEquality(
            string messagePrefix)
            => new ViewViewComponentResultAssertionException(string.Format(
                $"{messagePrefix} view result ViewEngine to be the same as the provided one, but instead received different result."));

        public static ViewViewComponentResultAssertionException ForViewEngineType(
            string messagePrefix,
            string expectedViewEngineType,
            string actualViewEngineType)
            => new ViewViewComponentResultAssertionException(string.Format(
                "{0} view result ViewEngine to be of {1} type, but instead received {2}.",
                messagePrefix,
                expectedViewEngineType,
                actualViewEngineType));

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
