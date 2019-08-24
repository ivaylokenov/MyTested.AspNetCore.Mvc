namespace MyTested.AspNetCore.Mvc.Utilities
{
    using System;
    using System.Dynamic;
    using System.Reflection;
    using Extensions;   

    public class ExposedObject : DynamicObject
    {
        private readonly object instance;

        private readonly Type type;

        public ExposedObject(object instance)
        {
            this.instance = instance;
            this.type = instance?.GetType();
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
        
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var property = this.type.GetProperty(binder.Name);

            result = property.GetValue(this.instance);

            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var method = this.type.GetMethod(binder.Name);

            try
            {
                result = method
                    .Invoke(this.instance, args)
                    .Exposed();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }

                throw ex;
            }

            return true;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            result = binder.Type.GetTypeInfo().IsInstanceOfType(this.instance) 
                ? this.instance
                : Convert.ChangeType(this.instance, binder.Type);

            return true;
        }
    }
}
