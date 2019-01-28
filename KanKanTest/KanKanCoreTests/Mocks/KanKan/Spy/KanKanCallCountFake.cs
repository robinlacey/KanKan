using System;
using System.Collections;
using System.Collections.Generic;
using KanKanCore.Interface;

namespace KanKanTest.KanKanCoreTests.Mocks.KanKan.Spy
{
    public class KanKanCallCountFake : IKanKan
    {
        private readonly bool _defaultReturn;
        private readonly bool _toReturn;
        private readonly int _afterTicks;

        public KanKanCallCountFake(bool defaultReturn, bool toReturn, int afterTicks)
        {
            _defaultReturn = defaultReturn;
            _toReturn = toReturn;
            _afterTicks = afterTicks;
        }
        public int MoveNextCallCount { get; private set; }
        public bool MoveNext()
        {
            MoveNextCallCount++;
            return MoveNextCallCount > _afterTicks ? _toReturn : _defaultReturn;
        }
        public int ResetCallCount { get; private set; }
        public void Reset()
        {
            ResetCallCount++;
        }

        public IKanKanCurrentState Current { get; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public string ID { get; }
        public List<IKarassState> AllKarassStates { get; }
        public void SendMessage(string message)
        {
            throw new System.NotImplementedException();
        }

        public void SetKarassMessage(IKarassMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}