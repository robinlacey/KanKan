using System;
using System.Collections.Generic;
using KanKanCore.Karass.Interface;

namespace KanKanCore.Karass.Dependencies
{
    // NOTE: Highly recommended you use an established DI framework and map functionality to IDependencies interface;
    
    public class KarassDependencies:IDependencies
    {
        private readonly Dictionary<Type, object> _dependencies;

        public KarassDependencies()
        {
            _dependencies = new Dictionary<Type, object>();
        }
        public T Get<T>() where T : class
        {
            return (T) _dependencies[typeof(T)];
        }

        public void Register<T>(object obj)
        {
            _dependencies.Add(typeof(T), obj);
        }
    }
}