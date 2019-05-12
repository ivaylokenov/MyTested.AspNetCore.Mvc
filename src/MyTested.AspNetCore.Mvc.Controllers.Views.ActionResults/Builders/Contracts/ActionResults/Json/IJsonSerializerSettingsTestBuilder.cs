namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Json
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Used for testing <see cref="JsonSerializerSettings"/> in a <see cref="Microsoft.AspNetCore.Mvc.JsonResult"/>.
    /// </summary>
    public interface IJsonSerializerSettingsTestBuilder
    {
        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.CheckAdditionalContent"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="checkAdditionalContent">Expected boolean value.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithCheckAdditionalContent(bool checkAdditionalContent);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.Culture"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="culture">Expected <see cref="CultureInfo"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithCulture(CultureInfo culture);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.ContractResolver"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="contractResolver">Expected <see cref="IContractResolver"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithContractResolver(IContractResolver contractResolver);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.ContractResolver"/> property
        /// in a <see cref="JsonSerializerSettings"/> object by using generic type.
        /// </summary>
        /// <typeparam name="TContractResolver">Expected <see cref="IContractResolver"/> type.</typeparam>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithContractResolverOfType<TContractResolver>()
             where TContractResolver : IContractResolver;

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.ContractResolver"/> property
        /// in a <see cref="JsonSerializerSettings"/> object by using the provided type.
        /// </summary>
        /// <param name="contractResolverType">Expected <see cref="IContractResolver"/> type.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithContractResolverOfType(Type contractResolverType);

        /// <summary>
        /// Tests whether the <see cref="JsonSerializerSettings"/> contains
        /// the provided <see cref="JsonConverter"/>.
        /// </summary>
        /// <param name="jsonConverter">Expected <see cref="JsonConverter"/></param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder ContainingConverter(JsonConverter jsonConverter);

        /// <summary>
        /// Tests whether the <see cref="JsonSerializerSettings"/> contains
        /// <see cref="JsonConverter"/> of the provided type.
        /// </summary>
        /// <typeparam name="TJsonConverter">Expected <see cref="JsonConverter"/> type.</typeparam>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder ContainingConverterOfType<TJsonConverter>()
            where TJsonConverter : JsonConverter;

        /// <summary>
        /// Tests whether the <see cref="JsonSerializerSettings"/> contains
        /// the provided <see cref="JsonConverter"/> objects.
        /// </summary>
        /// <param name="jsonConverters">Collection of <see cref="JsonConverter"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder ContainingConverters(IEnumerable<JsonConverter> jsonConverters);

        /// <summary>
        /// Tests whether the <see cref="JsonSerializerSettings"/> contains
        /// the provided <see cref="JsonConverter"/> objects.
        /// </summary>
        /// <param name="jsonConverters"><see cref="JsonConverter"/> parameters.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder ContainingConverters(params JsonConverter[] jsonConverters);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.ConstructorHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="constructorHandling">Expected <see cref="ConstructorHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithConstructorHandling(ConstructorHandling constructorHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.DateFormatHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="dateFormatHandling">Expected <see cref="DateFormatHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithDateFormatHandling(DateFormatHandling dateFormatHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.DateFormatString"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="dateFormatString">Expected date format string.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithDateFormatString(string dateFormatString);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.DateParseHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="dateParseHandling">Expected <see cref="DateParseHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithDateParseHandling(DateParseHandling dateParseHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.DateTimeZoneHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="dateTimeZoneHandling">Expected <see cref="DateTimeZoneHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithDateTimeZoneHandling(DateTimeZoneHandling dateTimeZoneHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.DefaultValueHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="defaultValueHandling">Expected <see cref="DefaultValueHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithDefaultValueHandling(DefaultValueHandling defaultValueHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.EqualityComparer"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="equalityComparer">Expected <see cref="IEqualityComparer"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithEqualityComparer(IEqualityComparer equalityComparer);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.EqualityComparer"/> property
        /// in a <see cref="JsonSerializerSettings"/> object by using the provided type.
        /// </summary>
        /// <typeparam name="TEqualityComparer">Expected <see cref="IEqualityComparer"/> type.</typeparam>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithEqualityComparerOfType<TEqualityComparer>()
            where TEqualityComparer : IEqualityComparer;

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.EqualityComparer"/> property
        /// in a <see cref="JsonSerializerSettings"/> object by using the provided type.
        /// </summary>
        /// <param name="equalityComparerType">Expected <see cref="IEqualityComparer"/> type.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithEqualityComparerOfType(Type equalityComparerType);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.FloatFormatHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="floatFormatHandling">Expected <see cref="FloatFormatHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithFloatFormatHandling(FloatFormatHandling floatFormatHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.FloatParseHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="floatParseHandling">Expected <see cref="FloatParseHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithFloatParseHandling(FloatParseHandling floatParseHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.Formatting"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="formatting">Expected <see cref="Formatting"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithFormatting(Formatting formatting);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.MaxDepth"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="maxDepth">Expected max depth.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithMaxDepth(int? maxDepth);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.MetadataPropertyHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="metadataPropertyHandling">Expected <see cref="MetadataPropertyHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithMetadataPropertyHandling(MetadataPropertyHandling metadataPropertyHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.MissingMemberHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="missingMemberHandling">Expected <see cref="MissingMemberHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithMissingMemberHandling(MissingMemberHandling missingMemberHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.NullValueHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="nullValueHandling">Expected <see cref="NullValueHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithNullValueHandling(NullValueHandling nullValueHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.ObjectCreationHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="objectCreationHandling">Expected <see cref="ObjectCreationHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithObjectCreationHandling(ObjectCreationHandling objectCreationHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.PreserveReferencesHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="preserveReferencesHandling">Expected <see cref="PreserveReferencesHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithPreserveReferencesHandling(
            PreserveReferencesHandling preserveReferencesHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.ReferenceLoopHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="referenceLoopHandling">Expected <see cref="ReferenceLoopHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithReferenceLoopHandling(ReferenceLoopHandling referenceLoopHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.ReferenceResolver"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="referenceResolver">Expected <see cref="IReferenceResolver"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithReferenceResolver(IReferenceResolver referenceResolver);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.ReferenceResolver"/> property
        /// in a <see cref="JsonSerializerSettings"/> object by using the provided type.
        /// </summary>
        /// <typeparam name="TReferenceResolver">Expected <see cref="IReferenceResolver"/> type.</typeparam>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithReferenceResolverOfType<TReferenceResolver>()
            where TReferenceResolver : IReferenceResolver;

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.ReferenceResolver"/> property
        /// in a <see cref="JsonSerializerSettings"/> object by using the provided type.
        /// </summary>
        /// <param name="referenceResolverType">Expected <see cref="IReferenceResolver"/> type.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithReferenceResolverOfType(Type referenceResolverType);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.StringEscapeHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="stringEscapeHandling">Expected <see cref="StringEscapeHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithStringEscapeHandling(StringEscapeHandling stringEscapeHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.TraceWriter"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="traceWriter">Expected <see cref="ITraceWriter"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithTraceWriter(ITraceWriter traceWriter);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.TraceWriter"/> property
        /// in a <see cref="JsonSerializerSettings"/> object by using the provided type.
        /// </summary>
        /// <typeparam name="TTraceWriter">Expected <see cref="ITraceWriter"/> type.</typeparam>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithTraceWriterOfType<TTraceWriter>()
            where TTraceWriter : ITraceWriter;

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.TraceWriter"/> property
        /// in a <see cref="JsonSerializerSettings"/> object by using the provided type.
        /// </summary>
        /// <param name="traceWriterType">Expected <see cref="ITraceWriter"/> type.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithTraceWriterOfType(Type traceWriterType);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.SerializationBinder"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="serializationBinder">Expected <see cref="ISerializationBinder"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithSerializationBinder(ISerializationBinder serializationBinder);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.SerializationBinder"/> property
        /// in a <see cref="JsonSerializerSettings"/> object by using the provided type.
        /// </summary>
        /// <typeparam name="TSerializationBinder">Expected <see cref="ISerializationBinder"/> type.</typeparam>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithSerializationBinderOfType<TSerializationBinder>()
            where TSerializationBinder : ISerializationBinder;

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.SerializationBinder"/> property
        /// in a <see cref="JsonSerializerSettings"/> object by using the provided type.
        /// </summary>
        /// <param name="serializationBinderType">Expected <see cref="ISerializationBinder"/> type.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithSerializationBinderOfType(Type serializationBinderType);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.TypeNameAssemblyFormatHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="typeNameAssemblyFormatHandling">Expected <see cref="TypeNameAssemblyFormatHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithTypeNameAssemblyFormatHandling(
            TypeNameAssemblyFormatHandling typeNameAssemblyFormatHandling);

        /// <summary>
        /// Tests the <see cref="JsonSerializerSettings.TypeNameHandling"/> property
        /// in a <see cref="JsonSerializerSettings"/> object.
        /// </summary>
        /// <param name="typeNameHandling">Expected <see cref="TypeNameHandling"/>.</param>
        /// <returns>The same <see cref="IAndJsonSerializerSettingsTestBuilder"/>.</returns>
        IAndJsonSerializerSettingsTestBuilder WithTypeNameHandling(TypeNameHandling typeNameHandling);
    }
}
