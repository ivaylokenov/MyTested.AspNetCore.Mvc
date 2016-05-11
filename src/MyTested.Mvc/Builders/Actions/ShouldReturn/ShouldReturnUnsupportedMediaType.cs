namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing <see cref="UnsupportedMediaTypeResult"/>.
    /// </summary>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IBaseTestBuilderWithActionResult<TActionResult> UnsupportedMediaType()
        {
            this.ValidateActionReturnType<UnsupportedMediaTypeResult>();
            return this.NewAndProvideTestBuilder();
        }
    }
}
