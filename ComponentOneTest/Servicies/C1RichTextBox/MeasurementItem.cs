using ComponentOneTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public class MeasurementItem : ITableHeader
    {
        private TableHeaderEntity _headerEntity;
        public int Id => _headerEntity.Id;
        public int Level => _headerEntity.Level;

        public IList<ITableHeader> Children {
            get
            {
                return new List<ITableHeader>()
                {
                    new Header(_headerEntity),
                    new Header(_headerEntity),
                    new Header(_headerEntity)
                };
            } }

        public MeasurementItem(TableHeaderEntity headerEntity)
        {
            _headerEntity = headerEntity;
        }

        public void Add(ITableHeader tableHeader)
        {
            throw new NotImplementedException();
        }

        public int GetDepth()
        {
            throw new NotImplementedException();
        }

        public TableHeaderEntity GetEntity()
        {
            throw new NotImplementedException();
        }

        public int GetWidth()
        {
            throw new NotImplementedException();
        }
    }
}
