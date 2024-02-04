//using C1.WPF.RichTextBox.Documents;
//using ComponentOneTest.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ComponentOneTest.Servicies.C1RichTextBox
//{
//    public class TsrCriteriaContainer : HeaderBase, IContainer
//    {
//        private TableHeaderEntity _headerEntity;
//        public int Id => _headerEntity.Id;
//        public int Level => _headerEntity.Level;

//        public IList<HeaderBase> Children { get; private set; }

//        public bool IsTitleVisible => _headerEntity.IsTitleVisible;

//        public bool IsMeasurementItem => _headerEntity.IsMeasurementItem;

//        public string? Name => _headerEntity.Name;

//        public bool IsRepeat => _headerEntity.IsRepeat;

//        public TsrCriteriaContainer(TableHeaderEntity headerEntity)
//        {
//            _headerEntity = headerEntity;
//            Children = new List<HeaderBase>()
//                {
//                    new TsrHeader(new TableHeaderEntity(this.GetEntity(), this.Id+1, "基準値")),
//                    new TsrHeader(new TableHeaderEntity(this.GetEntity(), this.Id+2, "公差"))
//                };
//        }

//        public override string ToString()
//        {
//            return Name;
//        }

//        public void Add(HeaderBase tableHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public int GetDepth()
//        {
//            return 2;
//        }

//        public TableHeaderEntity GetEntity()
//        {
//            return _headerEntity;
//        }

//        public int GetNodesCount()
//        {
//            return 2;
//        }

//        public spanCounter GetHeaderWidth(spanCounter spanCounter)
//        {
//            throw new NotImplementedException();
//        }

//        public C1TableCell CreateCellHeader()
//        {
//            throw new NotImplementedException();
//        }

//        public List<(C1TableCell header, int RowIndex)> CreateRowHedears(int cellHeight, int repeart)
//        {
//            throw new NotImplementedException();
//        }

//        public List<(C1TableCell header, int RowIndex)> CreateRowHedear(List<(C1TableCell, int)> list, HeaderBase header, int rowIndex, int cellHeight)
//        {
//            throw new NotImplementedException();
//        }

//        public C1TableCell CreateCellHeader(int columnHeaderHeight)
//        {
//            throw new NotImplementedException();
//        }

//        public List<(C1TableCell header, int RowIndex)> CreateRowHedear(List<(C1TableCell, int)> list, int rowIndex, int cellHeight)
//        {
//            throw new NotImplementedException();
//        }

//        public List<(C1TableCell header, int RowIndex)> CreateRowHedear(List<(C1TableCell, int)> list, int rowIndex, int cellHeight, int maxDepth)
//        {
//            throw new NotImplementedException();
//        }

//        public int GainRepeat(int repeat)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
