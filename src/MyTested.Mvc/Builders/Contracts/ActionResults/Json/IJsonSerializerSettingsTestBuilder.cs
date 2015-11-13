namespace MyTested.Mvc.Builders.Contracts.ActionResults.Json
{
    using System.Globalization;
    using System.Runtime.Serialization.Formatters;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Used for testing JSON serializer settings in a JSON result.
    /// </summary>
    public interface IJsonSerializerSettingsTestBuilder
    {
        /// <summary>
        /// Tests the Culture property in a JSON serializer settings object.
        /// </summary>
        /// <param name="culture">Expected Culture.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithCulture(CultureInfo culture);

        /// <summary>
        /// Tests the ContractResolver property in a JSON serializer settings object.
        /// </summary>
        /// <param name="contractResolver">Expected ContractResolver.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithContractResolver(IContractResolver contractResolver);

        /// <summary>
        /// Tests the ContractResolver property in a JSON serializer settings object by using generic type.
        /// </summary>
        /// <typeparam name="TContractResolver">Expected ContractResolver type.</typeparam>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithContractResolverOfType<TContractResolver>()
             where TContractResolver : IContractResolver, new();

        /// <summary>
        /// Tests the ConstructorHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="constructorHandling">Expected ConstructorHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithConstructorHandling(ConstructorHandling constructorHandling);

        /// <summary>
        /// Tests the DateFormatHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="dateFormatHandling">Expected DateFormatHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithDateFormatHandling(DateFormatHandling dateFormatHandling);

        /// <summary>
        /// Tests the DateParseHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="dateParseHandling">Expected DateParseHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithDateParseHandling(DateParseHandling dateParseHandling);

        /// <summary>
        /// Tests the WithDateTimeZoneHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="dateTimeZoneHandling">Expected WithDateTimeZoneHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithDateTimeZoneHandling(DateTimeZoneHandling dateTimeZoneHandling);

        /// <summary>
        /// Tests the DefaultValueHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="defaultValueHandling">Expected DefaultValueHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithDefaultValueHandling(DefaultValueHandling defaultValueHandling);

        /// <summary>
        /// Tests the Formatting property in a JSON serializer settings object.
        /// </summary>
        /// <param name="formatting">Expected Formatting.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithFormatting(Formatting formatting);

        /// <summary>
        /// Tests the MaxDepth property in a JSON serializer settings object.
        /// </summary>
        /// <param name="maxDepth">Expected max depth.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithMaxDepth(int? maxDepth);

        /// <summary>
        /// Tests the MissingMemberHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="missingMemberHandling">Expected MissingMemberHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithMissingMemberHandling(MissingMemberHandling missingMemberHandling);

        /// <summary>
        /// Tests the NullValueHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="nullValueHandling">Expected NullValueHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithNullValueHandling(NullValueHandling nullValueHandling);

        /// <summary>
        /// Tests the ObjectCreationHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="objectCreationHandling">Expected ObjectCreationHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithObjectCreationHandling(ObjectCreationHandling objectCreationHandling);

        /// <summary>
        /// Tests the PreserveReferencesHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="preserveReferencesHandling">Expected PreserveReferencesHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithPreserveReferencesHandling(
            PreserveReferencesHandling preserveReferencesHandling);

        /// <summary>
        /// Tests the ReferenceLoopHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="referenceLoopHandling">Expected ReferenceLoopHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithReferenceLoopHandling(ReferenceLoopHandling referenceLoopHandling);

        /// <summary>
        /// Tests the FormatterAssemblyStyle property in a JSON serializer settings object.
        /// </summary>
        /// <param name="typeNameAssemblyFormat">Expected FormatterAssemblyStyle.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithTypeNameAssemblyFormat(FormatterAssemblyStyle typeNameAssemblyFormat);

        /// <summary>
        /// Tests the TypeNameHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="typeNameHandling">Expected TypeNameHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        IAndJsonSerializerSettingsTestBuilder WithTypeNameHandling(TypeNameHandling typeNameHandling);
    }
}
