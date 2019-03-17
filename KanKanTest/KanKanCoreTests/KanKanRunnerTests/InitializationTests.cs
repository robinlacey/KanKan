using System;
using System.Collections;
using System.Linq;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanCore.Karass.Message;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan.Spy;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame;
using KanKanTest.KanKanCoreTests.Mocks.KarassMocks;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests
{
    public class InitializationTests
    {
        [Test]
        public void KanKanRunnerIsIEnumerable()
        {
            IEnumerator kanKanRunner = new KanKanSingleRunner(new KanKanDummy(), string.Empty);
            Assert.NotNull(kanKanRunner);
        }

        [Test]
        public void KanKanRunnerCreatesAKarassMessenger()
        {
            IKanKanRunner<IKanKan> kanKanRunner = new KanKanSingleRunner(new KanKanDummy(), string.Empty);
            Assert.NotNull(kanKanRunner.KarassMessage);
        }

        [Test]
        public void KanKanRunnerSetsKanKanFromConstructorAsCurrent()
        {
            IKanKan kanKanDummy = new KanKanDummy();
            IKanKanRunner<IKanKan> kanKanRunner = new KanKanSingleRunner(kanKanDummy, string.Empty);
            Assert.AreSame(kanKanRunner.Current, kanKanDummy);
        }
        
        [Test]
        public void IEnumeratorCurrentIsCorrectlySet()
        {
            IKanKan kanKanDummy = new KanKanDummy();
            IKanKanRunner<IKanKan> kanKanRunner = new KanKanSingleRunner(kanKanDummy, string.Empty);
            IEnumerator singleAsIEnumerator = kanKanRunner;
            Assert.AreSame((IKanKan) singleAsIEnumerator.Current,kanKanDummy);


            IKanKan[] kanKansDummy = {new KanKanDummy(), new KanKanDummy()};
            IKanKanRunner<IKanKan[]> kanKansRunner = new KanKanSequenceRunner(kanKansDummy, string.Empty);
            IEnumerator sequenceAsIEnumerator = kanKansRunner;
            Assert.AreSame((IKanKan) sequenceAsIEnumerator.Current,kanKansDummy[0]);
            
        }
    }

    public class KarassMessageTests
    {
        public class SetKarassMessageTests
        {
            public class SingleRunner
            {
                [Test]
                public void ThenKanKanKarassMessageIsTheSameAsRunnerKarassMessage()
                {
                    KanKanKarassMessageSpy kankanDummy =
                        new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    IKanKanRunner<IKanKan> kanKanRunner = new KanKanSingleRunner(kankanDummy, string.Empty);
                    Assert.AreSame(kanKanRunner.KarassMessage, kankanDummy.GetKarassMessage());

                    IKarassMessage newKarassMessage = new KarassMessage();
                    kanKanRunner.SetKarassMessage(newKarassMessage);

                    Assert.AreSame(kanKanRunner.KarassMessage, newKarassMessage);
                    Assert.AreSame(newKarassMessage, kankanDummy.GetKarassMessage());
                }
            }

            public class SequenceRunner
            {
                [Test]
                public void ThenKanKansKarassMessagesAreUpdated()
                {
                    KanKanKarassMessageSpy kankanDummyOne = new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    KanKanKarassMessageSpy kankanDummyTwo = new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    KanKanKarassMessageSpy kankanDummyThree = new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    IKanKan[] kankanDummy = {kankanDummyOne, kankanDummyTwo, kankanDummyThree};
                    IKanKanRunner< IKanKan[] > kanKanRunner = new KanKanSequenceRunner( kankanDummy,string.Empty);
                    
                    
                    IKarassMessage newKarassMessage = new KarassMessage();
                    kanKanRunner.SetKarassMessage(newKarassMessage);
                    
                    
                    foreach (IKanKan[] kanKans in kanKanRunner.KanKans.Values)
                    {
                        foreach (IKanKan kankan in kanKans)
                        {
                            KanKanKarassMessageSpy spy = kankan as KanKanKarassMessageSpy;
                            Assert.AreSame(newKarassMessage,spy.GetKarassMessage());
                        }
                    }
                }
            }
        }
        public class SingleRunner
        {
            public class GivenKanKanInConstructor
            {
                [Test]
                public void ThenKanKanKarassMessageIsTheSameAsRunnerKarassMessage()
                {
                    KanKanKarassMessageSpy kankanDummy = new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    IKanKanRunner<IKanKan> kanKanRunner = new KanKanSingleRunner( kankanDummy,string.Empty);
                    Assert.AreSame(kanKanRunner.KarassMessage,kankanDummy.GetKarassMessage());
                }
            }

            public class WhenAddingAKanKan
            {
                [Test]
                public void ThenKanKanHasKarassMessageSet()
                {
                    KanKanKarassMessageSpy kankanDummy = new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    IKanKanRunner<IKanKan> kanKanRunner = new KanKanSingleRunner( kankanDummy,string.Empty);
                    Assert.AreSame(kanKanRunner.KarassMessage,kankanDummy.GetKarassMessage());
                    
                    KanKanKarassMessageSpy newKanKan = new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    kanKanRunner.Add(newKanKan,"Scout");
                    
                    KanKanKarassMessageSpy newKanKanReturned = kanKanRunner.Get("Scout") as KanKanKarassMessageSpy;
                    Assert.AreSame(kanKanRunner.KarassMessage,newKanKanReturned.GetKarassMessage());
                }
            }
        }

        public class SequenceRunner
        {
            public class GivenKanKanInConstructor
            {
                [Test]
                public void ThenKanKanKarassMessageIsTheSameAsRunnerKarassMessage()
                {
                    KanKanKarassMessageSpy kankanDummyOne = new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    KanKanKarassMessageSpy kankanDummyTwo = new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    KanKanKarassMessageSpy kankanDummyThree = new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    IKanKan[] kankanDummy = {kankanDummyOne, kankanDummyTwo, kankanDummyThree};
                    IKanKanRunner< IKanKan[] > kanKanRunner = new KanKanSequenceRunner( kankanDummy,string.Empty);
                    foreach (IKanKan[] kanKans in kanKanRunner.KanKans.Values)
                    {
                        foreach (IKanKan kankan in kanKans)
                        {
                            KanKanKarassMessageSpy spy = kankan as KanKanKarassMessageSpy;
                            Assert.AreSame(kanKanRunner.KarassMessage,spy.GetKarassMessage());
                        }
                    }
                }
            }
            
            public class WhenAddingAKanKans
            {
                [Test]
                public void ThenKanKansHaveKarassMessageSet()
                {
                    KanKanKarassMessageSpy kankanDummyOne = new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    KanKanKarassMessageSpy kankanDummyTwo = new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    KanKanKarassMessageSpy kankanDummyThree = new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    IKanKan[] kankanDummy = {kankanDummyOne, kankanDummyTwo, kankanDummyThree};
                    IKanKanRunner< IKanKan[] > kanKanRunner = new KanKanSequenceRunner( kankanDummy,string.Empty);
                    
                    
                    
                    KanKanKarassMessageSpy kankanDummyFour = new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    KanKanKarassMessageSpy kankanDummyFive= new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    KanKanKarassMessageSpy kankanDummySix = new KanKanKarassMessageSpy(new KarassDummy(), new FrameFactoryDummy());
                    IKanKan[] newKanKans = {kankanDummyFour, kankanDummyFive, kankanDummySix};
                    kanKanRunner.Add(newKanKans,"Scout The Dog");


                    IKanKan[] returned = kanKanRunner.Get("Scout The Dog");
                    
                   
                    foreach (IKanKan kankan in returned)
                    {
                        KanKanKarassMessageSpy spy = kankan as KanKanKarassMessageSpy;
                        Assert.AreSame(kanKanRunner.KarassMessage,spy.GetKarassMessage());
                    }
                    
                }
            }
            
        }

    }
}