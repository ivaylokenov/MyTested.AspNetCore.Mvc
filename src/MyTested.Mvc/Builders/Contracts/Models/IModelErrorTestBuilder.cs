namespace MyTested.Mvc.Builders.Contracts.Models
{
    using Base;

    /// <summary>
    /// Used for testing model errors.
    /// </summary>
    public interface IModelErrorTestBuilder : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Tests whether tested action's model state is valid.
        /// </summary>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException ContainingNoModelStateErrors();
    }
}
