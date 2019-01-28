using System.Collections;
using System.Collections.Generic;
using KanKanCore.Exception;
using KanKanCore.Interface;
using KanKanCore.Karass.Message;

namespace KanKanCore.KanKan
{
    public abstract class KanKanRunner : IKanKanRunner
    {
        public IKanKan Current { get; protected set; }
        public IKarassMessage KarassMessage { get; set; }
        protected bool Paused;
        object IEnumerator.Current => Current;
        public void Pause(bool pauseState) => Paused = pauseState;
        public abstract bool MoveNext();
        public abstract void Reset();

        public abstract void Run(string tag);
        public abstract void Dispose();
    }
}