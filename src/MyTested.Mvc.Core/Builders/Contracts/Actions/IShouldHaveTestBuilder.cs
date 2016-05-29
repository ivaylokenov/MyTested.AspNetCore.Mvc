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
    /// Used for testing the action's additional data - action attributes, HTTP response, view bag and more.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IShouldHaveTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Tests whether the action has no attributes of any type. 
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> NoActionAttributes();

        /// <summary>
        /// Tests whether the action has at least 1 attribute of any type. 
        /// </summary>
        /// <param name="withTotalNumberOf">Optional parameter specifying the exact total number of attributes on the tested action.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> ActionAttributes(int? withTotalNumberOf = null);

        /// <summary>
        /// Tests whether the action has specific attributes. 
        /// </summary>
        /// <param name="attributesTestBuilder">Builder for testing specific attributes on the action.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> ActionAttributes(Action<IActionAttributesTestBuilder> attributesTestBuilder);

        /// <summary>
        /// Tests whether the action has specific <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
        /// </summary>
        /// <typeparam name="TRequestModel">Request model type to be tested for errors.</typeparam>
        /// <param name="modelErrorTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>
        /// errors for the provided model type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> ModelStateFor<TRequestModel>(Action<IModelErrorTestBuilder<TRequestModel>> modelErrorTestBuilder);

        /// <summary>
        /// Tests whether the action has valid <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> with no errors.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> ValidModelState();

        /// <summary>
        /// Tests whether the action has invalid <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
        /// </summary>
        /// <param name="withNumberOfErrors">Expected number of <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
        /// If default null is provided, the test builder checks only if any errors are found.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> InvalidModelState(int? withNumberOfErrors = null);

        /// <summary>
        /// Tests whether the action sets entries in the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/>.
        /// </summary>
        /// <param name="withNumberOfEntries">Expected number of <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entries.
        /// If default null is provided, the test builder checks only if any entries are found.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> MemoryCache(int? withNumberOfEntries = null);

        /// <summary>
        /// Tests whether the action sets specific <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> entries.
        /// </summary>
        /// <param name="memoryCacheTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> entries.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> MemoryCache(Action<IMemoryCacheTestBuilder> memoryCacheTestBuilder);

        /// <summary>
        /// Tests whether the action does not set any <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> entries.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> NoMemoryCache();
        
        /// <summary>
        /// Tests whether the action sets entries in the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
        /// </summary>
        /// <param name="withNumberOfEntries">Expected number of <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> entries.
        /// If default null is provided, the test builder checks only if any entries are found.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> TempData(int? withNumberOfEntries = null);

        /// <summary>
        /// Tests whether the action sets specific entries in the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/>.
        /// </summary>
        /// <param name="tempDataTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> entries.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> TempData(Action<ITempDataTestBuilder> tempDataTestBuilder);

        /// <summary>
        /// Tests whether the action does not set any <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary"/> entries.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> NoTempData();

        /// <summary>
        /// Tests whether the action sets entries in the <see cref="Microsoft.AspNetCore.Mvc.Controller.ViewBag"/>.
        /// </summary>
        /// <param name="withNumberOfEntries">Expected number of <see cref="Microsoft.AspNetCore.Mvc.Controller.ViewBag"/> entries.
        /// If default null is provided, the test builder checks only if any entries are found.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> ViewBag(int? withNumberOfEntries = null);

        /// <summary>
        /// Tests whether the action sets specific entries in the <see cref="Microsoft.AspNetCore.Mvc.Controller.ViewBag"/>.
        /// </summary>
        /// <param name="viewBagTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Mvc.Controller.ViewBag"/> entries.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> ViewBag(Action<IViewBagTestBuilder> viewBagTestBuilder);

        /// <summary>
        /// Tests whether the action does not set any <see cref="Microsoft.AspNetCore.Mvc.Controller.ViewBag"/> entries.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> NoViewBag();

        /// <summary>
        /// Tests whether the action sets entries in the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/>.
        /// </summary>
        /// <param name="withNumberOfEntries">Expected number of <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> entries.
        /// If default null is provided, the test builder checks only if any entries are found.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> ViewData(int? withNumberOfEntries = null);

        /// <summary>
        /// Tests whether the action sets specific entries in the <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/>.
        /// </summary>
        /// <param name="viewDataTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> entries.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> ViewData(Action<IViewDataTestBuilder> viewDataTestBuilder);

        /// <summary>
        /// Tests whether the action does not set any <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/> entries.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> NoViewData();

        /// <summary>
        /// Tests whether the action applies additional features to the <see cref="Microsoft.AspNetCore.Http.HttpResponse"/>.
        /// </summary>
        /// <param name="httpResponseTestBuilder">Builder for testing the <see cref="Microsoft.AspNetCore.Http.HttpResponse"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        IAndTestBuilder<TActionResult> HttpResponse(Action<IHttpResponseTestBuilder> httpResponseTestBuilder);
    }
}
