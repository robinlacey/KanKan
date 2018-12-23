using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using NUnit.Framework;

namespace KanKanTest.ProgressionTests
{
    public class GivenMultipleFrameSetsWhenNextFramesIsUpdated
    {
        
        public class WhenThereAreTwoEqualFrameSets
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
            public void ThenNextFramesIsCorrectlyUpdated()
            {
               
                
                FrameRequest setOneFrameOneSpyRequest = _mockFramesFactory.GetValidFrameRequest(SetOneFrameOneSpy);
                FrameRequest setOneFrameTwoSpyRequest = _mockFramesFactory.GetValidFrameRequest(SetOneFrameTwoSpy);
                FrameRequest setTwoFrameOneSpyRequest = _mockFramesFactory.GetValidFrameRequest(SetTwoFrameOneSpy);
                FrameRequest setTwoFrameTwoSpyRequest = _mockFramesFactory.GetValidFrameRequest(SetTwoFrameTwoSpy);

                
                bool setOneFrameOneRun = false;

                bool SetOneFrameOneSpy(string s)
                {
                    setOneFrameOneRun = true;
                    return true;
                }

                bool setOneFrameTwoRun = false;

                bool SetOneFrameTwoSpy(string s)
                {
                    setOneFrameTwoRun = true;
                    return true;
                }

                bool setTwoFrameOneRun = false;

                bool SetTwoFrameOneSpy(string s)
                {
                    setTwoFrameOneRun = true;
                    return true;
                }

                bool setTwoFrameTwoRun = false;

                bool SetTwoFrameTwoSpy(string s)
                {
                    setTwoFrameTwoRun = true;
                    return true;
                }


                Karass karass = _karassFactory.Get(new List<List<Action>>(), new List<List<Action>>(),
                    new List<FrameRequest[]>
                    {
                        new[]
                        {
                            setOneFrameOneSpyRequest,
                            setOneFrameTwoSpyRequest
                        },
                        new[]
                        {
                            setTwoFrameOneSpyRequest,
                            setTwoFrameTwoSpyRequest
                        }
                    });

                KanKan kankan = new KanKan(karass, _frameFactory);
                Assert.True(kankan.CurrentState.NextFrames.Count == 2);
                Assert.True(kankan.CurrentState.NextFrames.Contains(setOneFrameOneSpyRequest));
                Assert.True(kankan.CurrentState.NextFrames.Contains(setTwoFrameOneSpyRequest));

                kankan.MoveNext();
                Assert.True(kankan.CurrentState.NextFrames.Count == 2);
                Assert.True(kankan.CurrentState.NextFrames.Contains(setOneFrameTwoSpyRequest));
                Assert.True(kankan.CurrentState.NextFrames.Contains(setTwoFrameTwoSpyRequest));

                Assert.True(setOneFrameOneRun);
                Assert.False(setOneFrameTwoRun);
                Assert.True(setTwoFrameOneRun);
                Assert.False(setTwoFrameTwoRun);

                kankan.MoveNext();

                Assert.False(kankan.CurrentState.NextFrames.Any());

                Assert.True(setOneFrameOneRun);
                Assert.True(setOneFrameTwoRun);
                Assert.True(setTwoFrameOneRun);
                Assert.True(setTwoFrameTwoRun);
            }
        }

