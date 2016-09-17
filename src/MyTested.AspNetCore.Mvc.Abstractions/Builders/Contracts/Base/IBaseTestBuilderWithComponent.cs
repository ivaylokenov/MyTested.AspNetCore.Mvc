namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    using System;
    using And;

    /// <summary>
    /// Base class for all test builders with component.
    /// </summary>
    public interface IBaseTestBuilderWithComponent : IBaseTestBuilder
    {
        /// <summary>
        /// Tests whether the provided component type passes the given assertions.
        /// </summary>
        /// <typeparam name="TComponent">Type of component to test.</typeparam>
        /// <param name="assertions">Action containing assertions on the provided component.</param>
        /// <returns>The same <see cref="IBaseTestBuilderWithComponent"/>.</returns>
        IAndTestBuilder ShouldPassForThe<TComponent>(Action<TComponent> assertions)
            where TComponent : class;

        /// <summary>
        /// Tests whether the provided component passes the given predicate.
        /// </summary>
        /// <typeparam name="TComponent">Type of component to test.</typeparam>
        /// <param name="predicate">Predicate testing the provided component.</param>
        /// <returns>The same <see cref="IBaseTestBuilderWithComponent"/>.</returns>
        IAndTestBuilder ShouldPassForThe<TComponent>(Func<TComponent, bool> predicate)
            where TComponent : class;
    }
}
