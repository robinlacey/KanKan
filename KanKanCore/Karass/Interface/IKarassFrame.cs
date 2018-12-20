namespace KanKanCore.Karass.Interface
{
    public interface IKarassFrame<TRequestType>
    {
        TRequestType RequestData { get; }
        string Message { get; }
        bool Execute(string message, TRequestType payload);
    }
}