using System;

namespace KanKanCore.Karass.Interface
{
    public interface IDependencies
    {
        T Get<T>() where T : class;
        
        void Register<T>(Func<dynamic> resolver);
    }
}