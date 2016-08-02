namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
    /// </summary>
    public interface IModelErrorTestBuilder : IBaseTestBuilderWithInvokedAction
    {
    }
}
