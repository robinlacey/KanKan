using System.Collections;
using KanKanCore.Interface;
using KanKanCore.Karass.Message;

namespace KanKanCore.KanKan
{
    public class KanKanRunner: IKanKanRunner
    {
        public KanKanRunner()
        {
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

        public IKanKan Current { get; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IKarassMessage KarassMessage { get; }
    }
}