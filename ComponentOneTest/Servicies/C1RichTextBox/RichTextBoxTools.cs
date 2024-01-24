using C1.WPF.RichTextBox.Documents;
using System.Windows;
using ComponentOneTest.Entities;
using System.Windows.Media;
using ComponentOneTest.Servicies.C1RichTextBox;
using System.Reflection.PortableExecutable;
using System.Net.NetworkInformation;
using System.Windows.Documents;
using System.ComponentModel;

namespace ComponentOneTest.Serviceis.C1RichTextBox
{
    public static class RichTextBoxTools
    {
        private static C1TableCell CreateCell(string? name)
        {
            var cell = new C1TableCell();
            cell.BorderThickness = new Thickness(1);
            var paragraph = new C1Paragraph();
            paragraph.Children.Add(
                new C1Run()
                {
                    Text = name,
                    Padding = new Thickness(5, 0, 5, 0)
                });
            paragraph.Padding = new Thickness(0);
            cell.Children.Add(paragraph);
            cell.Padding = new Thickness(0);
            return cell;
        }

        private static C1TableCell CreateRowHeaderCell(
            string? name,
            int rowSpan,
            int columnSpan)
        {
            var cell = new C1TableCell();
            cell.BorderThickness = new Thickness(1);
            var paragraph = new C1Paragraph();
            paragraph.Children.Add(
                new C1Run()
                {
                    Text = name,
                    Padding = new Thickness(5, 0, 5, 0)
                });
            paragraph.Padding = new Thickness(0);
            cell.Children.Add(paragraph);
            cell.Padding = new Thickness(0);
            cell.RowSpan = rowSpan;
            cell.ColumnSpan = columnSpan;
            return cell;
        }

        public static C1Table CreateTable<T>(IList<T> list) where T : class
        {
            var table = new C1Table();
            var properties = typeof(T).GetProperties();

            //var columnNamesProperty = typeof(T).GetProperty("ColumnNames");
            //var columnNames =  columnNamesProperty?.GetValue(list[0]) as List<string>;

            // カラムの作成
            for (int i = 0; i < properties.Count(); i++)
            {
                table.Columns.Add(new C1TableColumn());
            }
            //if (columnNames == null) return null;
            //foreach (var col in columnNames)
            //{
            //    table.Columns.Add(new C1TableColumn());
            //}

            // タイトル行の生成
            var titleRow = new C1TableRow();
            foreach (var property in properties)
            {
                var cell = CreateCell(property.Name);
                cell.TextAlignment = C1TextAlignment.Center;
                cell.Background = new SolidColorBrush(Colors.LightGray);
                titleRow.Children.Add(cell);
            }
            table.Children.Add(titleRow);

            //foreach(var col in columnNames)
            //{
            //    var cell = CreateCell(col);
            //    cell.TextAlignment = C1TextAlignment.Center;
            //    cell.Background = new SolidColorBrush(Colors.LightGray);
            //    titleRow.Children.Add(cell);
            //}
            //table.Children.Add(titleRow);

            // データ行の生成
            foreach (var entity in list)
            {
                var valueRow = new C1TableRow();
                foreach (var prop in properties)
                {
                    var valueObj = prop.GetValue(entity);
                    var value = valueObj?.ToString();
                    valueRow.Children.Add(CreateCell(value));
                }
                table.Children.Add(valueRow);
            }

            // テーブル設定
            table.BorderCollapse = true;
            table.TableAlignment = C1TextAlignment.Center;
            table.Padding = new Thickness(0);

            return table;
        }

        public static IList<ITableHeader> GetItemSource(List<TableHeaderEntity> list)
        {
            var source = new List<ITableHeader>();

            foreach (var entity in list)
            {
                var parent = GetParent(source, entity.Parent);
                if (parent != null)
                {
                    parent.Add(new C1TableHeader(entity));
                }
                else
                {
                    source.Add(new HeaderContainer(entity));
                }
            }
            return source;
        }

        public static ITableHeader? GetParent(IList<ITableHeader> list, int parentId)
        {
            foreach (var entity in list)
            {
                if (entity.Id == parentId) return entity;

                var target = GetParent(entity.Children, parentId);

                if (target != null) return target;
            }
            return null;
        }

        private static int[] GetWidthArray(IEnumerable<ITableHeader> items)
        {
            var widths = new int[items.Count()];
            var i = 0;
            foreach (var item in items)
            {
                widths[i] = item.GetWidth();
                i++;
            }
            return widths;
        }

        private static void CreateCellHeader(
            TableContent tableContent,
            C1Table c1Table,
            int columnHeaderHeight)
        {
            foreach (var header in tableContent.RowHeaders)
            {
                var row = c1Table.Children.FirstOrDefault(x => x.Index == 0);
                if (row == null)
                {

                }
                int rowSpan = columnHeaderHeight;
                int columnSpan = header.GetDepth();

                var cell = CreateRowHeaderCell(
                        header.ToString(),
                        rowSpan,
                        columnSpan);
                cell.Background = Brushes.LightGray;
                cell.TextAlignment = C1TextAlignment.Center;
                cell.VerticalAlignment = C1VerticalAlignment.Middle;
                row.Children.Add(cell);
            }
        }

