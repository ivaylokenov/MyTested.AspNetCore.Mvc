namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing <see cref="NoContentResult"/>.
    /// </summary>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IBaseTestBuilderWithActionResult<TActionResult> NoContent()
        {
            this.ValidateActionReturnType<NoContentResult>();
            return this.NewAndProvideTestBuilder();
        }
    }
}
