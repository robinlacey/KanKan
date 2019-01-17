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
using KanKanTest.KanKanCoreTests.Mocks.KanKan.Spy;
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
                public void ThenKarassStatesAreCombinedIntoOneKarass(int size)
                {
                    Karass karassOne = _mockKarassFactory.GetKarassWithAmountOfData(1, size);
                    Karass karassTwo = _mockKarassFactory.GetKarassWithAmountOfData(1, size);

                    KanKan kanKanOne = new KanKan(karassOne, _frameFactory);
                    KanKan kanKanTwo = new KanKan(karassTwo, _frameFactory);

                    KanKan kanKanThree = kanKanOne + kanKanTwo;

                    Assert.True(kanKanThree.Current.NextFrames.Any(_ => karassOne.FramesCollection[0].Contains(_)));
                    Assert.True(kanKanThree.Current.NextFrames.Any(_ => karassTwo.FramesCollection[0].Contains(_)));
                }


                public class WhenThereAreMultipleFrameArraysInEachKarass
                {
                    private readonly MockKarassFactory _mockKarassFactory;
                    private readonly IFrameFactory _frameFactory;

                    public WhenThereAreMultipleFrameArraysInEachKarass()
                    {
                        _frameFactory = new FrameFactory(new KarassDependencies());
                        _mockKarassFactory = new MockKarassFactory(new MockFramesFactory(_frameFactory));
                    }

                    [TestCase(4, 3, 7, 2)]
                    public void ThenCorrectlyCombineKarassFrameArrays(
                        int karassOneArrayCount,
                        int karassOneFrameRequests,
                        int karassTwoArrayCount,
                        int karassTwoFrameRequests)
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

                      
                        int largerCount = Math.Max(karassOneFrameRequests, karassTwoFrameRequests);
                        int smallerCount = Math.Min(karassOneFrameRequests, karassTwoFrameRequests);

                        for (int i = 0; i < largerCount; i++)
                        {
                            if (i < smallerCount)
                            {
                                Assert.True(kanKanThree.Current.NextFrames.Count ==
                                            karassOneArrayCount + karassTwoArrayCount);
                            }
                            else
                            {
                                int largerFrameRequests = (karassOneFrameRequests>karassTwoFrameRequests)? karassOneArrayCount : karassTwoFrameRequests;
                                Assert.True(kanKanThree.Current.NextFrames.Count ==largerFrameRequests);
                            }

                            kanKanThree.MoveNext();
                        }

                        Assert.False(kanKanThree.Current.NextFrames.Any());
                    }
                }
            }

            private static void CheckCombinedKassStateInKanKan(int karassOneArrayCount, int karassOneFrameRequests,
                int karassTwoArrayCount,
                int karassTwoFrameRequests, KanKan kanKanThree)
            {
                bool karassOneIsLarger = karassOneFrameRequests > karassTwoFrameRequests;
                int largerCount = Math.Max(karassOneFrameRequests, karassTwoFrameRequests);
                int smallerCount = Math.Min(karassOneFrameRequests, karassTwoFrameRequests);

                for (int i = 0; i < largerCount; i++)
                {
                    CheckCorrectNextFrameCount(kanKanThree, i, karassOneArrayCount, karassTwoArrayCount,
                        smallerCount);

                    kanKanThree.MoveNext();
                }
            }

            private static void CheckCorrectNextFrameCount(KanKan newKanKan, int index, int karassOneArrayCount,
                int karassTwoArrayCount,
                int smallestArrayCount)
            {
                if (index < smallestArrayCount)
                {
                    Assert.True(newKanKan.Current.NextFrames.Count ==
                                karassOneArrayCount + karassTwoArrayCount);
                }
                else
                {
                    Assert.True(
                        newKanKan.Current.NextFrames.Count == Math.Max(karassOneArrayCount, karassTwoArrayCount));
                }
            }

            public class GivenMultipleStates
            {
                private static int GetTotalFrames(int numberOfKarassInArray, int frameRequestArrayCount,
                    int numberOfFrames, MockKarassFactory mockKarassFactory, IFrameFactory frameFactory)
                {
                    Karass[] karassOne =
                        mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArray,
                            frameRequestArrayCount, numberOfFrames);

                    Karass[] karassTwo =
                        mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArray,
                            frameRequestArrayCount, numberOfFrames);


                    KanKan kanKan = new KanKan(karassOne, frameFactory) + new KanKan(karassTwo, frameFactory);

                    int totalTicks = numberOfKarassInArray;
                    int ticker = 0;

                    while (kanKan.MoveNext())
                    {
                        ticker++;
                        if (ticker > 9999)
                        {
                            Assert.Fail(
                                "Get TotalFrames has exceeded 9999 frames. Aborting. Check FrameRequests are returning true.");
                        }
                    }

                    return totalTicks;
                }

                public class WhenThereAreMatchingSizedKarassArrays
                {
                    private readonly MockKarassFactory _mockKarassFactory;
                    private readonly IFrameFactory _frameFactory;

                    public WhenThereAreMatchingSizedKarassArrays()
                    {
                        _frameFactory = new FrameFactory(new KarassDependencies());
                        _mockKarassFactory = new MockKarassFactory(new MockFramesFactory(_frameFactory));
                    }

                    [TestCase(2)]
                    [TestCase(6)]
                    public void ThenThereIsCorrectNumberOfStates(int numberOfKarassInArray)
                    {
                        Karass[] karassOne =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArray, 1, 1);

                        Karass[] karassTwo =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArray, 1, 1);

                        KanKan kanKan = new KanKan(karassOne, _frameFactory) + new KanKan(karassTwo, _frameFactory);
                        Assert.True(kanKan.AllKarassStates.Count == numberOfKarassInArray);
                    }

                    [TestCase(2, 3)]
                    [TestCase(7, 5)]
                    public void ThenThereIsCorrectNumberOfFramesWhenMultipleFrameRequestArrays(
                        int numberOfKarassInArray, int frameRequestArrayCount)
                    {
                        Karass[] karassOne =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArray,
                                frameRequestArrayCount, 1);

                        Karass[] karassTwo =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArray,
                                frameRequestArrayCount, 1);

                        KanKan kanKan = new KanKan(karassOne, _frameFactory) + new KanKan(karassTwo, _frameFactory);

                        Assert.True(TotalFrames(kanKan) == (2 * numberOfKarassInArray * frameRequestArrayCount));
                    }


                    [TestCase(9, 2)]
                    [TestCase(5, 3)]
                    public void ThenKanKanTicksTheCorrectNumberOfTimesWhenMultipleFrameRequestArrays(
                        int numberOfKarassInArray, int frameRequestArrayCount)
                    {
                        int frameRequestsInEachArray = 1;
                        int expected = numberOfKarassInArray * frameRequestsInEachArray;
                        Karass[] karassOne =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArray,
                                frameRequestArrayCount, frameRequestsInEachArray);


                        Karass[] karassTwo =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArray,
                                frameRequestArrayCount, frameRequestsInEachArray);

                        KanKan kanKan = new KanKan(karassOne, _frameFactory) + new KanKan(karassTwo, _frameFactory);

                        int ticker = 0;
                        for (int i = 0; i < expected - 1; i++)
                        {
                            bool moveNext = kanKan.MoveNext();
                            Assert.True(moveNext);
                            ticker++;
                        }
                        Assert.False(kanKan.MoveNext());
                    }

                    [TestCase(2, 4, 5)]
                    [TestCase(1, 3, 4)]
                    public void ThereThereAreACorrectNumberOfFramesWhenMultipleNumberOfFrameRequests(
                        int numberOfKarassInArray, int frameRequestArrayCount, int numberOfFrameRequests)
                    {
                        Karass[] karassOne =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArray,
                                frameRequestArrayCount, numberOfFrameRequests);

                        Karass[] karassTwo =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArray,
                                frameRequestArrayCount, numberOfFrameRequests);

                        KanKan kanKan = new KanKan(karassOne, _frameFactory) + new KanKan(karassTwo, _frameFactory);

                        Assert.True(TotalFrames(kanKan) ==
                                    (2 * numberOfKarassInArray * frameRequestArrayCount * numberOfFrameRequests));
                    }

