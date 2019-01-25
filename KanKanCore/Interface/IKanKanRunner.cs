using System.Collections.Generic;

namespace KanKanCore.Interface
{
    public interface IKanKanRunner: IEnumerator<IKanKan>
    {
        IKarassMessage KarassMessage { get; }
        void Run(string tag);
        void Pause(bool pauseState);
    }
}