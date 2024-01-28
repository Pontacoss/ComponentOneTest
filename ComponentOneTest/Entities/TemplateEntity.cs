using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Entities
{
    public sealed class TemplateEntity
    {
        public string Clause { get; }
        public string TestItem { get; }
        public string TypeTest { get; }
        public string RoutineTest { get; }
        public string IECclause { get; }

        public TemplateEntity(string testItem, string clause, string typeTest, string routineTest, string iecClause)
        {
            TestItem = testItem;
            Clause = clause;
            TypeTest = typeTest;
            RoutineTest = routineTest;
            IECclause = iecClause;
        }
    }
}

