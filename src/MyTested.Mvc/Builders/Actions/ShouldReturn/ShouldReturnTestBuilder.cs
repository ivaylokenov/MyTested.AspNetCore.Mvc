namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Base;
    using Common.Extensions;
    using Contracts.Actions;
    using Exceptions;
    using Utilities;
    using Utilities.Validators;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Used for testing returned action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldReturnTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public ShouldReturnTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        private void ValidateActionReturnType(Type typeOfExpectedReturnValue, bool canBeAssignable = false, bool allowDifferentGenericTypeDefinitions = false)
        {
            // TODO: GetTypeInfo should be called once and not many times in this method
            CommonValidator.CheckForException(this.CaughtException);

            var typeOfActionResult = ActionResult.GetType();

            var isAssignableCheck = canBeAssignable && Reflection.AreNotAssignable(typeOfExpectedReturnValue, typeOfActionResult);
            if (isAssignableCheck && allowDifferentGenericTypeDefinitions
                && Reflection.IsGeneric(typeOfExpectedReturnValue) && Reflection.IsGenericTypeDefinition(typeOfExpectedReturnValue))
            {
                isAssignableCheck = Reflection.AreAssignableByGeneric(typeOfExpectedReturnValue, typeOfActionResult);

                if (!isAssignableCheck && !typeOfActionResult.GetTypeInfo().IsGenericType)
                {
                    isAssignableCheck = true;
                }
                else
                {
                    isAssignableCheck =
                        !Reflection.ContainsGenericTypeDefinitionInterface(typeOfExpectedReturnValue, typeOfActionResult);
                }
            }

            var strictlyEqualCheck = !canBeAssignable && Reflection.AreDifferentTypes(typeOfExpectedReturnValue, typeOfActionResult);

            var invalid = isAssignableCheck || strictlyEqualCheck;
            if (strictlyEqualCheck)
            {
                var genericTypeDefinitionCheck = Reflection.AreAssignableByGeneric(typeOfExpectedReturnValue, typeOfActionResult);

                if (genericTypeDefinitionCheck)
                {
                    invalid = false;
                }
            }

            if (invalid && typeOfExpectedReturnValue.GetTypeInfo().IsGenericTypeDefinition && typeOfActionResult.GetTypeInfo().IsGenericType)
            {
                var actionResultGenericDefinition = typeOfActionResult.GetTypeInfo().GetGenericTypeDefinition();
                if (actionResultGenericDefinition == typeOfExpectedReturnValue)
                {
                    invalid = false;
                }
            }

            if (invalid && typeOfExpectedReturnValue.GetTypeInfo().IsGenericType && typeOfActionResult.GetTypeInfo().IsGenericType)
            {
                invalid = !Reflection.AreAssignableByGeneric(typeOfExpectedReturnValue, typeOfActionResult);
            }

            if (invalid)
            {
                this.ThrowNewGenericHttpActionResultAssertionException(
                    typeOfExpectedReturnValue.ToFriendlyTypeName(),
                    typeOfActionResult.ToFriendlyTypeName());
            }
        }

        private void ValidateActionReturnType<TExpectedType>(bool canBeAssignable = false, bool allowDifferentGenericTypeDefinitions = false)
        {
            var typeOfResponseData = typeof(TExpectedType);
            this.ValidateActionReturnType(typeOfResponseData, canBeAssignable, allowDifferentGenericTypeDefinitions);
        }

        private void ValidateActionReturnType(params Type[] returnTypes)
        {
            var typeOfActionResult = this.ActionResult.GetType();
            if (returnTypes.All(t => !Reflection.AreAssignable(t, typeOfActionResult)))
            {
                this.ThrowNewGenericHttpActionResultAssertionException(
                    string.Join(" or ", returnTypes.Select(t => t.ToFriendlyTypeName())),
                    typeOfActionResult.ToFriendlyTypeName());
            }
        }

        private void ThrowNewGenericHttpActionResultAssertionException(
            string typeNameOfExpectedReturnValue,
            string typeNameOfActionResult)
        {
            throw new ActionResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected action result to be {2}, but instead received {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    typeNameOfExpectedReturnValue,
                    typeNameOfActionResult));
        }
    }
}
