namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using Internal.TestContexts;
    using Utilities;

    public abstract class BaseAttributeTestBuilderWithOrderAndType<TAttribute> : BaseAttributeTestBuilderWithOrder<TAttribute>
        where TAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAttributeTestBuilderWithOrderAndType{TAttribute}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="attributeName">Attribute name to use in case of failed validation.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        protected BaseAttributeTestBuilderWithOrderAndType(
            ComponentTestContext testContext,
            string attributeName,
            Action<string, string> failedValidationAction)
            : base(testContext, attributeName, failedValidationAction)
        {
        }

        protected virtual void ValidateType(Type type, Func<TAttribute, Type> getTypeValueFunc)
        {
            this.Attribute = (TAttribute)Activator.CreateInstance(typeof(TAttribute), type);
            this.Validations.Add((expected, actual) =>
            {
                var expectedType = getTypeValueFunc(expected);
                var actualType = getTypeValueFunc(actual);

                if (Reflection.AreDifferentTypes(expectedType, actualType))
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}'{expectedType.ToFriendlyTypeName()}' type",
                        $"in fact found '{actualType.ToFriendlyTypeName()}'");
                }
            });
        }
    }
}
