using System.Collections;
using System.Collections.Generic;
using KanKanCore.Exception;
using KanKanCore.Interface;
using KanKanCore.Karass.Message;

namespace KanKanCore.KanKan
{
    public abstract class KanKanRunner<T> : IKanKanRunner<T>
    {
        
        public IKanKan Current { get; protected set; }
        public T ConstructorKanKan { get; }
        public Dictionary<string, T> KanKans { get; }
        public IKarassMessage KarassMessage { get;  }
        protected bool Paused;

        protected KanKanRunner(T kankan, string tag)
        {
            ConstructorKanKan = kankan;
            KanKans = new Dictionary<string, T> {{tag, kankan}};
            KarassMessage = new KarassMessage();

        }

        object IEnumerator.Current => Current;
        public void Pause(bool pauseState) => Paused = pauseState;
        public T Get(string tag)
        {
            KanKans.TryGetValue(tag, out T kanKan);
            if (kanKan == null)
            {
                throw new NoKanKanWithTag(tag);
            }
            return kanKan;
        }

        public void Add(T kankan, string tag)
        {
            if (KanKans.ContainsKey(tag))
            {
                throw new DuplicateKanKanTag(tag);
            }

            KanKans.Add(tag, kankan);
        }
        public void Dispose()
        {
            KanKans.Clear();
        }

        public abstract bool MoveNext();
        public abstract void Reset();
        public abstract void Run(string tag);

      
    }
}