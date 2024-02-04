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
    public abstract class HeaderBase
    {
        protected TableHeaderEntity _headerEntity;

        public bool IsTitleVisible => _headerEntity.IsTitleVisible;
        public bool IsMeasurementItem => false;
        public bool IsRepeat => false;
        public int Id => _headerEntity.Id;
        public int Level => 0;
        public string? Name => _headerEntity.Name;

        public HeaderBase(TableHeaderEntity headerEntity)
        {
            _headerEntity = headerEntity;
        }

        public override string ToString()
        {
            return Name ?? "";
        }

        public IList<HeaderBase> Children { get; } = new List<HeaderBase>();
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
        public int GetNodesCount()
        {
            if (Children.Count == 0)
            {
                return 1;
            }

            int counter = 0;
            foreach (var item in Children)
            {
                counter += item.GetNodesCount();
            }
            return counter;
        }
        
        public TableHeaderEntity GetEntity()
        {
            return _headerEntity;
        }
        public List<(C1TableCell header, int RowIndex)> CreateRowHedear(
            List<(C1TableCell, int)> list,
            int rowIndex,
            int cellHeight,
            int maxDepth)
        {
            var columnSpan = Children.Count > 0 ? 1 : maxDepth - Level + 1;
            list.Add((RichTextBoxTools.CreateRowHeaderCell(
                    Name,
                    GetNodesCount() * cellHeight,
                    columnSpan
                    ), rowIndex));

            foreach (var cell in Children)
            {
                cell.CreateRowHedear(list, rowIndex, cellHeight, maxDepth);
                rowIndex += cell.GetNodesCount() * cellHeight;
            }
            return list;
        }
    }
}
