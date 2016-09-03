namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ViewComponentResults
{
    /// <summary>
    /// Contains AndAlso() method allowing additional assertions after the view component view result tests.
    /// </summary>
    public interface IAndViewTestBuilder : IViewTestBuilder
    {
        /// <summary>
        /// Method allowing additional assertions after the view component view result tests.
        /// </summary>
        /// <returns>Test builder of <see cref="IViewTestBuilder"/>.</returns>
        IViewTestBuilder AndAlso();
    }
}
