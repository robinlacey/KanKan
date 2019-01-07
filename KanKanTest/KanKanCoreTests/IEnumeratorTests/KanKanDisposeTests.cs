using System;
using System.Collections.Generic;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanCore.Karass;
using KanKanCore.Karass.Frame;
using KanKanTest.KanKanCoreTests.Mocks.KanKan.Spy;
using KanKanTest.KanKanCoreTests.Mocks.KarassFrame;
using KanKanTest.KanKanCoreTests.Mocks.KarassMocks;
using KanKanTest.KanKanCoreTests.Mocks.KarassState;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.IEnumeratorTests
{
    public class KanKanDisposeTests
    {
        public class GivenKanKanWithCurrentState
        {
            [TestCase(1)]
            [TestCase(21)]
            [TestCase(42)]
            public void OnDisposeKanKanWillRunResetOnAllKarassStates(int numberOfStates)
            {
                KanKanKarassStatesSpy kankanSpy = new KanKanKarassStatesSpy(new KarassDummy(), new FrameFactoryDummy());

                kankanSpy.SetKarassStates(GetKarassStatesSpies(numberOfStates));
                
                kankanSpy.Dispose();

                foreach (IKarassState karassState in kankanSpy.GetKarassStates())
                {
                    KarassStateSpy statesSpy = (KarassStateSpy) karassState;
                   Assert.True(statesSpy.ResetCalledCount == 1);
                }
            }

            List<IKarassState> GetKarassStatesSpies(int total)
            {
                List<IKarassState> returnList = new List<IKarassState>();
                for (int i = 0; i < total; i++)
                {
                    returnList.Add(new KarassStateSpy());
                }

                return returnList;
            }
        }
       
    }
}