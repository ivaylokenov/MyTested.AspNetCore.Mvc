namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Internal.TestContexts;

    /// <summary>
    /// Base class for all attribute test builders.
    /// </summary>
    public abstract class BaseAttributesTestBuilder : BaseTestBuilderWithComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAttributesTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseAttributesTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
            this.Validations = new List<Action<IEnumerable<object>>>();
        }

        /// <summary>
        /// Gets the validation actions for the tested attributes.
        /// </summary>
        /// <value>Collection of validation actions for the attributes.</value>
        protected ICollection<Action<IEnumerable<object>>> Validations { get; private set; }

        internal IEnumerable<Action<IEnumerable<object>>> GetAttributeValidations() => this.Validations;
    }
}
