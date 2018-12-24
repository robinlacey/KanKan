using KanKanTestHelper.Interface;

namespace KanKanTest.KanKanTestHelperTests.Mocks
{
    public class RunKanKanDummy: IRunKanKan
    {
        public IKanKanCurrentState For(int frames)
        {
            return new KanKanCurrentStateDummy();
        }
    }
}