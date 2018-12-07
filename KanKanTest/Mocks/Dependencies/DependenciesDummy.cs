using KanKanCore.Karass.Interface;

namespace KanKanTest.Mocks.Dependencies
{
    public class DependenciesDummy:IDependencies
    {
        public T Get<T>() where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}