//
                    [TestCase(2, 5, 3)]
                    [TestCase(2, 3, 5)]
                    public void ThenKanKanTicksTheCorrectNumberOfTimesWhenMultipleNumberOfFrameRequests(
                        int numberOfKarassInArray, int frameRequestArrayCount, int numberOfFrameRequests)
                    {
                        int expected = numberOfKarassInArray * frameRequestArrayCount;
                        Karass[] karassOne =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArray,
                                frameRequestArrayCount, numberOfFrameRequests);

                        Karass[] karassTwo =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArray,
                                frameRequestArrayCount, numberOfFrameRequests);


                        KanKan kanKan = new KanKan(karassOne, _frameFactory) + new KanKan(karassTwo, _frameFactory);


                        for (int i = 0; i < expected - 1; i++)
                        {
                            Console.WriteLine("Ticking KanKan index " + i + " expected" + expected);
                            Assert.True(kanKan.MoveNext());
                        }

                        Assert.False(kanKan.MoveNext());
                    }
                }

                public class WhenThereAreMisMatchingSizedKarassArrays
                {
                    private readonly MockKarassFactory _mockKarassFactory;
                    private readonly IFrameFactory _frameFactory;

                    public WhenThereAreMisMatchingSizedKarassArrays()
                    {
                        _frameFactory = new FrameFactory(new KarassDependencies());
                        _mockKarassFactory = new MockKarassFactory(new MockFramesFactory(_frameFactory));
                    }

                    [TestCase(4, 9)]
                    [TestCase(5, 2)]
                    public void ThenStateCountIsTheLargerOfTheTwo(
                        int numberOfKarassInArrayOne,
                        int numberOfKarassInArrayTwo)
                    {
                        Karass[] karassOne =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArrayOne, 1, 1);

                        Karass[] karassTwo =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArrayTwo, 1, 1);

                        KanKan kanKan = new KanKan(karassOne, _frameFactory) + new KanKan(karassTwo, _frameFactory);

                        Assert.True(kanKan.AllKarassStates.Count ==
                                    ((numberOfKarassInArrayOne > numberOfKarassInArrayTwo)
                                        ? numberOfKarassInArrayOne
                                        : numberOfKarassInArrayTwo));
                    }

                    [TestCase(2, 5, 4, 5)]
                    [TestCase(6, 5, 9, 3)]
                    public void ThenThereIsCorrectNumberOfFramesWhenMultipleFrameRequestArrays(
                        int numberOfKarassInArrayOne,
                        int numberOfKarassInArrayTwo,
                        int frameRequestArrayCountOne,
                        int frameRequestArrayCountTwo)
                    {
                        Karass[] karassOne =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArrayOne,
                                frameRequestArrayCountOne, 1);

                        Karass[] karassTwo =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArrayTwo,
                                frameRequestArrayCountTwo, 1);

                        KanKan kanKan = new KanKan(karassOne, _frameFactory) + new KanKan(karassTwo, _frameFactory);

                        Assert.True(TotalFrames(kanKan) ==
                                    ((numberOfKarassInArrayOne * frameRequestArrayCountOne) +
                                     (numberOfKarassInArrayTwo * frameRequestArrayCountTwo)));
                    }

                    [TestCase(2, 5, 4, 5, 5, 7)]
                    [TestCase(3, 5, 9, 4, 8, 9)]
                    public void ThenThereIsCorrectNumberOfFramesWhenMultipleNumberOfFrameRequests(
                        int numberOfKarassInArrayOne,
                        int numberOfKarassInArrayTwo,
                        int frameRequestArrayCountOne,
                        int frameRequestArrayCountTwo,
                        int numberOfFrameRequestsOne,
                        int numberOfFrameRequestsTwo
                    )
                    {
                        Karass[] karassOne =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArrayOne,
                                frameRequestArrayCountOne, numberOfFrameRequestsOne);

                        Karass[] karassTwo =
                            _mockKarassFactory.GetKarassArrayWithAmountOfData(numberOfKarassInArrayTwo,
                                frameRequestArrayCountTwo, numberOfFrameRequestsTwo);

                        KanKan kanKan = new KanKan(karassOne, _frameFactory) + new KanKan(karassTwo, _frameFactory);

                        Assert.True(TotalFrames(kanKan) ==
                                    (numberOfKarassInArrayOne * frameRequestArrayCountOne * numberOfFrameRequestsOne) +
                                    (numberOfKarassInArrayTwo * frameRequestArrayCountTwo * numberOfFrameRequestsTwo));
                    }
                }
            }
        }

        private static int TotalFrames(KanKan kanKan)
        {
            int totalFrames = 0;
            foreach (IKarassState state in kanKan.AllKarassStates)
            {
                foreach (FrameRequest[] frameRequests in state.Karass.FramesCollection)
                {
                    totalFrames += frameRequests.Length;
                }
            }

            return totalFrames;
        }
    }
}