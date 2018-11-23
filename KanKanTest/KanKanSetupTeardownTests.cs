using KanKanCore;
using KanKanTest.Mocks.UAction;
using Xunit;

namespace KanKanTest
{
    public class KanKanSetupTeardownTests
    {
        [Fact]
        void SetupIsRunOnMoveNext()
        {
            KarassSetupTeardownSpy karassSetupTeardownSpy = new KarassSetupTeardownSpy();
            KanKan actionRunner = new KanKan(karassSetupTeardownSpy, new KarassMessageDummy());
            actionRunner.MoveNext();
            Assert.True(karassSetupTeardownSpy.SetupCounter > 0 );
        }
        
        [Fact]
        void SetupIsRunOnFirstMoveNextOnly()
        {
            KarassSetupTeardownSpy karassSetupTeardownSpy = new KarassSetupTeardownSpy(2);
            KanKan actionRunner = new KanKan(karassSetupTeardownSpy,new KarassMessageDummy());
            actionRunner.MoveNext();
            Assert.True(karassSetupTeardownSpy.SetupCounter == 1);
            actionRunner.MoveNext();
            Assert.True(karassSetupTeardownSpy.SetupCounter == 1);
        }

        [Fact]
        void GivenNoFramesSetupAndTearDownAreRun()
        {
            KarassSetupTeardownSpy karassSetupTeardownSpy = new KarassSetupTeardownSpy();
            KanKan actionRunner = new KanKan(karassSetupTeardownSpy,new KarassMessageDummy());
            actionRunner.MoveNext();
            Assert.True(karassSetupTeardownSpy.SetupCounter == 1);
            Assert.True(karassSetupTeardownSpy.TeardownCounter == 1);
        }
        
        [Fact]
        void GivenOneFrameSetupIsRunAndTearDownIsNot()
        {
            KarassSetupTeardownSpy karassSetupTeardownSpy = new KarassSetupTeardownSpy(1);
            KanKan actionRunner = new KanKan(karassSetupTeardownSpy,new KarassMessageDummy());
            actionRunner.MoveNext();
            Assert.True(karassSetupTeardownSpy.SetupCounter == 1);
            Assert.True(karassSetupTeardownSpy.TeardownCounter == 0);
        }
    }
}