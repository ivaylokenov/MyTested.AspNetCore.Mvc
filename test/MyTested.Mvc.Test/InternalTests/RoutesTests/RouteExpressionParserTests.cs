namespace MyTested.Mvc.Tests.InternalTests.RoutesTests
{
    using Internal.Routes;
    using Setups.Routes;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Xunit;

    public class RouteExpressionParserTests
    {
        [Theory]
        [MemberData(nameof(NormalActionsWithNoParametersData))]
        public void ParseControllerAndActionWithoutParametersControllerAndActionNameAreParsed(
            Expression<Action<NormalController>> action, string controllerName, string actionName)
        {
            var result = RouteExpressionParser.Parse<NormalController>(action);

            Assert.Equal(controllerName, result.ControllerName);
            Assert.Equal(actionName, result.Action);
            Assert.Equal(2, result.RouteValues.Count);
            Assert.True(result.RouteValues.ContainsKey("controller"));
            Assert.Equal(controllerName, result.RouteValues["controller"].Value);
            Assert.True(result.RouteValues.ContainsKey("action"));
            Assert.Equal(actionName, result.RouteValues["action"].Value);
        }

        [Theory]
        [MemberData(nameof(NormalActionssWithParametersData))]
        public void ParseControllerAndActionWithPrimitiveParametersControllerActionNameAndParametersAreParsed(
            Expression<Action<NormalController>> action, string controllerName, string actionName, IDictionary<string, object> routeValues)
        {
            var result = RouteExpressionParser.Parse<NormalController>(action);

            Assert.Equal(controllerName, result.ControllerName);
            Assert.Equal(actionName, result.Action);
            Assert.Equal(routeValues.Count, result.RouteValues.Count);

            foreach (var routeValue in routeValues)
            {
                Assert.True(result.RouteValues.ContainsKey(routeValue.Key));
                Assert.Equal(routeValue.Value, result.RouteValues[routeValue.Key].Value);
            }
        }

        [Fact]
        public void ParseControllerAndActionWithObjectParametersControllerActionNameAndParametersAreParsed()
        {
            Expression<Action<NormalController>> expr = c => c.ActionWithMultipleParameters(1, "string", new RequestModel { Integer = 1, String = "Text" });
            var result = RouteExpressionParser.Parse<NormalController>(expr);

            Assert.Equal("Normal", result.ControllerName);
            Assert.Equal("ActionWithMultipleParameters", result.Action);
            Assert.Equal(5, result.RouteValues.Count);
            Assert.True(result.RouteValues.ContainsKey("controller"));
            Assert.Equal("Normal", result.RouteValues["controller"].Value);
            Assert.True(result.RouteValues.ContainsKey("action"));
            Assert.Equal("ActionWithMultipleParameters", result.RouteValues["action"].Value);
            Assert.Equal(1, result.RouteValues["id"].Value);
            Assert.Equal("string", result.RouteValues["text"].Value);
            Assert.IsAssignableFrom<RequestModel>(result.RouteValues["model"].Value);

            var model = (RequestModel)result.RouteValues["model"].Value;
            Assert.Equal(1, model.Integer);
            Assert.Equal("Text", model.String);
        }

        [Fact]
        public void ParsePocoControllerControllerActionNameAndParametersAreParsed()
        {
            Expression<Action<PocoController>> expr = c => c.Action(1);
            var result = RouteExpressionParser.Parse<PocoController>(expr);

            Assert.Equal("Poco", result.ControllerName);
            Assert.Equal("Action", result.Action);
            Assert.Equal(3, result.RouteValues.Count);
            Assert.True(result.RouteValues.ContainsKey("controller"));
            Assert.Equal("Poco", result.RouteValues["controller"].Value);
            Assert.True(result.RouteValues.ContainsKey("action"));
            Assert.Equal("Action", result.RouteValues["action"].Value);
            Assert.True(result.RouteValues.ContainsKey("id"));
            Assert.Equal(1, result.RouteValues["id"].Value);
        }

        [Fact]
        public void ParseInAreaControllerControllerActionNameAndAreaAreParsed()
        {
            Expression<Action<InAreaController>> expr = c => c.Action(1);
            var result = RouteExpressionParser.Parse<InAreaController>(expr);

            Assert.Equal("InArea", result.ControllerName);
            Assert.Equal("Action", result.Action);
            Assert.Equal(4, result.RouteValues.Count);
            Assert.True(result.RouteValues.ContainsKey("controller"));
            Assert.Equal("InArea", result.RouteValues["controller"].Value);
            Assert.True(result.RouteValues.ContainsKey("action"));
            Assert.Equal("Action", result.RouteValues["action"].Value);
            Assert.True(result.RouteValues.ContainsKey("id"));
            Assert.Equal(1, result.RouteValues["id"].Value);
            Assert.True(result.RouteValues.ContainsKey("area"));
            Assert.Equal("MyArea", result.RouteValues["area"].Value);
        }

        [Fact]
        public void ParseActionWithCustomRouteConstraintsRouteConstraintsAreParsed()
        {
            Expression<Action<RouteConstraintController>> expr = c => c.Action(1, 2);
            var result = RouteExpressionParser.Parse<RouteConstraintController>(expr);

            Assert.Equal("CustomController", result.ControllerName);
            Assert.Equal("CustomAction", result.Action);
            Assert.Equal(5, result.RouteValues.Count);
            Assert.True(result.RouteValues.ContainsKey("controller"));
            Assert.Equal("CustomController", result.RouteValues["controller"].Value);
            Assert.True(result.RouteValues.ContainsKey("action"));
            Assert.Equal("CustomAction", result.RouteValues["action"].Value);
            Assert.True(result.RouteValues.ContainsKey("id"));
            Assert.Equal("5", result.RouteValues["id"].Value);
            Assert.True(result.RouteValues.ContainsKey("key"));
            Assert.Equal("value", result.RouteValues["key"].Value);
            Assert.True(result.RouteValues.ContainsKey("anotherId"));
            Assert.Equal(2, result.RouteValues["anotherId"].Value);
        }

        [Fact]
        public void ParseCustomConventionsCustomConventionsAreParsed()
        {
            Expression<Action<ConventionsController>> expr = c => c.ConventionsAction(1);
            var result = RouteExpressionParser.Parse<ConventionsController>(expr);

            Assert.Equal("ChangedController", result.ControllerName);
            Assert.Equal("ChangedAction", result.Action);
            Assert.Equal(3, result.RouteValues.Count);
            Assert.True(result.RouteValues.ContainsKey("controller"));
            Assert.Equal("ChangedController", result.RouteValues["controller"].Value);
            Assert.True(result.RouteValues.ContainsKey("action"));
            Assert.Equal("ChangedAction", result.RouteValues["action"].Value);
            Assert.True(result.RouteValues.ContainsKey("ChangedParameter"));
            Assert.Equal(1, result.RouteValues["ChangedParameter"].Value);
        }

        [Fact]
        public void ParseStaticMethodCallThrowsInvalidOperationException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                Expression<Action<NormalController>> expr = c => NormalController.StaticCall();
                RouteExpressionParser.Parse<NormalController>(expr);
            });

            Assert.Equal("Provided expression is not valid - expected instance method call but instead received static method call.", exception.Message);
        }

        [Fact]
        public void ParseNonMethodCallExceptionThrowsInvalidOperationException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                Expression<Action<NormalController>> expr = c => new object();
                RouteExpressionParser.Parse<NormalController>(expr);
            });

            Assert.Equal("Provided expression is not valid - expected instance method call but instead received other type of expression.", exception.Message);
        }

        [Fact]
        public void ParseNonControllerActionThrowsInvalidOperationException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                Expression<Action<RequestModel>> expr = c => c.SomeMethod();
                RouteExpressionParser.Parse<RequestModel>(expr);
            });

            Assert.Equal("Method SomeMethod in class RequestModel is not a valid controller action.", exception.Message);
        }

        public static TheoryData<Expression<Action<NormalController>>, string, string> NormalActionsWithNoParametersData
        {
            get
            {
                var data = new TheoryData<Expression<Action<NormalController>>, string, string>();

                const string controllerName = "Normal";
                data.Add(c => c.ActionWithoutParameters(), controllerName, "ActionWithoutParameters");
                data.Add(c => c.ActionWithOverloads(), controllerName, "ActionWithOverloads");
                data.Add(c => c.VoidAction(), controllerName, "VoidAction");
                data.Add(c => c.ActionWithChangedName(), controllerName, "AnotherName");

                return data;
            }
        }

        public static TheoryData<
            Expression<Action<NormalController>>,
            string,
            string,
            IDictionary<string, object>> NormalActionssWithParametersData
        {
            get
            {
                var data = new TheoryData<Expression<Action<NormalController>>, string, string, IDictionary<string, object>>();

                const string controllerName = "Normal";
                string actionName = "ActionWithOverloads";
                data.Add(
                    c => c.ActionWithOverloads(1),
                    controllerName,
                    actionName,
                    new Dictionary<string, object> { ["controller"] = controllerName, ["action"] = actionName, ["id"] = 1 });

                actionName = "ActionWithOverloads";
                data.Add(
                    c => c.ActionWithOverloads(With.No<int>()),
                    controllerName,
                    actionName,
                    new Dictionary<string, object> { ["controller"] = controllerName, ["action"] = actionName });

                actionName = "ActionWithOverloads";
                data.Add(
                    c => c.ActionWithOverloads(GetInt()),
                    controllerName,
                    actionName,
                    new Dictionary<string, object> { ["controller"] = controllerName, ["action"] = actionName, ["id"] = 1 });

                actionName = "ActionWithMultipleParameters";
                data.Add(
                    c => c.ActionWithMultipleParameters(1, "string", null),
                    controllerName,
                    actionName,
                    new Dictionary<string, object> { ["controller"] = controllerName, ["action"] = actionName, ["id"] = 1, ["text"] = "string", ["model"] = null });

                return data;
            }
        }

        private static int GetInt()
        {
            return 1;
        }
    }
}
