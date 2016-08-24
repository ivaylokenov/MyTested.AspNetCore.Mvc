namespace MyTested.AspNetCore.Mvc.Builders.Contracts.And
{
    using Base;

    /// <summary>
    /// Contains AndAlso() method allowing additional assertions after the action call tests.
    /// </summary>
    public interface IAndTestBuilderWithInvokedAction : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Method allowing additional assertions after the action call tests.
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithInvokedAction"/>.</returns>
        IBaseTestBuilderWithInvokedAction AndAlso();
    }
}
