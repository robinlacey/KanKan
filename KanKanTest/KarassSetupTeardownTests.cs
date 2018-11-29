using System;
using System.Collections.Generic;
using System.ComponentModel;
using KanKanCore;
using KanKanCore.Karass;
using Xunit;

namespace KanKanTest
{
    public class KarassSetupTeardownTests
    {
        // Setup should be run all at the same time
        // Teardown only run at the end of frames 
        Tuple<Action,Func<string,bool>[],Action> thisIsAKarass = new Tuple<Action, Func<string, bool>[], Action>(null,null,null);
        
        // JUMP
        // PLAY PARTICLES
        // DO SOMETHING
        // START
            // RUN 
                //RUN RUN // TEARDOwN
            // RUN // TEARDOWN
            
        [Fact]
        void SetupRunsAllSetupActions()
        {
            bool setupOneRun = false;
            bool setupTwoRun = false;
            bool setupThreeRun = false;
            Action setupOne = () => { setupOneRun = true; };
            Action setupTwo = () => { setupTwoRun = true; };
            Action setupThree = () => { setupThreeRun = true; };
            Karass testKarass = new Karass(new[] {setupOne, setupTwo, setupThree}, new Action[0],
                new List<Func<string, bool>[]>());

            testKarass.Setup();
            
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
            Karass testKarass = new Karass( new Action[0],new[] {teardownOne, teardownTwo, teardownThree},
                new List<Func<string, bool>[]>());

            testKarass.Teardown();
            
            Assert.True(teardownOneRun);
            Assert.True(teardownTwoRun);
            Assert.True(teardownThreeRun);
        }

        [Fact]
        void KarassHasASetupMethod()
        {
            Karass testKarass = new Karass(new Action[0], new Action[0],
                new List<Func<string, bool>[]>());
            testKarass.Setup();
        }
        
        [Fact]
        void KarassHasATeardownMethod()
        {
            Karass testKarass = new Karass(new Action[0], new Action[0],
                new List<Func<string, bool>[]>());
            testKarass.Teardown();
        }
    }
    
    
    
// NOTE: When we combine Karass' we'll need to come up with a nice way to run setup and teardiwns for ONLY the frames being run 
// Suggest dictionary with frames as key and setup and teardown as tupel values
}