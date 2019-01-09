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
            IEnumerator kanKanRunner = new KanKanRunner();
            Assert.NotNull(kanKanRunner);
        } 
        
        [Test]
        public void KanKanRunnerCreatesAKarassMessenger()
        {
            IKanKanRunner kanKanRunner =  new KanKanRunner();
            Assert.NotNull( kanKanRunner.KarassMessage );
        } 
        // NOTES : 
        // #1
        // Sets same KarassMessage on all KanKan Added 
        
        // #2 
        // Can run X KanKan at same time, Can Add KanKan Together (DONE)
            //  Adding KanKan Together :
                // Karass are added eg:
                // K1[0] + K2[0]
                // K1[1] + K2[1]
                // K2[3]
                // K2[4]
                
        // #3
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