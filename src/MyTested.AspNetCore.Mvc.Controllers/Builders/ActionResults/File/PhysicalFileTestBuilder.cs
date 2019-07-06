namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.File
{
    using Contracts.ActionResults.File;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="PhysicalFileResult"/>.
    /// </summary>
    public class PhysicalFileTestBuilder
        : BaseTestBuilderWithFileResult<PhysicalFileResult, IAndPhysicalFileTestBuilder>, 
        IAndPhysicalFileTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicalFileTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public PhysicalFileTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the physical file result test builder.
        /// </summary>
        /// <value>Test builder of <see cref="IAndPhysicalFileTestBuilder"/> type.</value>
        public override IAndPhysicalFileTestBuilder ResultTestBuilder => this;
        
        /// <inheritdoc />
        public IPhysicalFileTestBuilder AndAlso() => this;
    }
}
