namespace MyTested.AspNetCore.Mvc
{
    using System.Collections;
    using System.Collections.Generic;
    using Utilities.Validators;

    public class ActionAttributes : IEnumerable<object>
    {
        private readonly IEnumerable<object> attributes;

        public ActionAttributes(IEnumerable<object> attributes)
        {
            CommonValidator.CheckForNullReference(attributes, nameof(ActionAttributes));
            this.attributes = attributes;
        }

        public IEnumerator<object> GetEnumerator() => this.attributes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}