using C1.WPF.RichTextBox.Documents;
using System.Windows;
using ComponentOneTest.Entities;
using System.Windows.Media;

namespace ComponentOneTest.ComponentOne.RichTextBox
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

        public static C1Table CreateTable(TableEntity table)
        {
            int rowHeaderDepth = GetHeaderDepth(table.RowHeaders);
            int rowHeaderWidth = GetHeaderWidth(table.RowHeaders);

            int columnHeaderDepth = GetHeaderDepth(table.ColumnHeaders);
            int columnHeaderWidth = GetHeaderWidth(table.ColumnHeaders);


            var c1Table = new C1Table();
            // カラムの作成
            for (int i = 0; i < rowHeaderDepth + columnHeaderWidth; i++)
            {
                c1Table.Columns.Add(new C1TableColumn());
            }
            // Rowの作成
            for (var i = 0; i < columnHeaderDepth; i++)
            {
                var titleRow = new C1TableRow();

                if (i == 0)
                {
                    for (int j = 0; j < rowHeaderDepth; j++)
                    {
                        var cell = CreateCell("111");
                        cell.TextAlignment = C1TextAlignment.Center;
                        cell.Background = new SolidColorBrush(Colors.LightGray);
                        cell.RowSpan = columnHeaderDepth;
                        titleRow.Children.Add(cell);
                    }
                }

                var list = new List<HeaderContainer>();
                list.AddRange(GetLevelList(table.ColumnHeaders, i));

                foreach (var col in list)
                {
                    var cell = CreateCell(col.Value);
                    cell.TextAlignment = C1TextAlignment.Center;
                    cell.Background = new SolidColorBrush(Colors.LightGray);
                    cell.ColumnSpan = GetEndNode(col);
                    titleRow.Children.Add(cell);
                }
                c1Table.Children.Add(titleRow);
            }

            c1Table.BorderCollapse = true;
            return c1Table;
        }

        private static IEnumerable<HeaderContainer> GetLevelList(IEnumerable<HeaderContainer> columns, int level)
        {
            var list=new List<HeaderContainer>();

            foreach(var col in columns)
            {
                if (col.Level == level)
                {
                    list.Add(col);
                }
                else
                {
                    list.AddRange(GetLevelList(col.Children, level));
                }
                
            }

            return list;
        }

        private static int GetHeaderWidth(IEnumerable<HeaderContainer> containers)
        {
            int width = 1;
            foreach (var container in containers)
            {
                width *= GetEndNode(container);
            }

            return width;
        }

        private static int GetEndNode(HeaderContainer container)
        {
            int nodeNumber = 0;
            
            if (container.Children.Count == 0)
            {
                return 1;
            }
            else
            {
                foreach (var child in container.Children)
                {
                    nodeNumber += GetEndNode(child);
                }
            }
            
            return nodeNumber;
        }

            private static int GetHeaderDepth(IEnumerable<HeaderContainer> containers)
        {
            int depth = 0;
            foreach (var container in containers)
            {
                depth += GetMaxLevel(container.Children) + 1;
            }

            return depth;
        }

        private static int GetMaxLevel(IList<HeaderContainer> list)
        {
            int maxLevel = 0;

            foreach (var entity in list)
            {
                if (entity.Children.Count > 0)
                {
                    var i = GetMaxLevel(entity.Children);
                    maxLevel = Math.Max(maxLevel, i);
                }
                else
                {
                    maxLevel = Math.Max(maxLevel, entity.Level);
                }
            }
            return maxLevel;
        }
    }

    public sealed class TableEntity
    {
        public string TableName { get; }
        public IEnumerable<HeaderContainer> RowHeaders { get; }
        public IEnumerable<HeaderContainer> ColumnHeaders { get; }

        public TableEntity(string tableName,
            IList<HeaderContainer> rowHeaders,
            IList<HeaderContainer> columnHeaders)
        {
            TableName = tableName;
            RowHeaders = rowHeaders;
            ColumnHeaders = columnHeaders;
        }
    }

    //public interface ITableHeader
    //{
    //    int Id { get; }
    //    int Parent { get; }
    //    string Name { get;  }
    //    int Level { get;  }

    //    void Add(ITableHeader tableHeader);
    //    IEnumerable<ITableHeader> GetChildren();
    //}

    public sealed class HeaderContainer 
    {
        public List<HeaderContainer> Children { get; } = new List<HeaderContainer>();
        private HeaderEntity _headerEntity;
        
        public int Level =>_headerEntity.Level;
        public int Id => _headerEntity.Id;
        public int Parent => _headerEntity.Parent;
        public string Value => _headerEntity.Value;

        public HeaderContainer(HeaderEntity headerEntity)
        {
            _headerEntity = headerEntity;
        }

        public void Add(HeaderContainer tableHeader)
        {
            Children.Add(tableHeader);
        }

        public override string ToString()
        {
            return Value;
        }

        public int GetDepth()
        {
            int depth = this.Level;
            foreach (HeaderContainer child in Children)
            {
                depth = Math.Max(depth, child.GetDepth());
            }
            return depth+1;
        }

        public int GetWidth()
        {
            int width = 0;
            foreach (HeaderContainer child in Children)
            {
                width += child.GetDepth();
            }
            return Math.Max(1, width);
        }

    }


 



}

