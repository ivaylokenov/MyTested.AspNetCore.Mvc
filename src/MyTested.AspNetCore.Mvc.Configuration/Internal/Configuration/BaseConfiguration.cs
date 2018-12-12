namespace MyTested.AspNetCore.Mvc.Internal.Configuration
{
    using System;
    using Microsoft.Extensions.Configuration;

    public abstract class BaseConfiguration
    {
        private IConfiguration configuration;

        protected BaseConfiguration(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration
        {
            get => this.configuration;

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Test configuration cannot be null.");
                }

                this.configuration = value;
            }
        }

        protected string Prefix { get; set; }

        protected string GetValue(string key)
            => this.GetValue<string>(key, null);

        protected T GetValue<T>(string key, T defaultValue)
            => this.Configuration.GetSection(this.Prefix).GetValue(key, defaultValue);
    }
}
