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
    public class BlockContainer : HeaderBase, IContainer
    {
        public BlockContainer(TableHeaderEntity headerEntity) 
            : base(headerEntity){ }

        public NodesCounter GetHeaderWidth(NodesCounter nodesCounter)
        {
            var counter = new NodesCounter();
            counter.BlockCount = Math.Max(nodesCounter.BlockCount, GetNodesCount());
            counter.RepeatCount = nodesCounter.RepeatCount;
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

        public List<(C1TableCell header, int RowIndex)> CreateRowHedears(int cellHeight, int repeart)
        {
            var list = new List<(C1TableCell, int)>();
            int rowIndex = 0;
            int maxDepth = GetDepth();

            foreach (var cell in Children)
            {
                cell.CreateRowHedear(list,  rowIndex, cellHeight, maxDepth);
                rowIndex += cell.GetNodesCount()* cellHeight;
            }
            return list;
        }

        public int GainRepeat(int repeat)
        {
            return repeat;
        }
    }
}
