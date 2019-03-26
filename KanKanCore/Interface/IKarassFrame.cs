namespace KanKanCore.Interface
{
    public interface IKarassFrame<TRequestType>
    {
        IDependencies Dependencies { get; }
        TRequestType RequestData { get; }
        string Message { get; }
        bool MoveToNextFrame(string message, TRequestType payload);
    }
}