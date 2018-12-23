using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTest.Factories;
using NUnit.Framework;

namespace KanKanTest.SequentialKarassTests
{
    public class GivenMultipleFullKarass
    {
        private KarassFactory _karassFactory;
        private IDependencies _dependencies;
        private IFrameFactory _frameFactory;
        private MockFramesFactory _mockFramesFactory;

        [SetUp]
        public void Setup()
        {
            _dependencies = new KarassDependencies();
            _frameFactory = new FrameFactory(_dependencies);
            _karassFactory = new KarassFactory();
            _mockFramesFactory = new MockFramesFactory(_frameFactory);
        }


        [Test]
        public void ThenKarassWillRunCorrectly()
        {
            bool karassOneSetupRun = false;

            void KarassOneSetupSpy()
            {
                karassOneSetupRun = true;
            }


            bool karassOneTeardownRun = false;

            void KarassOneTeardownSpy()
            {
                karassOneTeardownRun = true;
            }


            bool karassOneFrameOneRun = false;

            bool KarassOneFrameOneSpy(string message)
            {
                karassOneFrameOneRun = true;
                return true;
            }

            FrameRequest karassOneFrameOneSpyRequest =
                _mockFramesFactory.GetValidFrameRequest(KarassOneFrameOneSpy);

            Karass karassOne = _karassFactory.Get(KarassOneSetupSpy, KarassOneTeardownSpy,
                new List<FrameRequest[]>
                {
                    new[]
                    {
                        karassOneFrameOneSpyRequest
                    }
                });


            bool karassTwoSetupRun = false;

            void KarassTwoSetupSpy()
            {
                karassTwoSetupRun = true;
            }

            bool karassTwoTeardownRun = false;

            void KarassTwoTeardownSpy()
            {
                karassTwoTeardownRun = true;
            }

            bool karassTwoFrameOneRun = false;
            bool karassTwoFrameTwoRun = false;
            bool karassTwoFrameThreeRun = false;

            bool KarassTwoFrameOneSpy(string message)
            {
                karassTwoFrameOneRun = true;
                return true;
            }

            bool KarassTwoFrameTwoSpy(string message)
            {
                karassTwoFrameTwoRun = true;
                return true;
            }

            bool KarassTwoFrameThreeSpy(string message)
            {
                karassTwoFrameThreeRun = true;
                return true;
            }

            FrameRequest karassTwoFrameOneSpyRequest =
                _mockFramesFactory.GetValidFrameRequest(KarassTwoFrameOneSpy);


            FrameRequest karassTwoFrameTwoSpyRequest =
                _mockFramesFactory.GetValidFrameRequest(KarassTwoFrameTwoSpy);


            FrameRequest karassTwoFrameThreeSpyRequest =
                _mockFramesFactory.GetValidFrameRequest(KarassTwoFrameThreeSpy);


            Karass karassTwo = _karassFactory.Get(KarassTwoSetupSpy, KarassTwoTeardownSpy,
                new List<FrameRequest[]>
                {
                    new[]
                    {
                        karassTwoFrameOneSpyRequest,
                        karassTwoFrameTwoSpyRequest,
                        karassTwoFrameThreeSpyRequest,
                    }
                });


            bool karassThreeSetupRun = false;

            void KarassThreeSetupSpy()
            {
                karassThreeSetupRun = true;
            }

            bool karassThreeTeardownRun = false;

            void KarassThreeTeardownSpy()
            {
                karassThreeTeardownRun = true;
            }

            bool karassThreeFrameOneRun = false;


            bool KarassThreeFrameOneSpy(string message)
            {
                karassThreeFrameOneRun = true;
                return true;
            }

            FrameRequest karassThreeFrameOneSpyRequest =
                _mockFramesFactory.GetValidFrameRequest(KarassThreeFrameOneSpy);

            Karass karassThree = _karassFactory.Get(KarassThreeSetupSpy, KarassThreeTeardownSpy,
                new List<FrameRequest[]>
                {
                    new[]
                    {
                        karassThreeFrameOneSpyRequest
                    }
                });


            bool karassFourSetupRun = false;

            void KarassFourSetupSpy()
            {
                karassFourSetupRun = true;
            }

            bool karassFourTeardownRun = false;

            void KarassFourTeardownSpy()
            {
                karassFourTeardownRun = true;
            }

            bool karassFourFrameOneRun = false;
            bool karassFourFrameTwoRun = false;

            bool KarassFourFrameOneSpy(string message)
            {
                karassFourFrameOneRun = true;
                return true;
            }

            bool KarassFourFrameTwoSpy(string message)
            {
                karassFourFrameTwoRun = true;
                return true;
            }

            FrameRequest karassFourFrameOneSpyRequest =
                _mockFramesFactory.GetValidFrameRequest(KarassFourFrameOneSpy);


            FrameRequest karassFourFrameTwoSpyRequest =
                _mockFramesFactory.GetValidFrameRequest(KarassFourFrameTwoSpy);


            Karass karassFour = _karassFactory.Get(KarassFourSetupSpy, KarassFourTeardownSpy,
                new List<FrameRequest[]>
                {
                    new[]
                    {
                        karassFourFrameOneSpyRequest,
                        karassFourFrameTwoSpyRequest
                    }
                });

            KanKan kankan = new KanKan(
                new IKarass[] {karassOne, karassTwo, karassThree, karassFour},
                _frameFactory);
            bool moveNext = kankan.MoveNext();
            CheckFirstFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassTwoSetupRun,
                karassTwoTeardownRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun, moveNext,
                karassThreeSetupRun, karassThreeTeardownRun, karassThreeFrameOneRun, karassFourSetupRun,
                karassFourTeardownRun, karassFourFrameOneRun, karassFourFrameTwoRun);

