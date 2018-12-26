using System;
using System.Collections.Generic;
using KanKanCore.Interface;

namespace KanKanTest.KanKanCoreTests.Mocks.Dependencies
{
    
    public class KarassDependenciesSpy: IDependencies
    {
        public int GetCallCount { get; private set; }
        public int RegisterCallCount { get; private set; }
        private readonly Dictionary<Type, Func<dynamic>> _dependency = new Dictionary<Type, Func<dynamic>>();

        public T Get<T>() where T : class
        {
            GetCallCount++;
            return _dependency[typeof(T)]() as T;
        }

        public void Register<T>(Func<dynamic> resolver)
        {
            RegisterCallCount++;
            _dependency.Add(typeof(T), resolver);
        }
    }
}
