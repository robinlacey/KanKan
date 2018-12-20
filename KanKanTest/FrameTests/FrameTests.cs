using System;
using KanKanCore.Factories;
using KanKanCore.Karass.Frame.SimpleKarassFrame;
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
        public void FrameFactoryHasRegisterMethodWhichTakesStructIKarassFrame()
        {  
            IDependencies dependencies = new DependenciesDummy();
            FrameFactory frameFactory = new FrameFactory(dependencies);
            
            frameFactory.RegisterRoute<FrameStructDummy, IKarassFrame<FrameStructDummy>>();
        }

       

        [TestCase("Scout", "The Dog")]
        [TestCase("Cats", "Meow")]
        public void FrameFactoryGetMethodReturnsCorrectData(string testString, string message)
        {
            KarassDependenciesSpy dependenciesSpy = new KarassDependenciesSpy();
      
            FrameStructDummy frameActionData = new FrameStructDummy
            {
                Test = testString
            };
           
            KarassFrameSpy<FrameStructDummy> frameAction = new KarassFrameSpy<FrameStructDummy>();
            dependenciesSpy.Register<IKarassFrame<FrameStructDummy>>(()=> frameAction);

            FrameFactory frameFactory = new FrameFactory(dependenciesSpy);
            frameFactory.RegisterRoute<FrameStructDummy,IKarassFrame<FrameStructDummy>>();
      
            IKarassFrame<FrameStructDummy> frame = frameFactory.Get<FrameStructDummy>();
            
            frame.Execute(message, frameActionData);
       
            Assert.True(dependenciesSpy.GetCallCount == 1);
            Assert.True(dependenciesSpy.RegisterCallCount == 1);
            Assert.True(frameAction.ExecuteCallCount == 1);
            Assert.True(frame.Message == message);
            Assert.True(frame.RequestData.Test == testString);
        }    
    }

    public class KarassFrameTests
    {
        [Test]
        public void KarassFrameExists()
        {
            IKarassFrame<object> karassFrame = new KarassFrameDummy<object>();
        }


        public class SimpleKarassFrameTests
        {
           
            [TestCase("Scout")]
            [TestCase("Is")]
            [TestCase("A")]
            [TestCase("Good")]
            [TestCase("Dog")]
            [TestCase(null)]
            
            public void SimpleKarassFrameExecutesFunc(string testMessage)
            {
                
                 bool simpleFrameSpyRun = false;
                 string simpleFrameSpyMessage = string.Empty;
        
                 bool SimpleFrameSpy(string message)
                {
                    simpleFrameSpyRun = true;
                    simpleFrameSpyMessage = message;
                    return true;
                }

            
                 SimpleKarassFrame simpleKarassFrame = new SimpleKarassFrame(SimpleFrameSpy);
                 simpleKarassFrame.Execute(testMessage);
                 Assert.True(simpleFrameSpyRun);
                 Assert.True(simpleFrameSpyMessage == testMessage);
            }
            
           
        }
    }
}