namespace MyTested.Mvc.Builders.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Actions;
    using Authentication;
    using Contracts.Actions;
    using Contracts.Authentication;
    using Contracts.Controllers;
    using Contracts.Http;
    using Exceptions;
    using Http;
    using Internal.Application;
    using Internal.Contracts;
    using Utilities.Extensions;
    using Internal.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Utilities;
    using Utilities.Validators;
    using Internal.Routes;
    using Internal.TestContexts;
    using Internal;
    using Contracts.Data;
    using Data;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class inheriting ASP.NET MVC controller.</typeparam>
    public class ControllerBuilder<TController> : IAndControllerBuilder<TController>
        where TController : Controller
    {
        private readonly IDictionary<Type, object> aggregatedDependencies;

        private ControllerTestContext testContext;
        private Action<ITempDataBuilder> tempDataBuilderAction;
        private Action<TController> controllerSetupAction;
        private bool isPreparedForTesting;
        private bool enabledValidation;
        private bool resolveRouteValues;
        private object additionalRouteValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBuilder{TController}" /> class.
        /// </summary>
        /// <param name="testContext"></param>
        public ControllerBuilder(ControllerTestContext testContext)
        {
            this.TestContext = testContext;

            this.enabledValidation = true;
            this.aggregatedDependencies = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Gets the ASP.NET MVC controller instance to be tested.
        /// </summary>
        /// <value>Instance of the ASP.NET MVC controller.</value>
        private TController Controller
        {
            get
            {
                this.BuildControllerIfNotExists();
                return this.TestContext.ControllerAs<TController>();
            }
        }

        private ControllerTestContext TestContext
        {
            get
            {
                return this.testContext;
            }
            set
            {
                CommonValidator.CheckForNullReference(value, nameof(TestContext));
                this.testContext = value;
            }
        }

        private MockedHttpContext HttpContext => this.TestContext.MockedHttpContext;

        private HttpRequest HttpRequest => this.HttpContext.Request;

        private IServiceProvider Services => this.HttpContext.RequestServices;

        public IAndControllerBuilder<TController> WithControllerContext(ControllerContext controllerContext)
        {
            this.TestContext.ControllerContext = controllerContext;
            return this;
        }

        /// <summary>
        /// Sets the HTTP context for the current test case. If no request services are set on the provided context, the globally configured ones are initialized.
        /// </summary>
        /// <param name="httpContext">Instance of HttpContext.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithHttpContext(HttpContext httpContext)
        {
            CommonValidator.CheckForNullReference(httpContext, nameof(HttpContext));
            this.TestContext.HttpContext = httpContext; // TODO: add to the HTTP context accessor
            return this;
        }

        /// <summary>
        /// Adds HTTP request to the tested controller.
        /// </summary>
        /// <param name="httpRequest">Instance of HttpRequest.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithHttpRequest(HttpRequest httpRequest)
        {
            CommonValidator.CheckForNullReference(httpRequest, nameof(HttpRequest));
            this.HttpContext.CustomRequest = httpRequest;
            return this;
        }

        /// <summary>
        /// Adds HTTP request to the tested controller by using builder.
        /// </summary>
        /// <param name="httpRequestBuilder">HTTP request builder.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithHttpRequest(Action<IHttpRequestBuilder> httpRequestBuilder)
        {
            var newHttpRequestBuilder = new HttpRequestBuilder();
            httpRequestBuilder(newHttpRequestBuilder);
            newHttpRequestBuilder.ApplyTo(this.HttpRequest);
            return this;
        }

        public IAndControllerBuilder<TController> WithTempData(Action<ITempDataBuilder> tempDataBuilder)
        {
            this.tempDataBuilderAction = tempDataBuilder;
            return this;
        }

        public IAndControllerBuilder<TController> WithSession(Action<ISessionBuilder> sessionBuilder)
        {
            sessionBuilder(new SessionBuilder(this.HttpContext.Session));
            return this;
        }

        public IAndControllerBuilder<TController> WithMemoryCache(Action<IMemoryCacheBuilder> memoryCacheBuilder)
        {
            memoryCacheBuilder(new MemoryCacheBuilder(this.Services));
            return this;
        }

        public IAndControllerBuilder<TController> WithResolvedRouteData()
        {
            return this.WithResolvedRouteData(null);
        }

        public IAndControllerBuilder<TController> WithResolvedRouteData(object additionalRouteValues)
        {
            this.resolveRouteValues = true;
            this.additionalRouteValues = additionalRouteValues;
            return this;
        }

        /// <summary>
        /// Tries to resolve constructor dependency of given type.
        /// </summary>
        /// <typeparam name="TDependency">Type of dependency to resolve.</typeparam>
        /// <param name="dependency">Instance of dependency to inject into constructor.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithResolvedDependencyFor<TDependency>(TDependency dependency)
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

        /// <summary>
        /// Disables ModelState validation for the action call.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithoutValidation()
        {
            this.enabledValidation = false;
            return this;
        }

        /// <summary>
        /// Sets default authenticated user to the built controller with "TestId" identifier and "TestUser" username.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithAuthenticatedUser()
        {
            this.HttpContext.User = ClaimsPrincipalBuilder.CreateDefaultAuthenticated();
            return this;
        }

        /// <summary>
        /// Sets custom authenticated user using the provided user builder.
        /// </summary>
        /// <param name="userBuilder">User builder to create mocked user object.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithAuthenticatedUser(Action<IAndClaimsPrincipalBuilder> userBuilder)
        {
            var newUserBuilder = new ClaimsPrincipalBuilder();
            userBuilder(newUserBuilder);
            this.HttpContext.User = newUserBuilder.GetClaimsPrincipal();
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

        /// <summary>
        /// AndAlso method for better readability when building controller instance.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> AndAlso()
        {
            return this;
        }

        /// <summary>
        /// Used for testing controller attributes.
        /// </summary>
        /// <returns>Controller test builder.</returns>
        public IControllerTestBuilder ShouldHave()
        {
            this.BuildControllerIfNotExists();
            return new ControllerTestBuilder(this.TestContext);
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Type of result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing the action result.</returns>
        public IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, TActionResult>> actionCall)
        {
            var actionInfo = this.GetAndValidateActionResult(actionCall);

            this.TestContext.Apply(actionInfo);

            return new ActionResultTestBuilder<TActionResult>(this.TestContext);
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Asynchronous Task result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing the action result.</returns>
        public IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, Task<TActionResult>>> actionCall)
        {
            var actionInfo = this.GetAndValidateActionResult(actionCall);
            var actionResult = default(TActionResult);

            try
            {
                actionResult = actionInfo.ActionResult.Result;
            }
            catch (AggregateException aggregateException)
            {
                actionInfo.CaughtException = aggregateException;
            }

            this.TestContext.Apply(actionInfo);
            this.TestContext.ActionResult = actionResult;

            return new ActionResultTestBuilder<TActionResult>(this.TestContext);
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing void actions.</returns>
        public IVoidActionResultTestBuilder Calling(Expression<Action<TController>> actionCall)
        {
            var actionName = this.GetAndValidateAction(actionCall);
            Exception caughtException = null;

            try
            {
                actionCall.Compile().Invoke(this.Controller);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            this.TestContext.ActionName = actionName;
            this.TestContext.ActionCall = actionCall;
            this.TestContext.CaughtException = caughtException;

            return new VoidActionResultTestBuilder(this.TestContext);
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing void actions.</returns>
        public IVoidActionResultTestBuilder Calling(Expression<Func<TController, Task>> actionCall)
        {
            var actionInfo = this.GetAndValidateActionResult(actionCall);

            try
            {
                actionInfo.ActionResult.Wait();
            }
            catch (AggregateException aggregateException)
            {
                actionInfo.CaughtException = aggregateException;
            }

            this.TestContext.Apply(actionInfo);

            return new VoidActionResultTestBuilder(this.TestContext);
        }

        /// <summary>
        /// Gets ASP.NET MVC controller instance to be tested.
        /// </summary>
        /// <returns>Instance of the ASP.NET MVC controller.</returns>
        public TController AndProvideTheController()
        {
            return this.Controller;
        }

        /// <summary>
        /// Gets the HTTP request with which the action will be tested.
        /// </summary>
        /// <returns>HttpRequest from the tested controller.</returns>
        public HttpRequest AndProvideTheHttpRequest()
        {
            return this.Controller.Request;
        }

        /// <summary>
        /// Gets the HTTP context with which the action will be tested.
        /// </summary>
        /// <returns>HttpContext from the tested controller.</returns>
        public HttpContext AndProvideTheHttpContext()
        {
            return this.Controller.HttpContext;
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

        private ActionTestContext<TActionResult> GetAndValidateActionResult<TActionResult>(Expression<Func<TController, TActionResult>> actionCall)
        {
            var actionName = this.GetAndValidateAction(actionCall);
            var action = ExpressionParser.GetMethodInfo(actionCall);
            var actionResult = default(TActionResult);
            Exception caughtException = null;

            try
            {
                actionResult = actionCall.Compile().Invoke(this.Controller);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            return new ActionTestContext<TActionResult>(actionName, actionCall, actionResult, caughtException);
        }

        private string GetAndValidateAction(LambdaExpression actionCall)
        {
            this.SetRouteData(actionCall);

            var methodInfo = ExpressionParser.GetMethodInfo(actionCall);

            if (this.enabledValidation)
            {
                this.ValidateModelState(actionCall);
            }

            var controllerActionDescriptorCache = this.Services.GetService<IControllerActionDescriptorCache>();
            if (controllerActionDescriptorCache != null)
            {
                this.Controller.ControllerContext.ActionDescriptor
                    = controllerActionDescriptorCache.TryGetActionDescriptor(methodInfo);
            }

            return methodInfo.Name;
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

        private void SetRouteData(LambdaExpression actionCall)
        {
            if (this.resolveRouteValues)
            {
                this.TestContext.RouteData = RouteExpressionParser.ResolveRouteData(TestApplication.Router, actionCall);
                RouteExpressionParser.ApplyAdditionaRouteValues(
                    this.additionalRouteValues,
                    this.TestContext.RouteData.Values);
            }
        }

        private void ValidateModelState(LambdaExpression actionCall)
        {
            var arguments = ExpressionParser.ResolveMethodArguments(actionCall);
            foreach (var argument in arguments)
            {
                if (argument.Value != null)
                {
                    this.Controller.TryValidateModel(argument.Value);
                }
            }
        }
    }
}
