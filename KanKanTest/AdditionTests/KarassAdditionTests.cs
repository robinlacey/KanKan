using System;
using System.Collections.Generic;
using System.Linq;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;
using KanKanTest.Factories;
using KanKanTest.Mocks.Dependencies;
using KanKanTest.Mocks.KarassFrame;
using KanKanTest.Mocks.KarassFrame.FrameStruct;
using NUnit.Framework;

namespace KanKanTest.AdditionTests
{
    public class KarassAdditionTests
    {
        private static KarassFactory KarassFactory => new KarassFactory();
        private static MockFramesFactory MockFramesFactory => new MockFramesFactory(new FrameFactoryDummy());
        private static List<List<Action>> CreateActionListWith(Action a) =>
            new List<List<Action>> {new List<Action> {a}};

        public class CreatesAUniqueKarass
        {
            [Test]
            public void IDDoesNotMatch()
            {
                IDependencies dependencies = new KarassDependencies();
                IFrameFactory frameFactory = new FrameFactory(dependencies);
                IKarassFactory karassFactory = new KarassFactory();
               
                dependencies.Register<IKarassFrame<FrameStructDummy>>(() => new KarassFrameDummy<FrameStructDummy>(dependencies));

                frameFactory.RegisterRoute<FrameStructDummy, IKarassFrame<FrameStructDummy>>();


                List<Action> setupActions = new List<Action>() {};
                List<Action> tearDownActions = new List<Action>() {};
                FrameRequest[] frames = {
                    new FrameRequest(new FrameStructDummy() {Test = "Hello"}),
                    new FrameRequest(new FrameStructDummy() {Test = "My"}),
                    new FrameRequest(new FrameStructDummy() {Test = "Name"}),
                    new FrameRequest(new FrameStructDummy() {Test = "Is"}),
                    new FrameRequest(new FrameStructDummy() {Test = "Robin"}),
                };

                List<Karass> playList = new List<Karass>();
        
                Karass karassOne = karassFactory.Get(setupActions, tearDownActions, frames);
                Karass karassTwo = karassFactory.Get(setupActions, tearDownActions, frames);

                Karass karassThree = karassOne + karassTwo;
                Assert.True(karassThree.ID != karassOne.ID);
                Assert.True(karassThree.ID != karassTwo.ID);
            }
        }

        public class GivenMultipleSetupMethods
        {
            [Test]
            public void WhenAddedThenMethodsAreInInList_ExampleOne()
            {
                Action setupOne = () => { };
                Karass karassOne = KarassFactory.Get(
                    CreateActionListWith(setupOne),
                    new List<List<Action>>(),
                    new List<FrameRequest[]>());
                Action setupTwo = () => { };
                Karass karassTwo = KarassFactory.Get(
                    CreateActionListWith(setupTwo),
                    new List<List<Action>>(),
                    new List<FrameRequest[]>());

                Karass combinedKarass = karassOne + karassTwo;

                Assert.True(combinedKarass.SetupActions.Count == 2);
                Assert.True(combinedKarass.SetupActions[0].Contains(setupOne));
                Assert.True(combinedKarass.SetupActions[1].Contains(setupTwo));
            }

            [Test]
            public void WhenAddedThenMethodsAreInInList_ExampleTwo()
            {
                Action setupOne = () => { };
                Karass karassOne = KarassFactory.Get(
                    CreateActionListWith(setupOne),
                    new List<List<Action>>(),
                    new List<FrameRequest[]>());
                Action setupTwo = () => { };
                Karass karassTwo = KarassFactory.Get(
                    CreateActionListWith(setupTwo),
                    new List<List<Action>>(),
                    new List<FrameRequest[]>());

                Action setupThree = () => { };
                Karass karassThree =KarassFactory.Get(
                    CreateActionListWith(setupThree),
                    new List<List<Action>>(),
                    new List<FrameRequest[]>());

                Action setupFour = () => { };
                Karass karassFour = KarassFactory.Get(
                    CreateActionListWith(setupFour),
                    new List<List<Action>>(),
                    new List<FrameRequest[]>());


                Karass combinedKarass = karassOne + karassTwo + karassThree + karassFour;

                Assert.True(combinedKarass.SetupActions.Count == 4);
                Assert.True(combinedKarass.SetupActions[0].Contains(setupOne));
                Assert.True(combinedKarass.SetupActions[1].Contains(setupTwo));
                Assert.True(combinedKarass.SetupActions[2].Contains(setupThree));
                Assert.True(combinedKarass.SetupActions[3].Contains(setupFour));
            }
        }

        public class GivenMultipleTeardownMethods
        {
            [Test]
            public void WhenAddedThenMethodsAreInInList_ExampleOne()
            {
                Action teardownOne = () => { };
                Karass karassOne = KarassFactory.Get(
                    new List<List<Action>>(),
                    CreateActionListWith(teardownOne),
                    new List<FrameRequest[]>());
                Action teardownTwo = () => { };
                Karass karassTwo = KarassFactory.Get(
                    new List<List<Action>>(),
                    CreateActionListWith(teardownTwo),
                    new List<FrameRequest[]>());

                Karass combinedKarass = karassOne + karassTwo;

                Assert.True(combinedKarass.TeardownActions.Count == 2);
                Assert.True(combinedKarass.TeardownActions[0].Contains(teardownOne));
                Assert.True(combinedKarass.TeardownActions[1].Contains(teardownTwo));
            }

