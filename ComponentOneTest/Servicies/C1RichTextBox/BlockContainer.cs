using C1.WPF.RichTextBox.Documents;
using ComponentOneTest.Entities;
using ComponentOneTest.Serviceis.C1RichTextBox;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public class BlockContainer : HeaderBase, IContainer
    {
        private int _repeat;
        private int _unitSize;

        public BlockContainer(TableHeaderEntity headerEntity)
            : base(headerEntity) { }

        public SpanCounter GetHeaderWidth(SpanCounter spanCounter)
        {
            var counter = new SpanCounter();
            counter.BlockSpan = Math.Max(spanCounter.BlockSpan, GetSpanSum());
            counter.RepeatSpan = spanCounter.RepeatSpan;
            return counter;
        }
        public int SetUnitSize (SpanCounter spanCounter,int repaetCellHeight)
        {
            _unitSize = spanCounter.RepeatSpan;
            return repaetCellHeight;
        }

        public int SetRepeat( int repeat)
        {
            _repeat = 1;
            return repeat;
        }

        public C1TableCell CreateCellHeader(int columnHeaderHeight)
        {
            return RichTextBoxTools.CreateColumnHeaderCell(
                Name,
                columnHeaderHeight,
                GetDepth());
        }

        public int CreateRowHedears(C1TableRowGroup rows, int columnHeaderHeight)
        {
            int rowIndex = 0;
            int maxDepth = GetDepth();

            for (int i = 0; i < _repeat; i++)
            {
                foreach (var cell in Children)
                {
                    cell.CreateRowHedear(rows, rowIndex, _unitSize, maxDepth, columnHeaderHeight);
                    rowIndex += cell.GetSpanSum() * _unitSize;
                }
            }
            return rowIndex;
        }

        public List<(C1TableCell header, int RowIndex)> CreateColumnHedears()
        {
          throw new NotImplementedException();
        }

        int IContainer.CreateColumnHedears(C1TableRowGroup rows, int rowIndex)
        {
            throw new NotImplementedException();
        }

        public int CreateColumnContainerTitles(C1TableRowGroup rows, int rowIndex)
        {
            throw new NotImplementedException();
        }
    }
}
