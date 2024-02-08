using C1.WPF.RichTextBox.Documents;
using ComponentOneTest.Entities;
using ComponentOneTest.Serviceis.C1RichTextBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public abstract class HeaderBase
    {
        protected TableHeaderEntity _headerEntity;

        public bool IsColumn
        {
            get => _headerEntity.IsColumn;
            set
            {
                _headerEntity.IsColumn = value;
            }
        }
        public bool IsTitleVisible
        {
            get => _headerEntity.IsTitleVisible;
            set
            {
                _headerEntity.IsTitleVisible = value;
            }
        }
        public bool IsMeasurementItem
        {
            get => _headerEntity.IsMeasurementItem;
            set
            {
                _headerEntity.IsMeasurementItem = value;
            }
        }
        public bool IsRepeat
        {
            get => _headerEntity.IsRepeat;
            set
            {
                _headerEntity.IsRepeat = value;
            }
        }
        public int Id => _headerEntity.Id;
        public int Level => _headerEntity.Level;
        public string? Name => _headerEntity.Name;
        public int Span
        {
            get => _headerEntity.Span;
            set
            {
                _headerEntity.Span = value;
            }
        }
        public IList<HeaderBase> Children { get; }
            = new List<HeaderBase>();

        public HeaderBase(TableHeaderEntity headerEntity)
        {
            _headerEntity = headerEntity;
        }

        public override string ToString()
        {
            return Name ?? "";
        }

        public abstract string DisplayName();

        public void Add(HeaderBase tableHeader)
        {
            Children.Add(tableHeader);
        }
        public int GetDepth()
        {
            int depth = Level;
            foreach (var item in Children)
            {
                depth = Math.Max(depth, item.GetDepth());
            }
            return depth;
        }
        public int GetSpanSum()
        {
            if (Children.Count == 0)
            {
                return Span;
            }
            int counter = 0;
            foreach (var item in Children)
            {
                counter += item.GetSpanSum();
            }
            return counter;
        }

        public TableHeaderEntity GetEntity()
        {
            return _headerEntity;
        }

        public int CreateRowHeader(
            C1TableRowGroup rows,
            int rowIndex,
            int unitSize,
            int maxDepth,
            int columnHeaderHeight)
        {
            var rowSpan = GetSpanSum() * unitSize;
            var columnSpan = Children.Count > 0 ? 1 : maxDepth - Level + 1;

            var row = rows.First(
                       x => x.Index == rowIndex + columnHeaderHeight);

            row.Children.Add(RichTextBoxTools.CreateRowHeaderCell(
                    Name,
                    rowSpan,
                    columnSpan));

            var rowIndexSub = rowIndex;
            foreach (var cell in Children)
            {
                rowIndexSub += cell.CreateRowHeader(
                    rows, rowIndexSub, unitSize, maxDepth, columnHeaderHeight);
            }
            return rowSpan;
        }
        public int CreateColumnHeader(
            C1TableRowGroup rows,
            int rowIndex,
            int unitSize,
            int maxDepth)
        {
            var columnSpan = GetSpanSum() * unitSize;
            var rowSpan = Children.Count > 0 ? 1 : maxDepth - Level + 1;

            var row = rows.First(x => x.Index == rowIndex);
            row.Children.Add(
                RichTextBoxTools.CreateColumnHeaderCell(
                    Name,
                    rowSpan,
                    columnSpan));
            var rowIndexSub = rowIndex + 1;
            foreach (var cell in Children)
            {
                rowIndexSub = cell.CreateColumnHeader(
                    rows, rowIndexSub, unitSize, maxDepth);
            }
            return rowIndex;
        }
        protected string GetConditionStringRecursive(int Index, int unitSize, int counter = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DisplayName());

            var subCounter = 0;
            foreach (var header in Children)
            {
                var width = header.GetSpanSum() * unitSize;
                counter += width;
                if (counter >= Index)
                {
                    sb.Append(header.GetConditionStringRecursive(Index, unitSize, subCounter));
                    break;
                }
                subCounter += width;
            }
            return sb.ToString();
        }

    }
}
