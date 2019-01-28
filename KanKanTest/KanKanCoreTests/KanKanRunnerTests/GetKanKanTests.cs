using System;
using KanKanCore.Exception;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests
{
    public class GetKanKanTests
    {
        public class GivenInvalidTag
        {
            public class WhenNoKanKanExistsWith
            {
                [Test]
                public void ThenThrowNoKanKanWithTagException()
                {
                    IKanKan kanKanDummy = new KanKanDummy();
                    IKanKanRunner<IKanKan> kanKanRunner = new KanKanSingleRunner(kanKanDummy, String.Empty);
                    Assert.Throws<NoKanKanWithTag>(() => { kanKanRunner.Get(Guid.NewGuid().ToString()); });
                }
            }

            public class WhenDuplicateTagsExist
            {
                [Test]
                public void ThenThrowDuplicateKanKanTagException()
                {
                    string tag = "Scout The Dog";
                    IKanKan kanKanDummy = new KanKanDummy();
                    IKanKanRunner<IKanKan> kanKanRunner = new KanKanSingleRunner(kanKanDummy, tag);
                    Assert.Throws<DuplicateKanKanTag>(() => { kanKanRunner.Add(new KanKanDummy(), tag); });
                }
            }
        }

        public class GivenKanKanInConstructor
        {
            public class WhenThereAreNoAdditionalKanKan
            {
                [TestCase("Scout")]
                [TestCase("Dog")]
                public void ThenGetWillReturnCorrectKanKan(string tag)
                {
                    IKanKan kanKanDummy = new KanKanDummy();
                    IKanKanRunner<IKanKan> kanKanRunner = new KanKanSingleRunner(kanKanDummy, tag);
                    Assert.AreSame(kanKanRunner.Get(tag), kanKanDummy);
                }
            }
        }

        public class GivenKanKanAdded
        {
            [TestCase("Cats")]
            [TestCase("Dogs")]
            public void GetReturnsAddedKanKan(string tag)
            {
                IKanKan kanKanDummy = new KanKanDummy();

                IKanKanRunner<IKanKan> kanKanRunner = new KanKanSingleRunner(new KanKanDummy(), Guid.NewGuid().ToString());
                kanKanRunner.Add(kanKanDummy, tag);
                Assert.AreSame(kanKanRunner.Get(tag), kanKanDummy);
            }
        }
    }
}