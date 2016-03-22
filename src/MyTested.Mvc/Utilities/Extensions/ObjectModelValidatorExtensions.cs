namespace MyTested.Mvc.Utilities.Extensions
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    public static class ObjectModelValidatorExtensions
    {
        public static void Validate(this IObjectModelValidator objectModelValidator, ControllerContext controllerContext, object model)
        {
            objectModelValidator.Validate(
                        controllerContext,
                        new CompositeModelValidatorProvider(controllerContext.ValidatorProviders),
                        validationState: null,
                        prefix: string.Empty,
                        model: model);
        }
    }
}
