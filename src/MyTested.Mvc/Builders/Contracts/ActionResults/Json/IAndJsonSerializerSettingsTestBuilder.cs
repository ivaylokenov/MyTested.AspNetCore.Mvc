namespace MyTested.Mvc.Builders.Contracts.ActionResults.Json
{
    /// <summary>
    /// Used for testing JSON serializer settings in a JSON result with AndAlso() method.
    /// </summary>
    public interface IAndJsonSerializerSettingsTestBuilder : IJsonSerializerSettingsTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining JSON serializer settings test builder.
        /// </summary>
        /// <returns>JSON serializer settings test builder.</returns>
        IJsonSerializerSettingsTestBuilder AndAlso();
    }
}
