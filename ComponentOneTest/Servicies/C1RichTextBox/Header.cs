using ComponentOneTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public sealed class Header : ITableHeader
    {
        public IList<ITableHeader> Children { get; } = new List<ITableHeader>();
        private TableHeaderEntity _headerEntity;

        public int Level => _headerEntity.Level;
        public int Id => _headerEntity.Id;
        public int Parent => _headerEntity.Parent;
        public string Value => _headerEntity.Value;

        public Header(TableHeaderEntity headerEntity)
        {
            _headerEntity = headerEntity;
        }
        public override string ToString()
        {
            return Value;
        }
        public void Add(ITableHeader tableHeader)
        {
            Children.Add(tableHeader);
        }
        public int GetDepth()
        {
            int depth = this.Level;
            foreach (ITableHeader child in Children)
            {
                depth = Math.Max(depth, child.GetDepth());
            }
            return depth;
        }
        public int GetWidth()
        {
            int width = 0;
            foreach (ITableHeader child in Children)
            {
                width += child.GetWidth();
            }
            return Math.Max(1, width);
        }
        public TableHeaderEntity GetEntity()
        {
            return _headerEntity;
        }
    }
}
