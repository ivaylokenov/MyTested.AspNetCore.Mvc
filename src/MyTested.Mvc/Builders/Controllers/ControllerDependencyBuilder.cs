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
        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithNoResolvedDependencyFor<TDependency>()
            where TDependency : class
        {
            return this.WithResolvedDependencyFor<TDependency>(null);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithResolvedDependencies(IEnumerable<object> dependencies)
        {
            dependencies.ForEach(d => this.WithResolvedDependencyFor(d));
            return this;
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithResolvedDependencies(params object[] dependencies)
        {
            dependencies.ForEach(d => this.WithResolvedDependencyFor(d));
            return this;
        }
    }
}
