namespace MyTested.Mvc.Tests.Setups.Common
{
    using System;
    using Microsoft.AspNet.FileProviders;
    using Microsoft.Extensions.Primitives;

    public class CustomFileProvider : IFileProvider
    {
        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return null;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return null;
        }

        public IChangeToken Watch(string filter)
        {
            return null;
        }
    }
}
