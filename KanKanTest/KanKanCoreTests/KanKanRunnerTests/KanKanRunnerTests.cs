using System.Collections;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanTest.KanKanCoreTests.Mocks.KanKan;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests
{
    public class KanKanRunnerTests
    {
        [Test]
        public void KnaKanRunnerIsIEnumerable()
        {
            IEnumerator kanKanRunner = new KanKanRunner(new KanKanDummy());
            Assert.NotNull(kanKanRunner);
        } 
        
        [Test]
        public void KanKanRunnerCreatesAKarassMessenger()
        {
            IKanKanRunner kanKanRunner =  new KanKanRunner(new KanKanDummy());
            Assert.NotNull( kanKanRunner.KarassMessage );
        }

        [Test]
        public void KanKanRunnerSetsKanKanFromConstructorAsCurrent()
        {
            IKanKan kanKanDummy = new KanKanDummy();
            IKanKanRunner kanKanRunner =  new KanKanRunner(kanKanDummy);
            Assert.AreSame(kanKanRunner.Current, kanKanDummy);
            
        }
        // TODO 
        // #1
        // Create with (KanKan, Tag)
        // #3
        // Get KanKAnWith Tag returns constructor KanKan
        // MoveNext will run constructor KanKan be default
        // Stop KanKan with Tag will cause everything to tart down nicely
        // #2
        // Add(KanKan1, string KanKan1Tag)
          // Adds KanKan but doesnt make any difference to Current or Run (just adds to dictionary)       
        // #4
        // After Run(NewTag)
        // MoveNext will run KanKan NewTag on NEXT FRAME (will run Stop on OldKanKan, then Run)
        // #5
        // Will send message to all KanKan
        // 
        // Can Assign Triggers to KanKan/KanKan[] which will stop KanKan and Trigger new KanKan 
       
        // KanKanRunner will stop when no CurrentKanKan. Needs KanKan on constructor (KanKan, tag)
        
      
        // Run ("KanKanTag") - Adds KanKan To Current (should current be string?)
        // Stop ("KanKanTag") - Removed KanKan from Current
        // Pause ("KanKanTag") - Keeps KanKan at Current but doesnt tick (returns MoveNext true)
        
        // AddTriggerTo(KanKan1Tag, KanKan2Tag, string TriggerName)
        // Trigger ("TriggerName")
        // Stop KanKan1
        // Start KanKan2
    }
}