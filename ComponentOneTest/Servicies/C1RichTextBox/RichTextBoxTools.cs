using C1.WPF.RichTextBox.Documents;
using System.Windows;
using ComponentOneTest.Entities;
using System.Windows.Media;
using ComponentOneTest.Servicies.C1RichTextBox;
using System.Windows.Documents;

namespace ComponentOneTest.Serviceis.C1RichTextBox
{
    public static class RichTextBoxTools
    {
        internal static C1TableCell CreateCell(string? name,C1TableCell cell)
        {
            cell.BorderThickness = new Thickness(1);
            var paragraph = new C1Paragraph();
            paragraph.Children.Add(
                new C1Run()
                {
                    Text = name,
                    Padding = new Thickness(0, 0, 0, 0)
                });
            paragraph.Padding = new Thickness(0);
            paragraph.Margin = new Thickness(5);
            cell.Children.Add(paragraph);
            cell.Padding = new Thickness(0);
            return cell;
        }
        private static C1TableCell CreateDataCell(string? name)
        {
            var cell = CreateCell(name, new TsrDataCell());
            return cell;
        }

        internal static C1TableCell CreateColumnHeaderCell(
            string? name,
            int rowSpan,
            int columnSpan)
        {

            var cell = CreateCell(name, new TsrHeaderCell());

            cell.RowSpan = rowSpan;
            cell.ColumnSpan = columnSpan;
            cell.Background = Brushes.LightGray;
            cell.TextAlignment = C1TextAlignment.Center;
            cell.VerticalAlignment = C1VerticalAlignment.Middle;

            return cell;
        }
        internal static C1TableCell CreateColumnHeaderTitleCell(
            string? name,
            int rowSpan,
            int columnSpan)
        {

            var cell = CreateCell(name, new TsrHeaderCell());

            cell.RowSpan = rowSpan;
            cell.FontWeight = FontWeights.Bold;
            cell.ColumnSpan = columnSpan;
            cell.Background = Brushes.LightGray;
            cell.TextAlignment = C1TextAlignment.Center;
            cell.VerticalAlignment = C1VerticalAlignment.Middle;

            return cell;
        }

        internal static C1TableCell CreateRowHeaderCell(
        string? name,
        int rowSpan,
        int columnSpan)
        {
            
            var cell = CreateCell(name, new TsrHeaderCell());

            cell.RowSpan = rowSpan;
            cell.ColumnSpan = columnSpan;
            cell.TextAlignment = C1TextAlignment.Left;
            cell.VerticalAlignment = C1VerticalAlignment.Middle;

            char[] chars = name.ToCharArray();
            if (chars.Any(x => char.IsDigit(x) == false))
            {
                cell.TextAlignment = C1TextAlignment.Left;
            }
            else
            {
                cell.TextAlignment = C1TextAlignment.Right;
            }

            return cell;

            //var comboList = new List<string>()
            //{
            //    "±","+","-"
            //};

            //var combo= new ComboBox();
            //combo.ItemsSource=comboList;

            //var container = new C1InlineUIContainer()
            //{
            //    Content= combo
            //};
            //paragraph.Children.Add((container));


        }

        //public static C1Table CreateTable<T>(IList<T> list) where T : class
        //{
        //    var table = new C1Table();
        //    var properties = typeof(T).GetProperties();

        //    //var columnNamesProperty = typeof(T).GetProperty("ColumnNames");
        //    //var columnNames =  columnNamesProperty?.GetValue(list[0]) as List<string>;

        //    // カラムの作成
        //    for (int i = 0; i < properties.Count(); i++)
        //    {
        //        table.Columns.Add(new C1TableColumn());
        //    }
        //    //if (columnNames == null) return null;
        //    //foreach (var col in columnNames)
        //    //{
        //    //    table.Columns.Add(new C1TableColumn());
        //    //}

        //    // タイトル行の生成
        //    var titleRow = new C1TableRow();
        //    foreach (var property in properties)
        //    {
        //        var cell = CreateCell(property.Name);
        //        cell.TextAlignment = C1TextAlignment.Center;
        //        cell.Background = new SolidColorBrush(Colors.LightGray);
        //        titleRow.Children.Add(cell);
        //    }
        //    table.Children.Add(titleRow);

        //    //foreach(var col in columnNames)
        //    //{
        //    //    var cell = CreateCell(col);
        //    //    cell.TextAlignment = C1TextAlignment.Center;
        //    //    cell.Background = new SolidColorBrush(Colors.LightGray);
        //    //    titleRow.Children.Add(cell);
        //    //}
        //    //table.Children.Add(titleRow);

