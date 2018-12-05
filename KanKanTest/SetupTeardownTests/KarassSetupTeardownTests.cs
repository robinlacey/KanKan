using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Karass;
using Xunit;

namespace KanKanTest.SetupTeardownTests
{
    public class KarassSetupTeardownTests
    {
        private static List<List<Action>> CreateActionListWith(Action[] a) => new List<List<Action>> {a.ToList()};


        [Fact]
        void SetupRunsAllSetupActions()
        {
            bool setupOneRun = false;
            bool setupTwoRun = false;
            bool setupThreeRun = false;
            Action setupOne = () => { setupOneRun = true; };
            Action setupTwo = () => { setupTwoRun = true; };
            Action setupThree = () => { setupThreeRun = true; };
            Karass testKarass = new Karass(
                CreateActionListWith(new[] {setupOne, setupTwo, setupThree}),
                new List<List<Action>>(),
                new List<Func<string, bool>[]>());

            testKarass.Setup(0);

            Assert.True(setupOneRun);
            Assert.True(setupTwoRun);
            Assert.True(setupThreeRun);
        }


        [Fact]
        void TeardownRunsAllTeardownActions()
        {
            bool teardownOneRun = false;
            bool teardownTwoRun = false;
            bool teardownThreeRun = false;
            Action teardownOne = () => { teardownOneRun = true; };
            Action teardownTwo = () => { teardownTwoRun = true; };
            Action teardownThree = () => { teardownThreeRun = true; };
            Karass testKarass = new Karass(
                new List<List<Action>>(),
                CreateActionListWith(new[] {teardownOne, teardownTwo, teardownThree}),
                new List<Func<string, bool>[]>());

            testKarass.Teardown(0);

            Assert.True(teardownOneRun);
            Assert.True(teardownTwoRun);
            Assert.True(teardownThreeRun);
        }

        [Fact]
        void KarassHasASetupMethod()
        {
            Karass testKarass = new Karass(new List<List<Action>>(), new List<List<Action>>(),
                new List<Func<string, bool>[]>());
            testKarass.Setup(0);
        }

        [Fact]
        void KarassHasATeardownMethod()
        {
            Karass testKarass = new Karass(new List<List<Action>>(), new List<List<Action>>(),
                new List<Func<string, bool>[]>());
            testKarass.Teardown(0);
        }
    }
}