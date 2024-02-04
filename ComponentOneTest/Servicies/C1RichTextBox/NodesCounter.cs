using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public class NodesCounter
    {
        public int BlockCount { get; set; } = 1;
        public int RepeatCount { get; set; } = 1;
        
        public int GetNodesCount()
        {
            return BlockCount * RepeatCount;
        }
    }
}
