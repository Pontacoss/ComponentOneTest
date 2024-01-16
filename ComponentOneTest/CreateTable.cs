using C1.WPF.RichTextBox.Documents;
using System.Windows;
using System.Windows.Media;

namespace ComponentOneTest
{
    public static class CreateTable
    {
        private static C1TableCell CreateCell(string? name)
        {
            var cell = new C1TableCell();
            cell.BorderThickness=new Thickness(1);
            var paragraph= new C1Paragraph();
            paragraph.Children.Add(
                new C1Run()
                {
                    Text = name,
                    Padding = new Thickness(5,0,5,0)
                }) ;
            paragraph.Padding = new Thickness(0);
            cell.Children.Add(paragraph);
            cell.Padding = new Thickness(0);
            return cell;
        }

        public static C1Table ListType<T>(IList<T> list) where T : class
        {
            var table = new C1Table();
            var properties = typeof(T).GetProperties();

            // カラムの作成
            for (int i = 0; i < properties.Count(); i++)
            {
                table.Columns.Add(new C1TableColumn());
            }

            // タイトル行の生成
            var titleRow=new C1TableRow();
            foreach(var property in properties)
            {
                var cell=CreateCell(property.Name);
                cell.TextAlignment=C1TextAlignment.Center;
                cell.Background = new SolidColorBrush(Colors.LightGray);
                titleRow.Children.Add(cell);
            }
            table.Children.Add(titleRow);

            // データ行の生成
            foreach (var entity in list)
            {
                var valueRow = new C1TableRow();
                foreach (var prop in properties)
                {
                    var valueObj = prop.GetValue(entity);
                    var value = valueObj != null ? valueObj.ToString() : "";
                    valueRow.Children.Add(CreateCell(value));
                }
                table.Children.Add(valueRow);
            }

            // テーブル設定
            table.BorderCollapse = true;
            table.TableAlignment=C1TextAlignment.Center;

            return table;
        }
    }
}
