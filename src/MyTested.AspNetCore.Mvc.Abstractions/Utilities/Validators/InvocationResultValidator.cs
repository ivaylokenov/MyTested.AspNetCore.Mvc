﻿namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using System;
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

            if (invalid)
            {
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

        public static void ValidateInvocationResult<TResult>(ComponentTestContext testContext, TResult model)
        {
            if (!Reflection.IsAnonymousType(typeof(TResult)))
            {
                ValidateInvocationResultType<TResult>(testContext);
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
