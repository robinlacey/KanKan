using System;

namespace KanKanCore.Karass.Interface
{
    public interface IKarass
    {
        void Setup();
        void Teardown();
        Func<string,bool>[]  Frames { get;}
    }
}