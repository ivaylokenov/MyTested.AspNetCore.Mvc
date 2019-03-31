namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using Contracts.Attributes;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing <see cref="RequestFormLimitsAttribute"/>.
    /// </summary>
    public class RequestFormLimitsAttributeTestBuilder : BaseAttributeTestBuilderWithOrder<RequestFormLimitsAttribute>,
        IAndRequestFormLimitsAttributeTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestFormLimitsAttributeTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public RequestFormLimitsAttributeTestBuilder(
            ComponentTestContext testContext,
            Action<string, string> failedValidationAction)
            : base(testContext, nameof(RequestFormLimitsAttribute), failedValidationAction)
            => this.Attribute = new RequestFormLimitsAttribute();

        /// <inheritdoc />
        public IAndRequestFormLimitsAttributeTestBuilder WithBufferBody(bool bufferBody)
        {
            this.Attribute.BufferBody = bufferBody;
            this.Validations.Add((expected, actual) =>
            {
                var expectedBufferBody = expected.BufferBody;
                var actualBufferBody = actual.BufferBody;

                if (expectedBufferBody != actualBufferBody)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}buffer body value of {expectedBufferBody.GetErrorMessageName()}",
                        $"in fact found {actualBufferBody.GetErrorMessageName()}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndRequestFormLimitsAttributeTestBuilder WithMemoryBufferThreshold(int memoryBufferThreshold)
        {
            this.Attribute.MemoryBufferThreshold = memoryBufferThreshold;
            this.Validations.Add((expected, actual) =>
            {
                var expectedMemoryBufferThreshold = expected.MemoryBufferThreshold;
                var actualMemoryBufferThreshold = actual.MemoryBufferThreshold;

                if (expectedMemoryBufferThreshold != actualMemoryBufferThreshold)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}memory buffer threshold value of {expectedMemoryBufferThreshold}",
                        $"in fact found {actualMemoryBufferThreshold}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndRequestFormLimitsAttributeTestBuilder WithBufferBodyLengthLimit(long bufferBodyLengthLimit)
        {
            this.Attribute.BufferBodyLengthLimit = bufferBodyLengthLimit;
            this.Validations.Add((expected, actual) =>
            {
                var expectedBufferBodyLengthLimit = expected.BufferBodyLengthLimit;
                var actualBufferBodyLengthLimit = actual.BufferBodyLengthLimit;

                if (expectedBufferBodyLengthLimit != actualBufferBodyLengthLimit)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}buffer body length limit of {expectedBufferBodyLengthLimit}",
                        $"in fact found {actualBufferBodyLengthLimit}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndRequestFormLimitsAttributeTestBuilder WithValueCountLimit(int valueCountLimit)
        {
            this.Attribute.ValueCountLimit = valueCountLimit;
            this.Validations.Add((expected, actual) =>
            {
                var expectedValueCountLimit = expected.ValueCountLimit;
                var actualValueCountLimit = actual.ValueCountLimit;

                if (expectedValueCountLimit != actualValueCountLimit)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}value count limit of {expectedValueCountLimit}",
                        $"in fact found {actualValueCountLimit}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndRequestFormLimitsAttributeTestBuilder WithKeyLengthLimit(int keyLengthLimit)
        {
            this.Attribute.KeyLengthLimit = keyLengthLimit;
            this.Validations.Add((expected, actual) =>
            {
                var expectedKeyLengthLimit = expected.KeyLengthLimit;
                var actualKeyLengthLimit = actual.KeyLengthLimit;

                if (expectedKeyLengthLimit != actualKeyLengthLimit)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}key length limit of {expectedKeyLengthLimit}",
                        $"in fact found {actualKeyLengthLimit}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndRequestFormLimitsAttributeTestBuilder WithValueLengthLimit(int valueLengthLimit)
        {
            this.Attribute.ValueLengthLimit = valueLengthLimit;
            this.Validations.Add((expected, actual) =>
            {
                var expectedValueLengthLimit = expected.ValueLengthLimit;
                var actualValueLengthLimit = actual.ValueLengthLimit;

                if (expectedValueLengthLimit != actualValueLengthLimit)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}value length limit of {expectedValueLengthLimit}",
                        $"in fact found {actualValueLengthLimit}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndRequestFormLimitsAttributeTestBuilder WithMultipartBoundaryLengthLimit(int multipartBoundaryLengthLimit)
        {
            this.Attribute.MultipartBoundaryLengthLimit = multipartBoundaryLengthLimit;
            this.Validations.Add((expected, actual) =>
            {
                var expectedMultipartBoundaryLengthLimit = expected.MultipartBoundaryLengthLimit;
                var actualMultipartBoundaryLengthLimit = actual.MultipartBoundaryLengthLimit;

                if (expectedMultipartBoundaryLengthLimit != actualMultipartBoundaryLengthLimit)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}multipart boundary length limit of {expectedMultipartBoundaryLengthLimit}",
                        $"in fact found {actualMultipartBoundaryLengthLimit}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndRequestFormLimitsAttributeTestBuilder WithMultipartHeadersCountLimit(int multipartHeadersCountLimit)
        {
            this.Attribute.MultipartHeadersCountLimit = multipartHeadersCountLimit;
            this.Validations.Add((expected, actual) =>
            {
                var expectedMultipartHeadersCountLimit = expected.MultipartHeadersCountLimit;
                var actualMultipartHeadersCountLimit = actual.MultipartHeadersCountLimit;

                if (expectedMultipartHeadersCountLimit != actualMultipartHeadersCountLimit)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}multipart headers count limit of {expectedMultipartHeadersCountLimit}",
                        $"in fact found {actualMultipartHeadersCountLimit}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndRequestFormLimitsAttributeTestBuilder WithMultipartHeadersLengthLimit(int multipartHeadersLengthLimit)
        {
            this.Attribute.MultipartHeadersLengthLimit = multipartHeadersLengthLimit;
            this.Validations.Add((expected, actual) =>
            {
                var expectedMultipartHeadersLengthLimit = expected.MultipartHeadersLengthLimit;
                var actualMultipartHeadersLengthLimit = actual.MultipartHeadersLengthLimit;

                if (expectedMultipartHeadersLengthLimit != actualMultipartHeadersLengthLimit)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}multipart headers length limit of {expectedMultipartHeadersLengthLimit}",
                        $"in fact found {actualMultipartHeadersLengthLimit}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndRequestFormLimitsAttributeTestBuilder WithMultipartBodyLengthLimit(int multipartBodyLengthLimit)
        {
            this.Attribute.MultipartBodyLengthLimit = multipartBodyLengthLimit;
            this.Validations.Add((expected, actual) =>
            {
                var expectedMultipartBodyLengthLimit = expected.MultipartBodyLengthLimit;
                var actualMultipartBodyLengthLimit = actual.MultipartBodyLengthLimit;

                if (expectedMultipartBodyLengthLimit != actualMultipartBodyLengthLimit)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}multipart body length limit of {expectedMultipartBodyLengthLimit}",
                        $"in fact found {actualMultipartBodyLengthLimit}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndRequestFormLimitsAttributeTestBuilder WithOrder(int order)
        {
            this.ValidateOrder(order);
            return this;
        }

        /// <inheritdoc />
        public IRequestFormLimitsAttributeTestBuilder AndAlso() => this;
    }
}
