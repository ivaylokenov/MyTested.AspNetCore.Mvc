namespace MyTested.Mvc.Builders.ActionResults.View
{
    using System;
    using Contracts.ActionResults.View;
    using Microsoft.AspNet.Mvc;
    using Models;
    using Contracts.Models;

    /// <summary>
    /// Used for testing view results.
    /// </summary>
    public class ViewTestBuilder<TViewResult>
        : BaseResponseModelTestBuilder<TViewResult>, IViewTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public ViewTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TViewResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }
        
        public IModelDetailsTestBuilder<TModel> WithModel<TModel>(TModel model)
        {
            return this.WithResponseModel(model);
        }

        public IModelDetailsTestBuilder<TModel> WithModelOfType<TModel>()
        {
            return this.WithResponseModelOfType<TModel>();
        }
    }
}
