using KanKanCore.Factories;
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
           
            KarassFrameSpy frameAction = new KarassFrameSpy();
            dependenciesSpy.Register<IKarassFrame<FrameStructDummy>>(()=> frameAction);

            FrameFactory frameFactory = new FrameFactory(dependenciesSpy);
            frameFactory.RegisterRoute<FrameStructDummy,IKarassFrame<FrameStructDummy>>();
      
            IKarassFrame<FrameStructDummy> frame = frameFactory.Get<FrameStructDummy>();
            
            frame.Execute(frameActionData, message);
       
            Assert.True(dependenciesSpy.GetCallCount == 1);
            Assert.True(dependenciesSpy.RegisterCallCount == 1);
            Assert.True(frameAction.ExecuteCallCount == 1);
            Assert.True(frame.Message == message);
            Assert.True(frame.RequestData.Test == testString);
        }    
    }
}