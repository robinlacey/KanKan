using KanKanCore.Karass.Frame;

namespace KanKanCore.Karass.Struct
{
    public struct UniqueKarassFrameRequestID
    {
        public readonly string ID;
        public UniqueKarassFrameRequestID(string id, int index, FrameRequest[] frameRequests)
        {
            ID = id + index;
        }
    }
}