namespace MyTested.Mvc.Builders.Controllers
{
    using Contracts.Controllers;
    using Data;
    using Exceptions;
    using Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using System;
    using System.Linq;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class inheriting ASP.NET MVC controller.</typeparam>
    public partial class ControllerBuilder<TController>
    {
        public IAndControllerBuilder<TController> WithControllerContext(ControllerContext controllerContext)
        {
            this.TestContext.ControllerContext = controllerContext;
            return this;
        }

        public IAndControllerBuilder<TController> WithActionContext(ActionContext actionContext)
        {
            return this;
        }

        /// <summary>
        /// Sets custom properties to the controller using action delegate.
        /// </summary>
        /// <param name="controllerSetup">Action delegate to use for controller setup.</param>
        /// <returns>The same controller test builder.</returns>
        public IAndControllerBuilder<TController> WithSetup(Action<TController> controllerSetup)
        {
            this.controllerSetupAction = controllerSetup;
            return this;
        }

        private void BuildControllerIfNotExists()
        {
            var controller = this.TestContext.Controller;
            if (controller == null)
            {
                if (this.aggregatedDependencies.Any())
                {
                    // custom dependencies are set, try create instance with them
                    controller = Reflection.TryCreateInstance<TController>(this.aggregatedDependencies.Select(v => v.Value).ToArray());
                }
                else
                {
                    // no custom dependencies are set, try create instance with the global services
                    controller = TestHelper.TryCreateInstance<TController>();
                }

                if (controller == null)
                {
                    // no controller at this point, try to create one with default constructor
                    controller = Reflection.TryFastCreateInstance<TController>();
                }

                if (controller == null)
                {
                    var friendlyDependenciesNames = this.aggregatedDependencies
                        .Keys
                        .Select(k => k.ToFriendlyTypeName());

                    var joinedFriendlyDependencies = string.Join(", ", friendlyDependenciesNames);

                    throw new UnresolvedDependenciesException(string.Format(
                        "{0} could not be instantiated because it contains no constructor taking {1} parameters.",
                        typeof(TController).ToFriendlyTypeName(),
                        this.aggregatedDependencies.Count == 0 ? "no" : $"{joinedFriendlyDependencies} as"));
                }

                this.TestContext.ControllerConstruction = () => controller;
            }

            if (!this.isPreparedForTesting)
            {
                this.PrepareController();
                this.isPreparedForTesting = true;
            }
        }

        private void PrepareController()
        {
            var options = this.Services.GetRequiredService<IOptions<MvcOptions>>().Value;

            var controllerContext = new MockedControllerContext(this.TestContext);

            var controllerPropertyActivators = this.Services.GetServices<IControllerPropertyActivator>();

            controllerPropertyActivators.ForEach(a => a.Activate(controllerContext, this.TestContext.Controller));

            if (this.tempDataBuilderAction != null)
            {
                this.tempDataBuilderAction(new TempDataBuilder(this.TestContext.ControllerAs<TController>()?.TempData));
            }

            if (this.controllerSetupAction != null)
            {
                this.controllerSetupAction(this.TestContext.ControllerAs<TController>());
            }
        }
    }
}
