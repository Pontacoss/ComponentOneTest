using C1.WPF.RichTextBox.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public sealed class TsrDataCell : C1TableCell
    {
        public string? Conditions { get; }
        public TsrDataCell() : base() { }
        public TsrDataCell(string? condition) : base() 
        {
            Conditions = condition;
        }
    }
}
