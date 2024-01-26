using C1.WPF.RichTextBox.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public sealed class HeaderCell : C1TableCell
    {
        public HeaderCell() : base() { }
    }

    public sealed class DataCell : C1TableCell
    {
        public DataCell() : base() { }
    }

    public sealed class TSRTable : C1Table
    {
        private TableContent _content;
        public TSRTable(TableContent tableContent) : base() 
        {
            _content = tableContent;

        }

    }
}
