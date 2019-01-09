using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Factories;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Message;
using KanKanTest.KanKanCoreTests.Factories;
using KanKanTest.KanKanCoreTests.Mocks.KanKan.Fake;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame;
using KanKanTest.KanKanCoreTests.Mocks.KarassMocks;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanTests
{
    public class KanKanTests
    {
        [Test]
        public void KanKanHasUniqueID()
        {
            Assert.False(string.IsNullOrEmpty(new KanKan(new KarassDummy(), null).ID));
            Assert.False(string.IsNullOrWhiteSpace(new KanKan(new KarassDummy(), null).ID));
            Assert.False(new KanKan(new KarassDummy(), null).ID == new KanKan(new KarassDummy(), null).ID);
        }

        [Test]
        public void KanKanAssignsFrameFactory()
        {
            IFrameFactory frameFactory = new FrameFactoryDummy();
            IKanKan kankan = new KanKan(new KarassDummy(), frameFactory);
            Assert.AreSame(kankan.Current.FrameFactory, frameFactory);
        }

        [Test]
        public void KanKanSetKarassMessageAssignsKarassMessage()
        {
            IKarassMessage karassMessage = new KarassMessageDummy();
            IKanKan kankan = new KanKan(new KarassDummy(), new FrameFactoryDummy());
            kankan.SetKarassMessage(karassMessage);
            Assert.AreSame(kankan.Current.KarassMessage, karassMessage);
        }

        [Test]
        public void KanKanCreatesKarassMessageIfNoneGiven()
        {
            IKanKan kankan = new KanKan(new KarassDummy(), new FrameFactoryDummy());
            Assert.NotNull(kankan.Current.KarassMessage);
        }

        public class GetKanKanCurrentStateTests
        {
            public class NextAndLastFrames
            {
                [Test]
                public void NextFramesShouldReturnKarassStateNextFrames()
                {
                    IDependencies dependencies = new KarassDependencies();
                    IFrameFactory framesFactory = new FrameFactory(dependencies);
                    MockFramesFactory _mockFramesFactory = new MockFramesFactory(framesFactory);

                    bool FrameSpy(string message) => false;
                    FrameRequest frameRequest = _mockFramesFactory.GetValidFrameRequest(FrameSpy);
                    KarassFactory karassFactory = new KarassFactory();
                    IKarass karass = karassFactory.Get(new List<Action>(), new List<Action>(), new[]
                    {
                        frameRequest
                    });
                    IKanKan kankan = new KanKan(karass, framesFactory);

                    Assert.True(kankan.Current.NextFrames.Contains(frameRequest));
                }

                [Test]
                public void LastFramesShouldReturnPreviousFrames()
                {
                    IDependencies dependencies = new KarassDependencies();
                    IFrameFactory framesFactory = new FrameFactory(dependencies);
                    MockFramesFactory mockFramesFactory = new MockFramesFactory(framesFactory);

                    bool FrameSpyOne(string message) => true;
                    bool FrameSpyTwo(string message) => true;
                    FrameRequest frameRequestOne = mockFramesFactory.GetValidFrameRequest(FrameSpyOne);
                    FrameRequest frameRequestTwo = mockFramesFactory.GetValidFrameRequest(FrameSpyTwo);
                    KarassFactory karassFactory = new KarassFactory();
                    IKarass karass = karassFactory.Get(new List<Action>(), new List<Action>(), new[]
                    {
                        frameRequestOne,
                        frameRequestTwo
                    });

                    IKanKan kankan = new KanKan(karass, framesFactory);

                    Assert.True(kankan.Current.NextFrames.Contains(frameRequestOne));
                    Assert.False(kankan.Current.NextFrames.Contains(frameRequestTwo));
                    Assert.False(kankan.Current.LastFrames.Any());

                    kankan.MoveNext();

                    Assert.False(kankan.Current.NextFrames.Contains(frameRequestOne));
                    Assert.True(kankan.Current.NextFrames.Contains(frameRequestTwo));

                    Assert.True(kankan.Current.LastFrames.Contains(frameRequestOne));
                    Assert.False(kankan.Current.LastFrames.Contains(frameRequestTwo));

                    kankan.MoveNext();
                    Assert.False(kankan.Current.NextFrames.Any());
                    Assert.False(kankan.Current.LastFrames.Contains(frameRequestOne));
                    Assert.True(kankan.Current.LastFrames.Contains(frameRequestTwo));
                }
            }

            public class NextAndLastMessage
            {
                public class GivenPresetMessage
                {
                    [TestCase("Scout")]
                    [TestCase("Cat")]
                    public void ThenSetNextMessage(string message)
                    {
                        KarassMessage karassMessage = new KarassMessage();
                        IKanKan kankan = new KanKan(new KarassDummy(), new FrameFactoryDummy());
                        karassMessage.SetMessage(message);
                        kankan.SetKarassMessage(karassMessage);
                        Assert.True(kankan.Current.NextMessage == message);
                    }
                }

                public class GivenMessageSetMidFrames
                {
                    [TestCase(1, "Cow")]
                    [TestCase(2, "Moo")]
                    [TestCase(3, "Quack")]
                    public void ThenReturnNextMessageBeforeBeingSentToFrames(int frame, string inputMessage)
                    {
                        IDependencies dependencies = new KarassDependencies();
                        IFrameFactory framesFactory = new FrameFactory(dependencies);
                        MockFramesFactory mockFramesFactory = new MockFramesFactory(framesFactory);

                        string frameOneMessage = string.Empty;

                        bool FrameSpyOne(string message)
                        {
                            frameOneMessage = message;
                            return true;
                        }

                        string frameTwoMessage = string.Empty;

                        bool FrameSpyTwo(string message)
                        {
                            frameTwoMessage = message;
                            return true;
                        }

                        string frameThreeMessage = string.Empty;

                        bool FrameSpyThree(string message)
                        {
                            frameThreeMessage = message;
                            return true;
                        }


                        string frameFourMessage = string.Empty;

                        bool FrameSpyFour(string message)
                        {
                            frameFourMessage = message;
                            return true;
                        }

                        FrameRequest frameRequestOne = mockFramesFactory.GetValidFrameRequest(FrameSpyOne);
                        FrameRequest frameRequestTwo = mockFramesFactory.GetValidFrameRequest(FrameSpyTwo);
                        FrameRequest frameRequestThree = mockFramesFactory.GetValidFrameRequest(FrameSpyThree);
                        FrameRequest frameRequestFour = mockFramesFactory.GetValidFrameRequest(FrameSpyFour);


                        KarassFactory karassFactory = new KarassFactory();
                        IKarass karass = karassFactory.Get(new List<Action>(), new List<Action>(), new[]
                        {
                            frameRequestOne,
                            frameRequestTwo,
                            frameRequestThree,
                            frameRequestFour,
                        });


                        KanKanSetMessageFake kankan =
                            new KanKanSetMessageFake(karass, framesFactory, frame, inputMessage);

                        if (frame >= 4)
                        {
                            Assert.Fail("Only 4 frame spies. Set value to less than equal to 4.");
                        }

                        bool foundNext = false;
                        bool foundLast = false;
                        for (int i = 0; i <= 4; i++)
                        {
                            kankan.MoveNext();
                            CheckFrameForNextAndLastMessage(i, frame, inputMessage, kankan, ref foundLast,
                                ref foundNext);
                        }

                        Assert.True(foundNext);
                        Assert.True(foundLast);
                    }

                    private static bool CheckFrameForNextAndLastMessage(int i, int frame, string inputMessage,
                        KanKanSetMessageFake kankan, ref bool foundLast, ref bool foundNext)
                    {
                        if (i == frame && !foundNext)
                        {
                            foundNext = kankan.Current.NextMessage == inputMessage;
                            Assert.True(kankan.Current.LastMessage != inputMessage);
                        }
                        else if ((i == (frame + 1)) && !foundLast)
                        {
                            foundLast = kankan.Current.LastMessage == inputMessage;
                            Assert.True(kankan.Current.NextMessage != inputMessage);
                        }
                        else
                        {
                            Assert.True(kankan.Current.NextMessage == string.Empty);
                            Assert.True(kankan.Current.LastMessage == string.Empty);
                        }

                        return foundNext;
                    }
                }
            }
        }
    }
}