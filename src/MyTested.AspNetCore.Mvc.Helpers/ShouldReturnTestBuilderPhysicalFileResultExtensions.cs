namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/> extension
    /// methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderPhysicalFileResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>
        /// with the same physical file path and content type as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="physicalPath">Expected physical file path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder PhysicalFile<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string physicalPath,
            string contentType)
            => shouldReturnTestBuilder
                .PhysicalFile(physicalPath, contentType, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>
        /// with the same physical file path, content type, and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="physicalPath">Expected physical file path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder PhysicalFile<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string physicalPath,
            string contentType,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .PhysicalFile(result => result
                    .WithPath(physicalPath)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithEnabledRangeProcessing(enableRangeProcessing));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>
        /// with the same physical file path, content type, and download name as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="physicalPath">Expected physical file path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder PhysicalFile<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string physicalPath,
            string contentType,
            string fileDownloadName)
            => shouldReturnTestBuilder
                .PhysicalFile(physicalPath, contentType, fileDownloadName, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>
        /// with the same physical file path, content type, download name, and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="physicalPath">Expected physical file path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder PhysicalFile<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string physicalPath,
            string contentType,
            string fileDownloadName,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .PhysicalFile(result => result
                    .WithPath(physicalPath)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithDownloadName(fileDownloadName)
                    .WithEnabledRangeProcessing(enableRangeProcessing));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>
        /// with the same physical file path, content type, last modified value, and entity tag as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="physicalPath">Expected physical file path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder PhysicalFile<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string physicalPath,
            string contentType,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag)
            => shouldReturnTestBuilder
                .PhysicalFile(physicalPath, contentType, lastModified, entityTag, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>
        /// with the same physical file path, content type, last modified value, entity tag,
        /// and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="physicalPath">Expected physical file path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder PhysicalFile<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string physicalPath,
            string contentType,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .PhysicalFile(result => result
                    .WithPath(physicalPath)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithLastModified(lastModified)
                    .WithEntityTag(entityTag)
                    .WithEnabledRangeProcessing(enableRangeProcessing));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>
        /// with the same physical file path, content type, download name,
        /// last modified value, and entity tag as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="physicalPath">Expected physical file path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder PhysicalFile<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string physicalPath,
            string contentType,
            string fileDownloadName,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag)
            => shouldReturnTestBuilder
                .PhysicalFile(physicalPath, contentType, fileDownloadName, lastModified, entityTag, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.PhysicalFileResult"/>
        /// with the same physical file path, content type, download name, last modified value,
        /// entity tag, and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="physicalPath">Expected physical file path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder PhysicalFile<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string physicalPath,
            string contentType,
            string fileDownloadName,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .PhysicalFile(result => result
                    .WithPath(physicalPath)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithDownloadName(fileDownloadName)
                    .WithLastModified(lastModified)
                    .WithEntityTag(entityTag)
                    .WithEnabledRangeProcessing(enableRangeProcessing));
    }
}
