using System;
using System.Collections.Generic;
using KanKanCore.Karass;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Struct;
using KanKanTest.KanKanCoreTests.Mocks.KarassMocks;

namespace KanKanTest.KanKanCoreTests.Mocks.KarassState
{
    public class KarassStateWithStubbedData:KanKanCore.Karass.KarassState
    {

        public KarassStateWithStubbedData(int amountOfStubbedData) : base(new Karass(new List<List<Action>>(), new List<List<Action>>(), new List<FrameRequest[]>()))
        {
            for (int i = 0; i < amountOfStubbedData; i++)
            {
                int randomNumber = new Random(i).Next();
                string randomString = Guid.NewGuid().ToString();
                NextFrames.Add(new FrameRequest(randomString));
                LastFrames.Add(new FrameRequest(randomString));
                CurrentFrames.Add(new UniqueKarassFrameRequestID(randomString,randomNumber),i );
                Complete.Add(i%2==0);
            }
        }
    }
}