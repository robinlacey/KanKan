using System.Collections.Generic;
using KanKanCore.Exception;
using KanKanCore.Interface;
using KanKanCore.Karass.Message;

namespace KanKanCore.KanKan
{
    public class KanKanSingleRunner:KanKanRunner, IKanKanSingleRunner
    {
        protected readonly Dictionary<string, IKanKan> KanKans = new Dictionary<string, IKanKan>();
        public override bool MoveNext() => Paused || Current.MoveNext();
        private readonly IKanKan _constructorKanKan;
        public IKanKan Get(string tag)
        {
            KanKans.TryGetValue(tag, out IKanKan kanKan);
            return kanKan ?? throw new NoKanKanWithTag(tag);
        }

        public void Add(IKanKan kanKan, string tag)
        {
            if (KanKans.ContainsKey(tag))
            {
                throw new DuplicateKanKanTag(tag);
            }

            KanKans.Add(tag, kanKan);
        }

        public KanKanSingleRunner(IKanKan kanKan, string tag)
        {
            _constructorKanKan = kanKan;
            Current = _constructorKanKan;
            KanKans.Add(tag, Current);
            KarassMessage = new KarassMessage();
        }
        
        public override void Reset()
        {
            foreach (IKanKan kanKan in KanKans.Values)
            {
                kanKan.Reset();
            }

            Paused = false;
            Current = _constructorKanKan;
        }
         
        public override void Run(string tag)
        {
            KanKans.TryGetValue(tag, out IKanKan kanKan);
            if (kanKan != null)
            {
                Current.Reset();
                Current = kanKan;
            }
            else
            {
                throw new NoKanKanWithTag(tag);
            }
        }
        
        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}