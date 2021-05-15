namespace MyTested.AspNetCore.Mvc.Utilities
{
    using System;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;
    using Extensions;   

    public class ExposedObject : DynamicObject
    {
        private static readonly BindingFlags Flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        private readonly object instance;
        private readonly Type type;

        public ExposedObject(object instance)
        {
            this.instance = instance;
            this.type = instance?.GetType();
        }

        public ExposedObject(Type type) => this.type = type;

        public object Object => this.instance ?? this.type;

        private static object Unwrap(dynamic obj)
        {
            if (obj is ExposedObject exposedObject)
            {
                return exposedObject.Object;
            }

            return obj;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var property = this.type.GetProperty(binder.Name, Flags);

            if (property != null)
            {
                property.SetValue(this.instance, value);

                return true;
            }

            var field = this.type.GetField(binder.Name, Flags);

            if (field != null)
            {
                field.SetValue(this.instance, value);

                return true;
            }

            return base.TrySetMember(binder, value);
        }
        
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var property = this.type.GetProperty(binder.Name, Flags);

            if (property != null)
            {
                result = property.GetValue(this.instance);

                return true;
            }

            var field = this.type.GetField(binder.Name, Flags);

            if (field != null)
            {
                result = field.GetValue(this.instance);

                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            args = args
                .Select(a => Unwrap(a))
                .ToArray();

            var method = this.type.GetMethod(binder.Name, args.Select(a => a.GetType()).ToArray());

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

                throw;
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
