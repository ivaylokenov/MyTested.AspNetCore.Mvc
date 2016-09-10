namespace MyTested.AspNetCore.Mvc.Builders.Contracts.CaughtExceptions
{
    /// <summary>
    /// Used for testing whether the method invocation throws exception.
    /// </summary>
    public interface IShouldThrowTestBuilder
    {
        /// <summary>
        /// Tests whether the method invocation throws any exception.
        /// </summary>
        /// <returns>Test builder of <see cref="IExceptionTestBuilder"/> type.</returns>
        IExceptionTestBuilder Exception();

        /// <summary>
        /// Tests whether the method invocation throws <see cref="System.AggregateException"/>.
        /// </summary>
        /// <param name="withNumberOfInnerExceptions">Optional expected number of total inner exceptions.</param>
        /// <returns>Test builder of <see cref="IAggregateExceptionTestBuilder"/> type.</returns>
        IAggregateExceptionTestBuilder AggregateException(int? withNumberOfInnerExceptions = null);
    }
}
