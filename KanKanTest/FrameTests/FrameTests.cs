using System;
using KanKanCore.Factories;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTest.Mocks.Dependencies;
using KanKanTest.Mocks.KarassFrame;
using KanKanTest.Mocks.KarassFrame.FrameStruct;
using NUnit.Framework;

namespace KanKanTest.FrameTests
{
    public class FrameFactoryTests
    {
        [Test]
        public void HasRegisterMethodWhichTakesStructIKarassFrame()
        {
            IDependencies dependencies = new DependenciesDummy();
            FrameFactory frameFactory = new FrameFactory(dependencies);
            frameFactory.RegisterRoute<FrameStructDummy, IKarassFrame<FrameStructDummy>>();
        }


        [TestCase("Scout", "The Dog")]
        [TestCase("Cats", "Meow")]
        public void GetMethodReturnsCorrectData(string testString, string message)
        {
            // Arrange
            KarassDependenciesSpy dependenciesSpy = new KarassDependenciesSpy();
            FrameStructDummy frameActionData = new FrameStructDummy
            {
                Test = testString
            };
            KarassFrameSpy<FrameStructDummy> frameAction = new KarassFrameSpy<FrameStructDummy>(dependenciesSpy);
            dependenciesSpy.Register<IKarassFrame<FrameStructDummy>>(() => frameAction);
            FrameFactory frameFactory = new FrameFactory(dependenciesSpy);
            frameFactory.RegisterRoute<FrameStructDummy, IKarassFrame<FrameStructDummy>>();

            //Act
            IKarassFrame<FrameStructDummy> frame = frameFactory.Get<FrameStructDummy>();
            frame.Execute(message, frameActionData);

            
            //Assert
            Assert.True(dependenciesSpy.GetCallCount == 1);
            Assert.True(dependenciesSpy.RegisterCallCount == 1);
            Assert.True(frameAction.ExecuteCallCount == 1);
            Assert.True(frame.Message == message);
            Assert.True(frame.RequestData.Test == testString);
            Assert.AreEqual(frameAction.Dependencies , dependenciesSpy);
        }

        [TestCase("What is Scout?", "Scout is a Dog")]
        [TestCase("What is Scout not?", "Scout is not a Cat")]
        public void ExecuteMethodCorrectlyExecutesFrameRequest(string testMessage, string testActionDataPayload)
        {
            //Arrange
            KarassDependenciesSpy dependenciesSpy = new KarassDependenciesSpy();
            FrameStructDummy frameActionData = new FrameStructDummy
            {
                Test = testActionDataPayload
            };

            FrameRequest frameRequest = new FrameRequest(frameActionData);
            KarassFrameSpy<FrameStructDummy> frameAction = new KarassFrameSpy<FrameStructDummy>(dependenciesSpy);
            dependenciesSpy.Register<IKarassFrame<FrameStructDummy>>(() => frameAction);
            FrameFactory frameFactory = new FrameFactory(dependenciesSpy);
            frameFactory.RegisterRoute<FrameStructDummy, IKarassFrame<FrameStructDummy>>();
            
            //Act
            frameFactory.Execute(frameRequest, testMessage);

            //Assert
            Assert.True(dependenciesSpy.RegisterCallCount == 1);
            Assert.True(dependenciesSpy.GetCallCount == 1);
            Assert.True(frameAction.ExecuteCallCount == 1);
            Assert.True(frameAction.RequestData.Test == testActionDataPayload);
            Assert.True(frameAction.Message == testMessage);
            Assert.True(frameAction.Dependencies == dependenciesSpy);
        }
    }
}