namespace MyTested.AspNetCore.Mvc
{
    using Builders.ActionResults.File;
    using Builders.Contracts.ActionResults.File;
    using Utilities.Extensions;

    /// <summary>
    /// Contains extension methods for <see cref="IPhysicalFileTestBuilder"/>.
    /// </summary>
    public static class PhysicalFileTestBuilderExtensions
    {
        private const string FileName = "file name";

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>
        /// has the same physical file path as the provided one.
        /// </summary>
        /// <param name="physicalFileTestBuilder">
        /// Instance of <see cref="IPhysicalFileTestBuilder"/> type.
        /// </param>
        /// <param name="physicalPath">File physical path as string.</param>
        /// <returns>The same <see cref="IAndPhysicalFileTestBuilder"/>.</returns>
        public static IAndPhysicalFileTestBuilder WithPath(
            this IPhysicalFileTestBuilder physicalFileTestBuilder,
            string physicalPath)
        {
            var actualBuilder = (PhysicalFileTestBuilder)physicalFileTestBuilder;

            var actualPhysicalPath = actualBuilder.ActionResult.FileName;

            if (physicalPath != actualPhysicalPath)
            {
                actualBuilder.ThrowNewFailedValidationException(
                    FileName,
                    $"to be {physicalPath.GetErrorMessageName()}",
                    $"instead received '{actualPhysicalPath}'");
            }

            return actualBuilder;
        }
    }
}
