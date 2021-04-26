namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;
    using Utilities.Extensions;

    public class DeepEqualityResult
    {
        private readonly Type expectedType;
        private readonly Type actualType;

        private readonly Stack<string> expectedPathParts;
        private readonly Stack<string> actualPathParts;

        public DeepEqualityResult(Type expected, Type actual)
        {
            this.expectedType = expected;
            this.actualType = actual;

            var initialExpectedName = expectedType?.ToFriendlyTypeName();
            var initialActualName = actualType?.ToFriendlyTypeName();

            this.expectedPathParts = new Stack<string>();
            this.actualPathParts = new Stack<string>();

            this.expectedPathParts.Push(initialExpectedName ?? initialActualName);
            this.actualPathParts.Push(initialActualName ?? initialExpectedName);
        }

        public bool Result { get; private set; } = true;

        public object ExpectedValue { get; private set; }

        public object ActualValue { get; private set; }

        internal DeepEqualityResult Success
        {
            get
            {
                this.Result = true;
                return this;
            }
        }

        internal DeepEqualityResult Failure
        {
            get
            {
                this.Result = false;
                return this;
            }
        }

        public override string ToString()
        {
            if (this.Result)
            {
                return "Objects are deeply equal";
            }

            var singlePathPart = expectedPathParts.Count == 1;
            var pathMessage = string.Empty;

            if (singlePathPart)
            {
                if (this.expectedType != null && this.actualType != null && expectedType != actualType)
                {
                    var (expectedTypeName, actualTypeName) = (this.expectedType, this.actualType).GetTypeComparisonNames();

                    return $"Expected a value of {expectedTypeName} type, but in fact it was {actualTypeName}";
                }
            }
            else
            {
                var expectedPath = string.Join(".", this.expectedPathParts.Reverse()).Replace(".[", "[");

                pathMessage = $"Difference occurs at '{expectedPath}'";
            }

            var valuesMessage = string.Empty;
            if (singlePathPart || (this.ExpectedValue != null && this.ActualValue != null))
            {
                valuesMessage = $"{(pathMessage.Length > 0 ? ". " : string.Empty)}Expected a value of {this.ExpectedValue.GetErrorMessageName()}, but in fact it was {this.ActualValue.GetErrorMessageName()}";
            }

            return $"{pathMessage}{valuesMessage}";
        }

        internal DeepEqualityResult PushPath(string path)
        {
            this.expectedPathParts.Push(path);
            this.actualPathParts.Push(path);
            return this;
        }

        internal DeepEqualityResult PushPath(string expectedPath, string actualPath)
        {
            this.expectedPathParts.Push(expectedPath);
            this.actualPathParts.Push(actualPath);
            return this;
        }

        internal DeepEqualityResult PopPath()
        {
            this.expectedPathParts.Pop();
            this.actualPathParts.Pop();
            return this;
        }

        internal DeepEqualityResult ApplyValues(object expected, object actual)
        {
            this.ExpectedValue = expected;
            this.ActualValue = actual;
            return this;
        }

        internal DeepEqualityResult ClearValues()
        {
            this.ExpectedValue = null;
            this.ActualValue = null;
            return this;
        }

        public static implicit operator bool(DeepEqualityResult deepEqualResult) => deepEqualResult.Result;
    }
}
