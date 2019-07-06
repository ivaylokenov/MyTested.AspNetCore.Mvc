namespace MyTested.AspNetCore.Mvc.Internal.Contracts.ActionResults
{
    using Builders.Contracts.Base;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using TestContexts;

    public interface IBaseTestBuilderWithErrorResultInternal<TErrorResultTestBuilder>
        : IBaseTestBuilderWithOutputResultInternal<TErrorResultTestBuilder>
        where TErrorResultTestBuilder : IBaseTestBuilderWithActionResult
    {
        new ControllerTestContext TestContext { get; }

        object GetObjectResultValue();

        string GetErrorMessage();

        void ValidateErrorMessage(string expectedMessage, string actualMessage);

        ModelStateDictionary GetModelStateFromSerializableError(object error = null);
    }
}
