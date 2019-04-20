namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Json
{
    /// <summary>
    /// Used for testing <see cref="Newtonsoft.Json.JsonSerializerSettings"/>
    /// in a <see cref="Microsoft.AspNetCore.Mvc.JsonResult"/> with AndAlso() method.
    /// </summary>
    public interface IAndJsonSerializerSettingsTestBuilder : IJsonSerializerSettingsTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Newtonsoft.Json.JsonSerializerSettings"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IJsonSerializerSettingsTestBuilder"/>.</returns>
        IJsonSerializerSettingsTestBuilder AndAlso();
    }
}
