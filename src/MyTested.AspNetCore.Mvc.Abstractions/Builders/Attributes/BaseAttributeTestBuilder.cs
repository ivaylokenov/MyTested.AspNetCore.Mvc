namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Internal.TestContexts;
    using Utilities.Validators;

    /// <summary>
    /// Base class for attribute test builders.
    /// </summary>
    /// <typeparam name="TAttribute">Type of <see cref="System.Attribute"/> to use in the test builder.</typeparam>
    public abstract class BaseAttributeTestBuilder<TAttribute> : BaseTestBuilderWithComponent
        where TAttribute : Attribute
    {
        private readonly string attributeName;

        protected string ExceptionMessagePrefix => $"{this.attributeName} with ";

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAttributeTestBuilder{TAttribute}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="attributeName">Attribute name to use in case of failed validation.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        protected BaseAttributeTestBuilder(
            ComponentTestContext testContext,
            string attributeName,
            Action<string, string> failedValidationAction)
            : base(testContext)
        {
            CommonValidator.CheckForNullReference(failedValidationAction, nameof(failedValidationAction));
            CommonValidator.CheckForNotWhiteSpaceString(attributeName);

            this.attributeName = attributeName;

            this.FailedValidationAction = failedValidationAction;

            this.Validations = new List<Action<TAttribute, TAttribute>>();
        }

        protected TAttribute Attribute { get; set; }

        protected ICollection<Action<TAttribute, TAttribute>> Validations { get; }

        protected Action<string, string> FailedValidationAction { get; }
        
        public TAttribute GetAttribute() => this.Attribute;

        public ICollection<Action<TAttribute, TAttribute>> GetAttributeValidations()
            => this.Validations;
    }
}
