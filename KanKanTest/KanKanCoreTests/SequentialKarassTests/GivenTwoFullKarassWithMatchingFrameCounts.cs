using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTest.KanKanCoreTests.Factories;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.SequentialKarassTests
{
    public class GivenTwoFullKarassWithMatchingFrameCounts
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
        public void BothKarassWillBeRun()
        {
            bool karassOneFrameOneRun = false;
            bool karassOneFrameTwoRun = false;
            bool karassOneFrameThreeRun = false;

            FrameRequest karassOneFrameOneSpyRequest = _mockFramesFactory.GetValidFrameRequest(KarassOneFrameOneSpy);
            FrameRequest karassOneFrameTwoSpyRequest = _mockFramesFactory.GetValidFrameRequest(KarassOneFrameTwoSpy);
            FrameRequest karassOneFrameThreeSpyRequest =
                _mockFramesFactory.GetValidFrameRequest(KarassOneFrameThreeSpy);


            bool KarassOneFrameOneSpy(string message)
            {
                karassOneFrameOneRun = true;
                return true;
            }

            bool KarassOneFrameTwoSpy(string message)
            {
                karassOneFrameTwoRun = true;
                return true;
            }

            bool KarassOneFrameThreeSpy(string message)
            {
                karassOneFrameThreeRun = true;
                return true;
            }


            bool karassOneSetupRun = false;

            void KarassOneSetupSpy()
            {
                karassOneSetupRun = true;
            }

            bool karassTwoSetupRun = false;

            void KarassTwoSetupSpy()
            {
                karassTwoSetupRun = true;
            }

            bool karassOneTeardownRun = false;

            void KarassOneTeardownSpy()
            {
                karassOneTeardownRun = true;
            }

            bool karassTwoTeardownRun = false;

            void KarassTwoTeardownSpy()
            {
                karassTwoTeardownRun = true;
            }


            Karass karassOne = _karassFactory.Get(KarassOneSetupSpy, KarassOneTeardownSpy,
                new List<FrameRequest[]>
                {
                    new[]
                    {
                        karassOneFrameOneSpyRequest,
                        karassOneFrameTwoSpyRequest,
                        karassOneFrameThreeSpyRequest,
                    }
                });


            bool karassTwoFrameOneRun = false;
            bool karassTwoFrameTwoRun = false;
            bool karassTwoFrameThreeRun = false;


            FrameRequest karassTwoFrameOneSpyRequest = _mockFramesFactory.GetValidFrameRequest(KarassTwoFrameOneSpy);
            FrameRequest karassTwoFrameTwoSpyRequest = _mockFramesFactory.GetValidFrameRequest(KarassTwoFrameTwoSpy);
            FrameRequest karassTwoFrameThreeSpyRequest =
                _mockFramesFactory.GetValidFrameRequest(KarassTwoFrameThreeSpy);


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

            KanKan kankan = new KanKan(new IKarass[] {karassOne, karassTwo}, _frameFactory);

            kankan.MoveNext();
            CheckFirstFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassOneFrameTwoRun,
                karassOneFrameThreeRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                karassTwoSetupRun, karassTwoTeardownRun);

            kankan.MoveNext();
            CheckSecondFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassOneFrameTwoRun,
                karassOneFrameThreeRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                karassTwoSetupRun, karassTwoTeardownRun);

            kankan.MoveNext();
            CheckThirdFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassOneFrameTwoRun,
                karassOneFrameThreeRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                karassTwoSetupRun, karassTwoTeardownRun);


            kankan.MoveNext();
            CheckFourthFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassOneFrameTwoRun,
                karassOneFrameThreeRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                karassTwoSetupRun, karassTwoTeardownRun);

            kankan.MoveNext();
            CheckFifthFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassOneFrameTwoRun,
                karassOneFrameThreeRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                karassTwoSetupRun, karassTwoTeardownRun);

            kankan.MoveNext();
            CheckSixthFrame(karassOneSetupRun, karassOneTeardownRun, karassOneFrameOneRun, karassOneFrameTwoRun,
                karassOneFrameThreeRun, karassTwoFrameOneRun, karassTwoFrameTwoRun, karassTwoFrameThreeRun,
                karassTwoSetupRun, karassTwoTeardownRun);
        }

        private static void CheckFirstFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
            bool karassOneFrameOneRun,
            bool karassOneFrameTwoRun, bool karassOneFrameThreeRun, bool karassTwoFrameOneRun,
            bool karassTwoFrameTwoRun,
            bool karassTwoFrameThreeRun, bool karassTwoSetupRun, bool karassTwoTeardownRun)
        {
            Assert.True(karassOneSetupRun);
            Assert.False(karassOneTeardownRun);
            Assert.True(karassOneFrameOneRun);
            Assert.False(karassOneFrameTwoRun);
            Assert.False(karassOneFrameThreeRun);

            Assert.False(karassTwoFrameOneRun);
            Assert.False(karassTwoFrameTwoRun);
            Assert.False(karassTwoFrameThreeRun);

            Assert.False(karassTwoSetupRun);
            Assert.False(karassTwoTeardownRun);
        }

        private static void CheckSecondFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
            bool karassOneFrameOneRun,
            bool karassOneFrameTwoRun, bool karassOneFrameThreeRun, bool karassTwoFrameOneRun,
            bool karassTwoFrameTwoRun,
            bool karassTwoFrameThreeRun, bool karassTwoSetupRun, bool karassTwoTeardownRun)
        {
            Assert.True(karassOneSetupRun);
            Assert.False(karassOneTeardownRun);
            Assert.True(karassOneFrameOneRun);
            Assert.True(karassOneFrameTwoRun);
            Assert.False(karassOneFrameThreeRun);

            Assert.False(karassTwoFrameOneRun);
            Assert.False(karassTwoFrameTwoRun);
            Assert.False(karassTwoFrameThreeRun);

            Assert.False(karassTwoSetupRun);
            Assert.False(karassTwoTeardownRun);
        }

        private static void CheckThirdFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
            bool karassOneFrameOneRun,
            bool karassOneFrameTwoRun, bool karassOneFrameThreeRun, bool karassTwoFrameOneRun,
            bool karassTwoFrameTwoRun,
            bool karassTwoFrameThreeRun, bool karassTwoSetupRun, bool karassTwoTeardownRun)
        {
            Assert.True(karassOneSetupRun);
            Assert.True(karassOneTeardownRun);
            Assert.True(karassOneFrameOneRun);
            Assert.True(karassOneFrameTwoRun);
            Assert.True(karassOneFrameThreeRun);

            Assert.False(karassTwoFrameOneRun);
            Assert.False(karassTwoFrameTwoRun);
            Assert.False(karassTwoFrameThreeRun);

            Assert.False(karassTwoSetupRun);
            Assert.False(karassTwoTeardownRun);
        }

        private static void CheckFourthFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
            bool karassOneFrameOneRun,
            bool karassOneFrameTwoRun, bool karassOneFrameThreeRun, bool karassTwoFrameOneRun,
            bool karassTwoFrameTwoRun,
            bool karassTwoFrameThreeRun, bool karassTwoSetupRun, bool karassTwoTeardownRun)
        {
            Assert.True(karassOneSetupRun);
            Assert.True(karassOneTeardownRun);
            Assert.True(karassOneFrameOneRun);
            Assert.True(karassOneFrameTwoRun);
            Assert.True(karassOneFrameThreeRun);

            Assert.True(karassTwoSetupRun);
            Assert.False(karassTwoTeardownRun);
            Assert.True(karassTwoFrameOneRun);
            Assert.False(karassTwoFrameTwoRun);
            Assert.False(karassTwoFrameThreeRun);
        }

        private static void CheckFifthFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
            bool karassOneFrameOneRun,
            bool karassOneFrameTwoRun, bool karassOneFrameThreeRun, bool karassTwoFrameOneRun,
            bool karassTwoFrameTwoRun,
            bool karassTwoFrameThreeRun, bool karassTwoSetupRun, bool karassTwoTeardownRun)
        {
            Assert.True(karassOneSetupRun);
            Assert.True(karassOneTeardownRun);
            Assert.True(karassOneFrameOneRun);
            Assert.True(karassOneFrameTwoRun);
            Assert.True(karassOneFrameThreeRun);

            Assert.True(karassTwoSetupRun);
            Assert.False(karassTwoTeardownRun);
            Assert.True(karassTwoFrameOneRun);
            Assert.True(karassTwoFrameTwoRun);
            Assert.False(karassTwoFrameThreeRun);
        }

        private static void CheckSixthFrame(bool karassOneSetupRun, bool karassOneTeardownRun,
            bool karassOneFrameOneRun,
            bool karassOneFrameTwoRun, bool karassOneFrameThreeRun, bool karassTwoFrameOneRun,
            bool karassTwoFrameTwoRun,
            bool karassTwoFrameThreeRun, bool karassTwoSetupRun, bool karassTwoTeardownRun)
        {
            Assert.True(karassOneSetupRun);
            Assert.True(karassOneTeardownRun);
            Assert.True(karassOneFrameOneRun);
            Assert.True(karassOneFrameTwoRun);
            Assert.True(karassOneFrameThreeRun);

            Assert.True(karassTwoSetupRun);
            Assert.True(karassTwoTeardownRun);
            Assert.True(karassTwoFrameOneRun);
            Assert.True(karassTwoFrameTwoRun);
            Assert.True(karassTwoFrameThreeRun);
        }
    }
}