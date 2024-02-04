using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public class SpanCounter
    {
        public int BlockSpan { get; set; } = 1;
        public int RepeatSpan { get; set; } = 1;
        
        public int GetNodesCount()
        {
            return BlockSpan * RepeatSpan;
        }
    }
}
