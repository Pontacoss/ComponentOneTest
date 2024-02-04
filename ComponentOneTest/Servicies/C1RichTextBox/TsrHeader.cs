using C1.WPF.RichTextBox.Documents;
using ComponentOneTest.Entities;
using ComponentOneTest.Serviceis.C1RichTextBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public sealed class TsrHeader : HeaderBase
    {
        public TsrHeader(TableHeaderEntity headerEntity):base(headerEntity) { }
    
    }
}
