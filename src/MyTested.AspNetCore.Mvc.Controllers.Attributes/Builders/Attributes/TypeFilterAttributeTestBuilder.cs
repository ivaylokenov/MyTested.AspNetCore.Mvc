namespace MyTested.AspNetCore.Mvc.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Contracts.Attributes;
    using Internal.TestContexts;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing <see cref="TypeFilterAttribute"/>.
    /// </summary>
    public class TypeFilterAttributeTestBuilder : BaseAttributeTestBuilderWithOrderAndType<TypeFilterAttribute>,
        IAndTypeFilterAttributeTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeFilterAttributeTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public TypeFilterAttributeTestBuilder(
            ComponentTestContext testContext,
            Action<string, string> failedValidationAction)
            : base(testContext, nameof(TypeFilterAttribute), failedValidationAction)
                => this.Attribute = new TypeFilterAttribute(typeof(object));

        /// <inheritdoc />
        public IAndTypeFilterAttributeTestBuilder OfType(Type type)
        {
            this.ValidateType(type, attr => attr.ImplementationType);
            return this;
        }

        /// <inheritdoc />
        public IAndTypeFilterAttributeTestBuilder WithArguments(object[] args)
        {
            this.Validations.Add((expected, actual) =>
            {
                int expectedArgsCount = expected.Arguments.Length;
                int actualArgsCount = actual.Arguments != null ? actual.Arguments.Length : 0;

                if (expectedArgsCount != actualArgsCount)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}{expectedArgsCount} {(actualArgsCount != 1 ? "arguments" : "argument")}",
                        $"in fact found {actualArgsCount}");
                }
            });

            args.ForEach(argument => this.WithArgument(argument));

            return this;
        }

        /// <inheritdoc />
        public IAndTypeFilterAttributeTestBuilder WithOrder(int order)
        {
            this.ValidateOrder(order);
            return this;
        }

        /// <inheritdoc />
        public ITypeFilterAttributeTestBuilder AndAlso() => this;

        private IAndTypeFilterAttributeTestBuilder WithArgument(object arg)
        {
            var args = new List<object>(this.Attribute.Arguments ?? new object[0])
            {
                arg
            };

            this.Attribute.Arguments = args.ToArray();
            this.Validations.Add((expected, actual) =>
            {
                var sameEntry = actual.Arguments.FirstOrDefault(entry => Reflection.AreDeeplyEqual(arg, entry));
                if (sameEntry == null)
                {
                    this.FailedValidationAction(
                        $"{this.ExceptionMessagePrefix}argument with the provided value",
                        "in fact such was not found");
                }
            });

            return this;
        }
    }
}
