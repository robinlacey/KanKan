namespace KanKanCore.Interface
{
    public interface IKarassFrame<TRequestType>
    {
        IDependencies Dependencies { get; }
        TRequestType RequestData { get; }
        string Message { get; }
        bool MoveNextFrame(string message, TRequestType payload);
    }
}