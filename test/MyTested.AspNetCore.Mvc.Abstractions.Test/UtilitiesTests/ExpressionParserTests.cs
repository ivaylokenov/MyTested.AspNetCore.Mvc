﻿namespace MyTested.AspNetCore.Mvc.Test.UtilitiesTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Utilities;
    using Xunit;

    public class ExpressionParserTests
    {
        [Fact]
        public void GetMethodNameShouldReturnCorrectMethodNameWithValidMethodCallExpression()
        {
            Expression<Func<MvcController, IActionResult>> expression = c => c.OkResultAction();
            var methodName = ExpressionParser.GetMethodName(expression);
            Assert.Equal("OkResultAction", methodName);
        }

        [Fact]
        public void GetMethodNameShouldThrowArgumentExceptionWithInvalidMethodCallExpression()
        {
            Expression<Func<int>> expression = () => 0;

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    ExpressionParser.GetMethodName(expression);
                },
                "Provided expression is not valid - expected instance method call but instead received other type of expression.");
        }

        [Fact]
        public void GetMethodNameShouldThrowExceptionWithStaticMethodCall()
        {
            Expression<Func<int>> expression = () => int.Parse("0");

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    ExpressionParser.GetMethodName(expression);
                },
                "Provided expression is not valid - expected instance method call but instead received static method call.");
        }

        [Fact]
        public void ResolveMethodArgumentsShouldReturnCorrectCollectionOfArgumentsInformation()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            Expression<Func<MvcController, IActionResult>> expression = c => c.OkResultActionWithRequestBody(1, requestModel);

            var result = ExpressionParser.ResolveMethodArguments(expression);

            var arguments = result as IList<MethodArgumentTestContext> ?? result.ToArray();
            var firstArgument = arguments.First();
            var secondArgument = arguments.Last();
            var secondArgumentAsRequestModel = secondArgument.Value as RequestModel;

            Assert.Equal("id", firstArgument.Name);
            Assert.Equal(typeof(int), firstArgument.Type);
            Assert.Equal(1, firstArgument.Value);

            Assert.Equal("model", secondArgument.Name);
            Assert.Equal(typeof(RequestModel), secondArgument.Type);
            Assert.NotNull(secondArgumentAsRequestModel);
            Assert.Equal(2, secondArgumentAsRequestModel.Integer);
            Assert.Equal("Test", secondArgumentAsRequestModel.RequiredString);
        }

        [Fact]
        public void ResolveMethodArgumentsShouldReturnEmptyCollectionIfMethodDoesNotHaveArguments()
        {
            Expression<Func<MvcController, IActionResult>> expression = c => c.OkResultAction();
            var result = ExpressionParser.ResolveMethodArguments(expression);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ResolveMethodArgumentsShouldReturnProperArgumentsWithNullValues()
        {
            Expression<Func<MvcController, IActionResult>> expression = c => c.OkResultActionWithRequestBody(1, null);
            var result = ExpressionParser.ResolveMethodArguments(expression).ToList();

            var first = result[0];
            var second = result[1];

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, first.Value);
            Assert.Null(second.Value);
        }

        [Fact]
        public void ResolveMethodArgumentsShouldThrowArgumentExceptionWithInvalidMethodCallExpression()
        {
            Expression<Func<int>> expression = () => 0;

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    ExpressionParser.ResolveMethodArguments(expression);
                },
                "Provided expression is not valid - expected instance method call but instead received other type of expression.");
        }

        [Fact]
        public void GetPropertyNameShouldReturnProperMemberNameWithValidExpression()
        {
            Expression<Func<MvcController, object>> expression = c => c.InjectedService;
            var result = ExpressionParser.GetPropertyName(expression);

            Assert.Equal("InjectedService", result);
        }

        [Fact]
        public void GetPropertyNameShouldThrowExceptionWithInvalidMemberExpression()
        {
            Expression<Func<MvcController, object>> expression = c => c.OkResultWithResponse();

            Test.AssertException<ArgumentException>(
                () =>
                {
                    ExpressionParser.GetPropertyName(expression);
                }, 
                "Provided expression is not a valid member expression.");
        }

        [Fact]
        public void GetMethodAttributesShouldReturnProperAttributes()
        {
            Expression<Func<MvcController, object>> expression = c => c.VariousAttributesAction();
            var attributes = Reflection.GetCustomAttributes(((MethodCallExpression)expression.Body).Method).Select(a => a.GetType()).ToList();

            var expectedTypes = new List<Type>
            {
                typeof(AllowAnonymousAttribute),
                typeof(RouteAttribute),
                typeof(ActionNameAttribute),
                typeof(NonActionAttribute),
                typeof(AcceptVerbsAttribute),
                typeof(HttpDeleteAttribute),
                typeof(SkipStatusCodePagesAttribute),
                typeof(ResponseCacheAttribute)
            };

            Assert.NotNull(attributes);
            Assert.Equal(11, attributes.Count);

            var allAttributesArePresent = expectedTypes.All(attributes.Contains);
            Assert.True(allAttributesArePresent);
        }
    }
}
