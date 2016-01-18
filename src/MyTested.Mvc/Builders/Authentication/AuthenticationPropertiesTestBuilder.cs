namespace MyTested.Mvc.Builders.Authentication
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Http.Authentication;
    using Microsoft.AspNet.Mvc;
    using MyTested.Mvc.Builders.Contracts.Authentication;

    /// <summary>
    /// Used for testing authentication properties.
    /// </summary>
    public class AuthenticationPropertiesTestBuilder : BaseTestBuilderWithAction, IAndAuthenticationPropertiesTestBuilder
    {
        private readonly AuthenticationProperties authenticationProperties;
        private readonly ICollection<Action<AuthenticationProperties, AuthenticationProperties>> validations;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationPropertiesTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        public AuthenticationPropertiesTestBuilder(
            Controller controller,
            string actionName)
            : base(controller, actionName)
        {
            this.authenticationProperties = new AuthenticationProperties();
            this.validations = new List<Action<AuthenticationProperties, AuthenticationProperties>>();
        }
        
        /// <summary>
        /// Tests whether authentication properties has the same allow refresh value as the provided one.
        /// </summary>
        /// <param name="allowRefresh">Expected allow refresh value.</param>
        /// <returns>The same authentication properties test builder.</returns>
        public IAndAuthenticationPropertiesTestBuilder WithAllowRefresh(bool? allowRefresh)
        {
            this.authenticationProperties.AllowRefresh = allowRefresh;
            this.validations.Add((expected, actual) =>
            {
                if (expected.AllowRefresh != actual.AllowRefresh)
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        expected.AllowRefresh == null ? "not have allow refresh value" : string.Format("allow refresh value of '{0}'", expected.AllowRefresh),
                        string.Format("in fact found '{0}'", actual.AllowRefresh == null ? "null" : actual.AllowRefresh.ToString()));
                }
            });

            return this;
        }

        /// <summary>
        /// Tests whether authentication properties has the same expires value as the provided one.
        /// </summary>
        /// <param name="expiresUtc">Expected expires value.</param>
        /// <returns>The same authentication properties test builder.</returns>
        public IAndAuthenticationPropertiesTestBuilder WithExpires(DateTimeOffset? expiresUtc)
        {
            this.authenticationProperties.ExpiresUtc = expiresUtc;
            this.validations.Add((expected, actual) =>
            {
                if (expected.ExpiresUtc != actual.ExpiresUtc)
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        expected.ExpiresUtc == null ? "not have expires value" : string.Format("expires value of '{0}'", expected.ExpiresUtc),
                        string.Format("in fact found '{0}'", actual.ExpiresUtc == null ? "null" : actual.ExpiresUtc.ToString()));
                }
            });

            return this;
        }

        /// <summary>
        /// Tests whether authentication properties has the same is persistent value as the provided one.
        /// </summary>
        /// <param name="isPersistent">Expected is persistent value.</param>
        /// <returns>The same authentication properties test builder.</returns>
        public IAndAuthenticationPropertiesTestBuilder WithIsPersistent(bool isPersistent)
        {
            this.authenticationProperties.IsPersistent = isPersistent;
            this.validations.Add((expected, actual) =>
            {
                if (expected.IsPersistent != actual.IsPersistent)
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        string.Format("is persistent value of '{0}'", expected.IsPersistent),
                        string.Format("in fact found '{0}'", actual.IsPersistent));
                }
            });

            return this;
        }

        /// <summary>
        /// Tests whether authentication properties has the same issued value as the provided one.
        /// </summary>
        /// <param name="issuedUtc">Expected issued value.</param>
        /// <returns>The same authentication properties test builder.</returns>
        public IAndAuthenticationPropertiesTestBuilder WithIssued(DateTimeOffset? issuedUtc)
        {
            this.authenticationProperties.IssuedUtc = issuedUtc;
            this.validations.Add((expected, actual) =>
            {
                if (expected.IssuedUtc != actual.IssuedUtc)
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        expected.IssuedUtc == null ? "not have issued value" : string.Format("issued value of '{0}'", expected.IssuedUtc),
                        string.Format("in fact found '{0}'", actual.IssuedUtc == null ? "null" : actual.IssuedUtc.ToString()));
                }
            });

            return this;
        }

        /// <summary>
        /// Tests whether authentication properties contains the provided item key.
        /// </summary>
        /// <param name="itemKey">Expected item key.</param>
        /// <returns>The same authentication properties test builder.</returns>
        public IAndAuthenticationPropertiesTestBuilder WithItem(string itemKey)
        {
            this.authenticationProperties.Items.Add(itemKey, string.Empty);
            this.validations.Add((expected, actual) =>
            {
                if (!actual.Items.ContainsKey(itemKey))
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        string.Format("item with key '{0}'", itemKey),
                        "such was not found");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests whether authentication properties contains the provided item key and value.
        /// </summary>
        /// <param name="itemKey">Expected item key.</param>
        /// <param name="itemValue">Expected item value.</param>
        /// <returns>The same authentication properties test builder.</returns>
        public IAndAuthenticationPropertiesTestBuilder WithItem(string itemKey, string itemValue)
        {
            this.authenticationProperties.Items.Add(itemKey, itemValue);
            this.validations.Add((expected, actual) =>
            {
                if (!actual.Items.ContainsKey(itemKey) || actual.Items[itemKey] != itemValue)
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        string.Format("item with key '{0}'", itemKey),
                        "such was not found");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests whether authentication properties contains the provided items.
        /// </summary>
        /// <param name="items">Expected items as dictionary.</param>
        /// <returns>The same authentication properties test builder.</returns>
        public IAndAuthenticationPropertiesTestBuilder WithItems(IDictionary<string, string> items)
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedItems = expected.Items.Count;
                var actualItems = actual.Items.Count;

                if (expectedItems != actualItems)
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        $"{expectedItems} items",
                        $"in fact found {actualItems}");
                }
            });

            items.ForEach(item => this.WithItem(item.Key, item.Value));
            return this;
        }

        /// <summary>
        /// Tests whether authentication properties has the same redirect URI as the provided one.
        /// </summary>
        /// <param name="redirectUri">Expected redirect URI.</param>
        /// <returns>The same authentication properties test builder.</returns>
        public IAndAuthenticationPropertiesTestBuilder WithRedirectUri(string redirectUri)
        {
            this.authenticationProperties.RedirectUri = redirectUri;
            this.validations.Add((expected, actual) =>
            {
                if (expected.RedirectUri != actual.RedirectUri)
                {
                    this.ThrowNewAuthenticationPropertiesAssertionException(
                        string.Format("'{0}' redirect URI", expected.RedirectUri),
                        string.Format("in fact found '{0}'", actual.RedirectUri == null ? "null" : actual.RedirectUri));
                }
            });

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when testing authentication properties.
        /// </summary>
        /// <returns>The same authentication properties test builder.</returns>
        public IAuthenticationPropertiesTestBuilder AndAlso()
        {
            return this;
        }

        internal AuthenticationProperties GetAuthenticationProperties()
        {
            return this.authenticationProperties;
        }

        internal ICollection<Action<AuthenticationProperties, AuthenticationProperties>> GetAuthenticationPropertiesValidations()
        {
            return this.validations;
        }

        private void ThrowNewAuthenticationPropertiesAssertionException(string expectedValue, string actualValue)
        {
            throw new AuthenticationPropertiesAssertionException(string.Format(
                        "When calling {0} action in {1} expected authentication properties to have {2}, but {3}.",
                        this.ActionName,
                        this.Controller.GetName(),
                        expectedValue,
                        actualValue));
        }
    }
}
