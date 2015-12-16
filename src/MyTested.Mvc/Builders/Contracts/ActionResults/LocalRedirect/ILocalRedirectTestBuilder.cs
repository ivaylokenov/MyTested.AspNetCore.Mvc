namespace MyTested.Mvc.Builders.Contracts.ActionResults.LocalRedirect
{
    using Uris;
    using System;

    public interface ILocalRedirectTestBuilder
    {
        IAndLocalRedirectTestBuilder Permanent();

        IAndLocalRedirectTestBuilder To(string localUrl);

        IAndLocalRedirectTestBuilder To(Uri localUrl);

        IAndLocalRedirectTestBuilder To(Action<IUriTestBuilder> localUrlTestBuilder);

        // TODO: add route redirects
    }
}
