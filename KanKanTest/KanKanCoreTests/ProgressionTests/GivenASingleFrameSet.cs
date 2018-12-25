using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTest.KanKanCoreTests.Factories;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.ProgressionTests
{
    
    public class GivenEmptyFramesSet
    {
        private readonly IDependencies _dependencies = new KarassDependencies();
        private readonly KarassFactory _karassFactory = new KarassFactory();
        private IFrameFactory _frameFactory;
        private IKanKan _kankan;
        [SetUp]
        public void Setup()
        {
            _frameFactory = new FrameFactory(_dependencies);
        }


        [Test]
        public void ThenThrowNoValidRequestTypeException()
        {
            IKarass karass = _karassFactory.Get(new List<Action>(), new List<Action>(),
                new FrameRequest[] {});
            _kankan = new KanKan(karass,_frameFactory);
            
            
            karass = _karassFactory.Get(new List<Action>(), new List<Action>(),
               new List<FrameRequest[]>()
               {
                   new FrameRequest[]
                   {
                       
                   }
               });
            _kankan = new KanKan(karass,_frameFactory);
            Assert.False(_kankan.MoveNext());
        }
    }
    
    public class GivenASingleFrameSet : KanKanFrameProgressionTests
    {
        
     
        
       
        public class WhenThereAreMultipleFrames
        {
            private KarassFactory KarassFactory { get; set; }
            private MockFramesFactory _mockFramesFactory;
            private IDependencies _dependencies;
            private IFrameFactory _frameFactory;

            [SetUp]
            public void Setup()
            {
                _dependencies = new KarassDependencies();
                _frameFactory = new FrameFactory(_dependencies); 
                KarassFactory  = new KarassFactory();
                _mockFramesFactory = new MockFramesFactory(_frameFactory);
                
            }
            [Test]
            public void ThenNextFramesCorrectlyProgressThroughFrames()
            {
                FrameRequest frameRequestOne = _mockFramesFactory.GetValidFrameRequest(FrameOneSpy);
                FrameRequest frameRequestTwo = _mockFramesFactory.GetValidFrameRequest(FrameTwoSpy);
                FrameRequest frameRequestThree = _mockFramesFactory.GetValidFrameRequest(FrameThreeSpy);
                
                bool frameOneRun = false;

                bool FrameOneSpy(string s)
                {
                    frameOneRun = true;
                    return true;
                }

                bool frameTwoRun = false;

                bool FrameTwoSpy(string s)
                {
                    frameTwoRun = true;
                    return true;
                }

                bool frameThreeRun = false;

                bool FrameThreeSpy(string s)
                {
                    frameThreeRun = true;
                    return true;
                }

                bool setupRun = false;

                void SetupSpy()
                {
                    setupRun = true;
                }

                bool tearDownRun = false;

                void TeardownSpy()
                {
                    tearDownRun = true;
                }

                Karass karass = KarassFactory.Get(
                    CreateActionListWith(SetupSpy),
                    CreateActionListWith(TeardownSpy),
                    new List<FrameRequest[]>
                    {
                        new[]
                        {
                            frameRequestOne,
                            frameRequestTwo,
                            frameRequestThree
                        }
                    });

                KanKan kankan = new KanKan(karass,_frameFactory);

                Assert.True(kankan.CurrentState.NextFrames.Contains(frameRequestOne));
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameRequestTwo));
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameRequestThree));

                bool kankanReturnValue = kankan.MoveNext();
                Assert.True(kankanReturnValue);
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameRequestOne));
                Assert.True(kankan.CurrentState.NextFrames.Contains(frameRequestTwo));
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameRequestThree));


                CheckFirstFrame(setupRun, frameOneRun, tearDownRun, frameTwoRun, frameThreeRun);


                kankanReturnValue = kankan.MoveNext();
                Assert.True(kankanReturnValue);
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameRequestOne));
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameRequestTwo));
                Assert.True(kankan.CurrentState.NextFrames.Contains(frameRequestThree));


                CheckSecondFrame(setupRun, frameOneRun, tearDownRun, frameTwoRun, frameThreeRun);

                kankanReturnValue = kankan.MoveNext();
                Assert.False(kankanReturnValue);
                Assert.False(kankan.CurrentState.NextFrames.Any());

                CheckThirdFrame(setupRun, frameOneRun, tearDownRun, frameTwoRun, frameThreeRun);
            }


            private void CheckFirstFrame(bool setupRun, bool frameOneRun, bool tearDownRun, bool frameTwoRun,
                bool frameThreeRun)
            {
                Assert.True(setupRun);
                Assert.False(tearDownRun);
                Assert.True(frameOneRun);
                Assert.False(frameTwoRun);
                Assert.False(frameThreeRun);
            }

            private void CheckSecondFrame(bool setupRun, bool frameOneRun, bool tearDownRun, bool frameTwoRun,
                bool frameThreeRun)
            {
                Assert.True(setupRun);
                Assert.False(tearDownRun);
                Assert.True(frameOneRun);
                Assert.True(frameTwoRun);
                Assert.False(frameThreeRun);
            }

            private void CheckThirdFrame(bool setupRun, bool frameOneRun, bool tearDownRun, bool frameTwoRun,
                bool frameThreeRun)
            {
                Assert.True(setupRun);
                Assert.True(frameOneRun);
                Assert.True(tearDownRun);
                Assert.True(frameTwoRun);
                Assert.True(frameThreeRun);
            }
        }


        public class WhenThereIsACombinedKarass
        {
            private KarassFactory KarassFactory { get; set; }
            private MockFramesFactory _mockFramesFactory;
            private IDependencies _dependencies;
            private IFrameFactory _frameFactory;

            [SetUp]
            public void Setup()
            {
                _dependencies = new KarassDependencies();
                _frameFactory = new FrameFactory(_dependencies); 
                KarassFactory  = new KarassFactory();
                _mockFramesFactory = new MockFramesFactory(_frameFactory);
                
            }
            
            [Test]
            public void ThenFrameSetsAreRunIndependently()
            {
                FrameRequest frameSetOneRequestOne = _mockFramesFactory.GetValidFrameRequest(FrameSetOneFrameOneSpy);
                FrameRequest frameSetOneRequestTwo = _mockFramesFactory.GetValidFrameRequest(FrameSetOneFrameTwoSpy);
                FrameRequest frameSetOneRequestThree = _mockFramesFactory.GetValidFrameRequest(FrameSetOneFrameThreeSpy);
                
                
                FrameRequest frameSetTwpRequestOne = _mockFramesFactory.GetValidFrameRequest(FrameSetTwoFrameOneSpy);
                FrameRequest frameSetTwoRequestTwo = _mockFramesFactory.GetValidFrameRequest(FrameSetTwoFrameTwoSpy);
                
                bool setOneSetupRun = false;

                void FrameSetOneSetupSpy()
                {
                    setOneSetupRun = true;
                }

                bool setOneTeardownRun = false;

                void FrameSetOneTeardownSpy()
                {
                    setOneTeardownRun = true;
                }

                bool setOneFrameOneRun = false;

                bool FrameSetOneFrameOneSpy(string message)
                {
                    setOneFrameOneRun = true;
                    return true;
                }

                bool setOneFrameTwoRun = false;

                bool FrameSetOneFrameTwoSpy(string message)
                {
                    setOneFrameTwoRun = true;
                    return true;
                }

                bool setOneFrameThreeRun = false;

                bool FrameSetOneFrameThreeSpy(string message)
                {
                    setOneFrameThreeRun = true;
                    return true;
                }


                bool setTwoSetupRun = false;

                void FrameSetTwoSetupSpy()
                {
                    setTwoSetupRun = true;
                }

                bool setTwoTeardownRun = false;

                void FrameSetTwoTeardownSpy()
                {
                    setTwoTeardownRun = true;
                }

                bool setTwoFrameOneRun = false;

                bool FrameSetTwoFrameOneSpy(string message)
                {
                    setTwoFrameOneRun = true;
                    return true;
                }

                bool setTwoFrameTwoRun = false;

                bool FrameSetTwoFrameTwoSpy(string message)
                {
                    setTwoFrameTwoRun = true;
                    return true;
                }


                Karass karassOne = KarassFactory.Get(CreateActionListWith(FrameSetOneSetupSpy),
                    CreateActionListWith(FrameSetOneTeardownSpy),
                    new List<FrameRequest[]>
                    {
                        new[]
                        {
                            frameSetOneRequestOne,
                            frameSetOneRequestTwo,
                            frameSetOneRequestThree
                        }
                    });

                Karass karassTwo = KarassFactory.Get(CreateActionListWith(FrameSetTwoSetupSpy),
                    CreateActionListWith(FrameSetTwoTeardownSpy),
                    new List<FrameRequest[]>
                    {
                        new[]
                        {
                            frameSetTwpRequestOne,
                            frameSetTwoRequestTwo
                        }
                    });

                KanKan kankan = new KanKan(karassOne + karassTwo, _frameFactory);


                Assert.True(kankan.CurrentState.NextFrames.Contains(frameSetOneRequestOne));
                Assert.True(kankan.CurrentState.NextFrames.Contains(frameSetTwpRequestOne));
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameSetOneRequestTwo));
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameSetTwoRequestTwo));
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameSetOneRequestThree));


                kankan.MoveNext();

                Assert.False(kankan.CurrentState.NextFrames.Contains(frameSetOneRequestOne));
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameSetTwpRequestOne));
                Assert.True(kankan.CurrentState.NextFrames.Contains(frameSetOneRequestTwo));
                Assert.True(kankan.CurrentState.NextFrames.Contains(frameSetTwoRequestTwo));
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameSetOneRequestThree));

                CheckFirstFrame(setOneSetupRun, setTwoSetupRun, setOneTeardownRun, setTwoTeardownRun, setOneFrameOneRun,
                    setTwoFrameOneRun, setOneFrameTwoRun, setOneFrameThreeRun, setTwoFrameTwoRun);


                kankan.MoveNext();
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameSetOneRequestOne));
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameSetTwpRequestOne));
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameSetOneRequestTwo));
                Assert.False(kankan.CurrentState.NextFrames.Contains(frameSetTwoRequestTwo));
                Assert.True(kankan.CurrentState.NextFrames.Contains(frameSetOneRequestThree));

                CheckSecondFrame(setOneSetupRun, setTwoSetupRun, setOneTeardownRun, setTwoTeardownRun,
                    setOneFrameOneRun, setTwoFrameOneRun, setOneFrameTwoRun, setTwoFrameTwoRun, setOneFrameThreeRun);


                kankan.MoveNext();
                Assert.False(kankan.CurrentState.NextFrames.Any());
                CheckThirdFrame(setOneSetupRun, setTwoSetupRun, setOneTeardownRun, setTwoTeardownRun, setOneFrameOneRun,
                    setTwoFrameOneRun, setOneFrameTwoRun, setTwoFrameTwoRun, setOneFrameThreeRun);
            }

            private static void CheckThirdFrame(bool setOneSetupRun, bool setTwoSetupRun, bool setOneTeardownRun,
                bool setTwoTeardownRun, bool setOneFrameOneRun, bool setTwoFrameOneRun, bool setOneFrameTwoRun,
                bool setTwoFrameTwoRun, bool setOneFrameThreeRun)
            {
                Assert.True(setOneSetupRun);
                Assert.True(setTwoSetupRun);
                Assert.True(setOneTeardownRun);
                Assert.True(setTwoTeardownRun);


                Assert.True(setOneFrameOneRun);
                Assert.True(setTwoFrameOneRun);
                Assert.True(setOneFrameTwoRun);
                Assert.True(setTwoFrameTwoRun);
                Assert.True(setOneFrameThreeRun);
            }

            private static void CheckSecondFrame(bool setOneSetupRun, bool setTwoSetupRun, bool setOneTeardownRun,
                bool setTwoTeardownRun, bool setOneFrameOneRun, bool setTwoFrameOneRun, bool setOneFrameTwoRun,
                bool setTwoFrameTwoRun, bool setOneFrameThreeRun)
            {
                Assert.True(setOneSetupRun);
                Assert.True(setTwoSetupRun);
                Assert.False(setOneTeardownRun);
                Assert.True(setTwoTeardownRun);

                Assert.True(setOneFrameOneRun);
                Assert.True(setTwoFrameOneRun);
                Assert.True(setOneFrameTwoRun);
                Assert.True(setTwoFrameTwoRun);

                Assert.False(setOneFrameThreeRun);
            }

            private static void CheckFirstFrame(bool setOneSetupRun, bool setTwoSetupRun, bool setOneTeardownRun,
                bool setTwoTeardownRun, bool setOneFrameOneRun, bool setTwoFrameOneRun, bool setOneFrameTwoRun,
                bool setOneFrameThreeRun, bool setTwoFrameTwoRun)
            {
                Assert.True(setOneSetupRun);
                Assert.True(setTwoSetupRun);
                Assert.False(setOneTeardownRun);
                Assert.False(setTwoTeardownRun);


                Assert.True(setOneFrameOneRun);
                Assert.True(setTwoFrameOneRun);

                Assert.False(setOneFrameTwoRun);
                Assert.False(setOneFrameThreeRun);
                Assert.False(setTwoFrameTwoRun);
            }
        }
    }
}