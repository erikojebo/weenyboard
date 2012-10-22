using System;
using System.Collections.Generic;
using System.Linq;
using WeenyBoard.Extensions;

namespace WeenyBoard.Infrastructure
{
    public class ObjectRegistry
    {
        public static ObjectRegistry Instance = new ObjectRegistry();

        private readonly List<TypeRegistration> _registrations = new List<TypeRegistration>();

        public T Resolve<T>()
        {
            if (_registrations.Any(x => x.RequestedType == typeof(T)))
                return (T)_registrations.First(x => x.RequestedType == typeof(T)).Create();

            if (typeof(T).HasDefaultConstructor())
                return Activator.CreateInstance<T>();

            throw new InvalidOperationException("Could not resolve type: " + typeof(T).Name);
        }

        public void Register<TRequested, TConcrete>() where TConcrete : TRequested
        {
            var typeRegistration = new DefaultConstructorTypeRegistration<TRequested, TConcrete>();
            _registrations.Add(typeRegistration);
        }

        public void Register<TRequested>(TRequested singletonInstance)
        {
            var typeRegistration = new SingletonTypeRegistration<TRequested>(singletonInstance);
            _registrations.Add(typeRegistration);
        }

        public abstract class TypeRegistration
        {
            protected TypeRegistration(Type requestedType)
            {
                RequestedType = requestedType;
            }

            public readonly Type RequestedType;

            public abstract object Create();
        }

        private class DefaultConstructorTypeRegistration<TRequested, TConcrete> : TypeRegistration
        {
            public DefaultConstructorTypeRegistration() : base(typeof(TRequested))
            {
            }

            public override object Create()
            {
                return Activator.CreateInstance<TConcrete>();
            }
        }

        public class SingletonTypeRegistration<TRequested> : TypeRegistration
        {
            private readonly TRequested _singletonInstance;

            public SingletonTypeRegistration(TRequested singletonInstance) : base(typeof(TRequested))
            {
                _singletonInstance = singletonInstance;
            }

            public override object Create()
            {
                return _singletonInstance;
            }
        }
    }
}