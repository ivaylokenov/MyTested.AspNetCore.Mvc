namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using Internal.TestContexts;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Base class for attribute test builders with order.
    /// </summary>
    /// <typeparam name="TAttribute">Type of <see cref="Attribute"/> to use in the test builder.</typeparam>
    public abstract class BaseAttributeTestBuilderWithOrder<TAttribute> 
        : BaseAttributeTestBuilder<TAttribute>
        where TAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAttributeTestBuilderWithOrder{TAttribute}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="attributeName">Attribute name to use in case of failed validation.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        protected BaseAttributeTestBuilderWithOrder(
            ComponentTestContext testContext,
            string attributeName,
            Action<string, string> failedValidationAction)
            : base(testContext, attributeName, failedValidationAction)
        {
        }
        
        protected void ValidateOrder(int order)
        {
            if (order != 0)
            {
                RuntimeBinderValidator.ValidateBinding(() =>
                {
                    this.Attribute.AsDynamic().Order = order;
                    this.Validations.Add((expected, actual) =>
                    {
                        var expectedOrder = expected.AsDynamic().Order;
                        var actualOrder = actual.AsDynamic().Order;

                        if (expectedOrder != actualOrder)
                        {
                            this.FailedValidationAction(
                                $"{this.ExceptionMessagePrefix}order of {expectedOrder}",
                                $"in fact found {actualOrder}");
                        }
                    });
                });
            }
        }
    }
}
