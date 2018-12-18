using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanTest.Mocks.Dependencies;
using NUnit.Framework;

namespace KanKanTest.SetupTeardownTests
{
  
    public class KarassSetupTeardownTests
    {
        private static KarassFactory KarassFactory => new KarassFactory(new DependenciesDummy());

        private static List<List<Action>> CreateActionListWith(Action[] a) => new List<List<Action>> {a.ToList()};


        [Test]
        public void SetupRunsAllSetupActions()
        {
            bool setupOneRun = false;
            bool setupTwoRun = false;
            bool setupThreeRun = false;
            Action setupOne = () => { setupOneRun = true; };
            Action setupTwo = () => { setupTwoRun = true; };
            Action setupThree = () => { setupThreeRun = true; };
            Karass testKarass = KarassFactory.Get(
                CreateActionListWith(new[] {setupOne, setupTwo, setupThree}),
                new List<List<Action>>(),
                new List<Func<string, bool>[]>());

            testKarass.Setup(0);

            Assert.True(setupOneRun);
            Assert.True(setupTwoRun);
            Assert.True(setupThreeRun);
        }


        [Test]
        public void TeardownRunsAllTeardownActions()
        {
            bool teardownOneRun = false;
            bool teardownTwoRun = false;
            bool teardownThreeRun = false;
            Action teardownOne = () => { teardownOneRun = true; };
            Action teardownTwo = () => { teardownTwoRun = true; };
            Action teardownThree = () => { teardownThreeRun = true; };
            Karass testKarass = KarassFactory.Get(
                new List<List<Action>>(),
                CreateActionListWith(new[] {teardownOne, teardownTwo, teardownThree}),
                new List<Func<string, bool>[]>());

            testKarass.Teardown(0);

            Assert.True(teardownOneRun);
            Assert.True(teardownTwoRun);
            Assert.True(teardownThreeRun);
        }

        [Test]
        public void KarassHasASetupMethod()
        {
            Karass testKarass = KarassFactory.Get(new List<List<Action>>(), new List<List<Action>>(),
                new List<Func<string, bool>[]>());
            testKarass.Setup(0);
        }

        [Test]
        public void KarassHasATeardownMethod()
        {
            Karass testKarass = KarassFactory.Get(new List<List<Action>>(), new List<List<Action>>(),
                new List<Func<string, bool>[]>());
            testKarass.Teardown(0);
        }
    }
}