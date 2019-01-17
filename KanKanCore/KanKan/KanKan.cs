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


        private bool HasNextFrame(IKarassState karassState)
        {
            if (KarassStateBehaviour.IsEmptyKarass(karassState.Karass))
            {
                return ProcessEmptyKarass(karassState);
            }

            SetNextAndLastFrames(karassState);

            int frameRequestArraysCompleted = 0;
            
            for (int index = 0; index < karassState.Karass.FramesCollection.Count; index++)
            {
                // If we've finished the set or array is empty
                if (ShouldSkipFrame(karassState, index))
                {
                    // If we're at the end of the collection, bail out.
                    if (LastFrameCollection(index, karassState))
                    {

                        if (LastKarassState())
                        {
                            return false;
                        }
                        // Increment next frame and progress.
                        return HasNextFrame(AllKarassStates[_currentKarass++]);
                    }
                    // Keep spinning whist other frames run
                    // eg:
                    // [0]==== (done, this state)
                    // [1]============|current frame| 
                    // [2]== (done, this state)
                    continue;
                }
                
                // We use a complex struct called UniqueKarassFrameRequestID to reference the correct frame in a dictionary so we can run matching Karass alongside eachtother.
                UniqueKarassFrameRequestID frameRequestID = GetIDAndFrameRequests(karassState, index);
                int currentFrameNumber = karassState.CurrentFrames[frameRequestID];
                
                if (currentFrameNumber == 0)
                {
                    karassState.Karass.Setup(index);
                }

                // Run the frame
                bool progress = InvokeCurrentFrame(index, currentFrameNumber, KarassMessage, karassState.Karass);
                
                // We've run a frame, so we increase totalFramesRun. This is used when checking KarassState.
                _totalFramesRun = currentFrameNumber + 1;
                
                // If frame returns a 'false' we stay with it
                if (!progress)
                {
                    continue;
                }
               
                
                // Increase the frame number
                currentFrameNumber ++ ;
                // Update the dictionary so we can grab the frame next time around
                karassState.CurrentFrames[frameRequestID] = currentFrameNumber;
             
                bool shouldComplete = false;

              
                // If last frame flag 'should complete'
                if ( LastFrame(karassState, currentFrameNumber, index))
                { 
                    // runs teardown on array number in this Karass
                    karassState.Karass.Teardown(index);
                    // Mark this index as complete inside the karassState.
                    // This allows us to skip the frame on start
                    karassState.Complete[index] = true;
                    // Increase ticker
                    frameRequestArraysCompleted++;
                    // Are we dome with this karassState.Karass.FramesCollection? Yes if we've torn down all frames
                    shouldComplete = frameRequestArraysCompleted == karassState.Karass.FramesCollection.Count;
                }
                else
                {
                  // Otherwise, add the next frame
                    karassState.NextFrames.Add(karassState.Karass.FramesCollection[index][currentFrameNumber]);
                }

                if (!shouldComplete) {  continue; }

                if (HasFinishedAllFrameCollections())
                {
                    Console.WriteLine( "Returning false #1");
                    return false;
                }
                
                // Move to the next Karass
                _currentKarass++;
                return true;
            }
             return true;
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