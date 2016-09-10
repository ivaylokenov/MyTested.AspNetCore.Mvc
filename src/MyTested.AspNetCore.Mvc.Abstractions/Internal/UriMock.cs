namespace MyTested.AspNetCore.Mvc.Internal
{
    using System.Text;

    /// <summary>
    /// Mock of URI object.
    /// </summary>
    public class UriMock
    {
        /// <summary>
        /// Gets or sets the host of the URI.
        /// </summary>
        /// <value>Host as string.</value>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port of the URI.
        /// </summary>
        /// <value>Port as integer.</value>
        public int? Port { get; set; }

        /// <summary>
        /// Gets or sets the absolute path of the URI.
        /// </summary>
        /// <value>Absolute path as string.</value>
        public string AbsolutePath { get; set; }

        /// <summary>
        /// Gets or sets the scheme of the URI.
        /// </summary>
        /// <value>Scheme as string.</value>
        public string Scheme { get; set; }

        /// <summary>
        /// Gets or sets the query of the URI.
        /// </summary>
        /// <value>Query as string.</value>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the document fragment of the URI.
        /// </summary>
        /// <value>Document fragment as string.</value>
        public string Fragment { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();
            var scheme = this.Scheme ?? "http";
            var port = this.Port == null || this.Port == 80 ? string.Empty : $":{this.Port}";
            var path = string.IsNullOrEmpty(this.AbsolutePath) ? string.Empty : $"/{this.AbsolutePath.Trim('/')}";
            var query = string.IsNullOrEmpty(this.Query) ? string.Empty : $"?{this.Query.Trim('?')}";
            var fragment = string.IsNullOrEmpty(this.Fragment) ? string.Empty : $"#{this.Fragment.Trim('#')}";

            return $"{scheme}://{this.Host}{port}{path}{query}{fragment}";
        }
    }
}