        //    // データ行の生成
        //    foreach (var entity in list)
        //    {
        //        var valueRow = new C1TableRow();
        //        foreach (var prop in properties)
        //        {
        //            var valueObj = prop.GetValue(entity);
        //            var value = valueObj?.ToString();
        //            valueRow.Children.Add(CreateCell(value));
        //        }
        //        table.Children.Add(valueRow);
        //    }

        //    // テーブル設定
        //    table.BorderCollapse = true;
        //    table.TableAlignment = C1TextAlignment.Center;
        //    table.Padding = new Thickness(0);

        //    return table;
        //}

        public static IList<HeaderBase> GetItemSource(List<TableHeaderEntity> list)
        {
            var source = new List<HeaderBase>();

            foreach (var entity in list)
            {
                var parent = GetParent(source, entity.Parent);
                if (parent != null)
                {
                    if (entity.IsMeasurementItem)
                    {
                        parent.Add(new CriteriaContainer(entity));
                    }
                    else
                    {
                        parent.Add(new Header(entity));
                    }
                }
                else
                {
                    if (entity.IsMeasurementItem)
                    {
                        source.Add(new CriteriaContainer(entity));
                    }
                    else if (entity.IsRepeat)
                    {
                        source.Add(new RepeartContainer(entity));
                    }
                    else
                    {
                        source.Add(new BlockContainer(entity));
                    }
                }
            }
            return source;
        }

        public static HeaderBase? GetParent(IList<HeaderBase> list, int parentId)
        {
            foreach (var entity in list)
            {
                if (entity.Id == parentId) return entity;

                var target = GetParent(entity.Children, parentId);

                if (target != null) return target;
            }
            return null;
        }

        //private static int[] GetNodesCountArray(IEnumerable<HeaderBase> items)
        //{
        //    var widths = new int[items.Count()];
        //    var i = 0;
        //    foreach (var item in items)
        //    {
        //        widths[i] = item.GetSpanSum();
        //        i++;
        //    }
        //    return widths;
        //}

        private static void CreateCellHeaderArea(
            TableContent tableContent,
            C1TableRowGroup rows,
            int columnHeaderHeight)
        {
            if (tableContent.RowHeaders == null) return;
            var row = rows.First(x => x.Index == 0);
            foreach (var container in tableContent.RowHeaders.OfType<IContainer>())
            {
                row.Children.Add(container.CreateCellHeader(columnHeaderHeight));
            }

            //if (tableContent.RowHeaders == null) return;
            //foreach (var header in tableContent.RowHeaders)
            //{
            //    var row = c1Table.RowGroups[0].Children.First(x => x.Index == 0);

            //    int rowSpan = columnHeaderHeight;
            //    int columnSpan = header.GetDepth();
            //    var cell = CreateColumnHeaderCell(
            //            header.ToString(),
            //            rowSpan,
            //            columnSpan);

                
            //    row.Children.Add(cell);
            //}
        }

        
        

        private static void CreateRowHeaderArea(
            TableContent tableContent,
            C1TableRowGroup rows,
            SpanCounter spanCounter,
            int columnHeaderHeight)
        {
            if (tableContent.RowHeaders == null) return;

            // 各ContainerのUnitSizeとRepeat回数の設定
            int repeat = spanCounter.BlockSpan;
            int repaetHeaderUnitSize = spanCounter.RepeatSpan;
            foreach (var container in tableContent.RowHeaders.OfType<IContainer>())
            {
                repaetHeaderUnitSize =
                    container.SetUnitSize(
                        spanCounter,
                        repaetHeaderUnitSize);
                repeat = container.SetRepeat(repeat);
            }

            // Container毎にRowHeaderを作成
            foreach (var container in tableContent.RowHeaders.OfType<IContainer>())
            {
                container.CreateRowHedears(rows, columnHeaderHeight);
                //foreach (var item in list)
                //{
                //    var row = table.RowGroups[0].Rows.First(
                //        x => x.Index == item.RowIndex + columnHeaderHeight);
                //    row.Children.Add(item.header);
                //}
            }
        }

        
        private static void CreateDataCellArea(
           C1TableRowGroup rows,
            int rowHeaderWidth,
            int columnHeaderHeight,
            int columnHeaderWidth)
        {
            for (int i = 0; i < rowHeaderWidth; i++)
            {
                var row = rows.First(
                    x => x.Index == i + columnHeaderHeight);
                for (int j = 0; j < columnHeaderWidth; j++)
                {
                    row.Children.Add(CreateDataCell(" "));
                }
            }
        }

        //private static int CreateRowHeader(C1Table table, int pointer,
        //    HeaderBase header,
        //    int maxDepth,
        //    int widthRate)
        //{
        //    int pointerSub = pointer;
        //    int rowSpan = header.GetSpanSum() * widthRate;
        //    int columnSpan = maxDepth - header.Level - header.GetDepth() + 2;
        //    var row = table.RowGroups[0].Children.First(x => x.Index == pointerSub);
        //    var cell = CreateRowHeaderCell(
        //                header.ToString(),
        //                rowSpan,
        //                columnSpan);

