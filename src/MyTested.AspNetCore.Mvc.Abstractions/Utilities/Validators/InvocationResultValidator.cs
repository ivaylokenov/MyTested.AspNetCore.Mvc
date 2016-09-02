namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using System;
    using System.Reflection;
    using Exceptions;
    using Internal.TestContexts;

    public static class InvocationResultValidator
    {
        public static void ValidateInvocationResultType(
            ComponentTestContext testContext,
            Type typeOfExpectedReturnValue,
            bool canBeAssignable = false,
            bool allowDifferentGenericTypeDefinitions = false)
        {
            InvocationValidator.CheckForException(testContext.CaughtException, testContext.ExceptionMessagePrefix);

            var typeInfoOfExpectedReturnValue = typeOfExpectedReturnValue.GetTypeInfo();

            var typeOfActionResult = testContext.MethodResult?.GetType();
            if (typeOfActionResult == null)
            {
                ThrowNewInvocationResultAssertionException(
                    testContext,
                    typeOfExpectedReturnValue.ToFriendlyTypeName(),
                    "null");
            }

            var typeInfoOfActionResult = typeOfActionResult.GetTypeInfo();

            var isAssignableCheck = canBeAssignable && Reflection.AreNotAssignable(typeOfExpectedReturnValue, typeOfActionResult);
            if (isAssignableCheck && allowDifferentGenericTypeDefinitions
                && Reflection.IsGeneric(typeOfExpectedReturnValue) && Reflection.IsGenericTypeDefinition(typeOfExpectedReturnValue))
            {
                isAssignableCheck = Reflection.AreAssignableByGeneric(typeOfExpectedReturnValue, typeOfActionResult);

                if (!isAssignableCheck && !typeInfoOfActionResult.IsGenericType)
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
            if (invalid && typeInfoOfExpectedReturnValue.IsGenericTypeDefinition && typeInfoOfActionResult.IsGenericType)
            {
                var actionResultGenericDefinition = typeInfoOfActionResult.GetGenericTypeDefinition();
                if (actionResultGenericDefinition == typeOfExpectedReturnValue)
                {
                    invalid = false;
                }
            }

            if (invalid && typeInfoOfExpectedReturnValue.IsGenericType && typeInfoOfActionResult.IsGenericType)
            {
                invalid = !Reflection.AreAssignableByGeneric(typeOfExpectedReturnValue, typeOfActionResult);
            }

            if (invalid)
            {
                ThrowNewInvocationResultAssertionException(
                    testContext,
                    typeOfExpectedReturnValue.ToFriendlyTypeName(),
                    typeOfActionResult.ToFriendlyTypeName());
            }
        }

        public static void ValidateInvocationResultType<TExpectedType>(
            ComponentTestContext testContext,
            bool canBeAssignable = false,
            bool allowDifferentGenericTypeDefinitions = false)
        {
            ValidateInvocationResultType(testContext, typeof(TExpectedType), canBeAssignable, allowDifferentGenericTypeDefinitions);
        }

        public static void ValidateInvocationResult<TResult>(ComponentTestContext testContext, TResult model)
        {
            ValidateInvocationResultType<TResult>(testContext);

            if (Reflection.AreNotDeeplyEqual(model, testContext.MethodResult))
            {
                throw ResponseModelAssertionException.From(testContext.ExceptionMessagePrefix);
            }

            testContext.Model = model;
        }
        
        public static TResult GetInvocationResult<TResult>(ComponentTestContext testContext)
            where TResult : class
        {
            ValidateInvocationResultType<TResult>(testContext);
            return testContext.MethodResult as TResult;
        }

        private static void ThrowNewInvocationResultAssertionException(
            ComponentTestContext testContext,
            string typeNameOfExpectedReturnValue,
            string typeNameOfActionResult)
        {
            throw new InvocationResultAssertionException(string.Format(
                "{0} result to be {1}, but instead received {2}.",
                testContext.ExceptionMessagePrefix,
                typeNameOfExpectedReturnValue,
                typeNameOfActionResult));
        }
    }
}
