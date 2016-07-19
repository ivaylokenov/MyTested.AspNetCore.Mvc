namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    using ShouldPassFor;

    /// <summary>
    /// Base class for all test builders with component.
    /// </summary>
    public interface IBaseTestBuilderWithComponent : IBaseTestBuilder
    {
        /// <summary>
        /// Allows additional testing on various components.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldPassForTestBuilderWithComponent{TObject}"/> type.</returns>
        new IShouldPassForTestBuilderWithComponent<object> ShouldPassFor();
    }
}
