using System;
using KanKanCore.Interface;

namespace KanKanTest.KanKanCoreTests.Mocks.Dependencies
{
    public class DependenciesDummy:IDependencies
    {
        public T Get<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public void Register<T>(Func<dynamic> resolver)
        {
            throw new NotImplementedException();
        }
    }
}