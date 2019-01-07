using System.Collections.Generic;

namespace KanKanCore.Interface
{
    public interface IKanKan : IEnumerator<IKanKanCurrentState>
    {
        void SendMessage(string message);
        void SetKarassMessage(IKarassMessage message);
    }
}