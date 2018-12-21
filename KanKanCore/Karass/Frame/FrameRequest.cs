using System;

namespace KanKanCore.Karass.Frame
{
    public class FrameRequest
    {
        public object RequestObject;
        public Type RequestType;

        public FrameRequest(object frameObject)
        {
            RequestType = frameObject.GetType();
            RequestObject = frameObject;
        }
    }
}