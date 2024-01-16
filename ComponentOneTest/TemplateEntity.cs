using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest
{
    public sealed class TemplateEntity
    {
        public string Clause { get; set; }
        public string TestItem { get; set; }
        public string TypeTest { get; set; }
        public string RoutineTest { get; set; }
        public string IECClause {  get; set; }
        public TemplateEntity(string  testItem,string clause,string typeTest, string routineTest,string iecClause)
        {
            TestItem = testItem;
            Clause = clause;
            TypeTest = typeTest;
            RoutineTest = routineTest;
            IECClause = iecClause;
        }
    }
}
