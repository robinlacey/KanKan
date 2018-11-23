namespace KanKanCore.Karass.Interface
{
    public interface IKarassMessage
    {
        void SetMessage(string message);
        void ClearMessage();
        string Message { get; }
    }
}