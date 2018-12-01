using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Karass;
using KanKanCore.Karass.Message;
using Xunit;

namespace KanKanTest
{
    public class KanKanFirstFrameTests
    {
        public class GivenOneFrameSet
        {
            public class WhenThereIsOneFrame
            {
                [Fact]
                public void ThenTheFrameIsInCurrentFrames()
                {
                    bool Frame(string message) => true;
                    Karass karass = new Karass(
                        new Action[0],
                        new Action[0],
                        new List<Func<string, bool>[]>
                        {
                            new Func<string, bool>[]
                            {
                                Frame
                            }
                        });
                    KanKan kanKan = new KanKan(karass, new KarassMessage());
                    Assert.True(kanKan.NextFrames.Contains(Frame));
                }
            }

            public class WhenThereAreMultipleFrames
            {
                [Fact]
                public void ThenOnlyContainsFirstFramew()
                {
                    bool FrameOne(string message) => false;
                    bool FrameTwo(string message) => false;
                    bool FrameThree(string message) => false;
                    bool FrameFour(string message) => false;
                    Karass karass = new Karass(
                        new Action[0],
                        new Action[0],
                        new List<Func<string, bool>[]>
                        {
                            new Func<string, bool>[]
                            {
                                FrameOne,
                                FrameTwo,
                                FrameThree,
                                FrameFour
                            }
                        });
                    KanKan kanKan = new KanKan(karass, new KarassMessage());
                    Assert.True(kanKan.NextFrames.Contains(FrameOne));
                    Assert.False(kanKan.NextFrames.Contains(FrameTwo));
                    Assert.False(kanKan.NextFrames.Contains(FrameThree));
                    Assert.False(kanKan.NextFrames.Contains(FrameFour));
                }
            }
        }

        public class GivenMultipleFrameSets
        {
            public class WhenThereIsOneFrame
            {
                [Fact]
                public void ThenBothFramesAreInCurrentFrames()
                {
                    bool SetOneFrame(string message) => true;
                    bool SetTwoFrame(string message) => true;
                    Karass karass = new Karass(
                        new Action[0],
                        new Action[0],
                        new List<Func<string, bool>[]>
                        {
                            new Func<string, bool>[]
                            {
                                SetOneFrame
                            },
                            new Func<string, bool>[]
                            {
                                SetTwoFrame
                            }
                        });
                    KanKan kanKan = new KanKan(karass, new KarassMessage());
                    Assert.True(kanKan.NextFrames.Contains(SetOneFrame));
                    Assert.True(kanKan.NextFrames.Contains(SetTwoFrame));
                }
            }

            public class WhenThereAreMultipleFrames
            {
                [Fact]
                public void ThenOnlyTheFirstFramesAreInCurrentFrames()
                {
                    bool SetOneFrameOne(string message) => true;
                    bool SetOneFrameTwo(string message) => true;
                    bool SetOneFrameThree(string message) => true;


                    bool SetTwoFrameOne(string message) => true;
                    bool SetTwoFrameTwo(string message) => true;
                    bool SetTwoFrameThree(string message) => true;
                    Karass karass = new Karass(
                        new Action[0],
                        new Action[0],
                        new List<Func<string, bool>[]>
                        {
                            new Func<string, bool>[]
                            {
                                SetOneFrameOne,
                                SetOneFrameTwo,
                                SetOneFrameThree
                            },
                            new Func<string, bool>[]
                            {
                                SetTwoFrameOne,
                                SetTwoFrameTwo,
                                SetTwoFrameThree
                            }
                        });
                    KanKan kanKan = new KanKan(karass, new KarassMessage());
                    Assert.True(kanKan.NextFrames.Contains(SetOneFrameOne));
                    Assert.True(kanKan.NextFrames.Contains(SetTwoFrameOne));

                    Assert.False(kanKan.NextFrames.Contains(SetOneFrameTwo));
                    Assert.False(kanKan.NextFrames.Contains(SetTwoFrameTwo));

                    Assert.False(kanKan.NextFrames.Contains(SetOneFrameTwo));
                    Assert.False(kanKan.NextFrames.Contains(SetTwoFrameTwo));
                }
            }
        }
    }
    
}