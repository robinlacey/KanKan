using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore.Karass;
using KanKanTest.Mocks.UAction;
using Xunit;

namespace KanKanTest
{
    public class KarassMonoidTests
    {
        public class KarassAddition
        {
            public class GivenMultipleSetupMethods
            {
                [Fact]
                public void WhenAddedThenMethodsAreInInList_ExampleOne()
                {
                    Action setupOne = () => { };
                    Karass karassOne = new Karass(new Action[] {setupOne}, new Action[0],
                        new List<Func<string, bool>[]>());
                    Action setupTwo = () => { };
                    Karass karassTwo = new Karass(new Action[] {setupTwo}, new Action[0],
                        new List<Func<string, bool>[]>());

                    Karass combinedKarass = karassOne + karassTwo;

                    Assert.True(combinedKarass.SetupActions.Count() == 2);
                    Assert.True(combinedKarass.SetupActions.Contains(setupOne));
                    Assert.True(combinedKarass.SetupActions.Contains(setupTwo));
                }

                [Fact]
                public void WhenAddedThenMethodsAreInInList_ExampleTwo()
                {
                    Action setupOne = () => { };
                    Karass karassOne = new Karass(new Action[] {setupOne}, new Action[0],
                        new List<Func<string, bool>[]>());
                    Action setupTwo = () => { };
                    Karass karassTwo = new Karass(new Action[] {setupTwo}, new Action[0],
                        new List<Func<string, bool>[]>());
                    
                    Action setupThree = () => { };
                    Karass karassThree= new Karass(new Action[] {setupThree}, new Action[0],
                        new List<Func<string, bool>[]>());

                    
                    Action setupFour= () => { };
                    Karass karassFour = new Karass(new Action[] {setupFour}, new Action[0],
                        new List<Func<string, bool>[]>());


                    Karass combinedKarass = karassOne + karassTwo + karassThree + karassFour;

                    Assert.True(combinedKarass.SetupActions.Count() == 4);
                    Assert.True(combinedKarass.SetupActions.Contains(setupOne));
                    Assert.True(combinedKarass.SetupActions.Contains(setupTwo));
                    Assert.True(combinedKarass.SetupActions.Contains(setupThree));
                    Assert.True(combinedKarass.SetupActions.Contains(setupFour));
                    

                }

                // has both lists of funct
                // has list of setups 
                // has list of teardowns

                // KanKan running tests - will do multiple on same frames 
            }
            
            public class GivenMultipleTeardownMethods
            {
                [Fact]
                public void WhenAddedThenMethodsAreInInList_ExampleOne()
                {
                    Action teardownOne = () => { };
                    Karass karassOne = new Karass(new Action[0], new []{teardownOne},
                        new List<Func<string, bool>[]>());
                    Action teardownTwo = () => { };
                    Karass karassTwo = new Karass(new Action[0], new []{teardownTwo},
                        new List<Func<string, bool>[]>());

                    Karass combinedKarass = karassOne + karassTwo;

                    Assert.True(combinedKarass.TeardownActions.Count() == 2);

                }

                [Fact]
                public void WhenAddedThenMethodsAreInInList_ExampleTwo()
                {
                    Action teardownOne = () => { };
                    Karass karassOne = new Karass(new Action[0], new []{teardownOne},
                        new List<Func<string, bool>[]>());
                    Action teardownTwo = () => { };
                    Karass karassTwo = new Karass(new Action[0], new []{teardownTwo},
                        new List<Func<string, bool>[]>());
                    
                    Action teardownThree = () => { };
                    Karass karassThree= new Karass(new Action[0], new []{teardownThree},
                        new List<Func<string, bool>[]>());

                    
                    Action teardownFour= () => { };
                    Karass karassFour = new Karass(new Action[0], new []{teardownFour},
                        new List<Func<string, bool>[]>());


                    Karass combinedKarass = karassOne + karassTwo + karassThree + karassFour;

                        
                    Assert.True(combinedKarass.TeardownActions.Count() == 4);

                }

                // has both lists of funct
                // has list of setups 
                // has list of teardowns

                // KanKan running tests - will do multiple on same frames 
            }

            public class GivenMultipleFrameSets
            {
                [Fact]
                public void WhenAddedThenFrameSetsContainArrays()
                {
                    
                    
                    Func<string, bool>[] frameSetArrayOne = {};
                    
                    List<Func<string, bool>[]> frameSetOne = new List<Func<string, bool>[]>
                    {
                        frameSetArrayOne
                    };
                   

                    Func<string, bool>[] frameSetArrayTwo = {};
                    
                    List<Func<string, bool>[]> frameSetTwo = new List<Func<string, bool>[]>
                    {
                        frameSetArrayTwo
                    };
                    
                    Karass karassOne = new Karass( new Action[0], new Action[0],
                        frameSetOne);
                 
                    Karass karassTwo = new Karass( new Action[0], new Action[0],
                        frameSetTwo);

                    Karass combinedKarass = karassOne + karassTwo;

                    Assert.True(combinedKarass.Frames.Count == 2);
                    Assert.True(combinedKarass.Frames.Contains(frameSetArrayOne));
                    Assert.True(combinedKarass.Frames.Contains(frameSetArrayTwo));
                }
                
                
                public void WhenAddedThenFrameSetsContainFrames()
                {
                    bool FrameSetOneFrameOne(string message)
                    {
                        return true;
                    }
                    bool FrameSetOneFrameTwo(string message){
                    
                        return true;
                    }
                    
                    Func<string, bool>[] frameSetArrayOne = {
                        FrameSetOneFrameOne,
                        FrameSetOneFrameTwo
                    };
                    
                    List<Func<string, bool>[]> frameSetOne = new List<Func<string, bool>[]>
                    {
                        frameSetArrayOne
                    };
                    
                    
                    bool FrameSetTwoFrameOne(string message)
                    {
                        return true;
                    }
                    bool FrameSetTwoFrameTwo(string message){
                    
                        return true;
                    }


                    Func<string, bool>[] frameSetArrayTwo = {
                        FrameSetTwoFrameOne,
                        FrameSetTwoFrameTwo
                    };
                    
                    List<Func<string, bool>[]> frameSetTwo = new List<Func<string, bool>[]>
                    {
                        frameSetArrayTwo
                    };
                    
                    Karass karassOne = new Karass( new Action[0], new Action[0],
                        frameSetOne);
                 
                    Karass karassTwo = new Karass( new Action[0], new Action[0],
                        frameSetTwo);

                    Karass combinedKarass = karassOne + karassTwo;

                    Assert.True(combinedKarass.Frames.Count == 2);
                    Assert.True(combinedKarass.Frames.Contains(frameSetArrayOne));
                    Assert.True(combinedKarass.Frames.Contains(frameSetArrayTwo));
                    
                    Assert.True(combinedKarass.Frames.Any(_=>_.Contains(FrameSetOneFrameOne)));
                    Assert.True(combinedKarass.Frames.Any(_=>_.Contains(FrameSetOneFrameTwo)));
                    Assert.True(combinedKarass.Frames.Any(_=>_.Contains(FrameSetTwoFrameOne)));
                    Assert.True(combinedKarass.Frames.Any(_=>_.Contains(FrameSetTwoFrameTwo)));
                    
                }
                // NEXT : multiple framesets
            }
        }
    }
}
