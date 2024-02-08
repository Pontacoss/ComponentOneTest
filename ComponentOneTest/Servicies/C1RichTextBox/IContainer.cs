using C1.WPF.RichTextBox.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    interface IContainer
    {
        bool IsTitleVisible { get; }
        bool IsMeasurementItem { get; }
        bool IsRepeat { get; }

        public int GetSpanSum();
        SpanCounter GetHeaderWidth(SpanCounter spanCounter);
        /// <summary>
        /// 1セルあたりのサイズを設定
        /// </summary>
        /// <param name="spanCounter"></param>
        /// <param name="repaetCellHeight"></param>
        /// <returns></returns>
        public int SetUnitSize(SpanCounter spanCounter, int repaetCellHeight);
        public int SetRepeat(int repeat);

        C1TableCell CreateCellHeader(int columnHeaderHeight);
        int CreateRowHeaders(C1TableRowGroup rows,int columnHeaderHeight);
        int CreateColumnHeaders(C1TableRowGroup rows, int rowIndex);
        int CreateColumnContainerTitles(C1TableRowGroup rows, int rowIndex);
        string GetConditionString(int Index);

    }
}
