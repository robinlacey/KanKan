using System;
using System.Collections.Generic;
using KanKanCore.Exception;
using KanKanCore.Interface;
using KanKanCore.Karass.Message;

namespace KanKanCore.KanKan
{
    public class KanKanSequenceRunner: KanKanRunner, IKanKanSequenceRunner
    {
        private readonly IKanKan[] _kanKanSequence;
        private int _index;
        private Dictionary<string,IKanKan[]> _kanKans = new Dictionary<string,IKanKan[]>();

        public KanKanSequenceRunner(IKanKan[] kanKan, string tag)
        {
            _kanKanSequence = kanKan;

            Current = kanKan[_index];
            _kanKans.Add(tag, kanKan);
            KarassMessage = new KarassMessage();
            
        }

        public IKanKan[] Get(string tag)
        {
            throw new NotImplementedException();
        }

        public void Add(IKanKan[] kanKans, string tag)
        {
            if (_kanKans.ContainsKey(tag))
            {
                throw new DuplicateKanKanTag(tag);
            }

            _kanKans.Add(tag, kanKans);
        }
        
        public override bool MoveNext()
        {
            if (Paused)
            {
                return true;
            }

            if (_kanKanSequence == null || _kanKanSequence.Length <= 0)
            {
                return Current.MoveNext();
            }
            bool returnValue = Current.MoveNext();
            if (returnValue || (_index >= _kanKanSequence.Length - 1))
            {
                return returnValue;
            }
            _index++;
            Current = _kanKanSequence[_index];
            return true;

        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

    
        public override void Dispose()
        {
            throw new NotImplementedException();
        }
        
        public override void Run(string tag)
        {
            throw new NotImplementedException();
        }
    }
}