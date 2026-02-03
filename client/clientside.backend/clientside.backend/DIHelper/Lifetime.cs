namespace clientside.backend.DIHelper
{
    // Decorate classes with this attribute to specify their service lifetime
    // Recommended to use this attribute rather than implement ILifetime interfaces
    // Scoped only makes sense in the context of web applications

    // If more then one class implements the same interface, use the Key property to differentiate them.
    // The name of the implemementing classes will be the key of the service when resolving.
    // Not really recommended to use this feature unless necessary.
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class LifetimeAttribute : Attribute
    {
        public LifetimeAttribute(Lifetime serviceLifetime)
        {
            Lifetime = serviceLifetime;
        }
        public bool Key { get; set; } = false;
        public Lifetime Lifetime { get; }
    }
    public enum Lifetime
    {
        Transient,
        Singleton,
        Scoped
    }
    public interface ILifetime{}
    public interface ITransient : ILifetime{}
    public interface ISingleton : ILifetime{}
    public interface IScoped : ILifetime{}
}
