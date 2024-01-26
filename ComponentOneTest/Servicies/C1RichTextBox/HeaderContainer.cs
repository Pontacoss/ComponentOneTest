﻿using ComponentOneTest.Entities;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    
    public sealed class HeaderContainer : ITableHeader
    {
        public IList<ITableHeader> Children { get; } = new List<ITableHeader>();
        private TableHeaderEntity _headerEntity;

        public string? Title => _headerEntity.Title;
        public int Id => _headerEntity.Id;
        public int Level => 0;
        public bool IsVisibleTitle => _headerEntity.IsTitleVisible;
        public bool IsMeasurementItem => _headerEntity.IsMeasurementItem;

        public HeaderContainer(TableHeaderEntity headerEntity)
        {
            _headerEntity = headerEntity;
        }

        public void Add(ITableHeader tableHeader)
        {
            Children.Add(tableHeader);
        }

        public override string ToString()
        {
            return Title;
        }

        public int GetDepth()
        {
            int depth = 0;
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