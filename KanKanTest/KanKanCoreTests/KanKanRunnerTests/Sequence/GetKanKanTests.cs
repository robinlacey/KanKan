using System;
using KanKanCore.Exception;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests.Sequence
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
                    IKanKan[] kanKanDummy = {new KanKanDummy(), new KanKanDummy()};
                    IKanKanSequenceRunner kanKanRunner = new KanKanSequenceRunner(kanKanDummy, String.Empty);
                    Assert.Throws<NoKanKanWithTag>(() => { kanKanRunner.Get(Guid.NewGuid().ToString()); });
                }
            }

            public class WhenDuplicateTagsExist
            {
                [Test]
                public void ThenThrowDuplicateKanKanTagException()
                {
                    string tag = "Scout The Dog";
                    IKanKan[] kanKanDummyOne = {new KanKanDummy(), new KanKanDummy()};
                    IKanKan[] kanKanDummyTwo= {new KanKanDummy(), new KanKanDummy()};

                    IKanKanSequenceRunner kanKanRunner = new KanKanSequenceRunner(kanKanDummyOne, tag);
                    Assert.Throws<DuplicateKanKanTag>(() => { kanKanRunner.Add(kanKanDummyTwo, tag); });
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
                    IKanKan[] kanKanDummy = {new KanKanDummy(), new KanKanDummy()};
                    IKanKanSequenceRunner kanKanRunner = new KanKanSequenceRunner(kanKanDummy, tag);
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
                IKanKan[] kanKanDummyOne = {new KanKanDummy(), new KanKanDummy()};
                IKanKan[] kanKanDummyTwo = {new KanKanDummy(), new KanKanDummy()};

                IKanKanSequenceRunner kanKanRunner = new KanKanSequenceRunner (kanKanDummyOne, Guid.NewGuid().ToString());
                kanKanRunner.Add(kanKanDummyTwo, tag);
                Assert.AreSame(kanKanRunner.Get(tag), kanKanDummyTwo);
            }
        }
    }
}