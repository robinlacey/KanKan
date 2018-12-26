namespace KanKanCore.Karass.Struct
{
    public struct UniqueKarassFrameRequestID
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private string ID { get; }

        public UniqueKarassFrameRequestID(string id, int index)
        {
            ID = id + index;
        }
    }
}