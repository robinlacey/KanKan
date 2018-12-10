using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Interface;
using KanKanCore.Karass.Message;
using KanKanTest.Mocks.Dependencies;
using KanKanTest.Mocks.UAction;
using Xunit;

namespace KanKanTest.SequentialKarassTests
{
    public class SequentialKarassTests
    {
        [Fact]
        public void KanKanTakesArrayOfKarass()
        {
            Karass[] sequence = new Karass[]
            {
                new KarassDummy(), 
                new KarassDummy()
            }; 
            KanKan kankan = new KanKan(sequence, new KarassMessageDummy());
        }

        public class GivenTwoEmptyKarass
        {
            [Fact]
            public void BothKarassWillBeRun()
            {
                KarassFactory karassFactory = new KarassFactory(new DependenciesDummy());

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
                    new List<Func<string, bool>[]>());
                
                Karass karassTwo = karassFactory.Get(KarassTwoSetupSpy, KarassTwoTeardownSpy,
                    new List<Func<string, bool>[]>());
                
                KanKan kankan = new KanKan(new IKarass[]{ karassOne,karassTwo},new KarassMessage());

                kankan.MoveNext();
                
                Assert.True(karassOneSetupRun);
                Assert.True(karassTwoSetupRun);
                Assert.True(karassOneTeardownRun);
                Assert.True(karassTwoTeardownRun);
            }
        }
    }
}