namespace MyTested.Mvc.Builders.ExceptionErrors
{
    using System;
    using System.Linq;
    using Common.Extensions;
    using Contracts.ExceptionErrors;
    using Exceptions;
    using Utilities;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Used for testing AggregateException.
    /// </summary>
    public class AggregateExceptionTestBuilder : ExceptionTestBuilder, IAndAggregateExceptionTestBuilder
    {
        private readonly AggregateException aggregateException;

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateExceptionTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Actual received aggregate exception.</param>
        public AggregateExceptionTestBuilder(
            Controller controller,
            string actionName,
            AggregateException caughtException)
            : base(controller, actionName, caughtException)
        {
            this.aggregateException = caughtException;
        }

        /// <summary>
        /// Tests whether AggregateException contains inner exception of the provided type.
        /// </summary>
        /// <typeparam name="TInnerException">Expected inner exception type.</typeparam>
        /// <returns>The same aggregate exception test builder.</returns>
        public IAndAggregateExceptionTestBuilder ContainingInnerExceptionOfType<TInnerException>()
            where TInnerException : Exception
        {
            var expectedInnerExceptionType = typeof(TInnerException);
            var innerExceptionFound = this.aggregateException.InnerExceptions.Any(e => e.GetType() == expectedInnerExceptionType);
            if (!innerExceptionFound)
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "When calling {0} action in {1} expected AggregateException to contain {2}, but none was found.",
                    this.ActionName,
                    this.Controller.GetName(),
                    expectedInnerExceptionType.ToFriendlyTypeName()));
            }

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining aggregate exception tests.
        /// </summary>
        /// <returns>The same aggregate exception test builder.</returns>
        public new IAggregateExceptionTestBuilder AndAlso()
        {
            return this;
        }
    }
}
