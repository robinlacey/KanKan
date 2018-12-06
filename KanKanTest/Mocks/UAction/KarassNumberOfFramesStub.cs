using System;
using System.Collections.Generic;
using KanKanCore.Karass;
using KanKanTest.Mocks.Dependencies;

namespace KanKanTest.Mocks.UAction
{
    public class KarassNumberOfFramesStub:Karass
    {
        private static List<Func<string,bool>[]> GetFakeFrames(int frameCount)
        {
            Func<string, bool>[] frames = new Func<string,bool>[frameCount];
            for (int i = 0; i < frameCount; i++)
            {
                frames[i] = (s) => true;
            }
            return new List<Func<string, bool>[]>(){frames};
        }

        public KarassNumberOfFramesStub(int framesCount) : base(new DependenciesDummy(), new List<List<Action>>(), new List<List<Action>>(),
            GetFakeFrames(framesCount))
        {
           
        }
    }
}