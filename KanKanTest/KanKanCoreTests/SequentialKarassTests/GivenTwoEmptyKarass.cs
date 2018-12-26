using System.Collections.Generic;
using KanKanCore.Factories;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.SequentialKarassTests
{
    public class GivenTwoEmptyKarass
    {
        [Test]
        public void BothKarassSetupAndTeardownWillBeRun()
        {
            KarassFactory karassFactory = new KarassFactory();

            bool karassOneSetupRun = false;

            void KarassOneSetupSpy()
            {
                karassOneSetupRun = true;
            }

            bool karassTwoSetupRun = false;

            void KarassTwoSetupSpy()
            {
                karassTwoSetupRun = true;
            }

            bool karassOneTeardownRun = false;

            void KarassOneTeardownSpy()
            {
                karassOneTeardownRun = true;
            }

            bool karassTwoTeardownRun = false;

            void KarassTwoTeardownSpy()
            {
                karassTwoTeardownRun = true;
            }

            Karass karassOne = karassFactory.Get(KarassOneSetupSpy, KarassOneTeardownSpy,
                new List<FrameRequest[]>());

            Karass karassTwo = karassFactory.Get(KarassTwoSetupSpy, KarassTwoTeardownSpy,
                new List<FrameRequest[]>());

            KanKan kankan = new KanKan(new IKarass[] {karassOne, karassTwo}, new FrameFactory(new KarassDependencies()));

            kankan.MoveNext();

            Assert.True(karassOneSetupRun);
            Assert.True(karassTwoSetupRun);
            Assert.True(karassOneTeardownRun);
            Assert.True(karassTwoTeardownRun);
        }
    }
}