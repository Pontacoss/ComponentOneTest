using C1.WPF.RichTextBox.Documents;
using ComponentOneTest.Entities;
using ComponentOneTest.Serviceis.C1RichTextBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    class RepeartContainer : HeaderBase,IContainer
    {


        public RepeartContainer(TableHeaderEntity headerEntity):base(headerEntity) { }
 
        public NodesCounter GetHeaderWidth(NodesCounter nodesCounter)
        {
            var counter = new NodesCounter();
            counter.BlockCount = nodesCounter.BlockCount;
            counter.RepeatCount = nodesCounter.RepeatCount*GetNodesCount();
            return counter;
        }

        public C1TableCell CreateCellHeader(int columnHeaderHeight)
        {
            var depth=GetDepth();
            return RichTextBoxTools.CreateColumnHeaderCell(
                Name,
                columnHeaderHeight,
                GetDepth());
        }

        public List<(C1TableCell header, int RowIndex)> CreateRowHedears(int cellHeight,int repeat)
        {
            var list = new List<(C1TableCell, int)>();
            int rowIndex = 0;
            int maxDepth = GetDepth();

            for (int i = 0; i < repeat; i++)
            {
                foreach (var cell in Children)
                {
                    cell.CreateRowHedear(list, rowIndex, 1, maxDepth);
                    rowIndex += cell.GetNodesCount();
                }
            }
            return list;
        }

        public int GainRepeat(int repeat)
        {
            return repeat/GetNodesCount();
        }
    }
}
