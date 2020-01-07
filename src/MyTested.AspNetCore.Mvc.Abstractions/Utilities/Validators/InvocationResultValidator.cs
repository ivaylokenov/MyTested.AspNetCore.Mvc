namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Exceptions;
    using Extensions;
    using Internal.TestContexts;

    public static class InvocationResultValidator
    {
        public static void ValidateInvocationResultType(
            ComponentTestContext testContext,
            Type typeOfExpectedReturnValue,
            bool canBeAssignable = false,
            bool allowDifferentGenericTypeDefinitions = false,
            Type typeOfActualReturnValue = null)
        {
            var invalid = InvocationResultTypeIsInvalid(
                testContext,
                typeOfExpectedReturnValue,
                canBeAssignable,
                allowDifferentGenericTypeDefinitions,
                typeOfActualReturnValue);

            if (invalid)
            {
                var typeOfResult = typeOfActualReturnValue ?? testContext.MethodResult?.GetType();

                var (expectedTypeName, actualTypeName) = (typeOfExpectedReturnValue, typeOfResult).GetTypeComparisonNames();

                ThrowNewInvocationResultAssertionException(
                    testContext,
                    expectedTypeName,
                    actualTypeName);
            }
        }

        public static void ValidateInvocationResultType<TExpectedType>(
            ComponentTestContext testContext,
            bool canBeAssignable = false,
            bool allowDifferentGenericTypeDefinitions = false) 
            => ValidateInvocationResultType(testContext, typeof(TExpectedType), canBeAssignable, allowDifferentGenericTypeDefinitions);

        public static void ValidateInvocationResultTypes(
            ComponentTestContext testContext,
            bool canBeAssignable = false,
            bool allowDifferentGenericTypeDefinitions = false,
            params Type[] typesOfExpectedReturnValue)
        {
            var invalid = false;

            foreach (var type in typesOfExpectedReturnValue)
            {
                invalid = InvocationResultTypeIsInvalid(
                    testContext,
                    type,
                    canBeAssignable,
                    allowDifferentGenericTypeDefinitions);

                if (!invalid)
                {
                    break;
                }
            }

            if (invalid)
            {
                var expectedTypeName = string.Join(
                    " or ", 
                    typesOfExpectedReturnValue.Select(t => t.ToFriendlyTypeName()));

                var actualTypeName = testContext.MethodResult?.GetType().ToFriendlyTypeName();

                ThrowNewInvocationResultAssertionException(
                    testContext,
                    expectedTypeName,
                    actualTypeName);
            }
        }

        public static void ValidateInvocationResult<TResult>(ComponentTestContext testContext, TResult model, bool canBeAssignable = false)
        {
            if (!Reflection.IsAnonymousType(typeof(TResult)))
            {
                ValidateInvocationResultType<TResult>(testContext, canBeAssignable);
            }

            if (Reflection.AreNotDeeplyEqual(model, testContext.MethodResult, out var result))
            {
                throw ResponseModelAssertionException.From(testContext.ExceptionMessagePrefix, result);
            }

            testContext.Model = model;
        }
        
        public static TResult GetInvocationResult<TResult>(
            ComponentTestContext testContext,
            bool canBeAssignable = false)
            where TResult : class
        {
            ValidateInvocationResultType<TResult>(testContext, canBeAssignable);
            return testContext.MethodResult as TResult;
        }

        private static bool InvocationResultTypeIsInvalid(
            ComponentTestContext testContext,
            Type typeOfExpectedReturnValue,
            bool canBeAssignable = false,
            bool allowDifferentGenericTypeDefinitions = false,
            Type typeOfActualReturnValue = null)
        {
            InvocationValidator.CheckForException(testContext.CaughtException, testContext.ExceptionMessagePrefix);

            var typeInfoOfExpectedReturnValue = typeOfExpectedReturnValue.GetTypeInfo();

            var typeOfResult = typeOfActualReturnValue ?? testContext.MethodResult?.GetType();
            if (typeOfResult == null)
            {
                ThrowNewInvocationResultAssertionException(
                    testContext,
                    typeOfExpectedReturnValue.ToFriendlyTypeName(),
                    "null");
            }

            var typeInfoOfResult = typeOfResult.GetTypeInfo();

            var isAssignableCheck = canBeAssignable && Reflection.AreNotAssignable(typeOfExpectedReturnValue, typeOfResult);
            if (isAssignableCheck && allowDifferentGenericTypeDefinitions
                && Reflection.IsGeneric(typeOfExpectedReturnValue) && Reflection.IsGenericTypeDefinition(typeOfExpectedReturnValue))
            {
                isAssignableCheck = Reflection.AreAssignableByGeneric(typeOfExpectedReturnValue, typeOfResult);

                if (!isAssignableCheck && !typeInfoOfResult.IsGenericType)
                {
                    isAssignableCheck = true;
                }
                else
                {
                    isAssignableCheck =
                        !Reflection.ContainsGenericTypeDefinitionInterface(typeOfExpectedReturnValue, typeOfResult);
                }
            }

            var strictlyEqualCheck = !canBeAssignable && Reflection.AreDifferentTypes(typeOfExpectedReturnValue, typeOfResult);

            var invalid = isAssignableCheck || strictlyEqualCheck;
            if (invalid && typeInfoOfExpectedReturnValue.IsGenericTypeDefinition && typeInfoOfResult.IsGenericType)
            {
                var actionResultGenericDefinition = typeInfoOfResult.GetGenericTypeDefinition();
                if (actionResultGenericDefinition == typeOfExpectedReturnValue)
                {
                    invalid = false;
                }
            }

            if (invalid && typeInfoOfExpectedReturnValue.IsGenericType && typeInfoOfResult.IsGenericType)
            {
                invalid = !Reflection.AreAssignableByGeneric(typeOfExpectedReturnValue, typeOfResult);
            }

            return invalid;
        }

        private static void ThrowNewInvocationResultAssertionException(
            ComponentTestContext testContext,
            string typeNameOfExpectedReturnValue,
            string typeNameOfActionResult) 
            => throw new InvocationResultAssertionException(string.Format(
                "{0} result to be {1}, but instead received {2}.",
                testContext.ExceptionMessagePrefix,
                typeNameOfExpectedReturnValue,
                typeNameOfActionResult));
    }
}
