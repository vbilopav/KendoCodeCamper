using System;
using System.Web.Mvc;

namespace Infrastructure.Web.Engine
{
    internal class DefaultViewPageActivator : IViewPageActivator
    {
        private readonly Func<IDependencyResolver> _resolverThunk;

        public DefaultViewPageActivator()
            : this(null)
        {
        }

        static DefaultViewPageActivator()
        {
            Current = new DefaultViewPageActivator();
        }

        public static DefaultViewPageActivator Current { get; private set; }

        public DefaultViewPageActivator(IDependencyResolver resolver)
        {
            if (resolver == null)
                _resolverThunk = () => DependencyResolver.Current;
            else
                _resolverThunk = () => resolver;
        }

        public object Create(ControllerContext controllerContext, Type type)
        {
            return _resolverThunk().GetService(type) ?? Activator.CreateInstance(type);
        }
    }
}
