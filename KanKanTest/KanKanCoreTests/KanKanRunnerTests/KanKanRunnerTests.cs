using System.Collections;
using KanKanCore.Interface;
using KanKanCore.KanKan;
using NUnit.Framework;

namespace KanKanTest.KanKanCoreTests.KanKanRunnerTests
{
    public class KanKanRunnerTests
    {
        [Test]
        public void KnaKanRunnerIsIEnumerable()
        {
            IEnumerator kanKanRunner = (IEnumerator) new KanKanRunner();
            Assert.NotNull(kanKanRunner);
        } 
        
        [Test]
        public void KnaKanRunnerCreatesAKarassMessenger()
        {
            IKanKanRunner kanKanRunner =  new KanKanRunner();
            Assert.NotNull( kanKanRunner.KarassMessage );
        } 
        
        // Sets same KarassMessage on all KanKan Added
        
        // Can run X KanKan at same time, Can Chain KanKan Together (KanKan Adding??? )
        // Will send message to all KanKan
        
        // Can Assign Triggers to KanKan/KanKan[] which will stop KanKan and Trigger new KanKan 
        
   
        // KanKanRunner will stop when no CurrentKanKan. Needs KanKan on constructor (KanKan, tag)
        
        // Add(KanKan1, string KanKan1Tag)
        // Run ("KanKanTag") - Adds KanKan To Current (should current be string?)
        // Stop ("KanKanTag") - Removed KanKan from Current
        // Pause ("KanKanTag") - Keeps KanKan at Current but doesnt tick (returns MoveNext true)
        
        // AddTriggerTo(KanKan1Tag, KanKan2Tag, string TriggerName)
        // Trigger ("TriggerName")
            // Stop KanKan1
            // Start KanKan2
    }
}