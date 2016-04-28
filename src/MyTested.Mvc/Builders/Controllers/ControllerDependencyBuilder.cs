namespace MyTested.Mvc.Builders.Controllers
{
    using System;
    using System.Collections.Generic;
    using Contracts.Controllers;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public partial class ControllerBuilder<TController>
    {
        public IAndControllerBuilder<TController> WithNoResolvedDependencyFor<TDependency>()
            where TDependency : class
        {
            return this.WithResolvedDependencyFor<TDependency>(null);
        }

        /// <summary>
        /// Tries to resolve constructor dependency of given type.
        /// </summary>
        /// <typeparam name="TDependency">Type of dependency to resolve.</typeparam>
        /// <param name="dependency">Instance of dependency to inject into constructor.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithResolvedDependencyFor<TDependency>(TDependency dependency)
            where TDependency : class
        {
            var typeOfDependency = dependency != null
                ? dependency.GetType()
                : typeof(TDependency);

            if (this.aggregatedDependencies.ContainsKey(typeOfDependency))
            {
                throw new InvalidOperationException(string.Format(
                    "Dependency {0} is already registered for {1} controller.",
                    typeOfDependency.ToFriendlyTypeName(),
                    typeof(TController).ToFriendlyTypeName()));
            }

            this.aggregatedDependencies.Add(typeOfDependency, dependency);
            this.TestContext.ControllerConstruction = () => null;
            return this;
        }

        /// <summary>
        /// Tries to resolve constructor dependencies by the provided collection of dependencies.
        /// </summary>
        /// <param name="dependencies">Collection of dependencies to inject into constructor.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithResolvedDependencies(IEnumerable<object> dependencies)
        {
            dependencies.ForEach(d => this.WithResolvedDependencyFor(d));
            return this;
        }

        /// <summary>
        /// Tries to resolve constructor dependencies by the provided dependencies.
        /// </summary>
        /// <param name="dependencies">Dependencies to inject into constructor.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithResolvedDependencies(params object[] dependencies)
        {
            dependencies.ForEach(d => this.WithResolvedDependencyFor(d));
            return this;
        }
    }
}
