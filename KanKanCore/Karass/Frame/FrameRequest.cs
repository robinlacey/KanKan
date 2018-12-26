using System;

namespace KanKanCore.Karass.Frame
{
    public class FrameRequest
    {
        public readonly object RequestObject;
        public readonly Type RequestType;

        public FrameRequest(object frameObject)
        {
            RequestType = frameObject.GetType();
            RequestObject = frameObject;
        }
    }
}