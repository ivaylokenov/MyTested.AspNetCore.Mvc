namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="UnauthorizedResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IBaseTestBuilderWithActionResult<TActionResult> Unauthorized()
        {
            this.ValidateActionReturnType<UnauthorizedResult>();
            return this.NewAndProvideTestBuilder();
        }
    }
}
