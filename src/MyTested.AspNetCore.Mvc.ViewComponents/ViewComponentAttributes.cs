namespace MyTested.AspNetCore.Mvc
{
    using System.Collections;
    using System.Collections.Generic;
    using Utilities.Validators;

    /// <summary>
    /// Class used for specifying additional assertions on the view component attributes.
    /// </summary>
    public class ViewComponentAttributes : IEnumerable<object>
    {
        private readonly IEnumerable<object> attributes;

        public ViewComponentAttributes(IEnumerable<object> attributes)
        {
            CommonValidator.CheckForNullReference(attributes, nameof(ViewComponentAttributes));
            this.attributes = attributes;
        }

        public IEnumerator<object> GetEnumerator() => this.attributes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
