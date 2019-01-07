using System;
using System.Collections;
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
        public string ID { get; }
        
        private int _currentKarass;
        private int _currentFrame;

        protected List<IKarassState> AllKarassStates;
        protected IKarassMessage KarassMessage = new KarassMessage();
        private readonly IFrameFactory _frameFactory;
        private string _nextMessage;
        private string _lastMessage;

        public KanKan(IKarass karass, IFrameFactory frameFactory)
        {
            _frameFactory = frameFactory;
            AllKarassStates = new List<IKarassState>
            {
                new KarassState(karass)
            };
            _nextMessage = KarassMessage.Message;
            
            ID = GetID();
        }

        private static string GetID()
        {
            return Guid.NewGuid().ToString();
        }

        public KanKan(IReadOnlyList<IKarass> karass, IFrameFactory frameFactory)
        {
            _frameFactory = frameFactory;
            AllKarassStates = karass.ToList().Select(_ => (IKarassState) new KarassState(_)).ToList();
            _nextMessage = KarassMessage.Message;
            
            SetKarassStates(karass);

            ID =  GetID();
        }

        private void SetKarassStates(IReadOnlyList<IKarass> karass)
        {
            for (int i = 0; i < karass.Count; i++)
            {
                AllKarassStates[i] = new KarassState(karass[i]);
            }
        }

        public void SendMessage(string message)
        {
            KarassMessage.SetMessage(message);
        }

        public void SetKarassMessage(IKarassMessage message)
        {
            KarassMessage = message;
            _nextMessage = KarassMessage.Message;
        }

        public IKanKanCurrentState GetCurrentState()
        {
            Console.WriteLine(AllKarassStates.Count);
            return new KanKanCurrentState
            {
                NextFrames = AllKarassStates[_currentKarass].NextFrames,
                LastFrames = AllKarassStates[_currentKarass].LastFrames,
                FrameFactory = _frameFactory,
                KarassMessage = KarassMessage,
                NextMessage = _nextMessage,
                LastMessage = _lastMessage,
                Frame = _currentFrame
            };
        }


        public virtual bool MoveNext()
        {
            SetMessages();
            return HasNextFrame(AllKarassStates[_currentKarass]);
        }

        private void SetMessages()
        {
            if (KarassMessage.Message == _nextMessage)
            {
                KarassMessage.ClearMessage();
            }

            _lastMessage = _nextMessage;
            _nextMessage = KarassMessage.Message;
        }


        private bool HasNextFrame(IKarassState karassState)
        {
            
            if (KarassStateBehaviour.IsEmptyKarass(karassState.Karass))
            {
                return ProcessEmptyKarass(karassState);
            }

            SetNextAndLastFrames(karassState);
            
            for (int index = 0; index < karassState.Karass.FramesCollection.Count; index++)
            {
                if (ShouldSkipFrame(karassState, index))
                {
                  
                    if (LastFrameCollection(index, karassState))
                    {
                        return false;
                    }
                    continue;
                }

                bool progress = InvokeCurrentFrame(index,
                    karassState.CurrentFrames[GetIDAndFrameRequests(karassState, index)],
                    KarassMessage,
                    karassState.Karass);
                    _currentFrame = karassState.CurrentFrames[GetIDAndFrameRequests(karassState, index)]+1;
                if (!progress)
                {  
                    continue;
                }

                
                KarassStateBehaviour.InvokeSetupActionsOnFirstFrame(
                    karassState.CurrentFrames[GetIDAndFrameRequests(karassState, index)], index, karassState.Karass);

                if (HasNotFinishedFrameCollection(karassState, index))
                {
                    continue;
                }

                if (HasFinishedAllFrameCollections())
                {     
                    return false;
                }
                _currentKarass++;
                return true;
            }

            
            return true;
        }
        
        private bool HasFinishedAllFrameCollections()
        {
            return AllKarassStates.Count - 1 < _currentKarass + 1;
        }

        private bool HasNotFinishedFrameCollection(IKarassState karassState, int index)
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

        private bool ShouldSkipFrame(IKarassState karassState, int index)
        {
            return KarassStateBehaviour.FrameSetAlreadyFinished(index, karassState.Complete) ||
                   FrameRequestArrayIsEmpty(karassState, index);
        }
        
        private void SetNextAndLastFrames(IKarassState karassState)
        {
            KarassStateBehaviour.MoveNextFramesToLastFrames(karassState);

            karassState.NextFrames.Clear();
        }

        private bool ProcessEmptyKarass(IKarassState karassState)
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
            return AllKarassStates.Count - 1 < _currentKarass + 1;
        }

        private bool LastFrameCollection(int index, IKarassState karassState)
        {
            return index == karassState.Karass.FramesCollection.Count - 1;
        }

        private bool FrameRequestArrayIsEmpty(IKarassState karassState, int index)
        {
            return !karassState.Karass.FramesCollection[index].Any() ||
                   karassState.CurrentFrames[GetIDAndFrameRequests(karassState, index)] >
                   karassState.Karass.FramesCollection[index].Length - 1;
        }


        private UniqueKarassFrameRequestID GetIDAndFrameRequests(IKarassState karassState, int index)
        {
            return new UniqueKarassFrameRequestID(karassState.Karass.ID, index);
        }

        private bool InvokeCurrentFrame(int index, int karassStateCurrentFrame, IKarassMessage message, IKarass karass)
        {
            return _frameFactory.Execute(karass.FramesCollection[index][karassStateCurrentFrame], message.Message);
        }

        public void Reset()
        {
            foreach (KarassState data in AllKarassStates)
            {
                data.Reset();
            }
        }

        public IKanKanCurrentState Current => GetCurrentState();

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            AllKarassStates.ForEach(s=>s.Reset());
        }
    }
}