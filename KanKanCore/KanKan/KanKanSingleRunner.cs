using System.Collections.Generic;
using KanKanCore.Exception;
using KanKanCore.Interface;
using KanKanCore.Karass.Message;

namespace KanKanCore.KanKan
{
    public class KanKanSingleRunner:KanKanRunner, IKanKanSingleRunner
    {
        private readonly Dictionary<string, IKanKan> _kanKans = new Dictionary<string, IKanKan>();
        public override bool MoveNext() => Paused || Current.MoveNext();
        private readonly IKanKan _constructorKanKan;
        public IKanKan Get(string tag)
        {
            _kanKans.TryGetValue(tag, out IKanKan kanKan);
            return kanKan ?? throw new NoKanKanWithTag(tag);
        }

        public void Add(IKanKan kanKan, string tag)
        {
            if (_kanKans.ContainsKey(tag))
            {
                throw new DuplicateKanKanTag(tag);
            }

            _kanKans.Add(tag, kanKan);
        }

        public KanKanSingleRunner(IKanKan kanKan, string tag)
        {
            _constructorKanKan = kanKan;
            Current = _constructorKanKan;
            _kanKans.Add(tag, Current);
            KarassMessage = new KarassMessage();
        }
        
        public override void Reset()
        {
            foreach (IKanKan kanKan in _kanKans.Values)
            {
                kanKan.Reset();
            }

            Paused = false;
            Current = _constructorKanKan;
        }
         
        public override void Run(string tag)
        {
            _kanKans.TryGetValue(tag, out IKanKan kanKan);
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