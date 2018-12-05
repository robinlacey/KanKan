using System;
using System.Collections.Generic;

namespace KanKanTest.ProgressionTests
{
    public class KanKanFrameProgressionTests
    {
        protected static List<List<Action>> CreateActionListWith(Action a) =>
            new List<List<Action>> {new List<Action> {a}};
    }
}