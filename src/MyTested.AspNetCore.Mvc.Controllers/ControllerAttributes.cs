namespace MyTested.AspNetCore.Mvc
{
    using System.Collections;
    using System.Collections.Generic;
    using Utilities.Validators;

    public class ControllerAttributes : IEnumerable<object>
    {
        private readonly IEnumerable<object> attributes;

        public ControllerAttributes(IEnumerable<object> attributes)
        {
            CommonValidator.CheckForNullReference(attributes, nameof(ControllerAttributes));
            this.attributes = attributes;
        }

        public IEnumerator<object> GetEnumerator() => this.attributes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
