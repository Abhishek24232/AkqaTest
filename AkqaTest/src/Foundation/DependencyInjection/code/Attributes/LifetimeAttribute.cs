using System;

namespace AkqaTest.Foundation.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class LifetimeAttribute : Attribute
    {
        public LifetimeAttribute(Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Singleton:
                case Lifetime.Transient:
                case Lifetime.PerRequest:
                    Lifetime = lifetime;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }

        public LifetimeAttribute(Lifetime lifetime, Type serviceType) : this(lifetime)
        {
            this.ServiceType = serviceType;
        }

        public Lifetime Lifetime { get; }
        public Type ServiceType { get; }
    }
}