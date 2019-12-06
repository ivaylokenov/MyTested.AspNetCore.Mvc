namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class ObjectPropertyHelper : PropertyHelper
    {
        private static readonly ConcurrentDictionary<Type, ObjectPropertyHelper> ObjectPropertiesCache =
            new ConcurrentDictionary<Type, ObjectPropertyHelper>();

        private IEnumerable<ObjectPropertyDelegates> propertyDelegates;

        public ObjectPropertyHelper(Type type)
            : base(type)
        {
        }

        public IEnumerable<ObjectPropertyDelegates> PropertyDelegates
        {
            get
            {
                if (this.propertyDelegates == null)
                {
                    this.TryCreateObjectPropertyDelegates();
                }

                return this.propertyDelegates;
            }
        }

        public static ObjectPropertyHelper GetProperties<TObject>()
            where TObject : class
            => GetProperties(typeof(TObject));

        public static ObjectPropertyHelper GetProperties(Type type)
            => ObjectPropertiesCache.GetOrAdd(type, _ => new ObjectPropertyHelper(type));

        private void TryCreateObjectPropertyDelegates()
        {
            var propertyDelegates = new List<ObjectPropertyDelegates>();

            foreach (var property in this.Properties)
            {
                var name = property.Name;

                if (property.GetMethod?.IsPublic != true || property.SetMethod?.IsPublic != true)
                {
                    continue;
                }

                var getter = MakeFastPropertyGetter<object>(property);
                var setter = MakeFastPropertySetter(property);

                propertyDelegates.Add(new ObjectPropertyDelegates(getter, setter));
            }

            this.propertyDelegates = propertyDelegates;
        }

        public class ObjectPropertyDelegates
        {
            public ObjectPropertyDelegates(
                Func<object, object> getter, 
                Action<object, object> setter)
            {
                this.Getter = getter;
                this.Setter = setter;
            }

            public Func<object, object> Getter { get; private set; }

            public Action<object, object> Setter { get; private set; }
        }
    }
}
