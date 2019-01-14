using System.Collections.Generic;
using KanKanCore.Karass;

namespace KanKanCore.Interface
{
    public interface IKanKan : IEnumerator<IKanKanCurrentState>
    {
        string ID { get; }
        List<IKarassState> AllKarassStates { get; }
        void SendMessage(string message);
        void SetKarassMessage(IKarassMessage message);
    }
}