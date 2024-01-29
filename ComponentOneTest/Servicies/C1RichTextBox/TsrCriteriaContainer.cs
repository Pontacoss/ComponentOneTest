using ComponentOneTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public class TsrCriteriaContainer : ITsrHeader, IContainer
    {
        private TableHeaderEntity _headerEntity;
        public int Id => _headerEntity.Id;
        public int Level => _headerEntity.Level;
        public string Title => _headerEntity.Title;

        public IList<ITsrHeader> Children { get; private set; }= new List<ITsrHeader>();

        public bool IsVisibleTitle => _headerEntity.IsTitleVisible;

        public bool IsMeasurementItem => _headerEntity.IsMeasurementItem;

        public TsrCriteriaContainer(TableHeaderEntity headerEntity)
        {
            _headerEntity = headerEntity;
            //Children = new List<ITsrHeader>()
            //    {
            //        new TsrHeader(new TableHeaderEntity(this.GetEntity(), this.Id+1, "基準値")),
            //        new TsrHeader(new TableHeaderEntity(this.GetEntity(), this.Id+2, "公差"))
            //    };
        }

        public override string ToString()
        {
            return Title;
        }

        public void Add(ITsrHeader tableHeader)
        {
            Children.Add(tableHeader);
        }

        public int GetDepth()
        {
            return 2;
        }

        public TableHeaderEntity GetEntity()
        {
            return _headerEntity;
        }

        public int GetEndNodesCount()
        {
            int width = 0;
            foreach (ITsrHeader child in Children)
            {
                width += child.GetEndNodesCount();
            }
            return Math.Max(1, width);
        }
    }
}