        public class WhenThereAreMultipleUnequalFrameSets
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
            public void ThenNextFramesIsUpdatedCorrectly()
            {
                
                FrameRequest setOneFrameOneSpyRequest = _mockFramesFactory.GetValidFrameRequest(SetOneFrameOneSpy);
                FrameRequest setOneFrameTwoSpyRequest = _mockFramesFactory.GetValidFrameRequest(SetOneFrameTwoSpy);
                FrameRequest setTwoFrameOneSpyRequest = _mockFramesFactory.GetValidFrameRequest(SetTwoFrameOneSpy);
                FrameRequest setTwoFrameTwoSpyRequest = _mockFramesFactory.GetValidFrameRequest(SetTwoFrameTwoSpy);
                FrameRequest setTwoFrameThreeSpyRequest = _mockFramesFactory.GetValidFrameRequest(SetTwoFrameThreeSpy);
                FrameRequest setThreeFrameOneSpyRequest = _mockFramesFactory.GetValidFrameRequest(SetThreeFrameOneSpy);
                FrameRequest setThreeFrameTwoSpyRequest = _mockFramesFactory.GetValidFrameRequest(SetThreeFrameTwoSpy);
                FrameRequest setThreeFrameThreeSpyRequest = _mockFramesFactory.GetValidFrameRequest(SetThreeFrameThreeSpy);
                FrameRequest setThreeFrameFourSpyRequest = _mockFramesFactory.GetValidFrameRequest(SetThreeFrameFourSpy);
                
                bool setOneFrameOneRun = false;

                bool SetOneFrameOneSpy(string s)
                {
                    setOneFrameOneRun = true;
                    return true;
                }

                bool setOneFrameTwoRun = false;

                bool SetOneFrameTwoSpy(string s)
                {
                    setOneFrameTwoRun = true;
                    return true;
                }

                bool setTwoFrameOneRun = false;

                bool SetTwoFrameOneSpy(string s)
                {
                    setTwoFrameOneRun = true;
                    return true;
                }

                bool setTwoFrameTwoRun = false;

                bool SetTwoFrameTwoSpy(string s)
                {
                    setTwoFrameTwoRun = true;
                    return true;
                }

                bool setTwoFrameThreeRun = false;

                bool SetTwoFrameThreeSpy(string s)
                {
                    setTwoFrameThreeRun = true;
                    return true;
                }


                bool setThreeFrameOneRun = false;

                bool SetThreeFrameOneSpy(string s)
                {
                    setThreeFrameOneRun = true;
                    return true;
                }

                bool setThreeFrameTwoRun = false;

                bool SetThreeFrameTwoSpy(string s)
                {
                    setThreeFrameTwoRun = true;
                    return true;
                }

                bool setThreeFrameThreeRun = false;

                bool SetThreeFrameThreeSpy(string s)
                {
                    setThreeFrameThreeRun = true;
                    return true;
                }

                bool setThreeFrameFourRun = false;

                bool SetThreeFrameFourSpy(string s)
                {
                    setThreeFrameFourRun = true;
                    return true;
                }


                Karass karass = _karassFactory.Get(new List<List<Action>>(), new List<List<Action>>(),
                    new List<FrameRequest[]>
                    {
                        new[]
                        {
                            setOneFrameOneSpyRequest,
                            setOneFrameTwoSpyRequest
                        },
                        new[]
                        {
                            setTwoFrameOneSpyRequest,
                            setTwoFrameTwoSpyRequest,
                            setTwoFrameThreeSpyRequest
                        },
                        new[]
                        {
                            setThreeFrameOneSpyRequest,
                            setThreeFrameTwoSpyRequest,
                            setThreeFrameThreeSpyRequest,
                            setThreeFrameFourSpyRequest
                        }
                    });

                KanKan kankan = new KanKan(karass, _frameFactory);
                Assert.True(kankan.CurrentState.NextFrames.Count == 3);
                Assert.True(kankan.CurrentState.NextFrames.Contains(setOneFrameOneSpyRequest));
                Assert.True(kankan.CurrentState.NextFrames.Contains(setTwoFrameOneSpyRequest));
                Assert.True(kankan.CurrentState.NextFrames.Contains(setThreeFrameOneSpyRequest));

                kankan.MoveNext();
                Assert.True(kankan.CurrentState.NextFrames.Count == 3);
                Assert.True(kankan.CurrentState.NextFrames.Contains(setOneFrameTwoSpyRequest));
                Assert.True(kankan.CurrentState.NextFrames.Contains(setTwoFrameTwoSpyRequest));
                Assert.True(kankan.CurrentState.NextFrames.Contains(setThreeFrameTwoSpyRequest));

                Assert.True(setOneFrameOneRun);
                Assert.False(setOneFrameTwoRun);

                Assert.True(setTwoFrameOneRun);
                Assert.False(setTwoFrameTwoRun);
                Assert.False(setTwoFrameThreeRun);

                Assert.True(setThreeFrameOneRun);
                Assert.False(setThreeFrameTwoRun);
                Assert.False(setThreeFrameThreeRun);
                Assert.False(setThreeFrameFourRun);

                kankan.MoveNext();

                Assert.True(kankan.CurrentState.NextFrames.Count == 2);
                Assert.True(kankan.CurrentState.NextFrames.Contains(setTwoFrameThreeSpyRequest));
                Assert.True(kankan.CurrentState.NextFrames.Contains(setThreeFrameThreeSpyRequest));

                Assert.True(setOneFrameOneRun);
                Assert.True(setOneFrameTwoRun);

                Assert.True(setTwoFrameOneRun);
                Assert.True(setTwoFrameTwoRun);
                Assert.False(setTwoFrameThreeRun);

                Assert.True(setThreeFrameOneRun);
                Assert.True(setThreeFrameTwoRun);
                Assert.False(setThreeFrameThreeRun);
                Assert.False(setThreeFrameFourRun);


                kankan.MoveNext();

                Assert.True(kankan.CurrentState.NextFrames.Count == 1);
                Assert.True(kankan.CurrentState.NextFrames.Contains(setThreeFrameFourSpyRequest));

                Assert.True(setOneFrameOneRun);
                Assert.True(setOneFrameTwoRun);

                Assert.True(setTwoFrameOneRun);
                Assert.True(setTwoFrameTwoRun);
                Assert.True(setTwoFrameThreeRun);

                Assert.True(setThreeFrameOneRun);
                Assert.True(setThreeFrameTwoRun);
                Assert.True(setThreeFrameThreeRun);
                Assert.False(setThreeFrameFourRun);

                kankan.MoveNext();
                Assert.False(kankan.CurrentState.NextFrames.Any());

                Assert.True(setOneFrameOneRun);
                Assert.True(setOneFrameTwoRun);

                Assert.True(setTwoFrameOneRun);
                Assert.True(setTwoFrameTwoRun);
                Assert.True(setTwoFrameThreeRun);

                Assert.True(setThreeFrameOneRun);
                Assert.True(setThreeFrameTwoRun);
                Assert.True(setThreeFrameThreeRun);
                Assert.True(setThreeFrameFourRun);
            }
        }
    }
}