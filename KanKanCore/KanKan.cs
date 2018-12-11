using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Karass.Interface;

// Kan-Kan: the instrument that brings individuals into their karass
// (Cat's Cradle - Kurt Vonnegut)

namespace KanKanCore
{
    public class KanKan : IEnumerator
    {
        public object Current => CurrentState.NextFrames;
        private readonly IKarassMessage _message;
        private int _currentKarass;
        public KarassState CurrentState => _allKarassStates[_currentKarass];
        private readonly List<KarassState> _allKarassStates;


        public KanKan(IKarass karass, IKarassMessage karassMessage)
        {
            _allKarassStates = new List<KarassState> {new KarassState(karass)};
            _message = karassMessage;
        }

        public KanKan(IKarass[] karass, IKarassMessage karassMessage)
        {
            _allKarassStates = karass.ToList().Select(_ => new KarassState(_)).ToList();

            for (int i = 0; i < karass.Length; i++)
            {
                _allKarassStates[i] = new KarassState(karass[i]);
            }

            _message = karassMessage;
        }

        public void SendMessage(string message)
        {
            _message.SetMessage(message);
        }


        public bool MoveNext()
        {
            KarassState karassState = _allKarassStates[_currentKarass];

            int lastFrameCount = 0;

            if (KarassStateBehaviour.EmptyKarass(karassState.Karass))
            {
                KarassStateBehaviour.InvokeAllSetupActions(karassState.Karass);
                KarassStateBehaviour.InvokeAllTeardownActions(karassState.Karass);

                if (_allKarassStates.Count - 1 < _currentKarass + 1)
                {
                    return false;
                }

                _currentKarass++;
                return MoveNext();
            }

            karassState.NextFrames.Clear();

            for (int index = 0; index < karassState.Karass.FramesCollection.Count; index++)
            {
                if (KarassStateBehaviour.FrameSetAlreadyFinished(index, karassState.Complete))
                {
                    continue;
                }

                KarassStateBehaviour.InvokeSetupActionsOnFirstFrame(
                    karassState.CurrentFrames[karassState.Karass.FramesCollection[index]],
                    index,
                    karassState.Karass);

                if (!KarassStateBehaviour.InvokeCurrentFrame(index,
                    karassState.CurrentFrames[karassState.Karass.FramesCollection[index]],
                    _message,
                    karassState.Karass))
                {
                    continue;
                }

                KarassStateBehaviour.InvokeTeardownActionsIfLastFrame(
                    index,
                    KarassStateBehaviour.AddFrame(karassState.Karass.FramesCollection[index], karassState.CurrentFrames),
                    ref lastFrameCount,
                    out bool shouldComplete,
                    karassState);

                if (!shouldComplete)
                {
                    continue;
                }

                if (_allKarassStates.Count - 1 < _currentKarass + 1)
                {
                    return false;
                }

                _currentKarass++;
                return true;
            }

            _message.ClearMessage();

            return true;
        }

        public void Reset()
        {
            foreach (var data in _allKarassStates)
            {
                data.Reset();
            }
        }
    }
}