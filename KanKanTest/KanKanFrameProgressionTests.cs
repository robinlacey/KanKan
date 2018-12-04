using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore;
using KanKanCore.Karass;
using KanKanTest.Mocks.UAction;
using Xunit;
using Xunit.Abstractions;

namespace KanKanTest
{
    public class KanKanFrameProgressionTests
    {
        public class GivenASingleFrameSet
        {
            public class WhenThereAreMultipleFrames
            {
                [Fact]
                public void ThenNextFramesCorrectlyProgressThroughFrames()
                {
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

                    Karass karass = new Karass(
                        CreateActionListWith(SetupSpy), 
                        CreateActionListWith(TeardownSpy),
                        new List<Func<string, bool>[]>
                        {
                            new Func<string, bool>[]
                            {
                                FrameOneSpy,
                                FrameTwoSpy,
                                FrameThreeSpy
                            }
                        });

                    KanKan kankan = new KanKan(karass, new KarassMessageDummy());
                    // First Frame should be in next frames
                    Assert.True(kankan.NextFrames.Contains(FrameOneSpy));
                    Assert.False(kankan.NextFrames.Contains(FrameTwoSpy));
                    Assert.False(kankan.NextFrames.Contains(FrameThreeSpy));
                    
                    kankan.MoveNext();
                    // Second Frame Should be in next frames

                    Assert.False(kankan.NextFrames.Contains(FrameOneSpy));
                    Assert.True(kankan.NextFrames.Contains(FrameTwoSpy));
                    Assert.False(kankan.NextFrames.Contains(FrameThreeSpy));

                    
                    CheckFirstFrame(kankan, setupRun, frameOneRun, tearDownRun, frameTwoRun, frameThreeRun);
                   
                    
                    kankan.MoveNext();
                    // Third frame should be in next frames
                    Assert.False(kankan.NextFrames.Contains(FrameOneSpy));
                    Assert.False(kankan.NextFrames.Contains(FrameTwoSpy));
                    Assert.True(kankan.NextFrames.Contains(FrameThreeSpy));

                    
                    CheckSecondFrame(setupRun, frameOneRun, tearDownRun, frameTwoRun, frameThreeRun);
                    
                    kankan.MoveNext();
                    
                    Assert.False(kankan.NextFrames.Any());
                    
                    CheckThirdFrame(setupRun, frameOneRun, tearDownRun, frameTwoRun, frameThreeRun);
                   
                }

                
                private void CheckFirstFrame(KanKan kankan, bool setupRun, bool frameOneRun, bool tearDownRun, bool frameTwoRun,
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
            
            
            public class WhenThereIsACombinedKarass{
               
                [Fact]
                public void ThenFrameSetsAreRunIndependently()
                {
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

                   
                    Karass karassOne = new Karass(CreateActionListWith(FrameSetOneSetupSpy),CreateActionListWith(FrameSetOneTeardownSpy), 
                        new List<Func<string, bool>[]>
                        {
                           new Func<string, bool>[]
                           {
                               FrameSetOneFrameOneSpy,
                               FrameSetOneFrameTwoSpy,
                               FrameSetOneFrameThreeSpy
                           }
                        } );
                    
                    Karass karassTwo = new Karass(CreateActionListWith(FrameSetTwoSetupSpy),CreateActionListWith(FrameSetTwoTeardownSpy), 
                        new List<Func<string, bool>[]>
                        {
                            new Func<string, bool>[]
                            {
                                FrameSetTwoFrameOneSpy,
                                FrameSetTwoFrameTwoSpy
                                
                            }
                        } );
                    
                    KanKan kankan = new KanKan(karassOne+karassTwo, new KarassMessageDummy());
                    
                    // First Frames correctly in NextFrames
                    Assert.True(kankan.NextFrames.Contains(FrameSetOneFrameOneSpy));
                    Assert.True(kankan.NextFrames.Contains(FrameSetTwoFrameOneSpy));
                    Assert.False(kankan.NextFrames.Contains(FrameSetOneFrameTwoSpy));
                    Assert.False(kankan.NextFrames.Contains(FrameSetTwoFrameTwoSpy));
                    Assert.False(kankan.NextFrames.Contains(FrameSetOneFrameThreeSpy));
                    
                    //Run first frame
                    kankan.MoveNext();
              
                    Assert.True(setOneSetupRun);
                    Assert.True(setTwoSetupRun);
                    Assert.False(setOneTeardownRun);
                    Assert.False(setTwoTeardownRun);

                    // Check frames
                    Assert.True(setOneFrameOneRun);
                    Assert.True(setTwoFrameOneRun);

                    Assert.False(setOneFrameTwoRun);
                    Assert.False(setOneFrameThreeRun);
                    Assert.False(setTwoFrameTwoRun);
                    
                    
                    kankan.MoveNext();
                    
                    
                    Assert.True(setOneSetupRun);
                    Assert.True(setTwoSetupRun);
                    Assert.False(setOneTeardownRun);
                    Assert.True(setTwoTeardownRun);

                    // Check frames
                    Assert.True(setOneFrameOneRun);
                    Assert.True(setTwoFrameOneRun);
                    Assert.True(setOneFrameTwoRun);
                    Assert.True(setTwoFrameTwoRun);
                    
                    Assert.False(setOneFrameThreeRun);
                    

                    kankan.MoveNext();
                    
                    Assert.True(setOneSetupRun);
                    Assert.True(setTwoSetupRun);
                    Assert.True(setOneTeardownRun);
                    Assert.True(setTwoTeardownRun);

                    // Check frames
                    Assert.True(setOneFrameOneRun);
                    Assert.True(setTwoFrameOneRun);
                    Assert.True(setOneFrameTwoRun);
                    Assert.True(setTwoFrameTwoRun);
                    Assert.True(setOneFrameThreeRun);
                }
            }
        }
        
        
      
