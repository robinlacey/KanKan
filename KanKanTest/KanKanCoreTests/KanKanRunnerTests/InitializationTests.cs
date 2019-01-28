using System;
using System.Collections;
using System.Linq;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
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
    }
}