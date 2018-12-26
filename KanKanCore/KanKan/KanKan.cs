using System.Collections.Generic;
using System.Linq;
using KanKanCore.Interface;
using KanKanCore.KanKan.CurrentState;
using KanKanCore.Karass;
using KanKanCore.Karass.Message;
using KanKanCore.Karass.Struct;

// Kan-Kan - "A Kan-Kan is the instrument which brings one into his or her karass"
// (Cat's Cradle - Kurt Vonnegut)

namespace KanKanCore.KanKan
{
    public class KanKan : IKanKan
    {
        public object Current => GetCurrentState();
        private int _currentKarass;
        private int _currentFrame;
        
        private readonly List<KarassState> _allKarassStates;
        private readonly IKarassMessage _karassMessage;
        private readonly IFrameFactory _frameFactory;
        public KanKan(IKarass karass, IFrameFactory frameFactory, IKarassMessage message = null)
        {
            _karassMessage = message ?? new KarassMessage();
            _frameFactory = frameFactory;
            _allKarassStates = new List<KarassState> {new KarassState(karass)};
        }

        public KanKan(IKarass[] karass, IFrameFactory frameFactory)
        {
            _karassMessage = new KarassMessage();
            _frameFactory = frameFactory;

            _allKarassStates = karass.ToList().Select(_ => new KarassState(_)).ToList();

            for (int i = 0; i < karass.Length; i++)
            {
                _allKarassStates[i] = new KarassState(karass[i]);
            }
        }

        public void SendMessage(string message)
        {
            _karassMessage.SetMessage(message);
        }

        public IKanKanCurrentState GetCurrentState()
        {
            return new KanKanCurrentState()
            {
                NextFrames = _allKarassStates[_currentKarass].NextFrames,
                LastFrames = _allKarassStates[_currentKarass].LastFrames,
                FrameFactory = _frameFactory,
                KarassMessage = _karassMessage,
                Frame = _currentFrame
            };
        }


        public bool MoveNext()
        {
            KarassState karassState = _allKarassStates[_currentKarass];
            return HasNextFrame(karassState);
        }


        private bool HasNextFrame(KarassState karassState)
        {
         //Console.WriteLine("a");
            if (KarassStateBehaviour.IsEmptyKarass(karassState.Karass))
            {
                //Console.WriteLine("b");
                return ProcessEmptyKarass(karassState);
            }

            SetNextAndLastFrames(karassState);
            
            for (int index = 0; index < karassState.Karass.FramesCollection.Count; index++)
            {
//                Console.WriteLine("c"+index);
                if (ShouldSkipFrame(karassState, index))
                {
                  
                    if (LastFrameCollection(index, karassState))
                    {
//                        Console.WriteLine("d");
                        return false;
                    }
//                    Console.WriteLine("e");
                    continue;
                }

                bool progress = InvokeCurrentFrame(index,
                    karassState.CurrentFrames[GetIDAndFrameRequests(karassState, index)],
                    _karassMessage,
                    karassState.Karass);
                    _currentFrame = karassState.CurrentFrames[GetIDAndFrameRequests(karassState, index)]+1;
                if (!progress)
                {
                    if (LastFrameCollection(index, karassState))
                    {
//                        Console.WriteLine("d");
                        return false;
                    }

                    continue;
                }

                
                KarassStateBehaviour.InvokeSetupActionsOnFirstFrame(
                    karassState.CurrentFrames[GetIDAndFrameRequests(karassState, index)], index, karassState.Karass);

                if (HasNotFinishedFrameCollection(karassState, index))
                {
//                    Console.WriteLine("f");
                    continue;
                }

                if (HasFinishedAllFrameCollections())
                {     
//                    Console.WriteLine("g");
                    return false;
                }
//                Console.WriteLine("h");
                _currentKarass++;
                return true;
            }

            _karassMessage.ClearMessage();

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
                   FrameRequestArrayIsEmpty(karassState, index);
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
            return _frameFactory.Execute(karass.FramesCollection[index][karassStateCurrentFrame], message.Message);
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