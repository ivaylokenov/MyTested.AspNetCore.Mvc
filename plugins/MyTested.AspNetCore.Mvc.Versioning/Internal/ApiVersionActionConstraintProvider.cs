namespace MyTested.AspNetCore.Mvc.Internal
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;

    internal class ApiVersionActionConstraintProvider : IActionConstraintProvider
    {
        private static readonly ApiVersionActionConstraint SharedConstraint = new();

        public int Order => -1000;

        public void OnProvidersExecuting(ActionConstraintProviderContext context)
        {
            var metadata = context.Action.GetApiVersionMetadata();

            if (metadata != ApiVersionMetadata.Empty && !metadata.IsApiVersionNeutral)
            {
                context.Results.Add(new ActionConstraintItem(SharedConstraint)
                {
                    Constraint = SharedConstraint,
                    IsReusable = true
                });
            }
        }

        public void OnProvidersExecuted(ActionConstraintProviderContext context)
        {
        }
    }
}
