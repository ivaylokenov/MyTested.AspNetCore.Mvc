namespace MyTested.Mvc.Builders.Base
{
    using System;
    using Contracts.Base;
    using Contracts.Models;
    using Microsoft.AspNet.Mvc;
    using Models;

    public abstract class BaseTestBuilderWithViewFeature<TViewResult>
        : BaseResponseModelTestBuilder<TViewResult>, IBaseTestBuilderWithViewFeature
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
