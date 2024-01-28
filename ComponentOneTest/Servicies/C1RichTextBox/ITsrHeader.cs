using ComponentOneTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public interface ITsrHeader
    {
        int Id { get; }
        int Level { get; }

        IList<ITsrHeader> Children { get; }
        void Add(ITsrHeader tableHeader);
        public int GetDepth();
        public int GetEndNodesCount();
        public TableHeaderEntity GetEntity();
    }
}
