namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ShouldPassFor
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Test builder allowing additional assertions on various components.
    /// </summary>
    public interface IShouldPassForTestBuilderWithAction : IShouldPassForTestBuilderWithComponent<object>
    {
        /// <summary>
        /// Tests whether the action passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action delegate containing assertions on the controller action.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithAction"/>.</returns>
        IShouldPassForTestBuilderWithAction TheAction(Action<string> assertions);

        /// <summary>
        /// Tests whether the action passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the controller action.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithAction"/>.</returns>
        IShouldPassForTestBuilderWithAction TheAction(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether the action attributes passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action delegate containing assertions on the action attributes.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithAction"/>.</returns>
        IShouldPassForTestBuilderWithAction TheActionAttributes(Action<IEnumerable<object>> assertions);

        /// <summary>
        /// Tests whether the action attributes passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the action attributes.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithAction"/>.</returns>
        IShouldPassForTestBuilderWithAction TheActionAttributes(Func<IEnumerable<object>, bool> predicate);
    }
}
