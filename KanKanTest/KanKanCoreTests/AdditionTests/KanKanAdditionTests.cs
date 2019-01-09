using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Exception;
using KanKanCore.Factories;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanTest.KanKanCoreTests.Factories;
using KanKanTest.KanKanCoreTests.Mocks.KarassMocks;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.AdditionTests
{
    public class KanKanAdditionTests
    {
        [Test]
        public void ThenThrowInvalidKarassTypeException()
        {
            Assert.Throws<InvalidKarassTypeException>(() =>
            {
                KanKan kankanOne = new KanKan(new KarassDummy(), null);
                KanKan kankanTwo = new KanKan(new KarassDummy(), null);
                KanKan kanKanThree = kankanOne + kankanTwo;
            });
        }

        public class CreatesAUniqueKanKan
        {
            [Test]
            public void IDDoesNotMatch()
            {
                KanKan kanKanOne =
                    new KanKan(
                        new Karass(new List<List<Action>>(), new List<List<Action>>(), new List<FrameRequest[]>()),
                        null);
                KanKan kanKanTwo =
                    new KanKan(
                        new Karass(new List<List<Action>>(), new List<List<Action>>(), new List<FrameRequest[]>()),
                        null);

                KanKan kanKanThree = kanKanOne + kanKanTwo;
                Assert.False(kanKanThree.ID == kanKanOne.ID);
                Assert.False(kanKanThree.ID == kanKanTwo.ID);
                Assert.AreNotSame(kanKanThree.Current, kanKanTwo.Current);
                Assert.AreNotSame(kanKanThree.Current, kanKanOne.Current);
            }

            public class GivenSingleKarassStateInBothKanKan
            {
                private readonly MockKarassFactory _mockKarassFactory;
                private readonly IFrameFactory _frameFactory;

                public GivenSingleKarassStateInBothKanKan()
                {
                    _frameFactory = new FrameFactory(new KarassDependencies());
                    _mockKarassFactory = new MockKarassFactory(new MockFramesFactory(_frameFactory));
                }

                [TestCase(2)]
                public void KarassStatesAreCombinedResultingInSingleCombineKarass(int size)
                {
                    Karass karassOne = _mockKarassFactory.GetKarassWithAmountOfData(1, size);
                    Karass karassTwo = _mockKarassFactory.GetKarassWithAmountOfData(1, size);

                    KanKan kanKanOne = new KanKan(karassOne, _frameFactory);
                    KanKan kanKanTwo = new KanKan(karassTwo, _frameFactory);

                    KanKan kanKanThree = kanKanOne + kanKanTwo;

                    Assert.True(kanKanThree.Current.NextFrames.Any(_ => karassOne.FramesCollection[0].Contains(_)));
                    Assert.True(kanKanThree.Current.NextFrames.Any(_ => karassTwo.FramesCollection[0].Contains(_)));
                }
            }

            public class GivenMultipleKarassStatesInKanKan
            {
                private readonly MockKarassFactory _mockKarassFactory;
                private readonly IFrameFactory _frameFactory;

                public GivenMultipleKarassStatesInKanKan()
                {
                    _frameFactory = new FrameFactory(new KarassDependencies());
                    _mockKarassFactory = new MockKarassFactory(new MockFramesFactory(_frameFactory));
                }

                [TestCase(3, 20, 1, 10)]
                [TestCase(9, 3, 12, 40)]
                public void MultipleKarassAreCombined(int karassOneArrayCount, int karassOneFrameRequests,
                    int karassTwoArrayCount, int karassTwoFrameRequests)
                {
                    if (karassTwoArrayCount == karassOneArrayCount)
                    {
                        Assert.Fail("karassOneArrayCount and karassTwoArrayCount should not be equal");
                    }

                    Karass karassOne =
                        _mockKarassFactory.GetKarassWithAmountOfData(karassOneArrayCount, karassOneFrameRequests);
                    Karass karassTwo =
                        _mockKarassFactory.GetKarassWithAmountOfData(karassTwoArrayCount, karassTwoFrameRequests);

                    KanKan kanKanOne = new KanKan(karassOne, _frameFactory);
                    KanKan kanKanTwo = new KanKan(karassTwo, _frameFactory);
        
                    KanKan kanKanThree = kanKanOne + kanKanTwo;

                    bool karassOneIsLarger = karassOneFrameRequests > karassTwoFrameRequests;
                    int largerCount = (karassOneIsLarger ? karassOneFrameRequests : karassTwoFrameRequests);
                    int smallerCount = (karassOneIsLarger ? karassTwoFrameRequests : karassOneFrameRequests);
                    
                    for (int i = 0; i < largerCount; i++)
                    {
                        if (i < smallerCount)
                        {   
                            Assert.True(kanKanThree.Current.NextFrames.Count == karassOneArrayCount + karassTwoArrayCount);
                        }
                        else
                        {
                            Assert.True(kanKanThree.Current.NextFrames.Count == (karassOneIsLarger?karassOneArrayCount: karassTwoArrayCount));
                        }

                        kanKanThree.MoveNext();
                    }

                    Assert.False(kanKanThree.Current.NextFrames.Any());
                }
            }
        }
    }
}