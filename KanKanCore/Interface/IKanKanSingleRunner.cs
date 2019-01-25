namespace KanKanCore.Interface
{
    public interface IKanKanSingleRunner:IKanKanRunner
    {
        IKanKan Get(string tag);
        void Add(IKanKan kanKan, string tag);
    }
}