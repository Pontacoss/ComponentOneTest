using C1.WPF.RichTextBox.Documents;
using ComponentOneTest.Entities;
using ComponentOneTest.Serviceis.C1RichTextBox;
using ComponentOneTest.ViewModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public sealed class TsrTable : C1Table
    {
        private List<TableHeaderEntity> _headerList =
            new List<TableHeaderEntity>();
        private List<TableHeaderEntity> _criteriaList =
            new List<TableHeaderEntity>();
        private bool _documentType = false;
        private bool _criteriaPosition = false;

        public TsrTable() : base()
        {
            BorderCollapse = true;
            Margin = new Thickness(5);
        }

        public TsrTable(
            List<TableHeaderEntity> headerList,
            List<TableHeaderEntity> criteriaList,
            bool? documentType,
            bool? criteriaPosition = false) : base()
        {
            _headerList = headerList;
            _criteriaList = criteriaList;
            _documentType = documentType == null ? false : (bool)documentType;
            _criteriaPosition = criteriaPosition == null ? false : (bool)criteriaPosition;

            BorderCollapse = true;
            Margin = new Thickness(5);
        }
    }
}
