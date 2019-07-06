namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.IO;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>, <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>
    /// and <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/> extension
    /// methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderFileResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>
        /// with the same file contents and content type as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileContents">Expected file contents.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            byte[] fileContents,
            string contentType)
            => shouldReturnTestBuilder
                .File(fileContents, contentType, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>
        /// with the same file contents, content type, and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileContents">Expected file contents.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            byte[] fileContents,
            string contentType,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .File(result => result
                    .WithContents(fileContents)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithEnabledRangeProcessing(enableRangeProcessing));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>
        /// with the same file contents, content type, and download name as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileContents">Expected file contents.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            byte[] fileContents,
            string contentType,
            string fileDownloadName)
            => shouldReturnTestBuilder
                .File(fileContents, contentType, fileDownloadName, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>
        /// with the same file contents, content type, download name, and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileContents">Expected file contents.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            byte[] fileContents,
            string contentType,
            string fileDownloadName,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .File(result => result
                    .WithContents(fileContents)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithDownloadName(fileDownloadName)
                    .WithEnabledRangeProcessing(enableRangeProcessing));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>
        /// with the same file contents, content type, last modified value, and entity tag as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileContents">Expected file contents.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            byte[] fileContents,
            string contentType,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag)
            => shouldReturnTestBuilder
                .File(fileContents, contentType, lastModified, entityTag, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>
        /// with the same file contents, content type, last modified value, entity tag,
        /// and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileContents">Expected file contents.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            byte[] fileContents,
            string contentType,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .File(result => result
                    .WithContents(fileContents)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithLastModified(lastModified)
                    .WithEntityTag(entityTag)
                    .WithEnabledRangeProcessing(enableRangeProcessing));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>
        /// with the same file contents, content type, download name,
        /// last modified value, and entity tag as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileContents">Expected file contents.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            byte[] fileContents,
            string contentType,
            string fileDownloadName,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag)
            => shouldReturnTestBuilder
                .File(fileContents, contentType, fileDownloadName, lastModified, entityTag, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileContentResult"/>
        /// with the same file contents, content type, download name, last modified value, entity tag,
        /// and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileContents">Expected file contents.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            byte[] fileContents,
            string contentType,
            string fileDownloadName,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .File(result => result
                    .WithContents(fileContents)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithDownloadName(fileDownloadName)
                    .WithLastModified(lastModified)
                    .WithEntityTag(entityTag)
                    .WithEnabledRangeProcessing(enableRangeProcessing));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>
        /// with the same file stream and content type as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileStream">Expected file stream.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder, 
            Stream fileStream, 
            string contentType)
            => shouldReturnTestBuilder
                .File(fileStream, contentType, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>
        /// with the same file stream, content type, and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileStream">Expected file stream.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            Stream fileStream,
            string contentType,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .File(result => result
                    .WithStream(fileStream)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithEnabledRangeProcessing(enableRangeProcessing));
        
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>
        /// with the same file stream, content type, and download name as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileStream">Expected file stream.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            Stream fileStream,
            string contentType,
            string fileDownloadName)
            => shouldReturnTestBuilder
                .File(fileStream, contentType, fileDownloadName, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>
        /// with the same file stream, content type, download name, and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileStream">Expected file stream.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            Stream fileStream,
            string contentType,
            string fileDownloadName,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .File(result => result
                    .WithStream(fileStream)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithDownloadName(fileDownloadName)
                    .WithEnabledRangeProcessing(enableRangeProcessing));
        
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>
        /// with the same file stream, content type, last modified value, and entity tag as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileStream">Expected file stream.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            Stream fileStream,
            string contentType,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag)
            => shouldReturnTestBuilder
                .File(fileStream, contentType, lastModified, entityTag, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>
        /// with the same file stream, content type, last modified value, entity tag,
        /// and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileStream">Expected file stream.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            Stream fileStream,
            string contentType,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .File(result => result
                    .WithStream(fileStream)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithLastModified(lastModified)
                    .WithEntityTag(entityTag)
                    .WithEnabledRangeProcessing(enableRangeProcessing));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>
        /// with the same file stream, content type, download name,
        /// last modified value, and entity tag as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileStream">Expected file stream.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            Stream fileStream,
            string contentType,
            string fileDownloadName,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag)
            => shouldReturnTestBuilder
                .File(fileStream, contentType, fileDownloadName, lastModified, entityTag, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.FileStreamResult"/>
        /// with the same file stream, content type, download name, last modified value,
        /// entity tag, and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="fileStream">Expected file stream.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            Stream fileStream,
            string contentType,
            string fileDownloadName,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .File(result => result
                    .WithStream(fileStream)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithDownloadName(fileDownloadName)
                    .WithLastModified(lastModified)
                    .WithEntityTag(entityTag)
                    .WithEnabledRangeProcessing(enableRangeProcessing));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>
        /// with the same file virtual path and content type as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="virtualPath">Expected file virtual path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string virtualPath,
            string contentType)
            => shouldReturnTestBuilder
                .File(virtualPath, contentType, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>
        /// with the same file virtual path, content type, and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="virtualPath">Expected file virtual path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string virtualPath,
            string contentType,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .File(result => result
                    .WithName(virtualPath)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithEnabledRangeProcessing(enableRangeProcessing));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>
        /// with the same file virtual path, content type, and download name as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="virtualPath">Expected file virtual path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string virtualPath,
            string contentType,
            string fileDownloadName)
            => shouldReturnTestBuilder
                .File(virtualPath, contentType, fileDownloadName, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>
        /// with the same file virtual path, content type, download name, and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="virtualPath">Expected file virtual path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string virtualPath,
            string contentType,
            string fileDownloadName,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .File(result => result
                    .WithName(virtualPath)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithDownloadName(fileDownloadName)
                    .WithEnabledRangeProcessing(enableRangeProcessing));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>
        /// with the same file virtual path, content type, last modified value, and entity tag as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="virtualPath">Expected file virtual path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string virtualPath,
            string contentType,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag)
            => shouldReturnTestBuilder
                .File(virtualPath, contentType, lastModified, entityTag, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>
        /// with the same file virtual path, content type, last modified value, entity tag,
        /// and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="virtualPath">Expected file virtual path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string virtualPath,
            string contentType,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .File(result => result
                    .WithName(virtualPath)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithLastModified(lastModified)
                    .WithEntityTag(entityTag)
                    .WithEnabledRangeProcessing(enableRangeProcessing));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>
        /// with the same file virtual path, content type, download name,
        /// last modified value, and entity tag as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="virtualPath">Expected file virtual path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string virtualPath,
            string contentType,
            string fileDownloadName,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag)
            => shouldReturnTestBuilder
                .File(virtualPath, contentType, fileDownloadName, lastModified, entityTag, false);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.VirtualFileResult"/>
        /// with the same file virtual path, content type, download name, last modified value,
        /// entity tag, and end range processing value as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="virtualPath">Expected file virtual path.</param>
        /// <param name="contentType">Expected content type.</param>
        /// <param name="fileDownloadName">Expected file download name.</param>
        /// <param name="lastModified">Expected last modified value.</param>
        /// <param name="entityTag">Expected entity tag.</param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder File<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            string virtualPath,
            string contentType,
            string fileDownloadName,
            DateTimeOffset? lastModified,
            EntityTagHeaderValue entityTag,
            bool enableRangeProcessing)
            => shouldReturnTestBuilder
                .File(result => result
                    .WithName(virtualPath)
                    .WithContentType(MediaTypeHeaderValue.Parse(contentType))
                    .WithDownloadName(fileDownloadName)
                    .WithLastModified(lastModified)
                    .WithEntityTag(entityTag)
                    .WithEnabledRangeProcessing(enableRangeProcessing));
    }
}
