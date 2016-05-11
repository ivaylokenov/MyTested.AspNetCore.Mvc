namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing <see cref="EmptyResult"/>.
    /// </summary>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IBaseTestBuilderWithActionResult<TActionResult> Empty()
        {
            this.ValidateActionReturnType<EmptyResult>();
            return this.NewAndProvideTestBuilder();
        }
    }
}
