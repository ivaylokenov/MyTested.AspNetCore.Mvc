namespace MyTested.AspNetCore.Mvc.Test.UtilitiesTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Setups.Controllers;
    using Setups.Models;
    using Setups.Services;
    using Setups.Startups;
    using Utilities;
    using Xunit;

    public class ReflectionTests
    {
        [Fact]
        public void AreSameTypesShouldReturnTrueWithObjectsOfSameTypes()
        {
            var first = "Test";
            var second = "Another Test";

            Assert.True(Reflection.AreSameTypes(first, second));
        }

        [Fact]
        public void AreSameTypesShouldReturnFalseWithObjectsOfDifferentTypes()
        {
            var first = 1;
            var second = "Test";

            Assert.False(Reflection.AreSameTypes(first, second));
        }

        [Fact]
        public void AreSameTypesShouldReturnTrueWithSameTypes()
        {
            var first = typeof(int);
            var second = typeof(int);

            Assert.True(Reflection.AreSameTypes(first, second));
        }

        [Fact]
        public void AreSameTypesShouldReturnFalseWithDifferentTypes()
        {
            var first = typeof(List<>);
            var second = typeof(int);

            Assert.False(Reflection.AreSameTypes(first, second));
        }

        [Fact]
        public void AreSameTypesShouldReturnFalseWithInheritedTypes()
        {
            var first = typeof(List<>);
            var second = typeof(IEnumerable<>);

            Assert.False(Reflection.AreSameTypes(first, second));
        }

        [Fact]
        public void AreDifferentTypesShouldReturnTrueWithObjectsOfDifferentTypes()
        {
            var first = 0;
            var second = "Test";

            Assert.True(Reflection.AreDifferentTypes(first, second));
        }

        [Fact]
        public void AreDifferentTypesShouldReturnFalseWithObjectsOfSameTypes()
        {
            var first = 1;
            var second = 2;

            Assert.False(Reflection.AreDifferentTypes(first, second));
        }

        [Fact]
        public void AreDifferentTypesShouldReturnTrueWithDifferentTypes()
        {
            var first = typeof(int);
            var second = typeof(string);

            Assert.True(Reflection.AreDifferentTypes(first, second));
        }

        [Fact]
        public void AreDifferentTypesShouldReturnFalseWithSameTypes()
        {
            var first = typeof(List<>);
            var second = typeof(List<>);

            Assert.False(Reflection.AreDifferentTypes(first, second));
        }

        [Fact]
        public void AreDifferentTypesShouldReturnFalseWithInheritedTypes()
        {
            var first = typeof(List<>);
            var second = typeof(IEnumerable<>);

            Assert.True(Reflection.AreDifferentTypes(first, second));
        }

        [Fact]
        public void AreAssignableShouldReturnTrueWithTheSameTypes()
        {
            var first = typeof(int);
            var second = typeof(int);

            Assert.True(Reflection.AreAssignable(first, second));
            Assert.False(Reflection.AreNotAssignable(first, second));
        }

        [Fact]
        public void AreAssignableShouldReturnTrueWithInheritedTypes()
        {
            var baseType = typeof(IEnumerable<int>);
            var inheritedType = typeof(IList<int>);

            Assert.True(Reflection.AreAssignable(baseType, inheritedType));
            Assert.False(Reflection.AreNotAssignable(baseType, inheritedType));
        }

        [Fact]
        public void AreAssignableShouldReturnTrueWithInvertedInheritedTypes()
        {
            var baseType = typeof(IList<int>);
            var inheritedType = typeof(IEnumerable<int>);

            Assert.False(Reflection.AreAssignable(baseType, inheritedType));
            Assert.True(Reflection.AreNotAssignable(baseType, inheritedType));
        }

        [Fact]
        public void AreAssignableShouldReturnFalseWithGenericTypeDefinitions()
        {
            var baseType = typeof(IEnumerable<>);
            var inheritedType = typeof(IList<>);

            Assert.False(Reflection.AreAssignable(baseType, inheritedType));
            Assert.True(Reflection.AreNotAssignable(baseType, inheritedType));
        }

        [Fact]
        public void AreAssignableShouldReturnFalseWithOneGenericTypeDefinition()
        {
            var baseType = typeof(IEnumerable<>);
            var inheritedType = typeof(IList<int>);

            Assert.False(Reflection.AreAssignable(baseType, inheritedType));
            Assert.True(Reflection.AreNotAssignable(baseType, inheritedType));
        }

        [Fact]
        public void IsGenericShouldReturnTrueWithGenericType()
        {
            var type = typeof(IEnumerable<int>);

            Assert.True(Reflection.IsGeneric(type));
            Assert.False(Reflection.IsNotGeneric(type));
        }

        [Fact]
        public void IsGenericShouldReturnTrueWithGenericTypeDefinition()
        {
            var type = typeof(IEnumerable<>);

            Assert.True(Reflection.IsGeneric(type));
            Assert.False(Reflection.IsNotGeneric(type));
        }

        [Fact]
        public void IsGenericShouldReturnFalseWithNonGenericType()
        {
            var type = typeof(object);

            Assert.False(Reflection.IsGeneric(type));
            Assert.True(Reflection.IsNotGeneric(type));
        }

        [Fact]
        public void IsGenericTypeDefinitionShouldReturnFalseWithGenericType()
        {
            var type = typeof(IEnumerable<int>);

            Assert.False(Reflection.IsGenericTypeDefinition(type));
        }

        [Fact]
        public void IsGenericTypeDefinitionShouldReturnTrueWithGenericTypeDefinition()
        {
            var type = typeof(IEnumerable<>);

            Assert.True(Reflection.IsGenericTypeDefinition(type));
        }

        [Fact]
        public void IsGenericTypeDefinitionShouldReturnFalseWithNonGenericType()
        {
            var type = typeof(object);

            Assert.False(Reflection.IsGenericTypeDefinition(type));
        }

        [Fact]
        public void HaveDifferentGenericArgumentsShouldReturnFalseWithSameGenericArguments()
        {
            var first = typeof(IEnumerable<int>);
            var second = typeof(IEnumerable<int>);

            Assert.False(Reflection.HaveDifferentGenericArguments(first, second));
        }

        [Fact]
        public void HaveDifferentGenericArgumentsShouldReturnTrueWithSameDifferentArguments()
        {
            var first = typeof(IEnumerable<int>);
            var second = typeof(IEnumerable<string>);

            Assert.True(Reflection.HaveDifferentGenericArguments(first, second));
        }

        [Fact]
        public void HaveDifferentGenericArgumentsShouldReturnTrueWithNoGenericArguments()
        {
            var first = typeof(object);
            var second = typeof(int);

            Assert.True(Reflection.HaveDifferentGenericArguments(first, second));
        }

        [Fact]
        public void HaveDifferentGenericArgumentsShouldReturnTrueWithOneTypeHavingGenericArguments()
        {
            var first = typeof(IEnumerable<int>);
            var second = typeof(int);

            Assert.True(Reflection.HaveDifferentGenericArguments(first, second));
        }

        [Fact]
        public void HaveDifferentGenericArgumentsShouldReturnTrueWithDifferentNumberOfGenericArguments()
        {
            var first = typeof(IEnumerable<int>);
            var second = typeof(IDictionary<int, string>);

            Assert.True(Reflection.HaveDifferentGenericArguments(first, second));
        }

        [Fact]
        public void ContainsGenericTypeDefinitionInterfacesShouldReturnTrueWithValidInterfaces()
        {
            var result = Reflection.ContainsGenericTypeDefinitionInterface(typeof(IEnumerable<>), typeof(List<>));
            Assert.True(result);
        }

        [Fact]
        public void ContainsGenericTypeDefinitionInterfacesShouldReturnFalseWithInvalidInterfaces()
        {
            var result = Reflection.ContainsGenericTypeDefinitionInterface(typeof(IEnumerable<>), typeof(Array));
            Assert.False(result);
        }

        [Fact]
        public void CastToShouldReturnCorrectCastWhenCastIsPossible()
        {
            IEnumerable<int> original = new List<int>();
            var cast = typeof(IEnumerable<int>).CastTo<List<int>>(original);

            Assert.Equal(typeof(List<int>), cast.GetType());
        }

        [Fact]
        public void CastToShouldThrowExceptionWhenCastIsNotPossible()
        {
            IEnumerable<int> original = new List<int>();
            Assert.Throws<InvalidCastException>(() =>
            {
                typeof(IEnumerable<int>).CastTo<int>(original);
            });
        }

        [Fact]
        public void ToFriendlyTypeNameShouldReturnTheOriginalNameWhenTypeIsNotGeneric()
        {
            var name = typeof(object).ToFriendlyTypeName();
            Assert.Equal("Object", name);
        }

        [Fact]
        public void ToFriendlyTypeNameShouldReturnProperNameWhenTypeIsAnonymous()
        {
            var name = new { }.GetType().ToFriendlyTypeName();
            Assert.StartsWith("AnonymousType", name);
        }

        [Fact]
        public void ToFriendlyTypeNameShouldReturnProperNameWhenTypeIsAnonymousWithGeneric()
        {
            var name = new { Int = 1, String = "Test" }.GetType().ToFriendlyTypeName();
            Assert.StartsWith("AnonymousType", name);
            Assert.EndsWith("<Int32, String>", name);
        }

        [Fact]
        public void ToFriendlyTypeNameShouldReturnProperNameWhenTypeIsGenericWithoutArguments()
        {
            var name = typeof(List<>).ToFriendlyTypeName();
            Assert.Equal("List<T>", name);
        }

        [Fact]
        public void ToFriendlyTypeNameShouldReturnProperNameWhenTypeIsGenericWithoutMoreThanOneArguments()
        {
            var name = typeof(Dictionary<,>).ToFriendlyTypeName();
            Assert.Equal("Dictionary<TKey, TValue>", name);
        }

        [Fact]
        public void ToFriendlyTypeNameShouldReturnProperNameWhenTypeIsGenericWithOneArgument()
        {
            var name = typeof(List<int>).ToFriendlyTypeName();
            Assert.Equal("List<Int32>", name);
        }

        [Fact]
        public void ToFriendlyTypeNameShouldReturnProperNameWhenTypeIsGenericWithMoreThanOneArguments()
        {
            var name = typeof(Dictionary<string, int>).ToFriendlyTypeName();
            Assert.Equal("Dictionary<String, Int32>", name);
        }

        [Fact]
        public void TryFastCreateInstanceShouldReturnInstanceWhenTypeHasDefaultConstructor()
        {
            var instance = Reflection.TryFastCreateInstance<MvcController>();

            Assert.NotNull(instance);
        }

        [Fact]
        public void TryFastCreateInstanceShouldReturnNullWhenTypeDoNotHaveDefaultConstructor()
        {
            var instance = Reflection.TryFastCreateInstance<NoParameterlessConstructorController>();

            Assert.Null(instance);
        }

        [Fact]
        public void TryGetInstanceShouldReturnObjectWithDefaultConstructorWhenNoParametersAreProvided()
        {
            var instance = Reflection.TryCreateInstance<MvcController>();

            Assert.NotNull(instance);
            Assert.Equal(typeof(MvcController), instance.GetType());
            Assert.Null(instance.InjectedRequestModel);
            Assert.NotNull(instance.InjectedService);
        }

        [Fact]
        public void TryGetInstanceShouldReturnCorrectInitializationWithPartOfAllParameters()
        {
            var instance = Reflection.TryCreateInstance<MvcController>(new InjectedService());

            Assert.NotNull(instance);
            Assert.Equal(typeof(MvcController), instance.GetType());
            Assert.Null(instance.InjectedRequestModel);
            Assert.NotNull(instance.InjectedService);
        }

        [Fact]
        public void TryGetInstanceShouldReturnInitializedObjectWhenCorrectOrderOfParametersAreProvided()
        {
            var instance = Reflection.TryCreateInstance<MvcController>(new InjectedService(), new RequestModel());

            Assert.NotNull(instance);
            Assert.Equal(typeof(MvcController), instance.GetType());
            Assert.NotNull(instance.InjectedRequestModel);
            Assert.NotNull(instance.InjectedService);
        }

        [Fact]
        public void TryCreateInstanceShouldReturnInitializedObjectWhenIncorrectOrderOfParametersAreProvided()
        {
            var instance = Reflection.TryCreateInstance<MvcController>(new RequestModel(), new InjectedService());

            Assert.NotNull(instance);
            Assert.Equal(typeof(MvcController), instance.GetType());
            Assert.NotNull(instance.InjectedRequestModel);
            Assert.NotNull(instance.InjectedService);
        }

        [Fact]
        public void TryCreateInstanceShouldReturnNullWhenConstructorArgumentsDoNotMatch()
        {
            var instance = Reflection.TryCreateInstance<MvcController>(new ResponseModel());

            Assert.Null(instance);
        }

        [Fact]
        public void TryCreateInstanceShouldReturnNullWhenConstructorArgumentsDoNotMatchAndAreTooMany()
        {
            var instance = Reflection.TryCreateInstance<MvcController>(new RequestModel(), new InjectedService(), new ResponseModel());

            Assert.Null(instance);
        }

        [Fact]
        public void GetCustomAttributesShouldReturnProperAttributeTypes()
        {
            var attributes = Reflection.GetCustomAttributes(new MvcController()).ToList();
            var attributeTypes = attributes.Select(a => a.GetType()).ToList();

            Assert.NotNull(attributes);
            Assert.Equal(2, attributes.Count);
            Assert.Contains(typeof(AuthorizeAttribute), attributeTypes);
            Assert.Contains(typeof(RouteAttribute), attributeTypes);
        }

        [Fact]
        public void AreDeeplyEqualShouldWorkCorrectlyWithPrimitiveAndStructTypes()
        {
            Assert.True(Reflection.AreDeeplyEqual(1, 1));
            Assert.True(Reflection.AreDeeplyEqual(null, null));
            Assert.True(Reflection.AreDeeplyEqual("test", "test"));
            Assert.True(Reflection.AreDeeplyEqual('a', 'a'));
            Assert.True(Reflection.AreDeeplyEqual(1.1, 1.1));
            Assert.True(Reflection.AreDeeplyEqual(1.0m, (decimal)1));
            Assert.True(Reflection.AreDeeplyEqual(1L, (long)1));
            Assert.True(Reflection.AreDeeplyEqual(1.1m, 1.1m));
            Assert.True(Reflection.AreDeeplyEqual(true, true));
            Assert.True(Reflection.AreDeeplyEqual(new DateTime(2015, 10, 19), new DateTime(2015, 10, 19)));
            Assert.False(Reflection.AreDeeplyEqual(1, 0));
            Assert.False(Reflection.AreDeeplyEqual(1, null));
            Assert.False(Reflection.AreDeeplyEqual("test1", "test2"));
            Assert.False(Reflection.AreDeeplyEqual("Test", "test"));
            Assert.False(Reflection.AreDeeplyEqual('a', 'b'));
            Assert.False(Reflection.AreDeeplyEqual(1.1, 1.2));
            Assert.False(Reflection.AreDeeplyEqual(1.1m, 1.2m));
            Assert.False(Reflection.AreDeeplyEqual(true, false));
            Assert.False(Reflection.AreDeeplyEqual(1, "1"));
            Assert.False(Reflection.AreDeeplyEqual(new DateTime(2015, 10, 19), new DateTime(2015, 10, 20)));
        }

        [Fact]
        public void AreDeeplyEqualsShouldWorkCorrectlyWithNormalObjects()
        {
            Assert.True(Reflection.AreDeeplyEqual(new object(), new object()));
            Assert.True(Reflection.AreDeeplyEqual((object)5, (object)5));
            Assert.True(Reflection.AreDeeplyEqual((object)5, 5));
            Assert.True(Reflection.AreDeeplyEqual(new { Integer = 1, String = "Test", Nested = new byte[] { 1, 2, 3 } }, new { Integer = 1, String = "Test", Nested = new byte[] { 1, 2, 3 } }));
            Assert.True(Reflection.AreDeeplyEqual(new RequestModel { Integer = 1 }, new RequestModel { Integer = 1 }));
            Assert.True(Reflection.AreDeeplyEqual(new RequestModel { Integer = 1, NonRequiredString = "test" }, new RequestModel { Integer = 1, NonRequiredString = "test" }));
            Assert.True(Reflection.AreDeeplyEqual(new GenericComparableModel { Integer = 1, String = "test" }, new GenericComparableModel { Integer = 1, String = "another" }));
            Assert.True(Reflection.AreDeeplyEqual(new ComparableModel { Integer = 1, String = "test" }, new ComparableModel { Integer = 1, String = "another" }));
            Assert.True(Reflection.AreDeeplyEqual(new EqualsModel { Integer = 1, String = "test" }, new EqualsModel { Integer = 1, String = "another" }));
            Assert.True(Reflection.AreDeeplyEqual(new EqualityOperatorModel { Integer = 1, String = "test" }, new EqualityOperatorModel { Integer = 1, String = "another" }));
            Assert.False(Reflection.AreDeeplyEqual(new object(), "test"));
            Assert.False(Reflection.AreDeeplyEqual(DateTime.Now, "test"));
            Assert.False(Reflection.AreDeeplyEqual("test", DateTime.Now));
            Assert.False(Reflection.AreDeeplyEqual(true, new object()));
            Assert.False(Reflection.AreDeeplyEqual("test", new object()));
            Assert.False(Reflection.AreDeeplyEqual(new object(), true));
            Assert.False(Reflection.AreDeeplyEqual(new { Integer = 1, String = "Test", Nested = new byte[] { 1, 2, 3 } }, new { Integer = 1, String = "Test", Nested = new byte[] { 1, 2, 4 } }));
            Assert.False(Reflection.AreDeeplyEqual(new RequestModel { Integer = 2 }, new RequestModel { Integer = 1 }));
            Assert.False(Reflection.AreDeeplyEqual(new object(), new RequestModel { Integer = 1 }));
            Assert.False(Reflection.AreDeeplyEqual(new RequestModel { Integer = 2 }, new object()));
            Assert.False(Reflection.AreDeeplyEqual(new RequestModel { Integer = 2, NonRequiredString = "test" }, new RequestModel { Integer = 1 }));
            Assert.False(Reflection.AreDeeplyEqual(new GenericComparableModel { Integer = 1, String = "test" }, new GenericComparableModel { Integer = 2, String = "test" }));
            Assert.False(Reflection.AreDeeplyEqual(new ComparableModel { Integer = 1, String = "test" }, new ComparableModel { Integer = 2, String = "test" }));
            Assert.False(Reflection.AreDeeplyEqual(new EqualsModel { Integer = 1, String = "test" }, new EqualsModel { Integer = 2, String = "test" }));
            Assert.False(Reflection.AreDeeplyEqual(new EqualityOperatorModel { Integer = 1, String = "test" }, new EqualityOperatorModel { Integer = 2, String = "test" }));
            Assert.False(Reflection.AreDeeplyEqual(new ComparableModel { Integer = 1, String = "test" }, new RequestModel()));
        }

        [Fact]
        public void AreDeeplyEqualsShouldWorkCorrectlyWithNestedObjects()
        {
            Assert.True(Reflection.AreDeeplyEqual(
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                },
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                }));

            Assert.False(Reflection.AreDeeplyEqual(
                new NestedModel
                {
                    Integer = 1,
                    String = "test",
                    Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                },
                new NestedModel
                {
                    Integer = 1,
                    String = "test",
                    Nested = new NestedModel { Integer = 2, String = "test1", Nested = new NestedModel { Integer = 3, String = "test3" } }
                }));

            Assert.False(Reflection.AreDeeplyEqual(
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test2" } }
                },
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                }));
        }

        [Fact]
        public void AreDeeplyEqualShouldWorkCorrectlyWithCollections()
        {
            Assert.True(Reflection.AreDeeplyEqual(
                new List<NestedModel>
                {
                    new NestedModel
                    {
                        Integer = 1, String = "test1",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    },
                    new NestedModel
                    {
                        Integer = 1,
                        String = "test1",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    }
                },
                new List<NestedModel>
                {
                    new NestedModel
                    {
                        Integer = 1, String = "test1",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    },
                    new NestedModel
                    {
                        Integer = 1,
                        String = "test1",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    }
                }));

            var listOfNestedModels = new List<NestedModel>
            {
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 2,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                },
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 2,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                }
            };

            var arrayOfNestedModels = new[]
            {
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 2,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                },
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 2,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                }
            };

            Assert.True(Reflection.AreDeeplyEqual(listOfNestedModels, arrayOfNestedModels));
            
            Assert.True(Reflection.AreDeeplyEqual(
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new List<NestedModel>
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }
                    }
                },
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new List<NestedModel>
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }
                    }
                }));

            Assert.True(Reflection.AreDeeplyEqual(
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new List<NestedModel>
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }
                    }
                },
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new[]
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }
                    }
                }));

            Assert.False(Reflection.AreDeeplyEqual(
                new List<NestedModel>
                {
                    new NestedModel
                    {
                        Integer = 1, String = "test1",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    }, new NestedModel
                    {
                        Integer = 1,
                        String = "test1",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    }
                },
                new List<NestedModel>
                {
                    new NestedModel
                    {
                        Integer = 1, String = "test2",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    }, new NestedModel
                    {
                        Integer = 1,
                        String = "test1",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    }
                }));

            listOfNestedModels = new List<NestedModel>
            {
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 2,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                },
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 2,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                }
            };

            arrayOfNestedModels = new[]
            {
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 2,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                },
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 4,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                }
            };

            Assert.False(Reflection.AreDeeplyEqual(listOfNestedModels, arrayOfNestedModels));

            Assert.False(Reflection.AreDeeplyEqual(
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new List<NestedModel>
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }
                    }
                },
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new List<NestedModel>
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 5, String = "test3" } }
                        }
                    }
                }));

            Assert.False(Reflection.AreDeeplyEqual(
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new List<NestedModel>
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }
                    }
                },
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new[]
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test3", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }
                    }
                }));

            Assert.True(Reflection.AreDeeplyEqual(new List<int> { 1, 2, 3 }, new[] { 1, 2, 3 }));
            Assert.False(Reflection.AreDeeplyEqual(new List<int> { 1, 2, 3, 4 }, new[] { 1, 2, 3 }));
            Assert.False(Reflection.AreDeeplyEqual(new List<int>(), new object()));
            Assert.False(Reflection.AreDeeplyEqual(new object(), new List<int>()));
        }

        [Fact]
        public void AreDeeplyEqualShouldWorkCorrectlyWithDictionaries()
        {
            var firstDictionary = new Dictionary<string, string>
            {
                { "Key", "Value" },
                { "AnotherKey", "AnotherValue" },
            };

            var secondDictionary = new Dictionary<string, string>
            {
                { "Key", "Value" },
                { "AnotherKey", "AnotherValue" },
            };

            Assert.True(Reflection.AreDeeplyEqual(firstDictionary, secondDictionary));

            firstDictionary = new Dictionary<string, string>
            {
                { "Key", "Value" },
                { "AnotherKey", "Value" },
            };

            secondDictionary = new Dictionary<string, string>
            {
                { "Key", "Value" },
                { "AnotherKey", "AnotherValue" },
            };

            Assert.False(Reflection.AreDeeplyEqual(firstDictionary, secondDictionary));

            var firstDictionaryWithObject = new Dictionary<string, NestedModel>
            {
                { "Key", new NestedModel { Integer = 1, String = "Text" } },
                { "AnotherKey", new NestedModel { Integer = 2, String = "AnotherText" } }
            };

            var secondDictionaryWithObject = new Dictionary<string, NestedModel>
            {
                { "Key", new NestedModel { Integer = 1, String = "Text" } },
                { "AnotherKey", new NestedModel { Integer = 2, String = "AnotherText" } }
            };

            Assert.True(Reflection.AreDeeplyEqual(firstDictionaryWithObject, secondDictionaryWithObject));

            firstDictionaryWithObject = new Dictionary<string, NestedModel>
            {
                { "Key", new NestedModel { Integer = 1, String = "Text" } },
                { "AnotherKey", new NestedModel { Integer = 2, String = "Text" } }
            };

            secondDictionaryWithObject = new Dictionary<string, NestedModel>
            {
                { "Key", new NestedModel { Integer = 1, String = "Text" } },
                { "AnotherKey", new NestedModel { Integer = 2, String = "AnotherText" } }
            };

            Assert.False(Reflection.AreDeeplyEqual(firstDictionaryWithObject, secondDictionaryWithObject));
        }

        [Fact]
        public void AreDeeplyEqualShouldWorkCorrectlyWithSameReferences()
        {
            var firstObject = new NestedModel { Integer = 1, String = "Text" };
            var secondObject = new NestedModel { Integer = 1, String = "Text", Nested = firstObject };
            firstObject.Nested = secondObject;

            Assert.True(Reflection.AreDeeplyEqual(firstObject, secondObject));
        }

        [Fact]
        public void CreateDelegateShouldWorkCorrectlyWithAction()
        {
            var actionDelegate = Reflection.CreateDelegateFromMethod<Action<IServiceCollection>>(
                new CustomStartup(),
                m => m.Name == "ConfigureServices" && m.ReturnType == typeof(void));

            Assert.NotNull(actionDelegate);
        }

        [Fact]
        public void CreateDelegateShouldWorkCorrectlyWithFunc()
        {
            var actionDelegate = Reflection.CreateDelegateFromMethod<Func<IServiceCollection, IServiceProvider>>(
                new CustomStartup(),
                m => m.Name == "ConfigureServicesAndBuildProvider" && m.ReturnType == typeof(IServiceProvider));

            Assert.NotNull(actionDelegate);
        }

        [Fact]
        public void CreateDelegateShouldThrowExceptionIfGenericTypeIsNotCorrectDelegate()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var actionDelegate = Reflection.CreateDelegateFromMethod<Action<IServiceCollection>>(
                new CustomStartup(),
                m => m.Name == "ConfigureServicesAndBuildProvider" && m.ReturnType == typeof(IServiceProvider));
            });
        }

        [Fact]
        public void CreateDelegateShouldReturnNullIfGenericTypeIsNotDelegate()
        {
            var actionDelegate = Reflection.CreateDelegateFromMethod<CustomStartup>(
                new CustomStartup(),
                m => m.Name == "ConfigureServicesAndBuildProvider" && m.ReturnType == typeof(IServiceProvider));

            Assert.Null(actionDelegate);
        }
    }
}