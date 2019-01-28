using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Exception;
using KanKanCore.Interface;
using KanKanCore.Karass.Message;

namespace KanKanCore.KanKan
{
    public class KanKanSequenceRunner : KanKanRunner, IKanKanSequenceRunner
    {
        private IKanKan[] _currentKanKanSequence;
        private readonly string _constructorTag;
        private int _index;
        protected readonly Dictionary<string, IKanKan[]> KanKans = new Dictionary<string, IKanKan[]>();

        public KanKanSequenceRunner(IKanKan[] currentKanKan, string tag)
        {
            _currentKanKanSequence = currentKanKan;
            _constructorTag = tag;
            Current = currentKanKan[_index];
            KanKans.Add(tag, currentKanKan);
            KarassMessage = new KarassMessage();
        }

        public void Add(IKanKan[] kanKans, string tag)
        {
            if (KanKans.ContainsKey(tag))
            {
                throw new DuplicateKanKanTag(tag);
            }

            KanKans.Add(tag, kanKans);
        }

        public override bool MoveNext()
        {
            if (Paused)
            {
                return true;
            }


            bool returnValue = Current.MoveNext();
            
            if (returnValue || (_index >= _currentKanKanSequence.Length - 1))
            {
                return returnValue;
            }
            
            _index++;
            Current = _currentKanKanSequence[_index];
            return true;
        }

        public override void Reset()
        {
            KanKans.Values.ToList().ForEach(a => a.ToList().ForEach(k => k.Reset()));
           _currentKanKanSequence = KanKans[_constructorTag];
           _index = 0;
           Current = _currentKanKanSequence[0];

        }

        public override void Run(string tag)
        {
            KanKans.TryGetValue(tag, out IKanKan[] kanKans);
            if (kanKans != null)
            {
                _currentKanKanSequence.ToList().ForEach(k => k.Reset());
                _currentKanKanSequence = kanKans;
                _index = 0;
                Current = kanKans[0];
            }
            else
            {
                throw new NoKanKanWithTag(tag);
            }
        }

        public IKanKan[] Get(string tag)
        {
            KanKans.TryGetValue(tag, out IKanKan[] kanKan);
            return kanKan ?? throw new NoKanKanWithTag(tag);
        }
        
        
        public override void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}