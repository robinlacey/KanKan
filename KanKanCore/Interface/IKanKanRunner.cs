using System.Collections.Generic;

namespace KanKanCore.Interface
{
    public interface IKanKanRunner<T>: IEnumerator<IKanKan>
    {
        T ConstructorKanKan { get; }
        Dictionary<string, T> KanKans { get; }
        IKarassMessage KarassMessage { get; }
        void Run(string tag);
        void Pause(bool pauseState);
        T Get(string tag);
        void Add(T kankan, string tag);
    }
    
}