﻿namespace MyTested.AspNetCore.Mvc.Builders.Contracts.CaughtExceptions
{
    using System;

    /// <summary>
    /// Used for testing <see cref="AggregateException"/>.
    /// </summary>
    public interface IAggregateExceptionTestBuilder : IBaseExceptionTestBuilder
    {
        /// <summary>
        /// Tests whether <see cref="AggregateException"/> contains inner exception of the provided type.
        /// </summary>
        /// <typeparam name="TInnerException">Expected inner exception type.</typeparam>
        /// <returns>The same <see cref="IAndAggregateExceptionTestBuilder"/>.</returns>
        IAndAggregateExceptionTestBuilder ContainingInnerExceptionOfType<TInnerException>()
            where TInnerException : Exception;

        /// <summary>
        /// Tests whether <see cref="AggregateException"/> contains inner exception of the provided type.
        /// </summary>
        /// <param name="innerExeption"></param>
        /// <returns></returns>
        IAndAggregateExceptionTestBuilder ContainingInnerExceptionOfType(Type innerExeption);
    }
}
