using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Karass;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanCore.Karass.Message;
using KanKanCore.Karass.Struct;

// Kan-Kan - "A Kan-Kan is the instrument which brings one into his or her karass"
// (Cat's Cradle - Kurt Vonnegut)

namespace KanKanCore
{
    public class KanKan : IKanKan
    {
        public object Current => CurrentState.NextFrames;
        public IKarassMessage KarassMessage { get; }

        public List<FrameRequest> NextFrames => CurrentState.NextFrames;
        public List<FrameRequest> LastFrames => CurrentState.LastFrames;
        
        private int _currentKarass;
        public KarassState CurrentState => _allKarassStates[_currentKarass];
        private readonly List<KarassState> _allKarassStates;

        public IFrameFactory FrameFactory { get; }
        public KanKan(IKarass karass, IFrameFactory frameFactory, IKarassMessage message = null)
        {
            KarassMessage = message ?? new KarassMessage();
            FrameFactory = frameFactory;
            _allKarassStates = new List<KarassState> {new KarassState(karass)};
        }

        public KanKan(IKarass[] karass, IFrameFactory frameFactory)
        {
            KarassMessage = new KarassMessage();
            FrameFactory = frameFactory;
            
            _allKarassStates = karass.ToList().Select(_ => new KarassState(_)).ToList();

            for (int i = 0; i < karass.Length; i++)
            {
                _allKarassStates[i] = new KarassState(karass[i]);
            }
        }

        public void SendMessage(string message)
        {
            KarassMessage.SetMessage(message);
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
           
            KarassStateBehaviour.MoveNextFramesToLastFrames(karassState);
            
            karassState.NextFrames.Clear();

            for (int index = 0; index < karassState.Karass.FramesCollection.Count; index++)
            {
                if (KarassStateBehaviour.FrameSetAlreadyFinished(index, karassState.Complete))
                {
                    continue;
                }

                KarassStateBehaviour.InvokeSetupActionsOnFirstFrame(
                    karassState.CurrentFrames[GetIDAndFrameRequests(karassState, index)],
                    index,
                    karassState.Karass);

                if (!InvokeCurrentFrame(index,
                    karassState.CurrentFrames[GetIDAndFrameRequests(karassState, index)],
                    KarassMessage,
                    karassState.Karass))
                {
                    
                    continue;
                }

                KarassStateBehaviour.InvokeTeardownActionsIfLastFrame(
                    index,
                    KarassStateBehaviour.AddFrame(GetIDAndFrameRequests(karassState, index), karassState.CurrentFrames),
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

            KarassMessage.ClearMessage();

            return true;
        }


        private UniqueKarassFrameRequestID GetIDAndFrameRequests(KarassState karassState, int index)
        {
            return new UniqueKarassFrameRequestID(karassState.Karass.ID,index,karassState.Karass.FramesCollection[index]);
        }

        private bool InvokeCurrentFrame(int index, int karassStateCurrentFrame, IKarassMessage message, IKarass karass)
        {
            return FrameFactory.Execute(karass.FramesCollection[index][karassStateCurrentFrame], message.Message);
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