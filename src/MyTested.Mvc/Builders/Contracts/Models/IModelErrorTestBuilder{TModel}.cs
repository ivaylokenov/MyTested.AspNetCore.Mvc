namespace MyTested.Mvc.Builders.Contracts.Models
{
    using System;
    using System.Linq.Expressions;
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IModelErrorTestBuilder<TModel> : IModelErrorTestBuilder, IBaseTestBuilderWithModel<TModel>
    {
        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Test builder of <see cref="IModelErrorDetailsTestBuilder{TModel}"/> type.</returns>
        IModelErrorDetailsTestBuilder<TModel> ContainingError(string errorKey);

        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> contains error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for errors.</typeparam>
        /// <param name="memberWithError">Member expression for the tested member.</param>
        /// <returns>Test builder of <see cref="IModelErrorDetailsTestBuilder{TModel}"/> type.</returns>
        IModelErrorDetailsTestBuilder<TModel> ContainingErrorFor<TMember>(Expression<Func<TModel, TMember>> memberWithError);

        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> contains no error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>The same <see cref="IAndModelErrorTestBuilder{TModel}"/>.</returns>
        IAndModelErrorTestBuilder<TModel> ContainingNoErrorFor<TMember>(
            Expression<Func<TModel, TMember>> memberWithNoError);
    }
}
