using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Exception;
using KanKanCore.Interface;
using KanKanCore.KanKan.CurrentState;
using KanKanCore.Karass;
using KanKanCore.Karass.Frame;
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
        private int _totalFramesRun;

        public List<IKarassState> AllKarassStates { get; protected set; }
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

            ID = GetID();
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

        private IKanKanCurrentState GetCurrentState()
        {
            return new KanKanCurrentState
            {
                NextFrames = AllKarassStates[_currentKarass].NextFrames,
                LastFrames = AllKarassStates[_currentKarass].LastFrames,
                FrameFactory = _frameFactory,
                KarassMessage = KarassMessage,
                NextMessage = _nextMessage,
                LastMessage = _lastMessage,
                TotalFramesRun = _totalFramesRun
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
        
        
     

        private struct SplitFramesCollection
        {
            public List<int> FramesToSkip;
            public List<int> Frames;
        }

        private SplitFramesCollection SplitCurrentFramesCollection(IKarassState karassState)
        {
            SplitFramesCollection returnFrames = new SplitFramesCollection()
            {
                Frames = new List<int>(),
                FramesToSkip = new List<int>()
            };
            for (int i = 0; i < karassState.Karass.FramesCollection.Count; i++)
            {
                if (ShouldSkipFrame(karassState, i))
                {
                    returnFrames.FramesToSkip.Add(i);
                }
                else
                {
                    returnFrames.Frames.Add(i);
                }
            }
            return returnFrames;
        }

        private bool HasNextFrame(IKarassState karassState)
        {
            if (KarassStateBehaviour.IsEmptyKarass(karassState.Karass))
            {
                return ProcessEmptyKarass(karassState);
            }

            SetNextAndLastFrames(karassState);

            int frameRequestArraysCompleted = 0;

            SplitFramesCollection splitFramesCollection = SplitCurrentFramesCollection(karassState);
            
            // Deal with skipped frames
            for (int index = 0; index < splitFramesCollection.FramesToSkip.Count; index++)
            {
                // If we're at the end of the collection, bail out.
                if (LastFrameCollection(index, karassState))
                {

                    if (LastKarassState())
                    {
                        return false;
                    }

                    _currentKarass++;
                    // Increment next frame and progress.
                    return HasNextFrame(AllKarassStates[_currentKarass]);
                }
            }
            
            for (int i = 0; i < splitFramesCollection.Frames.Count; i++)
            {
                int index = splitFramesCollection.Frames[i];
                // We use a complex struct called UniqueKarassFrameRequestID to reference the correct frame in a dictionary so we can run matching Karass alongside eachtother.
                UniqueKarassFrameRequestID frameRequestID = GetIDAndFrameRequests(karassState, index);
                int currentFrameNumber = karassState.CurrentFrames[frameRequestID];
                
                if (currentFrameNumber == 0)
                {
                    karassState.Karass.Setup(index);
                }

                // Run the frame
                if (InvokeCurrentFrame(index, currentFrameNumber, KarassMessage, karassState.Karass))
                {
                     IncrementFrameNumbers(ref currentFrameNumber);

                    // Update the dictionary so we can grab the frame next time around
                    karassState.CurrentFrames[frameRequestID] = currentFrameNumber;
             
                
                    if ( LastFrame(karassState, currentFrameNumber, index))
                    {
                        frameRequestArraysCompleted++;
                        TeardownKarass(index, karassState);
                    
                        if (ShouldComplete(karassState,frameRequestArraysCompleted))
                        {
                            if (HasFinishedAllFrameCollections())
                            {
                                return false;
                            }
                
                            // Move to the next Karass
                            _currentKarass++;
                            return true;
                        }
                    }
                    else
                    {
                        AddNextFrame(karassState, index, currentFrameNumber);
                    } 
                }
            }
             return true;
        }

        private void IncrementFrameNumbers(ref int currentFrameNumber)
        {
            currentFrameNumber++;
            _totalFramesRun = currentFrameNumber;
         
        }

        private static void AddNextFrame(IKarassState karassState, int index, int currentFrameNumber)
        {
            karassState.NextFrames.Add(karassState.Karass.FramesCollection[index][currentFrameNumber]);
        }

        private bool ShouldComplete(IKarassState karassState, int frameRequestArraysCompleted)
        {
            // Are we dome with this karassState.Karass.FramesCollection? Yes if we've torn down all frames
            bool shouldComplete = frameRequestArraysCompleted == karassState.Karass.FramesCollection.Count;
            return shouldComplete;
        }

        private void TeardownKarass(int index, IKarassState karassState)
        {
              
            // runs teardown on array number in this Karass
            karassState.Karass.Teardown(index);
            // Mark this index as complete inside the karassState.
            // This allows us to skip the frame on start
            karassState.Complete[index] = true;
        }

        private bool LastKarassState()
        {
            return _currentKarass == AllKarassStates.Count - 1;
        }

        private static bool LastFrame(IKarassState karassState, int currentFrameNumber, int index)
        {
            bool lastFrame;
            if (karassState.Karass.FramesCollection.Any())
            {
                // Is last frame if current frame is  grater than all frames
                lastFrame = currentFrameNumber > karassState.Karass.FramesCollection[index].Length - 1;
            }
            else
            {
                // No frames - so it's last frame
                lastFrame = true;
            }

            return lastFrame;
        }

        private bool HasFinishedAllFrameCollections() =>_currentKarass+1 > (AllKarassStates.Count-1);


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
            foreach (IKarassState karassState in AllKarassStates)
            {
                KarassState data = (KarassState) karassState;
                data.Reset();
            }
        }

        public IKanKanCurrentState Current => GetCurrentState();

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            AllKarassStates.ForEach(s => s.Reset());
        }

        public static KanKan operator +(KanKan kanKanOne, KanKan kanKanTwo)
        {
            int maxSize = kanKanOne.AllKarassStates.Count > kanKanTwo.AllKarassStates.Count
                ? kanKanOne.AllKarassStates.Count
                : kanKanTwo.AllKarassStates.Count;
            int minSize = kanKanOne.AllKarassStates.Count > kanKanTwo.AllKarassStates.Count
                ? kanKanTwo.AllKarassStates.Count
                : kanKanOne.AllKarassStates.Count;
            IKarass[] karassArray = new IKarass[maxSize];
            for (int i = 0; i < maxSize; i++)
            {
                if (i < minSize)
                {
                    IKarass karass = CombineKarassInStatesAtIndex(kanKanOne, kanKanTwo, i);
                    karassArray[i] = karass;
                }
                else
                {
                    Karass.Karass karass = (kanKanOne.AllKarassStates.Count > kanKanTwo.AllKarassStates.Count)
                        ? kanKanOne.AllKarassStates[i].Karass as Karass.Karass
                        : kanKanTwo.AllKarassStates[i].Karass as Karass.Karass;

                    karassArray[i] = karass ?? throw new InvalidKarassTypeException();
                }
            }

            return new KanKan(karassArray, kanKanOne._frameFactory);
        }

        private static IKarass CombineKarassInStatesAtIndex(KanKan kanKanOne, KanKan kanKanTwo, int i)
        {
            Karass.Karass karassOne = kanKanOne.AllKarassStates[i].Karass as Karass.Karass;
            Karass.Karass karassTwo = kanKanTwo.AllKarassStates[i].Karass as Karass.Karass;
            if (karassOne == null || karassTwo == null)
            {
                throw new InvalidKarassTypeException();
            }

            IKarass karass = karassOne + karassTwo;
            return karass;
        }
    }
}