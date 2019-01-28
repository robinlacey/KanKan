using System;
using KanKanCore.Exception;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan.Spy;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests.Sequence
{
    public class RunKanKanTests
    {
        public class GivenValidTags
        {
            public class WhenRunIsCalledWithNewTagAndMoveNextIsCalled
            {
                [Test]
                public void ThenResetIsCalledOnCurrentKanKanAndMoveNextIsCalled()
                {
                    KanKanCallCountSpy kanKanCallCountSpyOne = new KanKanCallCountSpy();
                    KanKanCallCountSpy kanKanCallCountSpyTwo = new KanKanCallCountSpy();

                    IKanKan[] kanKansOne = {kanKanCallCountSpyOne, kanKanCallCountSpyTwo};
                    
                    KanKanCallCountFake kanKanCallCountSpyThree = new KanKanCallCountFake(true,false,1);;
                    KanKanCallCountSpy kanKanCallCountSpyFour = new KanKanCallCountSpy();
                    
                    IKanKan[] kanKansTwo = {kanKanCallCountSpyThree, kanKanCallCountSpyFour};

                   
                    IKanKanRunner<IKanKan[]> kanKanRunner = new KanKanSequenceRunner(kanKansOne, "Cats");
                    kanKanRunner.Add(kanKansTwo, "Dogs");

                    kanKanRunner.Run("Dogs");
                    kanKanRunner.MoveNext();

                    Assert.AreSame(kanKanRunner.Current, kanKanCallCountSpyThree);
                    Assert.True(kanKanCallCountSpyOne.MoveNextCallCount == 0);
                    Assert.True(kanKanCallCountSpyOne.ResetCallCount == 1);
                    Assert.True(kanKanCallCountSpyTwo.ResetCallCount == 1);
                    
                    Assert.True(kanKanCallCountSpyThree.MoveNextCallCount == 1);
                    Assert.True(kanKanCallCountSpyFour.MoveNextCallCount == 0);
                    
                    kanKanRunner.MoveNext();
                    Assert.True(kanKanCallCountSpyThree.MoveNextCallCount == 2);
                    Assert.True(kanKanCallCountSpyFour.MoveNextCallCount == 0);
                    
                    kanKanRunner.MoveNext();
                    Assert.True(kanKanCallCountSpyThree.MoveNextCallCount == 2);
                    Assert.True(kanKanCallCountSpyFour.MoveNextCallCount == 1);
                    
                }
            }
        }

        public class GivenInvalidTags
        {
            [Test]
            public void ThenThrowNoKanKanWithTagException()
            {
                IKanKan[] kanKanDummy = {new KanKanDummy(), new KanKanDummy(), };
                IKanKanRunner<IKanKan[]> kanKanRunner = new KanKanSequenceRunner(kanKanDummy, string.Empty);
                Assert.Throws<NoKanKanWithTag>(() => { kanKanRunner.Run(Guid.NewGuid().ToString()); });
            }
        }
    }
}