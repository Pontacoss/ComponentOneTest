//using ComponentOneTest.Entities;

//namespace ComponentOneTest.Servicies.C1RichTextBox
//{
    
//    public sealed class TsrHeaderContainer : ITsrHeader, IContainer
//    {
//        public IList<ITsrHeader> Children { get; } = new List<ITsrHeader>();
//        private TableHeaderEntity _headerEntity;

//        public string? Title => _headerEntity.Title;
//        public int Id => _headerEntity.Id;
//        public int Level => 0;
//        public bool IsTitleVisible => _headerEntity.IsTitleVisible;
//        public bool IsMeasurementItem => _headerEntity.IsMeasurementItem;

//        public TsrHeaderContainer(TableHeaderEntity headerEntity)
//        {
//            _headerEntity = headerEntity;
//        }

//        public void Add(ITsrHeader tableHeader)
//        {
//            Children.Add(tableHeader);
//        }

//        public override string ToString()
//        {
//            return Title;
//        }

//        public int GetDepth()
//        {
//            int depth = 0;
//            foreach (ITsrHeader child in Children)
//            {
//                depth = Math.Max(depth, child.GetDepth());
//            }
//            return depth;
//        }
//        public int GetNodesCount()
//        {
//            int width = 0;
//            foreach (ITsrHeader child in Children)
//            {
//                width += child.GetNodesCount();
//            }
//            return Math.Max(1, width);
//        }
//        public TableHeaderEntity GetEntity()
//        {
//            return _headerEntity;
//        }
//    }

    
//}
