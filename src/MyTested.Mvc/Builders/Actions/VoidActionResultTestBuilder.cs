namespace MyTested.Mvc.Builders.Actions
{
    using Base;
    using Contracts.Actions;
    using Contracts.Base;
    using Utilities.Validators;
    using Microsoft.AspNet.Mvc;
    using System;
    using System.Collections.Generic;
    using Internal;
    using ShouldHave;

    /// <summary>
    /// Used for testing void actions.
    /// </summary>
    public class VoidActionResultTestBuilder : BaseTestBuilderWithCaughtException, IVoidActionResultTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoidActionResultTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionAttributes">Collected action attributes from the method call.</param>
        public VoidActionResultTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            IEnumerable<object> actionAttributes)
            : base(controller, actionName, caughtException, actionAttributes)
        {
        }

        /// <summary>
        /// Tests whether action result is void.
        /// </summary>
        /// <returns>Base test builder.</returns>
        public IBaseTestBuilderWithCaughtException ShouldReturnEmpty()
        {
            CommonValidator.CheckForException(this.CaughtException);
            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Used for testing action attributes and model state.
        /// </summary>
        /// <returns>Should have test builder.</returns>
        public IShouldHaveTestBuilder<VoidActionResult> ShouldHave()
        {
            return new ShouldHaveTestBuilder<VoidActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                VoidActionResult.Create(),
                this.ActionLevelAttributes);
        }

        /// <summary>
        /// Used for testing whether action throws exception.
        /// </summary>
        /// <returns>Should throw test builder.</returns>
        public IShouldThrowTestBuilder ShouldThrow()
        {
            return new ShouldThrowTestBuilder(this.Controller, this.ActionName, this.CaughtException);
        }
    }
}
