using System.Collections.Generic;
using KanKanCore.Factories;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanTest.KanKanCoreTests.Factories;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests.Sequence
{
    public class MoveNextTests
    {
        [TestFixture(1)]
        [TestFixture(4)]
        public class GivenAnArrayOfKanKan:MoveNextTests
        {
          
            private readonly int _arraySize;

            public GivenAnArrayOfKanKan(int arraySize)
            {
                _arraySize = arraySize;
            }

            [TestCase(1)]
            [TestCase(3)]
            public void ThenRunKarassInOrder(int framesInEachKanKan)
            {
                KarassFactory karassFactory = new KarassFactory();
                IDependencies dependencies = new KarassDependencies();
                FrameFactory frameFactory = new FrameFactory(dependencies);
                MockFramesFactory mockFramesFactory = new MockFramesFactory(frameFactory);
                
                
                IKanKan[] kanKanArray = new IKanKan[_arraySize];
                for (int i = 0; i < _arraySize; i++)
                {
                    List<FrameRequest> spies = new List<FrameRequest>();
                    for (int j = 0; j < framesInEachKanKan; j++)
                    {
                        bool Spy(string message)
                        {
                            return true;
                        }

                        FrameRequest frameRequest = mockFramesFactory.GetValidFrameRequest(Spy);
                        spies.Add(frameRequest);
                    }

                  
                    IKarass karass = karassFactory.Get(() => { }, () => { }, spies.ToArray());
                    kanKanArray[i] = new KanKan(karass, frameFactory);
                }
                
                KanKanSequenceRunner kanKanRunner = new KanKanSequenceRunner(kanKanArray, "Cat");
                int totalSteps = _arraySize * framesInEachKanKan;
               
                for (int i = 0; i < totalSteps-1; i++)
                {
                    Assert.True(kanKanRunner.MoveNext());
                }
                Assert.False(kanKanRunner.MoveNext());
            } 
        }
    }
}