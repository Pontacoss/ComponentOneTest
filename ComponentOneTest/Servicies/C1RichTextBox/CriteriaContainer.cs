using C1.WPF.RichTextBox.Documents;
using ComponentOneTest.Entities;
using ComponentOneTest.Serviceis.C1RichTextBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
class CriteriaContainer : HeaderBase, IContainer
    {
        private int _repeat;
        private int _unitSize;
        public CriteriaContainer(TableHeaderEntity headerEntity)
            : base(headerEntity) { }

        public SpanCounter GetHeaderWidth(SpanCounter spanCounter)
        {
            var counter = new SpanCounter();
            counter.BlockSpan = spanCounter.BlockSpan;
            counter.RepeatSpan = spanCounter.RepeatSpan * GetSpanSum();
            return counter;
        }
        public int SetUnitSize(SpanCounter spanCounter, int repaetHeaderUnitSize)
        {
            _unitSize = 1;// repaetHeaderUnitSize / GetSpanSum();
            return _unitSize;
        }

        public int SetRepeat(int repeat)
        {
            _repeat = repeat;
            return repeat* GetSpanSum();
        }

        public C1TableCell CreateCellHeader(int columnHeaderHeight)
        {
            var depth = GetDepth();
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
        public int CreateColumnHedears(C1TableRowGroup rows, int rowIndex)
        {
            int maxDepth = GetDepth();

            for (int i = 0; i < _repeat; i++)
            {
                foreach (var cell in Children)
                {
                    rowIndex = cell.CreateColumnHedear(
                        rows, rowIndex, _unitSize, maxDepth);
                }
            }
            return 0;
        }

        public int CreateColumnContainerTitles(C1TableRowGroup rows, int rowIndex)
        {
            if (!IsTitleVisible) return 0;

            var row = rows.First(x => x.Index == rowIndex);
            int rowSpan = 1;
            int columnSpan = GetSpanSum() * _unitSize;

            for (int i = 0; i < _repeat; i++)
            {
                row.Children.Add(
                    RichTextBoxTools.CreateColumnHeaderTitleCell(
                        ToString(),
                        rowSpan,
                        columnSpan));
            }
            return 1;
        }
    }
}
