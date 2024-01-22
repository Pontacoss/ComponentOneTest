using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using ComponentOneTest.ComponentOne.RichTextBox;
using ComponentOneTest.Entities;
using System.Windows;

namespace ComponentOneTest
{
    public sealed class TreeViewData
    {
        public string Name { get; }
        public List<TreeViewData> Children { get; } = new();

        public TreeViewData(HeaderContainer header)
        {
            Name = header.Value;


            //foreach (var child in header.GetChildren())
            //{
            //    Children.Add(new TreeViewData(child));
            //}
        }
    }

    /// <summary>
    /// Window2.xaml の相互作用ロジック
    /// </summary>
    public partial class Window2 : Window
    {
        private List<HeaderContainer> _headers = new();
        public Window2()
        {
            InitializeComponent();
            rtb.ViewMode = C1.WPF.RichTextBox.TextViewMode.Print;
            rtb.Zoom = 1.5;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var list = new List<TemplateEntity>();
            list.Add(new TemplateEntity("Visual inspection", "6.1", "x", "x", "4.5.3.1"));
            list.Add(new TemplateEntity("Verification of dimensions and tolerance", "6.2", "x", "-", "4.5.3.2"));
            list.Add(new TemplateEntity("Weighting", "6.3", "x", "-", "4.5.3.3"));
            list.Add(new TemplateEntity("Marking inspection", "6.4", "x", "x", "4.5.3.4"));
            list.Add(new TemplateEntity("Leakage test", "6.5", "x", "-", "4.5.3.5.4"));
            list.Add(new TemplateEntity("Test of the degree of protection", "6.6", "x", "-", "4.5.3.6"));
            list.Add(new TemplateEntity("Visual inspection", "6.1", "x", "x", "4.5.3.1"));
            list.Add(new TemplateEntity("Verification of dimensions and tolerance", "6.2", "x", "-", "4.5.3.2"));
            list.Add(new TemplateEntity("Weighting", "6.3", "x", "-", "4.5.3.3"));
            list.Add(new TemplateEntity("Marking inspection", "6.4", "x", "x", "4.5.3.4"));
            list.Add(new TemplateEntity("Leakage test", "6.5", "x", "-", "4.5.3.5.4"));
            list.Add(new TemplateEntity("Test of the degree of protection", "6.6", "x", "-", "4.5.3.6"));
            list.Add(new TemplateEntity("Visual inspection", "6.1", "x", "x", "4.5.3.1"));
            list.Add(new TemplateEntity("Verification of dimensions and tolerance", "6.2", "x", "-", "4.5.3.2"));
            list.Add(new TemplateEntity("Weighting", "6.3", "x", "-", "4.5.3.3"));
            list.Add(new TemplateEntity("Marking inspection", "6.4", "x", "x", "4.5.3.4"));
            list.Add(new TemplateEntity("Leakage test", "6.5", "x", "-", "4.5.3.5.4"));
            list.Add(new TemplateEntity("Test of the degree of protection", "6.6", "x", "-", "4.5.3.6"));
            list.Add(new TemplateEntity("Visual inspection", "6.1", "x", "x", "4.5.3.1"));
            list.Add(new TemplateEntity("Verification of dimensions and tolerance", "6.2", "x", "-", "4.5.3.2"));
            list.Add(new TemplateEntity("Weighting", "6.3", "x", "-", "4.5.3.3"));
            list.Add(new TemplateEntity("Marking inspection", "6.4", "x", "x", "4.5.3.4"));
            list.Add(new TemplateEntity("Leakage test", "6.5", "x", "-", "4.5.3.5.4"));
            list.Add(new TemplateEntity("Test of the degree of protection", "6.6", "x", "-", "4.5.3.6"));
            list.Add(new TemplateEntity("Visual inspection", "6.1", "x", "x", "4.5.3.1"));
            list.Add(new TemplateEntity("Verification of dimensions and tolerance", "6.2", "x", "-", "4.5.3.2"));
            list.Add(new TemplateEntity("Weighting", "6.3", "x", "-", "4.5.3.3"));
            list.Add(new TemplateEntity("Marking inspection", "6.4", "x", "x", "4.5.3.4"));
            list.Add(new TemplateEntity("Leakage test", "6.5", "x", "-", "4.5.3.5.4"));
            list.Add(new TemplateEntity("Test of the degree of protection", "6.6", "x", "-", "4.5.3.6"));
            list.Add(new TemplateEntity("Visual inspection", "6.1", "x", "x", "4.5.3.1"));
            list.Add(new TemplateEntity("Verification of dimensions and tolerance", "6.2", "x", "-", "4.5.3.2"));
            list.Add(new TemplateEntity("Weighting", "6.3", "x", "-", "4.5.3.3"));
            list.Add(new TemplateEntity("Marking inspection", "6.4", "x", "x", "4.5.3.4"));
            list.Add(new TemplateEntity("Leakage test", "6.5", "x", "-", "4.5.3.5.4"));
            list.Add(new TemplateEntity("Test of the degree of protection", "6.6", "x", "-", "4.5.3.6"));

            //rtb.Document.Blocks.Add(RichTextBoxTools.CreateTable(list));

            //var table = rtb.Document.Children[1] as C1Table;
            //var row = table.Children[0] as C1TableRow;
            //var cell = row.Children[0] as C1TableCell;
            //cell.RowSpan = 2;

            var data1 = GetItemSource(HeaderFake.GetData());
            var data2 = GetItemSource(HeaderFake.GetData2());

            tv1.ItemsSource = data1;
            tv2.ItemsSource = data2;

            var table = new TableEntity("name", data1, data2);

            rtb.Document.Blocks.Add(RichTextBoxTools.CreateTable(table));
        }

        private IList<HeaderContainer> GetItemSource(List<HeaderEntity> list)
        {
            var source=new List<HeaderContainer>();

            foreach (var entity in list)
            {
                var parent = GetParent(source, entity.Parent);
                if (parent != null)
                {
                    parent.Add(new HeaderContainer(entity));
                }
                else
                {
                    source.Add(new HeaderContainer(entity));
                }
            }
            return source;
        }

        private HeaderContainer? GetParent(IList<HeaderContainer> list, int parentId)
        {
            foreach (var entity in list)
            {
                if (entity.Id == parentId) return entity;

                var target= GetParent(entity.Children, parentId);

                if (target != null) return target;
            }
            return null;
        }
    }
}
