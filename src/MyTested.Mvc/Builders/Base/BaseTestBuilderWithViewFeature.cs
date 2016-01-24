namespace MyTested.Mvc.Builders.Base
{
    using System;
    using Contracts.Base;
    using Contracts.Models;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Base class for all test builders with view features.
    /// </summary>
    /// <typeparam name="TViewResult">Type of view result - ViewResult, PartialViewResult or ViewComponentResult.</typeparam>
    public abstract class BaseTestBuilderWithViewFeature<TViewResult>
        : BaseTestBuilderWithResponseModel<TViewResult>, IBaseTestBuilderWithViewFeature
        where TViewResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithViewFeature{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="viewResult">Result from the tested action.</param>
        protected BaseTestBuilderWithViewFeature(
            Controller controller,
            string actionName,
            Exception caughtException,
            TViewResult viewResult)
            : base(controller, actionName, caughtException, viewResult)
        {
        }
        
        /// <summary>
        /// Tests whether view result contains deeply equal model object as the provided one.
        /// </summary>
        /// <typeparam name="TModel">Type of model object.</typeparam>
        /// <param name="model">Model object.</param>
        /// <returns>Model details test builder.</returns>
        public IModelDetailsTestBuilder<TModel> WithModel<TModel>(TModel model)
        {
            return this.WithResponseModel(model);
        }

        /// <summary>
        /// Tests whether view result contains model object of the provided type.
        /// </summary>
        /// <typeparam name="TModel">Type of model object.</typeparam>
        /// <returns>Model details test builder.</returns>
        public IModelDetailsTestBuilder<TModel> WithModelOfType<TModel>()
        {
            return this.WithResponseModelOfType<TModel>();
        }

        public new ActionResult AndProvideTheActionResult()
        {
            return this.ActionResult;
        }
    }
}
