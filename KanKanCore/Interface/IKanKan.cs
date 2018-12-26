using System.Collections;

namespace KanKanCore.Interface
{
    public interface IKanKan:IEnumerator
    {
        void SendMessage(string message);

        IKanKanCurrentState GetCurrentState();
    }
}