        private static int CreateColumnContainerTitles(
            ITableHeader header,
            C1Table c1Table,
            int itr,
            int widthRate,
            int repeat)
        {
            var container = header as HeaderContainer;
            if (container == null || !(container.IsVisibleTitle)) return 0;
            var row = c1Table.Children.First(x => x.Index == itr);
            int rowSpan = 1;
            int columnSpan = container.GetWidth() * widthRate;

            for (int i = 0; i < repeat; i++)
            {
                var cell = CreateRowHeaderCell(
                        container.ToString(),
                        rowSpan,
                        columnSpan);

                cell.Background = Brushes.LightGray;
                cell.FontWeight = FontWeights.Bold;
                cell.TextAlignment = C1TextAlignment.Center;
                cell.VerticalAlignment = C1VerticalAlignment.Middle;
                row.Children.Add(cell);
            }
            return 1;
        }

        private static int CreateColumnContainer(
            ITableHeader container,
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
        private static void CreateColumnHeader(
            C1Table table,
            ITableHeader header,
            int itr,
            int maxDepth,
            int widthRate)
        {
            int subItr = itr;
            int columnSpan = header.GetWidth() * widthRate;

            var row = table.Children.First(x => x.Index == itr);
            var cell = CreateRowHeaderCell(
                        header.ToString(),
                        maxDepth - header.Level - header.GetDepth() + 2,
                        columnSpan);

            cell.TextAlignment = C1TextAlignment.Center;
            cell.Background = Brushes.LightGray;
            cell.VerticalAlignment = C1VerticalAlignment.Middle;
            row.Children.Add(cell);
            itr++;

            foreach (var child in header.Children)
            {
                CreateColumnHeader(table, child, itr, maxDepth, widthRate);
            }
        }

        private static void CreateRowHeaders(
            TableContent tableContent,
            C1Table c1Table,
            int columnHeaderHight)
        {
            int endPoint = 1;
            var widthRate = GetWidthArray(tableContent.RowHeaders).Aggregate((x, y) => x * y);
            foreach (var header in tableContent.RowHeaders)
            {
                var itr = columnHeaderHight;
                int columnSpan = header.GetDepth();
                widthRate /= header.GetWidth();
                for (int j = 0; j < endPoint; j++)
                {
                    foreach (var child in header.Children)
                    {
                        itr += CreateRowHeader(c1Table, itr, child, columnSpan, widthRate);
                    }
                }
                endPoint *= header.GetWidth();
            }
        }

        private static void CreateDataCell(
            TableContent tableContent,
            C1Table c1Table,
            int columnHeaderHeight)
        {
            int rowWidth = GetWidthArray(tableContent.RowHeaders).Aggregate((x, y) => x * y);
            int columnWidth = GetWidthArray(tableContent.ColumnHeaders).Aggregate((x, y) => x * y);

            for (int i = 0; i < rowWidth; i++)
            {
                var row = c1Table.Children.First(x => x.Index == i + columnHeaderHeight);
                for (int j = 0; j < columnWidth; j++)
                {
                    var cell = CreateCell("");
                    cell.Width = new C1Length(50, C1LengthUnitType.Pixel);
                    row.Children.Add(cell);
                }
            }
        }
        private static int CreateRowHeader(C1Table table, int itr,
            ITableHeader header,
            int maxDepth,
            int widthRate)
        {
            int itrSub = itr;
            int rowSpan = header.GetWidth() * widthRate;
            var row = table.Children.First(x => x.Index == itrSub);
            var cell = CreateRowHeaderCell(
                        header.ToString(),
                        rowSpan,
                        maxDepth - header.Level - header.GetDepth() + 2);
            cell.TextAlignment = C1TextAlignment.Left;
            row.Children.Add(cell);

            foreach (var child in header.Children)
            {
                itrSub += CreateRowHeader(table, itrSub, child, maxDepth, widthRate);
            }

            return rowSpan;
        }
        private static void CreateColumnHeaders(
            TableContent tableContent,
            C1Table c1Table,
            int columnHeaderHeight)
        {
            int itr = 0;
            int widthRate = GetWidthArray(tableContent.ColumnHeaders)
                .Aggregate((x, y) => x * y);
            int repeat = 1;
            foreach (var container in tableContent.ColumnHeaders)
            {
                widthRate /= container.GetWidth();
                // ColumnHeaderTitleの作成
                itr += CreateColumnContainerTitles(container, c1Table, itr, widthRate, repeat);
                // ColumnHeaderの作成
                itr += CreateColumnContainer(container, c1Table, itr, widthRate, repeat);
                repeat *= container.GetWidth();
            }
        }

        public static C1Table CreateTable(TableContent tableContent)
        {
            int rowHeaderWidth =
                GetWidthArray(tableContent.RowHeaders)
                .Aggregate((x, y) => x * y);
            var visibleTitleNumber =
                tableContent.ColumnHeaders
                .Count(x => x.ToString() != string.Empty);
            int columnHeaderHeight =
                tableContent.ColumnHeaders.Sum(x => x.GetDepth())
                + visibleTitleNumber;

            var c1Table = new C1Table();

            for (int i = 0; i < rowHeaderWidth + columnHeaderHeight; i++)
            {
                var row = new C1TableRow();
                //var cell = (CreateCell(i.ToString()));
                //row.Cells.Add(cell);
                c1Table.Children.Add(row);
            }

            // CellHeaderの作成
            CreateCellHeader(tableContent, c1Table, columnHeaderHeight);
            // ColumnHeaderの作成
            CreateColumnHeaders(tableContent, c1Table, columnHeaderHeight);
            // RowHeaderの作成
            CreateRowHeaders(tableContent, c1Table, columnHeaderHeight);
            // DataCellの作成
            CreateDataCell(tableContent, c1Table, columnHeaderHeight);

            c1Table.BorderCollapse = true;
            return c1Table;
        }
    }
}


