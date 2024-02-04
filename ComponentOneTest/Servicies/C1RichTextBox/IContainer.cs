using C1.WPF.RichTextBox.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    interface IContainer
    {
        bool IsTitleVisible { get; }
        bool IsMeasurementItem { get; }
        bool IsRepeat { get; }
        public int GetNodesCount();
        NodesCounter GetHeaderWidth(NodesCounter nodesCounter);

        C1TableCell CreateCellHeader(int columnHeaderHeight);
        List<(C1TableCell header, int RowIndex)> CreateRowHedears(int cellHeight,int repeart);
        int GainRepeat(int repeat);

    }
}
