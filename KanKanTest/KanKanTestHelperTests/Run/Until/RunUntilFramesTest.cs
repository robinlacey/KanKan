using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTest.KanKanCoreTests.Factories;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame.FrameStruct;
using KanKanTest.KanKanTestHelperTests.Mocks;
using KanKanTestHelper.Exception;
using KanKanTestHelper.Interface;
using KanKanTestHelper.Run;
using KanKanTestHelper.Run.Until;
using NUnit.Framework;

namespace KanKanTest.KanKanTestHelperTests.Run.Until
{
    
    
        
    public class RunUntilFramesTest
    {
        [Test]
        public void RunContainsIRunUntil()
        {
            IRunKanKan runKanKan = new RunKanKan(new KanKanDummy(), new RunUntilDummy());
            IRunUntil until = (IRunUntil) runKanKan.Until;
           Assert.IsNotNull(until);
        }
        
        [Test]
        public void RunSetsIRunUntilWithConstructor()
        {
            IRunUntil runUntil = new RunUntilDummy();
            RunKanKan runKanKan = new RunKanKan(new KanKanDummy(),runUntil);
            Assert.IsNotNull(runKanKan.Until);
            Assert.AreSame(runUntil,runKanKan.Until);
        }

        [Test]
        public void RunUntilSetsKanKanFromConstructor()
        {
            KanKanDummy kankanDummy = new KanKanDummy();
            IRunUntil runUntil = new RunUntil(kankanDummy);
            Assert.AreSame(runUntil.KanKan,kankanDummy);
        }

        
        public class GivenNoFrames
        {
            private readonly IDependencies _dependencies = new KarassDependencies();
            private readonly KarassFactory _karassFactory = new KarassFactory();
            private IFrameFactory _frameFactory;
            private IRunUntil _runUntil;
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
                _runUntil = new RunUntil(_kankan);
                FrameRequest fr = new MockFramesFactory(_frameFactory).GetInvalidFrameRequest();
                Assert.Throws<NoValidRequestType>(()=>_runUntil.LastFrame(fr));
                Assert.Throws<NoValidRequestType>(()=>_runUntil.NextFrame(fr));
            }
        }

        public class ShouldReturnKanKanStateTests
        {
            [Test]
            public void GivenEmptyFrameRequestList()
            {
                Assert.False( RunUntil.ShouldReturnKanKanState(new List<FrameRequest>(),"Scout" ));
            }
            
            [Test]
            public void GivenNoMatchingInRequestList()
            {
                Assert.False( RunUntil.ShouldReturnKanKanState(new List<FrameRequest>(){new FrameRequest(42), new FrameRequest(Guid.NewGuid())},"hello" ));
            }
            [Test]     
            public void GivenMatchingFrameRequest()
            {
                Assert.True( RunUntil.ShouldReturnKanKanState(new List<FrameRequest>(){ new FrameRequest("Scout The Dog")},"Scout The Dog"));
                Assert.True( RunUntil.ShouldReturnKanKanState(new List<FrameRequest>(){ new FrameRequest(42)},42));
                Guid testGuid = Guid.NewGuid();
                Assert.True( RunUntil.ShouldReturnKanKanState(new List<FrameRequest>(){ new FrameRequest(testGuid)},testGuid));
            }
        }
        
        public class GivenMatchingTypesInAnyFrames
        {
            private readonly IDependencies _dependencies = new KarassDependencies();
            private readonly KarassFactory _karassFactory = new KarassFactory();
            private IFrameFactory _frameFactory;
            private IRunUntil _runUntil;
            private IKanKan _kankan;
            
            [SetUp]
            public void Setup()
            {
                _frameFactory = new FrameFactory(_dependencies);
                IKarassFrame<FrameStructDummy> frameDummy = new KarassFrameDummy<FrameStructDummy>(_dependencies);
                _dependencies.Register<IKarassFrame<FrameStructDummy>>(()=>frameDummy);
                _frameFactory.RegisterRoute<FrameStructDummy,IKarassFrame<FrameStructDummy>>();
               
            }
           
            [Test]
            public void ThenDoesNotThrowException()
            {
                FrameStructDummy test = new FrameStructDummy()
                {
                    Test = "Scout"
                };
                
                IKarass karass = _karassFactory.Get(new List<Action>(), new List<Action>(),
                    new[]
                    {
                        new FrameRequest(test)
                    });
                var f = _frameFactory.Get<FrameStructDummy>();
                Console.WriteLine(f);
                
                _kankan = new KanKan(karass,_frameFactory);
                _runUntil = new RunUntil(_kankan);
                
               
                Assert.DoesNotThrow(()=>_runUntil.LastFrame(test));
                Assert.DoesNotThrow(()=>_runUntil.NextFrame(test));
            }
        }
    }
}