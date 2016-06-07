namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ShouldPassFor
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Test builder allowing additional assertions on various components.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public interface IShouldPassForTestBuilderWithController<TController> : IShouldPassForTestBuilder
        where TController : class
    {
        /// <summary>
        /// Tests whether the controller passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing assertions on the controller.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithController{TController}"/>.</returns>
        IShouldPassForTestBuilderWithController<TController> TheController(Action<TController> assertions);

        /// <summary>
        /// Tests whether the controller passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the controller.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithController{TController}"/>.</returns>
        IShouldPassForTestBuilderWithController<TController> TheController(Func<TController, bool> predicate);

        /// <summary>
        /// Tests whether the controller attributes passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing assertions on the controller attributes.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithController{TController}"/>.</returns>
        IShouldPassForTestBuilderWithController<TController> TheControllerAttributes(Action<IEnumerable<object>> assertions);

        /// <summary>
        /// Tests whether the controller attributes passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the controller attributes.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithController{TController}"/>.</returns>
        IShouldPassForTestBuilderWithController<TController> TheControllerAttributes(Func<IEnumerable<object>, bool> predicate);
    }
}
