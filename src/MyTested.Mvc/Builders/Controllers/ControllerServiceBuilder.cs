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
    public partial class ControllerBuilder<TController>
    {
        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithNoServiceFor<TService>()
            where TService : class
        {
            return this.WithServiceFor<TService>(null);
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithServiceFor<TService>(TService service)
            where TService : class
        {
            var typeOfService = service != null
                ? service.GetType()
                : typeof(TService);

            if (this.aggregatedServices.ContainsKey(typeOfService))
            {
                throw new InvalidOperationException(string.Format(
                    "Dependency {0} is already registered for {1} controller.",
                    typeOfService.ToFriendlyTypeName(),
                    typeof(TController).ToFriendlyTypeName()));
            }

            this.aggregatedServices.Add(typeOfService, service);
            this.TestContext.ControllerConstruction = () => null;
            return this;
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithServices(IEnumerable<object> services)
        {
            services.ForEach(s => this.WithServiceFor(s));
            return this;
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithServices(params object[] services)
        {
            services.ForEach(s => this.WithServiceFor(s));
            return this;
        }
    }
}