        //    row.Children.Add(cell);

        //    foreach (var child in header.Children)
        //    {
        //        pointerSub += CreateRowHeader(table, pointerSub, child, maxDepth, widthRate);
        //    }
        //    return rowSpan;
        //}

        private static void CreateColumnHeader(
            C1Table table,
            HeaderBase header,
            int rowPointer,
            int maxDepth,
            int widthRate)
        {
            int columnSpan = header.GetSpanSum() * widthRate;

            var row = table.RowGroups[0].Children.First(x => x.Index == rowPointer);
            row.Children.Add(CreateColumnHeaderCell(
                        header.ToString(),
                        maxDepth - header.Level - header.GetDepth() + 2,
                        columnSpan));

            rowPointer++;
            foreach (var child in header.Children)
            {
                CreateColumnHeader(table, child, rowPointer, maxDepth, widthRate);
            }
        }

        private static void CreateColumnHeaderArea(
            TableContent tableContent,
            C1TableRowGroup rows,
            SpanCounter spanCounter,
            int columnHeaderHeight)
        {
            int repeat = 1;
            int repaetHeaderUnitSize = spanCounter.RepeatSpan;
            foreach (var container in tableContent.ColumnHeaders.OfType<IContainer>())
            {
                repaetHeaderUnitSize =
                    container.SetUnitSize(
                        spanCounter,
                        repaetHeaderUnitSize);
                repeat = container.SetRepeat(repeat);
            }

            var rowIndex = 0;
            foreach (var container in tableContent.ColumnHeaders.OfType<IContainer>())
            {
                rowIndex += container.CreateColumnContainerTitles(rows, rowIndex);
                rowIndex += container.CreateColumnHedears(rows, rowIndex);

                //foreach (var item in list)
                //{
                //    var row = table.RowGroups[0].Rows.First(
                //        x => x.Index == item.RowIndex + columnHeaderHeight);
                //    row.Children.Add(item.header);
                //}
            }
            // todo
            //IEnumerable<HeaderBase> tsrHeaderContainers=
            //    tableContent.ColumnHeaders.OfType<HeaderBase>();

            ////IEnumerable<HeaderBase> tsrCriteriaContainers =
            ////    tableContent.ColumnHeaders.OfType<TsrCriteriaContainer>();


            //var rowPointer = 0;
            //var endNodesCountArray =
            //    GetNodesCountArray(tsrHeaderContainers);

            //var widthRate =  endNodesCountArray.Aggregate((x, y) => x * y);

            //var counter = 0;
            ////var criteriaCount = 0;
            ////foreach (var container in tsrHeaderContainers)
            ////{
            ////    if (container is TsrCriteriaContainer)
            ////    {
            ////        criteriaCount += endNodesCountArray[counter];
            ////    }
            ////    else
            ////    {
            ////        widthRate *= endNodesCountArray[counter];
            ////    }
            ////}
            ////widthRate *= criteriaCount;

            //var repeat = 1;
            //counter = 0;
            ////foreach (var container in
            ////    tableContent.ColumnHeaders.OfType<IContainer>())
            ////{
            ////    widthRate /= endNodesCountArray[counter];
            ////    var rate = container.IsMeasurementItem ? 1 : widthRate;

            ////    var subPointer = rowPointer;
            ////    // ContainerTitleの作成
            ////    subPointer += CreateColumnContainerTitles(
            ////        container, c1Table, subPointer, rate, repeat);
            ////    // ColumnHeaderの作成
            ////    subPointer += CreateColumnContainer(
            ////        container, c1Table, subPointer, rate, repeat);

            ////    if (container.IsMeasurementItem == false)
            ////    {
            ////        repeat *= endNodesCountArray[counter];
            ////        rowPointer = subPointer;
            ////    }
            ////    counter++;

            ////}
        }

        //private static int CreateColumnContainerTitles(
        //    IContainer container,
        //    C1Table c1Table,
        //    int rowPointer)
        //{
        //    //if (container == null || !(container.IsTitleVisible)) return 0;
        //    //var row = c1Table.RowGroups[0].Children.First(x => x.Index == rowPointer);
        //    //int rowSpan = 1;
        //    //int columnSpan = container.GetNodesCount() * widthRate;

        //    //for (int i = 0; i < repeat; i++)
        //    //{
        //    //    var cell = CreateColumnHeaderCell(
        //    //            container.ToString(),
        //    //            rowSpan,
        //    //            columnSpan);