        public class GivenMultipleFrameSets
        {
            public class WhenNextFramesIsUpdated
            {
                public class WhenThereAreTwoEqualFrameSets
                {
                    [Fact]
                    public void ThenNextFramesIsCorrectlyUpdated()
                    {
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


                        Karass karass = new Karass(new List<List<Action>>(), new List<List<Action>>(),
                            new List<Func<string, bool>[]>
                            {
                                new Func<string, bool>[]
                                {
                                    SetOneFrameOneSpy,
                                    SetOneFrameTwoSpy
                                },
                                new Func<string, bool>[]
                                {
                                    SetTwoFrameOneSpy,
                                    SetTwoFrameTwoSpy
                                }
                            });

                        KanKan kankan = new KanKan(karass, new KarassMessageDummy());
                       
                        Assert.True(kankan.NextFrames.Contains(SetOneFrameOneSpy));
                        Assert.True(kankan.NextFrames.Contains(SetTwoFrameOneSpy));
                        
                        kankan.MoveNext();
                        
                        Assert.True(kankan.NextFrames.Contains(SetOneFrameTwoSpy));
                        Assert.True(kankan.NextFrames.Contains(SetTwoFrameTwoSpy));

                        Assert.True(setOneFrameOneRun);
                        Assert.False(setOneFrameTwoRun);
                        Assert.True(setTwoFrameOneRun);
                        Assert.False(setTwoFrameTwoRun);

                        kankan.MoveNext();
                        
                        Assert.False(kankan.NextFrames.Any());
                     
                        Assert.True(setOneFrameOneRun);
                        Assert.True(setOneFrameTwoRun);
                        Assert.True(setTwoFrameOneRun);
                        Assert.True(setTwoFrameTwoRun);
                    }
                }

                public class WhenThereAreMultipleUnequalFrameSets
                {
                    [Fact]
                    public void ThenNextFramesIsUpdatedCorrectly()
                    {
                        Assert.True(1==2);
                    }
                }
                
            }
        }
        
        private static List<List<Action>> CreateActionListWith(Action a) => new List<List<Action>> { new List<Action> { a } };
        

        // Given Single Set Multple frames
        // current Frame contains frames

        // Given Multiple SEts with Mutlple Frames
    }
}