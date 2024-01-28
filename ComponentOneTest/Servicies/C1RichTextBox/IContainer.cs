using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    interface IContainer
    {
        IList<ITsrHeader> Children { get; }
        string? Title { get; }
        bool IsVisibleTitle { get; }
        bool IsMeasurementItem { get; }
        int GetEndNodesCount();
        int GetDepth();

    }
}
