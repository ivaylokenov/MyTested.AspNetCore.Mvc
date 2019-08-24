namespace MyTested.AspNetCore.Mvc.Utilities
{
    using System;
    using System.Dynamic;

    public class ExposedObject : DynamicObject
    {
        private readonly object instance;

        private readonly Type type;

        public ExposedObject(object instance)
        {
            this.instance = instance;
            this.type = instance.GetType();
        }

        public ExposedObject(Type type)
        {
            this.type = type;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var property = this.type.GetProperty(binder.Name);

            property.SetValue(this.instance, value);

            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var method = this.type.GetMethod(binder.Name);

            result = method.Invoke(this.instance, args);

            return true;
        }
    }
}
