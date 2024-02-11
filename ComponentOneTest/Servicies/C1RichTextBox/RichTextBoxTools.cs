using C1.WPF.RichTextBox.Documents;
using ComponentOneTest.Entities;
using ComponentOneTest.Servicies.C1RichTextBox;
using ComponentOneTest.ViewModelEntities;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ComponentOneTest.Serviceis.C1RichTextBox
{
    public static class RichTextBoxTools
    {
        internal static C1TableCell CreateCell(string? name, C1TableCell cell)
        {
            var paragraph = new C1Paragraph();
            paragraph.Children.Add(
                new C1Run()
                {
                    Text = name,
                    Padding = new Thickness(0, 0, 0, 0),
                    Margin = new Thickness(0)
                });
            paragraph.Padding = new Thickness(0);
            paragraph.Margin = new Thickness(2);
            cell.Children.Add(paragraph);

            cell.BorderThickness = new Thickness(1);
            cell.Padding = new Thickness(0);
            cell.Margin = new Thickness(0);

            return cell;
        }
        internal static C1TableCell CreateComboBoxCell(string? name,C1TableCell cell)
        {
            var paragraph = new C1Paragraph();

            var combo= new System.Windows.Controls.ComboBox();
            combo.ItemsSource = new string[] { "+", "-", "±" };
            combo.SelectionChanged += Combo_SelectionChanged;

            paragraph.Children.Add(new C1InlineUIContainer() { Content = combo });
            

            paragraph.Children.Add(
                new C1Run()
                {
                    Text = name,
                    Padding = new Thickness(0, 0, 0, 0),
                    Margin = new Thickness(0)
                });
            paragraph.Padding = new Thickness(0);
            paragraph.Margin = new Thickness(2);
            cell.Children.Add(paragraph);

            cell.BorderThickness = new Thickness(1);
            cell.Padding = new Thickness(0);
            cell.Margin = new Thickness(0);

            return cell;
        }

        private static void Combo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
        }

        private static C1TableCell CreateDataCell(string? name)
        {
            var cell = CreateCell(string.Empty, new TsrDataCell(name));
            cell.TextAlignment = C1TextAlignment.Right;
            cell.VerticalAlignment = C1VerticalAlignment.Middle;
            return cell;
        }

        private static C1TableCell CreateComboBoxDataCell(string? name)
        {
            var cell = CreateComboBoxCell(string.Empty, new TsrDataCell(name));
            cell.TextAlignment = C1TextAlignment.Right;
            cell.VerticalAlignment = C1VerticalAlignment.Middle;
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
            cell.VerticalAlignment = C1VerticalAlignment.Middle;
            cell.RowSpan = rowSpan;
            cell.ColumnSpan= columnSpan;

            if (name == null) return cell;
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

        }


        public static List<HeaderBase> GetItemSource(IList<TableHeaderEntity> list)
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
                        if (entity.IsColumn)
                        {
                            source.Add(new RepeartContainer(entity));
                        }
                        else
                        {
                            source.Add(new BlockContainer(entity));
                        }
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
                container.CreateRowHeaders(rows, columnHeaderHeight);
            }
        }

        
        private static void CreateDataCellArea(
            TableContent tableContent,
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
                    StringBuilder sb = new StringBuilder();
                    sb.Append( tableContent.RowHeaders == null ?
                        string.Empty :
                        GetConditionString(tableContent.RowHeaders, i + 1));
                    sb.Append(GetConditionString(tableContent.ColumnHeaders, j + 1));

                    //if (sb.ToString().Contains("公差"))
                    //{
                    //    row.Children.Add(CreateComboBoxDataCell(sb.ToString()));
                    //}
                    //else
                    //{
                    //    row.Children.Add(CreateDataCell(sb.ToString()));
                    //}

                    row.Children.Add(CreateDataCell(sb.ToString()));
                }
            }
        }

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
                rowIndex += container.CreateColumnHeaders(rows, rowIndex);
            }
        }

        private static List<TableHeaderEntity> CriteriaSetting(
            TableHeaderEntity criteriaSubContainer,
            bool? documentType)
        {
            var criteriaHeaders=new List<TableHeaderEntity>();
            criteriaHeaders.Add(
                new TableHeaderEntity(
                    Convert.ToInt32(criteriaSubContainer.Id + "1"),
                    "基準値",
                    criteriaSubContainer.Id,
                    criteriaSubContainer.Level));
            if (documentType == true)
            {
                criteriaHeaders.Add(
                    new TableHeaderEntity(
                        Convert.ToInt32(criteriaSubContainer.Id + "2"),
                        "公差",
                        criteriaSubContainer.Id,
                        criteriaSubContainer.Level));
            }
            else
            {
                criteriaHeaders.Add(
                    new TableHeaderEntity(
                         Convert.ToInt32(criteriaSubContainer.Id + "2"),
                        "測定値",
                        criteriaSubContainer.Id,
                        criteriaSubContainer.Level));
                criteriaHeaders.Add(
                        new TableHeaderEntity(
                             Convert.ToInt32(criteriaSubContainer.Id + "3"),
                            "判定",
                            criteriaSubContainer.Id,
                            criteriaSubContainer.Level));
            }
            return criteriaHeaders;
        }

        public static TsrTable CreateTable(
            List<TableHeaderEntity> headerList, 
            List<TableHeaderEntity> criteriaList,
            bool? documentType,
            bool? criteriaPosition=false)
        {
            // ヘッダーを行、列に振り分け
            var rowHeaderList = RichTextBoxTools.GetItemSource(
                headerList.ToList().FindAll(x => x.IsColumn == false));
            var columnHeaderList = RichTextBoxTools.GetItemSource(
                headerList.ToList().FindAll(x => x.IsColumn == true));
            
            // 基準値コンテナをメインコンテナに格納＋ヘッダーを追加
            var entities =new List<TableHeaderEntity>();
            entities.Add(new TableHeaderEntity(1000, "試験項目", false, true, true));
            entities.AddRange(criteriaList);
            foreach (var criteriaSubContainer in criteriaList)
            {
                entities.AddRange(CriteriaSetting(criteriaSubContainer, documentType));
            }

            // 基準値を行、列に振り分け
            if (criteriaPosition==true)
                columnHeaderList.AddRange(RichTextBoxTools.GetItemSource(entities));
            else
                rowHeaderList.AddRange(RichTextBoxTools.GetItemSource(entities));

            //　現状、列ヘッダーなしでは表が作れない。
            if(columnHeaderList.Count == 0)
            {
                MessageBox.Show("列ヘッダーを一つ以上配置してください。","",MessageBoxButton.OK);
                return new TsrTable();
            }

            var tableContent = new TableContent("name", rowHeaderList, columnHeaderList);

            if (tableContent.RowHeaders == null) return new TsrTable();

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


            //　表のColumnHeader部分の高さcolumnHeaderHeightを算出
            var visibleTitleNumber =
                tableContent.ColumnHeaders.OfType<IContainer>()
                .Count(x => x.IsTitleVisible == true);
            int columnHeaderHeight =
                tableContent.ColumnHeaders.Sum(x => x.GetDepth())
                + visibleTitleNumber;

            // 表コントロールの作成と行の挿入
            var table = new TsrTable();
            var rg = new C1TableRowGroup();
            for (int i = 0; i < rowHeaderWidth + columnHeaderHeight; i++)
            {
                rg.Rows.Add(new C1TableRow());
            }
            table.RowGroups.Add(rg);

            // CellHeaderの作成
            CreateCellHeaderArea(tableContent, rg, columnHeaderHeight);
            // RowHeaderの作成
            CreateRowHeaderArea(tableContent, rg, rowSpanCounter, columnHeaderHeight);

            // 表の列ヘッダー部分の幅を算出
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
                tableContent,
                rg,
                rowHeaderWidth,
                columnHeaderHeight,
                columnHeaderWidth);

            return table;
        }

        public static string GetConditionString(IEnumerable<HeaderBase> HeaderList, int Index)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var header in HeaderList.OfType<IContainer>())
            {
                sb.Append(header.GetConditionString(Index));
                sb.Append("\n");
            }
            return sb.ToString();
        }
}
}


