namespace KanKanCore.Karass.Interface
{
    public interface IDependencies
    {
        T Get<T>() where T : class;
    }
}