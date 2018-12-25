using System;
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
        public object Current => CurrentState;
        public IKarassMessage KarassMessage { get; }

        public List<FrameRequest> NextFrames => CurrentState.NextFrames;
        public List<FrameRequest> LastFrames => CurrentState.LastFrames;

        private int _currentKarass;
        private KarassState CurrentState => _allKarassStates[_currentKarass];
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
            return HasNextFrame(karassState);
        }


        private bool HasNextFrame(KarassState karassState)
        {
            Console.WriteLine("a");
            if (KarassStateBehaviour.IsEmptyKarass(karassState.Karass))
            {
                Console.WriteLine("b");
                return ProcessEmptyKarass(karassState);
            }

            SetNextAndLastFrames(karassState);

            for (int index = 0; index < karassState.Karass.FramesCollection.Count; index++)
            {
                Console.WriteLine("c"+index);
                if (ShouldSkipFrame(karassState, index))
                {
                  
                    if (LastFrameCollection(index, karassState))
                    {
                        Console.WriteLine("d");
                        return false;
                    }
                    Console.WriteLine("e");
                    continue;
                }

                KarassStateBehaviour.InvokeSetupActionsOnFirstFrame(
                    karassState.CurrentFrames[GetIDAndFrameRequests(karassState, index)], index, karassState.Karass);

                if (HasNotFinishedFrameCollection(karassState, index))
                {
                    Console.WriteLine("f");
                    continue;
                }

                if (HasFinishedAllFrameCollections())
                {     
                    Console.WriteLine("g");
                    return false;
                }
                Console.WriteLine("h");
                _currentKarass++;
                return true;
            }

            KarassMessage.ClearMessage();

            return true;
        }
        
        private bool HasFinishedAllFrameCollections()
        {
            return _allKarassStates.Count - 1 < _currentKarass + 1;
        }

        private bool HasNotFinishedFrameCollection(KarassState karassState, int index)
        {
            int lastFrameCount = 0;
            KarassStateBehaviour.InvokeTeardownActionsIfLastFrame(
                index,
                KarassStateBehaviour.AddFrame(GetIDAndFrameRequests(karassState, index), karassState.CurrentFrames),
                ref lastFrameCount,
                out bool shouldComplete,
                karassState);
            return !shouldComplete;
        }

        private bool ShouldSkipFrame(KarassState karassState, int index)
        {
            return KarassStateBehaviour.FrameSetAlreadyFinished(index, karassState.Complete) ||
                   FrameRequestArrayIsEmpty(karassState, index) ||
                   !InvokeCurrentFrame(index,
                       karassState.CurrentFrames[GetIDAndFrameRequests(karassState, index)],
                       KarassMessage,
                       karassState.Karass);
        }
        
        private void SetNextAndLastFrames(KarassState karassState)
        {
            KarassStateBehaviour.MoveNextFramesToLastFrames(karassState);

            karassState.NextFrames.Clear();
        }

        private bool ProcessEmptyKarass(KarassState karassState)
        {
            KarassStateBehaviour.InvokeAllSetupActions(karassState.Karass);
            KarassStateBehaviour.InvokeAllTeardownActions(karassState.Karass);

            if (LastKarass())
            {
               
                return false;
            }

            _currentKarass++;
            return MoveNext();
        }

        private bool LastKarass()
        {
            return _allKarassStates.Count - 1 < _currentKarass + 1;
        }

        private bool LastFrameCollection(int index, KarassState karassState)
        {
            return index == karassState.Karass.FramesCollection.Count - 1;
        }

        private bool FrameRequestArrayIsEmpty(KarassState karassState, int index)
        {
            return !karassState.Karass.FramesCollection[index].Any() ||
                   karassState.CurrentFrames[GetIDAndFrameRequests(karassState, index)] >
                   (karassState.Karass.FramesCollection[index].Length - 1);
        }


        private UniqueKarassFrameRequestID GetIDAndFrameRequests(KarassState karassState, int index)
        {
            return new UniqueKarassFrameRequestID(karassState.Karass.ID, index,
                karassState.Karass.FramesCollection[index]);
        }

        private bool InvokeCurrentFrame(int index, int karassStateCurrentFrame, IKarassMessage message, IKarass karass)
        {
            return FrameFactory.Execute(karass.FramesCollection[index][karassStateCurrentFrame], message.Message);
        }

        public void Reset()
        {
            foreach (KarassState data in _allKarassStates)
            {
                data.Reset();
            }
        }
    }
}