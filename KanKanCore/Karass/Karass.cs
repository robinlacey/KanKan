using System;
using KanKanCore.Karass.Interface;

// Karass: team of individuals who do God's will without ever discovering what they are doing; every person belongs to one

namespace KanKanCore.Karass
{
    public abstract class Karass<T> : IKarass
    {
        protected Karass(T param){}

        public abstract void Setup();

        public abstract void Teardown();
        
        public abstract Func<string,bool>[] Frames { get; }
    }
}