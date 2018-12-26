namespace KanKanCore.Karass.Struct
{
    public struct UniqueKarassFrameRequestID
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private readonly string _id;

        public UniqueKarassFrameRequestID(string id, int index)
        {
            _id = id + index;
        }
    }
}