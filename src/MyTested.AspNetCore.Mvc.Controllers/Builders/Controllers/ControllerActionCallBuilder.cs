namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Actions;
    using Contracts.Actions;

    /// <content>
    /// Used for building the controller which will be tested.
    /// </content>
    public partial class ControllerBuilder<TController>
    {
        /// <inheritdoc />
        public IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, TActionResult>> actionCall)
        {
            this.Invoke(actionCall);
            return new ActionResultTestBuilder<TActionResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, Task<TActionResult>>> actionCall)
        {
            this.Invoke(actionCall);
            return new ActionResultTestBuilder<TActionResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IVoidActionResultTestBuilder Calling(Expression<Action<TController>> actionCall)
        {
            this.Invoke(actionCall);
            return new VoidActionResultTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IVoidActionResultTestBuilder Calling(Expression<Func<TController, Task>> actionCall)
        {
            this.Invoke(actionCall);
            return new VoidActionResultTestBuilder(this.TestContext);
        }
    }
}
