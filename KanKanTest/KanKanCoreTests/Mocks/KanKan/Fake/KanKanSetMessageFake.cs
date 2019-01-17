using KanKanCore.Interface;
using KanKanCore.Karass.Message;

namespace KanKanTest.KanKanCoreTests.Mocks.KanKan.Fake
{
    public class KanKanSetMessageFake:KanKanCore.KanKan.KanKan
    {
        private readonly int _onFrame;
        private readonly string _message;

        public KanKanSetMessageFake(IKarass karass, IFrameFactory frameFactory, int onFrame, string message) : base(karass, frameFactory)
        {
            _onFrame = onFrame;
            _message = message;
        }

        public override bool MoveNext()
        {
            if (Current.TotalFramesRun == _onFrame)
            {
               KarassMessage.SetMessage(_message);
            }
            return base.MoveNext();
        }
    }
}