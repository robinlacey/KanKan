using System;
using System.Collections.Generic;
using KanKanCore.Exception;
using KanKanCore.Factories;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanTest.KanKanCoreTests.Mocks.KanKan.Fake;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame.FrameStruct;
using KanKanTestHelper;
using KanKanTestHelper.Interface;
using KanKanTestHelper.Run;
using KanKanTestHelper.Run.Until;
using NUnit.Framework;

namespace KanKanTest.KanKanTestHelperTests.Run.Until.MessageReceived
{
    public class RunUntilMessageReceivedTests
    {
           
        protected static FrameRequest[] CreateFrames(int frameNumber, string message)
        {
            FrameRequest[] returnList = new FrameRequest[frameNumber+10];
            for (int i = 0; i < frameNumber+10; i++)
            {
                if (i == frameNumber)
                {
                    returnList[i] = new FrameRequest(new FrameStructDummy {Test = message});
                }
                else
                {
                    returnList[i] = new FrameRequest(new FrameStructDummy {Test = Guid.NewGuid().ToString()});
                }
            }
            return returnList;
        }
       
        public class MessageReceivedTests
        {
            public class GivenNoMessageMarching
            {
                [TestCase(12)]
                [TestCase(4)]
                [TestCase(42)]
                public void ThenThrowMessageNotReceivedException(int frames)
                {
                    IDependencies dependencies = new KarassDependencies();
                    IFrameFactory frameFactory = new FrameFactory(dependencies);
                    dependencies.Register<IKarassFrame<FrameStructDummy>>(() => new KarassFrameDummy<FrameStructDummy>(dependencies));
                    frameFactory.RegisterRoute<FrameStructDummy, IKarassFrame<FrameStructDummy>>();

                    IKarass karass = new KarassFactory().Get(new List<Action>(), new List<Action>(),
                        CreateFrames(frames, "Message Will Not Be Found"));
                    KanKan kankan = new KanKanSetMessageFake(karass,frameFactory,frames-1,"Scout A Dog");
                    IRunUntil runUntil = new RunUntil(kankan);
                    IRunKanKan runKanKan = new RunKanKan(runUntil);
                    IKanKanTestHelper testHelper = new TestHelper(runKanKan,frameFactory);
               
                    Assert.Throws<MessageNotReceivedException>(()=>testHelper.Run.Until.WillReceive("Doggo"));
                    Assert.Throws<MessageNotReceivedException>(()=>testHelper.Run.Until.HasReceived("Doggo"));
                }
            }
            public class GivenMessageSetOnFrame
            {
                [TestCase(2, "Cats")]
                [TestCase(4,"Chickens")]
                [TestCase(42,"The Answer")]
                public void ThenReturnCorrectFrame(int willReceiveFrame, string testMessage)
                {
                    IDependencies dependencies = new KarassDependencies();
                    IFrameFactory frameFactory = new FrameFactory(dependencies);
                    dependencies.Register<IKarassFrame<FrameStructDummy>>(() => new KarassFrameDummy<FrameStructDummy>(dependencies));
                    frameFactory.RegisterRoute<FrameStructDummy, IKarassFrame<FrameStructDummy>>();

                    IKarass karass = new KarassFactory().Get(new List<Action>(), new List<Action>(),
                        CreateFrames(willReceiveFrame, testMessage));
                    KanKan kankan = new KanKanSetMessageFake(karass,frameFactory,willReceiveFrame-1,testMessage);
                    IRunUntil runUntil = new RunUntil(kankan);
                    IRunKanKan runKanKan = new RunKanKan(runUntil);
                    IKanKanTestHelper testHelper = new TestHelper(runKanKan,frameFactory);
               
                    Assert.True(testHelper.Run.Until.WillReceive(testMessage).TotalFramesRun == willReceiveFrame);
                    Assert.True(testHelper.Run.Until.HasReceived(testMessage).TotalFramesRun == willReceiveFrame+1);
                }
             

            }

          
        }
     
    }
}