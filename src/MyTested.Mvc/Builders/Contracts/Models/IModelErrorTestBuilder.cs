namespace MyTested.Mvc.Builders.Contracts.Models
{
    using Base;

    /// <summary>
    /// Used for testing model errors.
    /// </summary>
    public interface IModelErrorTestBuilder : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Tests whether tested action's model state is valid.
        /// </summary>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithInvokedAction ContainingNoErrors();
    }
}
