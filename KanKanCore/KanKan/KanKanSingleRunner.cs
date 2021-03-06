using System;
using KanKanCore.Exception;
using KanKanCore.Interface;
namespace KanKanCore.KanKan
{
    public class KanKanSingleRunner:KanKanRunner<IKanKan>
    {
        public override bool MoveNext() => Paused || Current.MoveNext();

        public override void Reset()
        {
            foreach (IKanKan kanKan in KanKans.Values)
            {
                kanKan.Reset();
            }

            Paused = false;
            Current = ConstructorKanKan;
        }
         
        public override void Run(string tag)
        {
            KanKans.TryGetValue(tag, out IKanKan kanKan);
            if (kanKan != null)
            {
                Current.Reset();
                Current = kanKan;
            }
            else
            {
                throw new NoKanKanWithTag(tag);
            }
        }
        
        public override void Add(IKanKan kankan, string tag)
        {
            kankan.SetKarassMessage(KarassMessage);
            base.Add(kankan,tag);
        }
        
        public override void SetKarassMessage(IKarassMessage message)
        {
            Current.SetKarassMessage(message);
            base.SetKarassMessage(message);
        }


        public KanKanSingleRunner(IKanKan kankan, string tag) : base(kankan, tag)
        {
            Current = kankan;
            Current.SetKarassMessage(KarassMessage);
        }
    }
}