            [Test]
            public void WhenAddedThenMethodsAreInInList_ExampleTwo()
            {
                Action teardownOne = () => { };
                Karass karassOne = KarassFactory.Get(
                    new List<List<Action>>(),
                    CreateActionListWith(teardownOne),
                    new List<FrameRequest[]>());
                Action teardownTwo = () => { };
                Karass karassTwo = KarassFactory.Get(
                    new List<List<Action>>(),
                    CreateActionListWith(teardownTwo),
                    new List<FrameRequest[]>());

                Action teardownThree = () => { };
                Karass karassThree = KarassFactory.Get(
                    new List<List<Action>>(),
                    CreateActionListWith(teardownThree),
                    new List<FrameRequest[]>());

                Action teardownFour = () => { };
                Karass karassFour = KarassFactory.Get(
                    new List<List<Action>>(),
                    CreateActionListWith(teardownFour),
                    new List<FrameRequest[]>());


                Karass combinedKarass = karassOne + karassTwo + karassThree + karassFour;


                Assert.True(combinedKarass.TeardownActions.Count == 4);
                Assert.True(combinedKarass.TeardownActions[0].Contains(teardownOne));
                Assert.True(combinedKarass.TeardownActions[1].Contains(teardownTwo));
                Assert.True(combinedKarass.TeardownActions[2].Contains(teardownThree));
                Assert.True(combinedKarass.TeardownActions[3].Contains(teardownFour));
            }
        }

        public class GivenMultipleFrameSets
        {
            [Test]
            public void WhenAddedThenFrameSetsContainArrays()
            {
                FrameRequest[] frameSetArrayOne = { };

                List<FrameRequest[]> frameSetOne = new List<FrameRequest[]>
                {
                    frameSetArrayOne
                };


                FrameRequest[] frameSetArrayTwo = { };

                List<FrameRequest[]> frameSetTwo = new List<FrameRequest[]>
                {
                    frameSetArrayTwo
                };

                Karass karassOne = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    frameSetOne);

                Karass karassTwo = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    frameSetTwo);

                Karass combinedKarass = karassOne + karassTwo;

                Assert.True(combinedKarass.FramesCollection.Count == 2);
                Assert.True(combinedKarass.FramesCollection.Contains(frameSetArrayOne));
                Assert.True(combinedKarass.FramesCollection.Contains(frameSetArrayTwo));
            }

            [Test]
            public void WhenAddedThenFrameSetsContainFrames()
            {
                FrameRequest frameSetOneFrameOne = MockFramesFactory.GetInvalidFrameRequest();
                FrameRequest frameSetOneFrameTwo = MockFramesFactory.GetInvalidFrameRequest();
                FrameRequest[] frameSetArrayOne =
                {
                    frameSetOneFrameOne,
                    frameSetOneFrameTwo
                };

                List<FrameRequest[]> frameSetOne = new List<FrameRequest[]>
                {
                    frameSetArrayOne
                };

                FrameRequest frameSetTwoFrameOne = MockFramesFactory.GetInvalidFrameRequest();
                FrameRequest frameSetTwoFrameTwo = MockFramesFactory.GetInvalidFrameRequest();
                FrameRequest[] frameSetArrayTwo =
                {
                    frameSetTwoFrameOne,
                    frameSetTwoFrameTwo
                };

                List<FrameRequest[]> frameSetTwo = new List<FrameRequest[]>
                {
                    frameSetArrayTwo
                };

                Karass karassOne = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    frameSetOne);

                Karass karassTwo = KarassFactory.Get(
                    new List<List<Action>>(),
                    new List<List<Action>>(),
                    frameSetTwo);

                Karass combinedKarass = karassOne + karassTwo;

                Assert.True(combinedKarass.FramesCollection.Count == 2);
                Assert.True(combinedKarass.FramesCollection.Contains(frameSetArrayOne));
                Assert.True(combinedKarass.FramesCollection.Contains(frameSetArrayTwo));

                Assert.True(combinedKarass.FramesCollection.Any(_ => _.Contains(frameSetOneFrameOne)));
                Assert.True(combinedKarass.FramesCollection.Any(_ => _.Contains(frameSetOneFrameTwo)));
                Assert.True(combinedKarass.FramesCollection.Any(_ => _.Contains(frameSetTwoFrameOne)));
                Assert.True(combinedKarass.FramesCollection.Any(_ => _.Contains(frameSetTwoFrameTwo)));
            }
        }

        public class GivenIdenticalKarassToKanKan
        {
            [Test]
            public void ThenDoNotThrowException()
            {
                IDependencies dependencies = new KarassDependencies();
                IFrameFactory frameFactory = new FrameFactory(dependencies);
                IKarassFactory karassFactory = new KarassFactory();
               
                dependencies.Register<IKarassFrame<FrameStructDummy>>(() => new KarassFrameDummy<FrameStructDummy>(dependencies));

                frameFactory.RegisterRoute<FrameStructDummy, IKarassFrame<FrameStructDummy>>();


                List<Action> setupActions = new List<Action>() {};
                List<Action> tearDownActions = new List<Action>() {};
                FrameRequest[] frames = {
                    new FrameRequest(new FrameStructDummy() {Test = "Hello"}),
                    new FrameRequest(new FrameStructDummy() {Test = "My"}),
                    new FrameRequest(new FrameStructDummy() {Test = "Name"}),
                    new FrameRequest(new FrameStructDummy() {Test = "Is"}),
                    new FrameRequest(new FrameStructDummy() {Test = "Robin"}),
                };

                List<Karass> playList = new List<Karass>();
        
                Karass karassOne = karassFactory.Get(setupActions, tearDownActions, frames);
                Karass karassTwo = karassFactory.Get(setupActions, tearDownActions, frames);
                Karass karassThree = karassOne + karassTwo;
                Assert.DoesNotThrow(()=>
                {
                    KanKan kanKan = new KanKan(karassThree,frameFactory);
                });

            }
        }
    }
}