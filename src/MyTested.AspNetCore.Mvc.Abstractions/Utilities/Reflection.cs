namespace MyTested.AspNetCore.Mvc.Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Extensions;
    using Internal;
    using Microsoft.AspNetCore.Routing;

    /// <summary>
    /// Class for validating reflection checks.
    /// </summary>
    public static class Reflection
    {
        private static readonly ConcurrentDictionary<Type, ConstructorInfo> TypesWithOneConstructorCache = new ConcurrentDictionary<Type, ConstructorInfo>();
        private static readonly ConcurrentDictionary<Type, object> TypeAttributesCache = new ConcurrentDictionary<Type, object>();
        private static readonly ConcurrentDictionary<MethodInfo, IEnumerable<object>> MethodAttributesCache = new ConcurrentDictionary<MethodInfo, IEnumerable<object>>();
        private static readonly ConcurrentDictionary<Type, string> FriendlyTypeNames = new ConcurrentDictionary<Type, string>();
        private static readonly ConcurrentDictionary<Type, string> FullFriendlyTypeNames = new ConcurrentDictionary<Type, string>();

        /// <summary>
        /// Checks whether two objects have the same types.
        /// </summary>
        /// <param name="firstObject">First object to be checked.</param>
        /// <param name="secondObject">Second object to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool AreSameTypes(object firstObject, object secondObject)
        {
            if (firstObject == null || secondObject == null)
            {
                return firstObject == secondObject;
            }

            return AreSameTypes(firstObject.GetType(), secondObject.GetType());
        }

        /// <summary>
        /// Checks whether two types are different.
        /// </summary>
        /// <param name="firstType">First type to be checked.</param>
        /// <param name="secondType">Second type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool AreSameTypes(Type firstType, Type secondType)
            => firstType == secondType;

        /// <summary>
        /// Checks whether two objects have different types.
        /// </summary>
        /// <param name="firstObject">First object to be checked.</param>
        /// <param name="secondObject">Second object to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool AreDifferentTypes(object firstObject, object secondObject)
            => !AreSameTypes(firstObject, secondObject);

        /// <summary>
        /// Checks whether two types are different.
        /// </summary>
        /// <param name="firstType">First type to be checked.</param>
        /// <param name="secondType">Second type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool AreDifferentTypes(Type firstType, Type secondType)
            => !AreSameTypes(firstType, secondType);

        /// <summary>
        /// Checks whether two types are assignable.
        /// </summary>
        /// <param name="baseType">Base type to be checked.</param>
        /// <param name="inheritedType">Inherited type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool AreAssignable(Type baseType, Type inheritedType)
            => baseType.IsAssignableFrom(inheritedType);

        /// <summary>
        /// Checks whether two types are not assignable.
        /// </summary>
        /// <param name="baseType">Base type to be checked.</param>
        /// <param name="inheritedType">Inherited type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool AreNotAssignable(Type baseType, Type inheritedType)
            => !AreAssignable(baseType, inheritedType);

        /// <summary>
        /// Checks whether a type is generic.
        /// </summary>
        /// <param name="type">Type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool IsGeneric(Type type)
            => type.GetTypeInfo().IsGenericType;

        /// <summary>
        /// Checks whether a type is not generic.
        /// </summary>
        /// <param name="type">Type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool IsNotGeneric(Type type)
            => !IsGeneric(type);

        /// <summary>
        /// Checks whether a type is generic definition.
        /// </summary>
        /// <param name="type">Type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool IsGenericTypeDefinition(Type type)
            => type.GetTypeInfo().IsGenericTypeDefinition;

        public static bool HasGenericTypeDefinition(Type type, Type genericTypeDefinition)
            => type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition;

        /// <summary>
        /// Checks whether two types are assignable by generic definition.
        /// </summary>
        /// <param name="baseType">Base type to be checked.</param>
        /// <param name="inheritedType">Inherited type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool AreAssignableByGeneric(Type baseType, Type inheritedType)
            => IsGeneric(inheritedType) 
                && IsGeneric(baseType) 
                && baseType.IsAssignableFrom(inheritedType.GetGenericTypeDefinition());

        /// <summary>
        /// Checks whether two generic types have different generic arguments.
        /// </summary>
        /// <param name="baseType">Base type to be checked.</param>
        /// <param name="inheritedType">Inherited type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool HaveDifferentGenericArguments(Type baseType, Type inheritedType)
        {
            if (IsNotGeneric(baseType) || IsNotGeneric(inheritedType))
            {
                return true;
            }

            var baseTypeGenericArguments = baseType.GetGenericArguments();
            var inheritedTypeGenericArguments = inheritedType.GetGenericArguments();

            if (baseTypeGenericArguments.Length != inheritedTypeGenericArguments.Length)
            {
                return true;
            }

            return baseTypeGenericArguments.Where((t, i) => t != inheritedTypeGenericArguments[i]).Any();
        }

        /// <summary>
        /// Checks whether generic definition contains an base interface generic definition.
        /// </summary>
        /// <param name="baseType">Base type to be checked.</param>
        /// <param name="inheritedType">Inherited type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool ContainsGenericTypeDefinitionInterface(Type baseType, Type inheritedType)
            => inheritedType
                .GetInterfaces()
                .Where(t => t.GetTypeInfo().IsGenericType)
                .Select(t => t.GetGenericTypeDefinition())
                .Any(t => t == baseType);

        /// <summary>
        /// Performs dynamic casting from type to generic result.
        /// </summary>
        /// <typeparam name="TResult">Result type from casting.</typeparam>
        /// <param name="type">Type from which the casting should be done.</param>
        /// <param name="data">Object from which the casting should be done.</param>
        /// <returns>Cast object of type TResult.</returns>
        public static TResult CastTo<TResult>(this Type type, object data)
        {
            var dataParam = Expression.Parameter(typeof(object), "data");
            var firstConvert = Expression.Convert(dataParam, data.GetType());
            var secondConvert = Expression.Convert(firstConvert, type);
            var body = Expression.Block(secondConvert);

            var run = Expression.Lambda(body, dataParam).Compile();
            var ret = run.DynamicInvoke(data);
            return (TResult)ret;
        }

        /// <summary>
        /// Transforms generic type name to friendly one, showing generic type arguments.
        /// </summary>
        /// <param name="type">Type which name will be transformed.</param>
        /// <param name="useFullName">Indicates whether the type name should be full ot not.</param>
        /// <returns>Transformed name as string.</returns>
        public static string ToFriendlyTypeName(this Type type, bool useFullName = false)
        {
            if (type == null)
            {
                return "null";
            }

            return useFullName
                ? FullFriendlyTypeNames
                    .GetOrAdd(type, _ => GetFriendlyTypeName(type, true))
                : FriendlyTypeNames
                    .GetOrAdd(type, _ => GetFriendlyTypeName(type, false));
        }

        public static T TryFastCreateInstance<T>()
            where T : class
        {
            try
            {
                return New<T>.Instance();
            }
            catch
            {
                return null;
            }
        }

        public static T TryCreateInstance<T>(params object[] constructorParameters)
            where T : class
            => TryCreateInstance<T>(constructorParameters.ToDictionary(k => k?.GetType()));

        /// <summary>
        /// Tries to create instance of type T by using the provided unordered constructor parameters.
        /// </summary>
        /// <typeparam name="T">Type of created instance.</typeparam>
        /// <param name="constructorParameters">Unordered constructor parameters.</param>
        /// <returns>Created instance or null, if no suitable constructor found.</returns>
        public static T TryCreateInstance<T>(IDictionary<Type, object> constructorParameters = null)
            where T : class
        {
            var type = typeof(T);
            T instance = null;

            try
            {
                constructorParameters ??= new Dictionary<Type, object>();
                instance = Activator.CreateInstance(type, constructorParameters.Select(p => p.Value).ToArray()) as T;
            }
            catch (Exception)
            {
                if (constructorParameters == null || constructorParameters.Count == 0)
                {
                    return instance;
                }

                var constructorParameterTypes = constructorParameters
                    .Select(cp => cp.Key)
                    .ToList();

                var constructor = type.GetConstructorByUnorderedParameters(constructorParameterTypes);
                if (constructor == null)
                {
                    return instance;
                }

                var constructorParameterInfos = constructor.GetParameters();

                var selectedConstructorParameters = constructorParameterInfos
                    .Select(cp => cp.ParameterType)
                    .ToList();

                var resultParameters = new List<object>();
                foreach (var selectedConstructorParameterType in selectedConstructorParameters)
                {
                    foreach (var constructorParameterType in constructorParameterTypes)
                    {
                        if (selectedConstructorParameterType.IsAssignableFrom(constructorParameterType))
                        {
                            resultParameters.Add(constructorParameters[constructorParameterType]);
                            break;
                        }
                    }
                }

                if (selectedConstructorParameters.Count != resultParameters.Count)
                {
                    return instance;
                }

                instance = constructor.Invoke(resultParameters.ToArray()) as T;
            }

            return instance;
        }

        /// <summary>
        /// Gets custom attributes on the provided object.
        /// </summary>
        /// <param name="obj">Object decorated with custom attribute.</param>
        /// <returns>IEnumerable of objects representing the custom attributes.</returns>
        public static IEnumerable<object> GetCustomAttributes(object obj)
        {
            CacheComponentAttributes(obj);
            return TypeAttributesCache.Values;
        }

        /// <summary>
        /// Gets custom attributes including inherited ones on the provided object.
        /// </summary>
        /// <param name="obj">Object decorated with custom attribute.</param>
        /// <returns>IEnumerable of objects representing the custom attributes.</returns>
        public static IEnumerable<object> GetCustomAttributesIncludingInherited(object obj)
        {
            CacheComponentAttributes(obj, true);
            return TypeAttributesCache.Values;
        }

        public static IEnumerable<object> GetCustomAttributes(MethodInfo method)
            => MethodAttributesCache
                .GetOrAdd(method, _ => method.GetCustomAttributes(false));

        /// <summary>
        /// Checks whether two objects are deeply equal by reflecting all their public properties recursively. Resolves successfully value and reference types, overridden Equals method, custom == operator, IComparable, nested objects and collection properties.
        /// </summary>
        /// <param name="expected">Expected object.</param>
        /// <param name="actual">Actual object.</param>
        /// <returns>True or false.</returns>
        public static bool AreDeeplyEqual(object expected, object actual)
            => AreDeeplyEqual(expected, actual, out _);

        /// <summary>
        /// Checks whether two objects are deeply equal by reflecting all their public properties recursively. Resolves successfully value and reference types, overridden Equals method, custom == operator, IComparable, nested objects and collection properties.
        /// </summary>
        /// <param name="expected">Expected object.</param>
        /// <param name="actual">Actual object.</param>
        /// <param name="result">Result object containing differences between the two objects.</param>
        /// <returns>True or false.</returns>
        public static bool AreDeeplyEqual(object expected, object actual, out DeepEqualityResult result)
        {
            result = new DeepEqualityResult(expected?.GetType(), actual?.GetType());

            return AreDeeplyEqual(expected, actual, new ConditionalWeakTable<object, object>(), result);
        }

        /// <summary>
        /// Checks whether two objects are not deeply equal by reflecting all their public properties recursively. Resolves successfully value and reference types, overridden Equals method, custom == operator, IComparable, nested objects and collection properties.
        /// </summary>
        /// <param name="expected">Expected object.</param>
        /// <param name="actual">Actual object.</param>
        /// <returns>True or false.</returns>
        /// <remarks>This method is used for the route testing. Since the ASP.NET Core MVC model binder creates new instances, circular references are not checked.</remarks>
        public static bool AreNotDeeplyEqual(object expected, object actual)
            => AreNotDeeplyEqual(expected, actual, out _);

        /// <summary>
        /// Checks whether two objects are not deeply equal by reflecting all their public properties recursively. Resolves successfully value and reference types, overridden Equals method, custom == operator, IComparable, nested objects and collection properties.
        /// </summary>
        /// <param name="expected">Expected object.</param>
        /// <param name="actual">Actual object.</param>
        /// <param name="result">Result object containing differences between the two objects.</param>
        /// <returns>True or false.</returns>
        /// <remarks>This method is used for the route testing. Since the ASP.NET Core MVC model binder creates new instances, circular references are not checked.</remarks>
        public static bool AreNotDeeplyEqual(object expected, object actual, out DeepEqualityResult result)
            => !AreDeeplyEqual(expected, actual, out result);

        /// <summary>
        /// Copies the non-null property values from one object to another.
        /// </summary>
        /// <param name="source">Source object for the property values.</param>
        /// <param name="destination">Destination object for the property values.</param>
        public static void CopyProperties(object source, object destination)
        {
            var sourceType = source?.GetType();
            var destinationType = destination?.GetType();

            if (sourceType == null || destinationType == null || sourceType != destinationType)
            {
                throw new InvalidOperationException("Cannot copy properties from or to null objects, and when the object types are different.");
            }

            var properties = ObjectPropertyHelper.GetProperties(sourceType);

            foreach (var property in properties.PropertyDelegates)
            {
                var originalValue = property.Getter(source);
                if (originalValue != null)
                {
                    property.Setter(destination, originalValue);
                }
            }
        }

        /// <summary>
        /// Creates a delegate from an object by providing MethodInfo filter.
        /// </summary>
        /// <typeparam name="TDelegate">Type of delegate to create.</typeparam>
        /// <param name="instance">Object to get method from.</param>
        /// <param name="methodFilter">Filter to find the method.</param>
        /// <returns>Delegate of type TDelegate.</returns>
        public static TDelegate CreateDelegateFromMethod<TDelegate>(object instance, Func<MethodInfo, bool> methodFilter)
            where TDelegate : class
        {
            if (!typeof(TDelegate).GetTypeInfo().IsSubclassOf(typeof(Delegate)))
            {
                return null;
            }

            return instance
                .GetType()
                .GetTypeInfo()
                .DeclaredMethods
                .FirstOrDefault(methodFilter)
                ?.CreateDelegate(typeof(TDelegate), instance) as TDelegate;
        }
        
        /// <summary>
        /// Checks whether property with the provided name exists in a dynamic object.
        /// </summary>
        /// <param name="dynamicObject">Dynamic object to check.</param>
        /// <param name="propertyName">Property name to check.</param>
        /// <returns>True or False.</returns>
        public static bool DynamicPropertyExists(dynamic dynamicObject, string propertyName)
            => dynamicObject.GetType().GetProperty(propertyName) != null;

        public static bool IsAnonymousType(Type type)
        {
            if (!(type.Name.StartsWith("<>") || type.Name.StartsWith("VB$")))
            {
                return false;
            }

            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsDefined(typeof(CompilerGeneratedAttribute), false)
                && typeInfo.IsGenericType
                && (type.Name.Contains("AnonymousType") || type.Name.Contains("AnonType"))
                && typeInfo.Attributes.HasFlag(TypeAttributes.NotPublic);
        }

        public static MethodInfo GetNonPublicMethod(Type type, string name)
            => type.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

        private static bool IsDateTimeRelated(this Type type)
            => type == typeof(DateTime)
            || type == typeof(DateTime?)
            || type == typeof(TimeSpan)
            || type == typeof(TimeSpan?)
            || type == typeof(DateTimeOffset)
            || type == typeof(DateTimeOffset?);

        private static ConstructorInfo GetConstructorByUnorderedParameters(this Type type, IEnumerable<Type> types)
        {
            if (TypesWithOneConstructorCache.TryGetValue(type, out var cachedConstructor))
            {
                return cachedConstructor;
            }

            var allConstructors = type.GetConstructors();
            if (allConstructors.Length == 1)
            {
                var singleConstructor = allConstructors[0];
                TypesWithOneConstructorCache.TryAdd(type, singleConstructor);
                return singleConstructor;
            }

            var orderedTypes = types
                .OrderBy(t => t.FullName)
                .ToArray();

            return allConstructors
                .Where(c =>
                {
                    var parameters = c.GetParameters();
                    if (orderedTypes.Length != parameters.Length)
                    {
                        return false;
                    }

                    var parameterTypes = parameters
                        .OrderBy(p => p.ParameterType.FullName)
                        .Select(p => p.ParameterType)
                        .ToArray();

                    return !orderedTypes.Where((t, i) => !parameterTypes[i].IsAssignableFrom(t)).Any();
                })
                .FirstOrDefault();
        }

        private static bool AreDeeplyEqual(
            object expected, 
            object actual, 
            ConditionalWeakTable<object, object> processedElements,
            DeepEqualityResult result)
        {
            result.ApplyValues(expected, actual);

            if (expected == null && actual == null)
            {
                return result.Success;
            }

            if (expected == null || actual == null)
            {
                return result.Failure;
            }

            var expectedType = expected.GetType();

            if (expectedType != typeof(string) && !expectedType.GetTypeInfo().IsValueType)
            {
                if (processedElements.TryGetValue(expected, out _))
                {
                    return result.Success;
                }

                processedElements.Add(expected, expected);
            }

            var actualType = actual.GetType();
            var objectType = typeof(object);

            if ((expectedType == objectType && actualType != objectType)
                || (actualType == objectType && expectedType != objectType))
            {
                return result.Failure;
            }

            var stringType = typeof(string);

            if (expected is IEnumerable && expectedType != stringType)
            {
                return CollectionsAreDeeplyEqual(expected, actual, processedElements, result);
            }

            var expectedTypeIsAnonymous = IsAnonymousType(expectedType);
            if (expectedTypeIsAnonymous)
            {
                var actualIsAnonymous = IsAnonymousType(actualType);
                if (!actualIsAnonymous)
                {
                    return result.Failure;
                }
            }

            if (!expectedTypeIsAnonymous
                && expectedType != actualType
                && !expectedType.IsAssignableFrom(actualType)
                && !actualType.IsAssignableFrom(expectedType))
            {
                return result.Failure;
            }

            if (expectedType.GetTypeInfo().IsPrimitive || expectedType.GetTypeInfo().IsEnum)
            {
                return expected.ToString() == actual.ToString()
                    ? result.Success
                    : result.Failure;
            }
            
            var equalsOperator = expectedType.GetMethods().FirstOrDefault(m => m.Name == "op_Equality");
            if (equalsOperator != null)
            {
                var equalsOperatorResult = (bool)equalsOperator.Invoke(null, new[] { expected, actual });
                if (!equalsOperatorResult && expectedType != stringType)
                {
                    result.PushPath("== (Equality Operator)");
                    if (!expectedType.IsDateTimeRelated())
                    {
                        result.ClearValues();
                    }
                }

                return equalsOperatorResult
                    ? result.Success
                    : result.Failure;
            }

            if (expectedType != objectType && !expectedTypeIsAnonymous)
            {
                var equalsMethod = expectedType.GetMethods().FirstOrDefault(m => m.Name == "Equals" && m.DeclaringType == expectedType);
                if (equalsMethod != null)
                {
                    var equalsMethodResult = (bool)equalsMethod.Invoke(expected, new[] { actual });
                    if (!equalsMethodResult)
                    {
                        result
                            .PushPath("Equals()")
                            .ClearValues();
                    }

                    return equalsMethodResult
                        ? result.Success
                        : result.Failure;
                }
            }

            if (ComparablesAreDeeplyEqual(expected, actual, result))
            {
                return result.Success;
            }

            if (!ObjectPropertiesAreDeeplyEqual(expected, actual, processedElements, result))
            {
                return false;
            }

            return true;
        }

        private static bool AreNotDeeplyEqual(
            object expected, 
            object actual, 
            ConditionalWeakTable<object, object> processedElements,
            DeepEqualityResult result)
            => !AreDeeplyEqual(expected, actual, processedElements, result);

        private static bool CollectionsAreDeeplyEqual(
            object expected, 
            object actual, 
            ConditionalWeakTable<object, object> processedElements,
            DeepEqualityResult result)
        {
            var expectedAsEnumerable = (IEnumerable)expected;
            if (!(actual is IEnumerable actualAsEnumerable))
            {
                return result.Failure;
            }

            var listOfExpectedValues = expectedAsEnumerable.Cast<object>().ToList();
            var listOfActualValues = actualAsEnumerable.Cast<object>().ToList();

            var listOfExpectedValuesCount = listOfExpectedValues.Count;
            var listOfActualValuesCount = listOfActualValues.Count;

            if (listOfExpectedValuesCount != listOfActualValuesCount)
            {
                return result
                    .PushPath(nameof(listOfExpectedValues.Count))
                    .ApplyValues(listOfExpectedValuesCount, listOfActualValuesCount)
                    .Failure;
            }

            for (int i = 0; i < listOfExpectedValues.Count; i++)
            {
                var expectedValue = listOfExpectedValues[i];
                var actualValue = listOfActualValues[i];

                var collectionIsDictionary = expected is IDictionary;

                var indexPath = collectionIsDictionary
                    ? $"[{expectedValue.AsDynamic().Key}]"
                    : $"[{i}]";

                result.PushPath(indexPath);

                if (AreNotDeeplyEqual(expectedValue, actualValue, processedElements, result))
                {
                    return result.Failure;
                }

                result.PopPath();
            }

            return result.Success;
        }

        private static bool ComparablesAreDeeplyEqual(object expected, object actual, DeepEqualityResult result)
        {
            if (expected is IComparable expectedAsComparable)
            {
                if (expectedAsComparable.CompareTo(actual) == 0)
                {
                    return result.Success;
                }
            }

            if (ObjectImplementsIComparable(expected) && ObjectImplementsIComparable(actual))
            {
                var methodName = "CompareTo";

                var method = expected.GetType().GetMethod(methodName);
                if (method != null)
                {
                    var compareToResult = (int)method.Invoke(expected, new[] { actual }) == 0;
                    if (!compareToResult)
                    {
                        result.PushPath($"{methodName}()");
                    }

                    return compareToResult;
                }
            }

            return result.Failure;
        }

        private static bool ObjectImplementsIComparable(object obj)
            => obj.GetType()
                .GetInterfaces()
                .FirstOrDefault(i => i.Name.StartsWith("IComparable")) != null;

        private static bool ObjectPropertiesAreDeeplyEqual(
            object expected, 
            object actual, 
            ConditionalWeakTable<object, object> processedElements,
            DeepEqualityResult result)
        {
            // Using RouteValueDictionary because it caches internally property getters as delegates.
            // It is better not to implement own cache, because these object types may be used
            // during the action call thus they will be evaluated and cached twice.
            var expectedProperties = new RouteValueDictionary(expected);
            var actualProperties = new RouteValueDictionary(actual);

            foreach (var key in expectedProperties.Keys)
            {
                var expectedPropertyValue = expectedProperties[key];
                var actualPropertyValue = actualProperties[key];

                result.PushPath(key);

                if (expectedPropertyValue is IEnumerable && expectedPropertyValue.GetType() != typeof(string))
                {
                    if (!CollectionsAreDeeplyEqual(
                        expectedPropertyValue, 
                        actualPropertyValue, 
                        processedElements,
                        result))
                    {
                        return result.Failure;
                    }
                }

                var propertiesAreDifferent = AreNotDeeplyEqual(
                    expectedPropertyValue, 
                    actualPropertyValue, 
                    processedElements,
                    result);

                if (propertiesAreDifferent)
                {
                    return result.Failure;
                }

                result.PopPath();
            }

            return result.Success;
        }

        private static void CacheComponentAttributes(object obj, bool shouldInherit = false)
        {
            var type = obj.GetType();
            var attributes = type.GetTypeInfo().GetCustomAttributes(shouldInherit);
            foreach (var attribute in attributes)
            {
                var attributeType = attribute.GetType();
                if (!TypeAttributesCache.ContainsKey(attributeType))
                {
                    TypeAttributesCache.TryAdd(attributeType, attribute);
                }
            }
        }

        private static string GetFriendlyTypeName(Type type, bool useFullName)
        {
            const string anonymousTypePrefix = "<>f__";

            var typeName = useFullName 
                ? type?.FullName ?? type?.Name
                : type?.Name;

            if (typeName == null)
            {
                throw new InvalidOperationException("Type name cannot be null.");
            }

            if (!type.GetTypeInfo().IsGenericType)
            {
                return typeName.Replace(anonymousTypePrefix, string.Empty);
            }

            var genericArgumentNames = type.GetGenericArguments().Select(ga => ga.ToFriendlyTypeName(useFullName));
            var friendlyGenericName = typeName.Split('`')[0].Replace(anonymousTypePrefix, string.Empty);

            var anonymousName = "AnonymousType";

            if (friendlyGenericName.StartsWith(anonymousName))
            {
                friendlyGenericName = friendlyGenericName.Remove(anonymousName.Length);
            }

            var joinedGenericArgumentNames = string.Join(", ", genericArgumentNames);

            return $"{friendlyGenericName}<{joinedGenericArgumentNames}>";
        }

        private static class New<T>
        {
            public static readonly Func<T> Instance = Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
        }
    }
}
