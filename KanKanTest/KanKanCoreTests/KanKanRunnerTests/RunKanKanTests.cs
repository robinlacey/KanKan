using System;
using KanKanCore.Exception;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan.Spy;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests
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

                    IKanKanSingleRunner kanKanRunner = new KanKanSingleRunner(kanKanCallCountSpyOne, "Cats");
                    kanKanRunner.Add(kanKanCallCountSpyTwo, "Dogs");

                    kanKanRunner.Run("Dogs");
                    kanKanRunner.MoveNext();

                    Assert.AreSame(kanKanRunner.Current, kanKanCallCountSpyTwo);
                    Assert.True(kanKanCallCountSpyOne.MoveNextCallCount == 0);
                    Assert.True(kanKanCallCountSpyOne.ResetCallCount == 1);
                    Assert.True(kanKanCallCountSpyTwo.MoveNextCallCount == 1);
                }
            }
        }

        public class GivenInvalidTags
        {
            [Test]
            public void ThenThrowNoKanKanWithTagException()
            {
                IKanKan kanKanDummy = new KanKanDummy();
                IKanKanRunner kanKanRunner = new KanKanSingleRunner(kanKanDummy, String.Empty);
                Assert.Throws<NoKanKanWithTag>(() => { kanKanRunner.Run(Guid.NewGuid().ToString()); });
            }
        }
    }
}