namespace MyTested.AspNetCore.Mvc
{
    using System.Collections.Generic;

    using Microsoft.Extensions.Configuration;

    public static class ConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adds the provided key-value pair to the configuration builder.
        /// </summary>
        /// <param name="configurationBuilder">The <see cref="Microsoft.Extensions.Configuration.IConfigurationBuilder" /> to add to.</param>
        /// <param name="key">The configuration key to add.</param>
        /// <param name="value">The configuration value to add.</param>
        /// <returns>The same <see cref="Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</returns>
        public static IConfigurationBuilder Add(
            this IConfigurationBuilder configurationBuilder,
            string key,
            string value)
            => configurationBuilder.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>(key, value)
            });
    }
}
