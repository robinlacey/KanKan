using System.Collections;
using KanKanCore.Interface;
using KanKanCore.Karass.Message;

namespace KanKanCore.KanKan
{
    public class KanKanRunner: IKanKanRunner
    {
        public IKanKan Current { get; }
        public KanKanRunner(IKanKan kanKan)
        {
            Current = kanKan;
            KarassMessage = new KarassMessage();
        }
        public bool MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

       

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IKarassMessage KarassMessage { get; }
    }
}