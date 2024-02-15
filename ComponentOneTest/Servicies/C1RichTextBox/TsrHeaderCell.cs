using C1.WPF.RichTextBox.Documents;
using ComponentOneTest.Servicies.TableData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public sealed class TsrHeaderCell : C1TableCell
    {
        private CellEntity _cellEntity;
        public int RowIndex => _cellEntity.RowIndex;
        public int ColumnIndex => _cellEntity.ColumnIndex;

        public TsrHeaderCell() : base() { }

        public TsrHeaderCell(CellEntity cellEntity) : base() 
        {
            _cellEntity = cellEntity;
            VerticalAlignment = C1VerticalAlignment.Middle;
            RowSpan = _cellEntity.RowSpan;
            ColumnSpan = _cellEntity.ColumnSpan;
        }
    }
}
