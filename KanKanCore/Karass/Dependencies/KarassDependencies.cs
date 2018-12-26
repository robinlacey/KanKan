using System;
using System.Collections.Generic;
using KanKanCore.Exception;
using KanKanCore.Interface;

namespace KanKanCore.Karass.Dependencies
{
    public class KarassDependencies : IDependencies
    {
        private readonly Dictionary<Type, Func<dynamic>> _dependency = new Dictionary<Type, Func<dynamic>>();

        public T Get<T>() where T : class
        {
            try
            {
                return _dependency[typeof(T)]() as T;
            }
            catch
            {
                throw new MissingDependencyException();
            }
        }

        public void Register<T>(Func<dynamic> resolver)
        {
            _dependency.TryGetValue(typeof(T), out Func<dynamic> foundResolver);
            if (foundResolver!=null)
            {
                _dependency[typeof(T)] = resolver;
            }
            else
            {
                _dependency.Add(typeof(T), resolver);
            }
        }
    }
}