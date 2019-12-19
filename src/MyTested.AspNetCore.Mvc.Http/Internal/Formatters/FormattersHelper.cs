﻿namespace MyTested.AspNetCore.Mvc.Internal.Formatters
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Services;
    using Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Primitives;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    public static class FormattersHelper
    {
        private static ConcurrentDictionary<string, IInputFormatter> inputFormatters
            = new ConcurrentDictionary<string, IInputFormatter>();

        private static ConcurrentDictionary<string, IOutputFormatter> outputFormatters
            = new ConcurrentDictionary<string, IOutputFormatter>();

        public static TModel ReadFromStream<TModel>(Stream stream, string contentType, Encoding encoding)
        {
            stream.Restart();

            // Formatters do not support non-HTTP context processing.
            var httpContext = new HttpContextMock();
            httpContext.Request.Body = stream;
            httpContext.Request.ContentType = contentType;

            var typeOfModel = typeof(TModel);
            var modelMetadataProvider = TestServiceProvider.GetRequiredService<IModelMetadataProvider>();
            var modelMetadata = modelMetadataProvider.GetMetadataForType(typeOfModel);

            var inputFormatterContext = new InputFormatterContext(
                httpContext,
                string.Empty,
                new ModelStateDictionary(),
                modelMetadata,
                (str, enc) => new StreamReader(httpContext.Request.Body, encoding));

            var inputFormatter = inputFormatters.GetOrAdd(contentType, _ =>
            {
                var mvcOptions = TestServiceProvider.GetRequiredService<IOptions<MvcOptions>>();
                var formatter = mvcOptions.Value?.InputFormatters?.FirstOrDefault(f => f.CanRead(inputFormatterContext));
                ServiceValidator.ValidateFormatterExists(formatter, contentType);

                return formatter;
            });
            
            var result = AsyncHelper.RunSync(() => inputFormatter.ReadAsync(inputFormatterContext)).Model;

            try
            {
                return (TModel)result;
            }
            catch (Exception)
            {
                throw new InvalidDataException($"Expected stream content to be formatted to {typeOfModel.ToFriendlyTypeName()} when using '{contentType}', but instead received {result.GetName()}.");
            }
        }

        public static Stream WriteToStream<TBody>(TBody value, string contentType, Encoding encoding)
        {
            // Formatters do not support non-HTTP context processing.
            var httpContext = new HttpContextMock();
            httpContext.Response.Body = new MemoryStream();

            var outputFormatterCanWriteContext = new OutputFormatterWriteContext(
                httpContext,
                (str, enc) => new StreamWriter(httpContext.Response.Body, encoding),
                value?.GetType(),
                value)
            {
                ContentType = new StringSegment(contentType)
            };

            var outputFormatter = outputFormatters.GetOrAdd(contentType, _ =>
            {
                var mvcOptions = TestServiceProvider.GetRequiredService<IOptions<MvcOptions>>();

                var formatter = mvcOptions.Value?.OutputFormatters?.FirstOrDefault(f => f.CanWriteResult(outputFormatterCanWriteContext));
                ServiceValidator.ValidateFormatterExists(formatter, contentType);

                return formatter;
            });
            
            AsyncHelper.RunSync(() => outputFormatter.WriteAsync(outputFormatterCanWriteContext));

            // Copy memory stream because formatters close the original one.
            return new MemoryStream(((MemoryStream)httpContext.Response.Body).ToArray());
        }

        public static Stream WriteAsStringToStream<TBody>(TBody value, string contentType, Encoding encoding)
        {
            var stream = WriteToStream(value, contentType, encoding);

            using (var streamReader = new StreamReader(stream))
            {
                var streamAsString = streamReader.ReadToEnd();

                return WriteToStream(streamAsString, ContentType.TextPlain, encoding);
            }
        }
    }
}
