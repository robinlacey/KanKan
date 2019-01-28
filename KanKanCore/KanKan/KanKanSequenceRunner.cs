using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Exception;
using KanKanCore.Interface;
using KanKanCore.Karass.Message;

namespace KanKanCore.KanKan
{
    public class KanKanSequenceRunner : KanKanRunner<IKanKan[]>
    {
        private IKanKan[] _currentKanKanSequence;
        private readonly string _constructorTag;
        private int _index;

    
 
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

        public KanKanSequenceRunner(IKanKan[] kankan, string tag) : base(kankan, tag)
        {            
            _currentKanKanSequence = kankan;
            _constructorTag = tag;
            Current = kankan[_index];
        }
    }
}