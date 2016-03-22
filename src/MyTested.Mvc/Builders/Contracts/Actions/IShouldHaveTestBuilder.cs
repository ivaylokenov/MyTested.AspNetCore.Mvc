namespace MyTested.Mvc.Builders.Contracts.Actions
{
    using System;
    using And;
    using Attributes;
    using Base;
    using Data;
    using Http;
    using Models;

    /// <summary>
    /// Used for testing action attributes and model state.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public interface IShouldHaveTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Checks whether the tested action has no attributes of any type. 
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        IAndTestBuilder<TActionResult> NoActionAttributes();

        /// <summary>
        /// Checks whether the tested action has at least 1 attribute of any type. 
        /// </summary>
        /// <param name="withTotalNumberOf">Optional parameter specifying the exact total number of attributes on the tested action.</param>
        /// <returns>Test builder with AndAlso method.</returns>
        IAndTestBuilder<TActionResult> ActionAttributes(int? withTotalNumberOf = null);

        /// <summary>
        /// Checks whether the tested action has at specific attributes. 
        /// </summary>
        /// <param name="attributesTestBuilder">Builder for testing specific attributes on the action.</param>
        /// <returns>Test builder with AndAlso method.</returns>
        IAndTestBuilder<TActionResult> ActionAttributes(Action<IActionAttributesTestBuilder> attributesTestBuilder);

        /// <summary>
        /// Provides way to continue test case with specific model state errors.
        /// </summary>
        /// <typeparam name="TRequestModel">Request model type to be tested for errors.</typeparam>
        /// <returns>Test builder with AndAlso method.</returns>
        IAndTestBuilder<TActionResult> ModelStateFor<TRequestModel>(Action<IModelErrorTestBuilder<TRequestModel>> modelErrorTestBuilder);

        /// <summary>
        /// Checks whether the tested action's provided model state is valid.
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        IAndTestBuilder<TActionResult> ValidModelState();

        /// <summary>
        /// Checks whether the tested action's provided model state is not valid.
        /// </summary>
        /// <param name="withNumberOfErrors">Expected number of errors. If default null is provided, the test builder checks only if any errors are found.</param>
        /// <returns>Test builder with AndAlso method.</returns>
        IAndTestBuilder<TActionResult> InvalidModelState(int? withNumberOfErrors = null);
        
        IAndTestBuilder<TActionResult> MemoryCache(int? withNumberOfEntries = null);

        IAndTestBuilder<TActionResult> MemoryCache(Action<IMemoryCacheTestBuilder> memoryCacheTestBuilder);

        IAndTestBuilder<TActionResult> NoMemoryCache();

        IAndTestBuilder<TActionResult> Session(int? withNumberOfEntries = null);

        IAndTestBuilder<TActionResult> Session(Action<ISessionTestBuilder> sessionTestBuilder);

        IAndTestBuilder<TActionResult> NoSession();

        IAndTestBuilder<TActionResult> TempData(int? withNumberOfEntries = null);

        IAndTestBuilder<TActionResult> TempData(Action<ITempDataTestBuilder> tempDataTestBuilder);

        IAndTestBuilder<TActionResult> NoTempData();
        
        IAndTestBuilder<TActionResult> ViewBag(int? withNumberOfEntries = null);

        IAndTestBuilder<TActionResult> ViewBag(Action<IViewBagTestBuilder> viewDataTestBuilder);

        IAndTestBuilder<TActionResult> NoViewBag();

        IAndTestBuilder<TActionResult> ViewData(int? withNumberOfEntries = null);

        IAndTestBuilder<TActionResult> ViewData(Action<IViewDataTestBuilder> viewDataTestBuilder);

        IAndTestBuilder<TActionResult> NoViewData();

        /// <summary>
        /// Checks whether the tested action applies additional features to the HTTP response.
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        IAndTestBuilder<TActionResult> HttpResponse(Action<IHttpResponseTestBuilder> httpResponseTestBuilder);
    }
}
