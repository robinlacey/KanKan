using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanCore.Karass.Message;
using KanKanTest.Mocks.Dependencies;
using KanKanTest.Mocks.KarassFrame;
using NUnit.Framework;

namespace KanKanTest.SequentialKarassTests
{
    public class GivenTwoEmptyKarass
    {
        [Test]
        public void BothKarassSetupAndTeardownWillBeRun()
        {
            KarassFactory karassFactory = new KarassFactory(new DependenciesDummy(), new FrameFactoryDummy());

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

            KanKan kankan = new KanKan(new IKarass[] {karassOne, karassTwo}, new KarassMessage());

            kankan.MoveNext();

            Assert.True(karassOneSetupRun);
            Assert.True(karassTwoSetupRun);
            Assert.True(karassOneTeardownRun);
            Assert.True(karassTwoTeardownRun);
        }
    }
}