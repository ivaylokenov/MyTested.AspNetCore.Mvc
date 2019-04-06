namespace MyTested.AspNetCore.Mvc.Builders.Authentication
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Contracts.Authentication;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Authentication;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing authentication properties.
    /// </summary>
    public class AuthenticationPropertiesTestBuilder : BaseTestBuilderWithComponent, IAndAuthenticationPropertiesTestBuilder
    {
        private const int DefaultItemsCount = 5;

        private readonly AuthenticationProperties authenticationProperties;
        private readonly ICollection<Action<AuthenticationProperties, AuthenticationProperties>> validations;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationPropertiesTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public AuthenticationPropertiesTestBuilder(ComponentTestContext testContext)
            :base(testContext)
        {
            this.authenticationProperties = new AuthenticationProperties();
            this.validations = new List<Action<AuthenticationProperties, AuthenticationProperties>>();
        }
        
        /// <inheritdoc />
        public IAndAuthenticationPropertiesTestBuilder WithAllowRefresh(bool? allowRefresh)
        {
            this.authenticationProperties.AllowRefresh = allowRefresh;
            this.validations.Add((expected, actual) =>
            {
                if (expected.AllowRefresh != actual.AllowRefresh)
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        expected.AllowRefresh == null ? "not have allow refresh value" : $"have allow refresh value of {expected.AllowRefresh.GetErrorMessageName()}",
                        $"in fact found {actual.AllowRefresh.GetErrorMessageName()}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndAuthenticationPropertiesTestBuilder WithExpires(DateTimeOffset? expiresUtc)
        {
            this.authenticationProperties.ExpiresUtc = expiresUtc;
            this.validations.Add((expected, actual) =>
            {
                if (expected.ExpiresUtc != actual.ExpiresUtc)
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        expected.ExpiresUtc == null ? "not have expires value" : $"have expires value of {expected.ExpiresUtc.GetErrorMessageName()}",
                        $"in fact found {actual.ExpiresUtc.GetErrorMessageName()}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndAuthenticationPropertiesTestBuilder WithIsPersistent(bool isPersistent)
        {
            this.authenticationProperties.IsPersistent = isPersistent;
            this.validations.Add((expected, actual) =>
            {
                if (expected.IsPersistent != actual.IsPersistent)
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        $"have is persistent value of {expected.IsPersistent.GetErrorMessageName()}",
                        $"in fact found {actual.IsPersistent.GetErrorMessageName()}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndAuthenticationPropertiesTestBuilder WithIssued(DateTimeOffset? issuedUtc)
        {
            this.authenticationProperties.IssuedUtc = issuedUtc;
            this.validations.Add((expected, actual) =>
            {
                if (expected.IssuedUtc != actual.IssuedUtc)
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        expected.IssuedUtc == null ? "not have issued value" : $"have issued value of {expected.IssuedUtc.GetErrorMessageName()}",
                        $"in fact found {actual.IssuedUtc.GetErrorMessageName()}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndAuthenticationPropertiesTestBuilder WithItem(string itemKey)
        {
            this.authenticationProperties.Items.Add(itemKey, string.Empty);
            this.validations.Add((expected, actual) =>
            {
                if (!actual.Items.ContainsKey(itemKey))
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        $"have item with key '{itemKey}'",
                        "such was not found");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndAuthenticationPropertiesTestBuilder WithItem(string itemKey, string itemValue)
        {
            this.authenticationProperties.Items.Add(itemKey, itemValue);
            this.validations.Add((expected, actual) =>
            {
                var itemExists = actual.Items.ContainsKey(itemKey);
                var actualValue = itemExists ? actual.Items[itemKey] : null;

                if (!itemExists || actualValue != itemValue)
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        $"have item with key '{itemKey}' and value '{itemValue}'",
                        $"{(itemExists ? $"the value was '{actualValue}'" : "such was not found")}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndAuthenticationPropertiesTestBuilder WithItems(object items)
            => this.WithItems(items.ToStringValueDictionary());

        /// <inheritdoc />
        public IAndAuthenticationPropertiesTestBuilder WithItems(IDictionary<string, string> items)
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedItems = expected.Items.Count;
                var actualItems = actual.Items.Count - DefaultItemsCount;

                if (expectedItems != actualItems)
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        $"have {expectedItems} custom {(expectedItems != 1 ? "items" : "item")}",
                        $"in fact found {actualItems}");
                }
            });

            items.ForEach(item => this.WithItem(item.Key, item.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndAuthenticationPropertiesTestBuilder WithRedirectUri(string redirectUri)
        {
            this.authenticationProperties.RedirectUri = redirectUri;
            this.validations.Add((expected, actual) =>
            {
                if (expected.RedirectUri != actual.RedirectUri)
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        expected.RedirectUri == null ? "not have redirect URI value" : $"have {expected.RedirectUri.GetErrorMessageName()} redirect URI",
                        $"in fact found {actual.RedirectUri.GetErrorMessageName()}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAuthenticationPropertiesTestBuilder AndAlso() => this;

        internal AuthenticationProperties GetAuthenticationProperties() 
            => this.authenticationProperties;

        internal ICollection<Action<AuthenticationProperties, AuthenticationProperties>> GetAuthenticationPropertiesValidations() 
            => this.validations;

        private void ThrowNewAuthenticationPropertiesAssertionException(string expectedValue, string actualValue) 
            => throw new AuthenticationPropertiesAssertionException(string.Format(
                "When calling {0} action in {1} expected authentication properties to {2}, but {3}.",
                this.TestContext.MethodName,
                this.TestContext.Component.GetName(),
                expectedValue,
                actualValue));
    }
}
