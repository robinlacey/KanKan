using System;

namespace KanKanCore.Interface
{
    public interface IDependencies
    {
        T Get<T>() where T : class;
        
        void Register<T>(Func<dynamic> resolver);
    }
}