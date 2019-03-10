namespace MyTested.AspNetCore.Mvc.Controllers.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Mvc.Builders.Attributes;
    using Mvc.Builders.Contracts.Attributes;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing <see cref="ResponseCacheAttribute"/>.
    /// </summary>
    public class ResponseCacheAttributeTestBuilder : BaseAttributeTestBuilderWithOrder<ResponseCacheAttribute>,
        IAndResponseCacheAttributeTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseCacheAttributeTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public ResponseCacheAttributeTestBuilder(
            ComponentTestContext testContext,
            Action<string, string> failedValidationAction)
            : base(testContext, nameof(ResponseCacheAttribute), failedValidationAction)
            => this.Attribute = new ResponseCacheAttribute();
        
        /// <inheritdoc />
        public IAndResponseCacheAttributeTestBuilder WithDuration(int duration)
        {
            this.Attribute.Duration = duration;
            this.Validations.Add((expected, actual) =>
            {
                var expectedDuration = expected.Duration;
                var actualDuration = actual.Duration;

                if (expectedDuration != actualDuration)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}duration of {expectedDuration} seconds",
                        $"in fact found {actualDuration}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndResponseCacheAttributeTestBuilder WithLocation(ResponseCacheLocation location)
        {
            this.Attribute.Location = location;
            this.Validations.Add((expected, actual) =>
            {
                var expectedLocation = expected.Location;
                var actualLocation = actual.Location;

                if (expectedLocation != actualLocation)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}'{expectedLocation}' location",
                        $"in fact found '{actualLocation}'");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndResponseCacheAttributeTestBuilder WithNoStore(bool noStore)
        {
            this.Attribute.NoStore = noStore;
            this.Validations.Add((expected, actual) =>
            {
                var expectedNoStore = expected.NoStore;
                var actualNoStore = actual.NoStore;

                if (expectedNoStore != actualNoStore)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}no store value of {expectedNoStore.GetErrorMessageName()}",
                        $"in fact found {actualNoStore.GetErrorMessageName()}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndResponseCacheAttributeTestBuilder WithVaryByHeader(string varyByHeader)
        {
            this.Attribute.VaryByHeader = varyByHeader;
            this.Validations.Add((expected, actual) =>
            {
                var expectedVaryByHeader = expected.VaryByHeader;
                var actualVaryByHeader = actual.VaryByHeader;

                if (expectedVaryByHeader != actualVaryByHeader)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}vary by header value of {expectedVaryByHeader.GetErrorMessageName()}",
                        $"in fact found {actualVaryByHeader.GetErrorMessageName()}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndResponseCacheAttributeTestBuilder WithVaryByQueryKey(string varyByQueryKey)
        {
            var varyByQueryKeys = new List<string>(this.Attribute.VaryByQueryKeys ?? new string[0])
            {
                varyByQueryKey
            };

            this.Attribute.VaryByQueryKeys = varyByQueryKeys.ToArray();
            this.Validations.Add((expected, actual) =>
            {
                if (!actual.VaryByQueryKeys.Contains(varyByQueryKey))
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}vary by query string key value of '{varyByQueryKey}'",
                        "in fact such was not found");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndResponseCacheAttributeTestBuilder WithVaryByQueryKeys(IEnumerable<string> varyByQueryKeys)
        {
            this.Validations.Add((expected, actual) =>
            {
                var expectedVaryByQueryKeys = expected.VaryByQueryKeys.Length;
                var actualVaryByQueryKeys = actual.VaryByQueryKeys.Length;

                if (expectedVaryByQueryKeys != actualVaryByQueryKeys)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}{expectedVaryByQueryKeys} vary by query string key {(expectedVaryByQueryKeys != 1 ? "values" : "value")}",
                        $"in fact found {actualVaryByQueryKeys}");
                }
            });

            varyByQueryKeys.ForEach(queryKey => this.WithVaryByQueryKey(queryKey));

            return this;
        }

        /// <inheritdoc />
        public IAndResponseCacheAttributeTestBuilder WithVaryByQueryKeys(params string[] varyByQueryKeys)
            => this.WithVaryByQueryKeys(varyByQueryKeys.AsEnumerable());

        /// <inheritdoc />
        public IAndResponseCacheAttributeTestBuilder WithCacheProfileName(string cacheProfileName)
        {
            this.Attribute.CacheProfileName = cacheProfileName;
            this.Validations.Add((expected, actual) =>
            {
                var expectedCacheProfileName = expected.CacheProfileName;
                var actualCacheProfileName = actual.CacheProfileName;

                if (expectedCacheProfileName != actualCacheProfileName)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}{expectedCacheProfileName.GetErrorMessageName()} cache profile name",
                        $"in fact found {actualCacheProfileName.GetErrorMessageName()}");
                }
            });

            return this;
        }
        
        /// <inheritdoc />
        public IAndResponseCacheAttributeTestBuilder WithOrder(int order)
        {
            this.ValidateOrder(order);
            return this;
        }

        public IResponseCacheAttributeTestBuilder AndAlso() => this;
    }
}
