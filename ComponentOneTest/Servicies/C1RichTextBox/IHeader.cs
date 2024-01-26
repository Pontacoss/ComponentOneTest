using ComponentOneTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public interface ITableHeader
    {
        int Id { get; }
        int Level { get; }

        IList<ITableHeader> Children { get; }
        void Add(ITableHeader tableHeader);
        public int GetDepth();
        public int GetWidth();
        public TableHeaderEntity GetEntity();
    }
}
