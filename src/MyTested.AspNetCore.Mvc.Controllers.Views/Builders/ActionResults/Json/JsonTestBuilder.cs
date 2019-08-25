namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Json
{
    using Builders.Base;
    using Contracts.ActionResults.Json;
    using Exceptions;
    using Internal;
    using Internal.Contracts.ActionResults;
    using Internal.Services;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    /// <summary>
    /// Used for testing <see cref="JsonResult"/>.
    /// </summary>
    public class JsonTestBuilder 
        : BaseTestBuilderWithResponseModel<JsonResult>, 
        IAndJsonTestBuilder,
        IBaseTestBuilderWithStatusCodeResultInternal<IAndJsonTestBuilder>,
        IBaseTestBuilderWithContentTypeResultInternal<IAndJsonTestBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public JsonTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the JSON result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndJsonTestBuilder"/> type.</value>
        public IAndJsonTestBuilder ResultTestBuilder => this;

        /// <inheritdoc />
        public IJsonTestBuilder AndAlso() => this;

        public override object GetActualModel() => this.GetJsonResult()?.Value;
        
        public JsonResult GetJsonResult() => this.TestContext.MethodResult as JsonResult;

        public JsonSerializerSettings GetServiceDefaultSerializerSettings()
            => TestServiceProvider.GetService<IOptions<MvcNewtonsoftJsonOptions>>()?.Value?.SerializerSettings;

        /// <summary>
        /// Throws new <see cref="JsonResultAssertionException"/> for the provided property name, expected value and actual value.
        /// </summary>
        /// <param name="propertyName">Property name on which the testing failed.</param>
        /// <param name="expectedValue">Expected value of the tested property.</param>
        /// <param name="actualValue">Actual value of the tested property.</param>
        public override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => throw new JsonResultAssertionException(string.Format(
                ExceptionMessages.ActionResultFormat,
                this.TestContext.ExceptionMessagePrefix,
                "JSON",
                propertyName,
                expectedValue,
                actualValue));
    }
}