        //    //    cell.Background = Brushes.LightGray;
        //    //    cell.FontWeight = FontWeights.Bold;
        //    //    cell.TextAlignment = C1TextAlignment.Center;
        //    //    cell.VerticalAlignment = C1VerticalAlignment.Middle;
        //    //    row.Children.Add(cell);
        //    //}
        //    //return 1;
        //}

        private static int CreateColumnContainer(
            HeaderBase container,
            C1Table c1Table,
            int itr,
            int widthRate,
            int repeat)
        {
            int maxDepth = container.GetDepth();

            for (int j = 0; j < repeat; j++)
            {
                foreach (var header in container.Children)
                {
                    CreateColumnHeader(c1Table, header, itr, maxDepth, widthRate);
                }
            }
            return maxDepth;
        }

        public static TsrTable CreateTable(TableContent tableContent)
        {
            //　表全体の行数を算出
            var rowSpanCounter = new SpanCounter();
            if (tableContent.RowHeaders != null)
            {
                foreach (var container in tableContent.RowHeaders.OfType<IContainer>())
                {
                    rowSpanCounter = container.GetHeaderWidth(rowSpanCounter);
                }
            }
            int rowHeaderWidth = rowSpanCounter.GetNodesCount();
            //int rowHeaderWidth =
            //    tableContent.RowHeaders == null ? 0 : 
            //    GetNodesCountArray(tableContent.RowHeaders)
            //        .Aggregate((x, y) => x * y);

            //　表のColumnHeader部分の高さcolumnHeaderHeightを算出
            var visibleTitleNumber =
                tableContent.ColumnHeaders.OfType<IContainer>()
                .Count(x => x.IsTitleVisible == true);
            int columnHeaderHeight =
                tableContent.ColumnHeaders.Sum(x => x.GetDepth())
                + visibleTitleNumber;

            // 表コントロールの作成と行の挿入
            var c1Table = new TsrTable();
            var rg = new C1TableRowGroup();
            for (int i = 0; i < rowHeaderWidth + columnHeaderHeight; i++)
            {
                rg.Rows.Add(new C1TableRow());
            }
            c1Table.RowGroups.Add(rg);

            // CellHeaderの作成
            CreateCellHeaderArea(tableContent, rg, columnHeaderHeight);
            // RowHeaderの作成
            CreateRowHeaderArea(tableContent, rg, rowSpanCounter, columnHeaderHeight);


            


            var columnSpanCounter = new SpanCounter();
            if (tableContent.ColumnHeaders != null)
            {
                foreach (var container in tableContent.ColumnHeaders.OfType<IContainer>())
                {
                    columnSpanCounter = container.GetHeaderWidth(columnSpanCounter);
                }
            }
            int columnHeaderWidth = columnSpanCounter.GetNodesCount();


            // ColumnHeaderの作成
            CreateColumnHeaderArea(
                tableContent, 
                rg,
                columnSpanCounter,
                columnHeaderHeight);

            // DataCellの作成
            CreateDataCellArea(
                rg,
                rowHeaderWidth,
                columnHeaderHeight,
                columnHeaderWidth);




            c1Table.BorderCollapse = true;
            c1Table.Margin = new Thickness(5);
            return c1Table;
        }

        public static IList<HeaderBase> CreateColumnDataStructure(IList<HeaderBase> container)
        {
            var columnDS = new List<HeaderBase>();
            //foreach (var header in container[0].Children)
            //    //{
            //    //    columnDS.Add(CombineContainer(header));
            //    //}
            //    columnDS.Add(CombineContainer(container[0]));

            //    for (int i = 1; i < container.Count(); i++)
            //    {
            //        foreach (var ds in columnDS)
            //        {
            //            CombineContainer2(ds, container[i]);
            //        }
            //    }
            //    return columnDS;
            //}

            //private static void CombineContainer2(ITsrHeader dataStructure, ITsrHeader container )
            //{
            //    if (dataStructure.Children.Count == 0)
            //    {
            //        dataStructure.Children.Add(CombineContainer(container));
            //    }
            //    else
            //    {
            //        foreach (var child in dataStructure.Children)
            //        {
            //            CombineContainer2(child, container);
            //        }
            //    }
            //}

            //private static ITsrHeader CombineContainer(ITsrHeader header)
            //{
            //    ITsrHeader ds;
            //    if (header is TsrHeaderContainer)
            //    {
            //        ds = new TsrHeaderContainer(header.GetEntity());
            //    }
            //    else
            //    {
            //        ds = new TsrHeader(header.GetEntity());
            //    }

            //    foreach (var child in header.Children)
            //    {
            //        ds.Children.Add(CombineContainer(child));
            //    }
            return columnDS;
        }
    }
}