            moveNext = kankan.MoveNext();


            CheckSecondFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassTwoSetupRun,
                karassTwoTeardownRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                karassThreeSetupRun, karassThreeTeardownRun, karassThreeFrameOneRun, karassFourSetupRun,
                karassFourTeardownRun, karassFourFrameOneRun, karassFourFrameTwoRun, moveNext);


            moveNext = kankan.MoveNext();

            CheckThirdFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassTwoSetupRun,
                karassTwoTeardownRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                karassThreeSetupRun, karassThreeTeardownRun, karassThreeFrameOneRun, karassFourSetupRun,
                karassFourTeardownRun, karassFourFrameOneRun, karassFourFrameTwoRun, moveNext);
            moveNext = kankan.MoveNext();

            CheckFourthFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassTwoSetupRun,
                karassTwoTeardownRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                karassThreeSetupRun, karassThreeTeardownRun, karassThreeFrameOneRun, karassFourSetupRun,
                karassFourTeardownRun, karassFourFrameOneRun, karassFourFrameTwoRun, moveNext);

            moveNext = kankan.MoveNext();

            CheckFifthFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassTwoSetupRun,
                karassTwoTeardownRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                karassThreeSetupRun, karassThreeTeardownRun, karassThreeFrameOneRun, karassFourSetupRun,
                karassFourTeardownRun, karassFourFrameOneRun, karassFourFrameTwoRun, moveNext);

            moveNext = kankan.MoveNext();

            CheckSixthFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassTwoSetupRun,
                karassTwoTeardownRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                karassThreeSetupRun, karassThreeTeardownRun, karassThreeFrameOneRun, karassFourSetupRun,
                karassFourTeardownRun, karassFourFrameOneRun, karassFourFrameTwoRun, moveNext);

            moveNext = kankan.MoveNext();

            CheckSeventhFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassTwoSetupRun,
                karassTwoTeardownRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                karassThreeSetupRun, karassThreeTeardownRun, karassThreeFrameOneRun, karassFourSetupRun,
                karassFourTeardownRun, karassFourFrameOneRun, karassFourFrameTwoRun, moveNext);
        }

        private static void CheckSeventhFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
            bool karassOneFrameOneRun,
            bool karassTwoSetupRun, bool karassTwoTeardownRun, bool karassTwoFrameOneRun, bool karassTwoFrameTwoRun,
            bool karassTwoFrameThreeRun, bool karassThreeSetupRun, bool karassThreeTeardownRun,
            bool karassThreeFrameOneRun,
            bool karassFourSetupRun, bool karassFourTeardownRun, bool karassFourFrameOneRun, bool karassFourFrameTwoRun,
            bool moveNext)
        {
            Assert.True(karassOneSetupRun);
            Assert.True(karassOneTeardownRun);
            Assert.True(karassOneFrameOneRun);

            Assert.True(karassTwoSetupRun);
            Assert.True(karassTwoTeardownRun);
            Assert.True(karassTwoFrameOneRun);
            Assert.True(karassTwoFrameTwoRun);
            Assert.True(karassTwoFrameThreeRun);


            Assert.True(karassThreeSetupRun);
            Assert.True(karassThreeTeardownRun);
            Assert.True(karassThreeFrameOneRun);


            Assert.True(karassFourSetupRun);
            Assert.True(karassFourTeardownRun);
            Assert.True(karassFourFrameOneRun);
            Assert.True(karassFourFrameTwoRun);

            Assert.False(moveNext);
        }

        private static void CheckSixthFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
            bool karassOneFrameOneRun,
            bool karassTwoSetupRun, bool karassTwoTeardownRun, bool karassTwoFrameOneRun, bool karassTwoFrameTwoRun,
            bool karassTwoFrameThreeRun, bool karassThreeSetupRun, bool karassThreeTeardownRun,
            bool karassThreeFrameOneRun,
            bool karassFourSetupRun, bool karassFourTeardownRun, bool karassFourFrameOneRun, bool karassFourFrameTwoRun,
            bool moveNext)
        {
            Assert.True(karassOneSetupRun);
            Assert.True(karassOneTeardownRun);
            Assert.True(karassOneFrameOneRun);

            Assert.True(karassTwoSetupRun);
            Assert.True(karassTwoTeardownRun);
            Assert.True(karassTwoFrameOneRun);
            Assert.True(karassTwoFrameTwoRun);
            Assert.True(karassTwoFrameThreeRun);


            Assert.True(karassThreeSetupRun);
            Assert.True(karassThreeTeardownRun);
            Assert.True(karassThreeFrameOneRun);


            Assert.True(karassFourSetupRun);
            Assert.False(karassFourTeardownRun);
            Assert.True(karassFourFrameOneRun);
            Assert.False(karassFourFrameTwoRun);

            Assert.True(moveNext);
        }

        private static void CheckFifthFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
            bool karassOneFrameOneRun,
            bool karassTwoSetupRun, bool karassTwoTeardownRun, bool karassTwoFrameOneRun, bool karassTwoFrameTwoRun,
            bool karassTwoFrameThreeRun, bool karassThreeSetupRun, bool karassThreeTeardownRun,
            bool karassThreeFrameOneRun,
            bool karassFourSetupRun, bool karassFourTeardownRun, bool karassFourFrameOneRun, bool karassFourFrameTwoRun,
            bool moveNext)
        {
            Assert.True(karassOneSetupRun);
            Assert.True(karassOneTeardownRun);
            Assert.True(karassOneFrameOneRun);

            Assert.True(karassTwoSetupRun);
            Assert.True(karassTwoTeardownRun);
            Assert.True(karassTwoFrameOneRun);
            Assert.True(karassTwoFrameTwoRun);
            Assert.True(karassTwoFrameThreeRun);


            Assert.True(karassThreeSetupRun);
            Assert.True(karassThreeTeardownRun);
            Assert.True(karassThreeFrameOneRun);


            Assert.False(karassFourSetupRun);
            Assert.False(karassFourTeardownRun);
            Assert.False(karassFourFrameOneRun);
            Assert.False(karassFourFrameTwoRun);

            Assert.True(moveNext);
        }

        private static void CheckFourthFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
            bool karassOneFrameOneRun,
            bool karassTwoSetupRun, bool karassTwoTeardownRun, bool karassTwoFrameOneRun, bool karassTwoFrameTwoRun,
            bool karassTwoFrameThreeRun, bool karassThreeSetupRun, bool karassThreeTeardownRun,
            bool karassThreeFrameOneRun,
            bool karassFourSetupRun, bool karassFourTeardownRun, bool karassFourFrameOneRun, bool karassFourFrameTwoRun,
            bool moveNext)
        {
            Assert.True(karassOneSetupRun);
            Assert.True(karassOneTeardownRun);
            Assert.True(karassOneFrameOneRun);

            Assert.True(karassTwoSetupRun);
            Assert.True(karassTwoTeardownRun);
            Assert.True(karassTwoFrameOneRun);
            Assert.True(karassTwoFrameTwoRun);
            Assert.True(karassTwoFrameThreeRun);


            Assert.False(karassThreeSetupRun);
            Assert.False(karassThreeTeardownRun);
            Assert.False(karassThreeFrameOneRun);


            Assert.False(karassFourSetupRun);
            Assert.False(karassFourTeardownRun);
            Assert.False(karassFourFrameOneRun);
            Assert.False(karassFourFrameTwoRun);

            Assert.True(moveNext);
        }

        private static void CheckThirdFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
            bool karassOneFrameOneRun,
            bool karassTwoSetupRun, bool karassTwoTeardownRun, bool karassTwoFrameOneRun, bool karassTwoFrameTwoRun,
            bool karassTwoFrameThreeRun, bool karassThreeSetupRun, bool karassThreeTeardownRun,
            bool karassThreeFrameOneRun,
            bool karassFourSetupRun, bool karassFourTeardownRun, bool karassFourFrameOneRun, bool karassFourFrameTwoRun,
            bool moveNext)
        {
            Assert.True(karassOneSetupRun);
            Assert.True(karassOneTeardownRun);
            Assert.True(karassOneFrameOneRun);

            Assert.True(karassTwoSetupRun);
            Assert.False(karassTwoTeardownRun);
            Assert.True(karassTwoFrameOneRun);
            Assert.True(karassTwoFrameTwoRun);
            Assert.False(karassTwoFrameThreeRun);


            Assert.False(karassThreeSetupRun);
            Assert.False(karassThreeTeardownRun);
            Assert.False(karassThreeFrameOneRun);


            Assert.False(karassFourSetupRun);
            Assert.False(karassFourTeardownRun);
            Assert.False(karassFourFrameOneRun);
            Assert.False(karassFourFrameTwoRun);

            Assert.True(moveNext);
        }

        private static void CheckSecondFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
            bool karassOneFrameOneRun,
            bool karassTwoSetupRun, bool karassTwoTeardownRun, bool karassTwoFrameOneRun, bool karassTwoFrameTwoRun,
            bool karassTwoFrameThreeRun, bool karassThreeSetupRun, bool karassThreeTeardownRun,
            bool karassThreeFrameOneRun,
            bool karassFourSetupRun, bool karassFourTeardownRun, bool karassFourFrameOneRun, bool karassFourFrameTwoRun,
            bool moveNext)
        {
            Assert.True(karassOneSetupRun);
            Assert.True(karassOneTeardownRun);
            Assert.True(karassOneFrameOneRun);

            Assert.True(karassTwoSetupRun);
            Assert.False(karassTwoTeardownRun);
            Assert.True(karassTwoFrameOneRun);
            Assert.False(karassTwoFrameTwoRun);
            Assert.False(karassTwoFrameThreeRun);


            Assert.False(karassThreeSetupRun);
            Assert.False(karassThreeTeardownRun);
            Assert.False(karassThreeFrameOneRun);


            Assert.False(karassFourSetupRun);
            Assert.False(karassFourTeardownRun);
            Assert.False(karassFourFrameOneRun);
            Assert.False(karassFourFrameTwoRun);

            Assert.True(moveNext);
        }

        private static void CheckFirstFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
            bool karassOneFrameOneRun,
            bool karassTwoSetupRun, bool karassTwoTeardownRun, bool karassTwoFrameOneRun, bool karassTwoFrameTwoRun,
            bool karassTwoFrameThreeRun, bool moveNext, bool karassThreeSetupRun, bool karassThreeTeardownRun,
            bool karassThreeFrameOneRun, bool karassFourSetupRun, bool karassFourTeardownRun,
            bool karassFourFrameOneRun,
            bool karassFourFrameTwoRun)
        {
            Assert.True(karassOneSetupRun);
            Assert.True(karassOneTeardownRun);
            Assert.True(karassOneFrameOneRun);

            Assert.False(karassTwoSetupRun);
            Assert.False(karassTwoTeardownRun);
            Assert.False(karassTwoFrameOneRun);
            Assert.False(karassTwoFrameTwoRun);
            Assert.False(karassTwoFrameThreeRun);

            Assert.True(moveNext);

            Assert.False(karassThreeSetupRun);
            Assert.False(karassThreeTeardownRun);
            Assert.False(karassThreeFrameOneRun);


            Assert.False(karassFourSetupRun);
            Assert.False(karassFourTeardownRun);
            Assert.False(karassFourFrameOneRun);
            Assert.False(karassFourFrameTwoRun);
        }
    }
}