using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using ComponentOneTest.Serviceis.C1RichTextBox;
using ComponentOneTest.Entities;
using System.Windows;
using ComponentOneTest.Servicies.C1RichTextBox;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace ComponentOneTest
{
    //public sealed class TreeViewData
    //{
    //    public string Name { get; }
    //    public List<TreeViewData> Children { get; } = new();

        //public TreeViewData(HeaderContainer header)
        //{
        //    Name = header.Value;


        //    //foreach (var child in header.GetChildren())
        //    //{
        //    //    Children.Add(new TreeViewData(child));
        //    //}
        //}
    //}

    /// <summary>
    /// Window2.xaml の相互作用ロジック
    /// </summary>
    public partial class Window2 : Window
    {
        public ObservableCollection<TableHeaderEntity> ContainerList 
            = new ObservableCollection<TableHeaderEntity>();
        public ObservableCollection<TableHeaderEntity> CriteriaList
            = new ObservableCollection<TableHeaderEntity>();
        public Window2()
        {
            InitializeComponent();
            rtb.ViewMode = C1.WPF.RichTextBox.TextViewMode.Draft;
            rtb.Zoom = 1.5;

           
            TableHeaderFake.GetData(1).FindAll(x => x.Parent == 0).ForEach(x=>ContainerList.Add(x));
            ContainerDataGrid.ItemsSource = ContainerList;

            TableHeaderFake.GetData(0).FindAll(x => x.Parent == 0).ForEach(x => CriteriaList.Add(x));
            CriteriaDataGrid.ItemsSource = CriteriaList;

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

            var containerList = new List<TableHeaderEntity>();
            var gridList = ContainerList.ToList();
            var allList = TableHeaderFake.GetData(1);
            foreach (var item in allList)
            {
                var container = gridList.FirstOrDefault(x => x.Id == item.Id);
                if (container == null)
                {
                    containerList.Add(item);
                }
                else
                {
                    containerList.Add((TableHeaderEntity)container);
                }
            }
            var criteriaList = new List<TableHeaderEntity>();
            gridList = criteriaList.ToList();
            allList = TableHeaderFake.GetData(0);
            foreach (var item in allList)
            {
                var criteria = gridList.FirstOrDefault(x => x.Id == item.Id);
                if (criteria == null)
                {
                    criteriaList.Add(item);
                }
                else
                {
                    criteriaList.Add((TableHeaderEntity)criteria);
                }
            }
            var data1 = RichTextBoxTools.GetItemSource(containerList);
            var data2 = RichTextBoxTools.GetItemSource(criteriaList);

            CheckDataGrid.ItemsSource = containerList;

            //tv1.ItemsSource = data1;
            //tv2.ItemsSource = data2;
            //var ds =new List<HeaderBase>();
            //var ds1 = data1.ToList();
            //var ds2 = data2.ToList();
            //ds1.ForEach(x => ds.Add(x));
            //ds2.ForEach(x => ds.Add(x));

            //tv3.ItemsSource = RichTextBoxTools.CreateColumnDataStructure(ds);
            //tv4.ItemsSource = RichTextBoxTools.CreateColumnDataStructure(data2);

            var table = new TableContent("name", data1, data2);

            var tsrTable = RichTextBoxTools.CreateTable(table);
            rtb.Document.Blocks.Clear();
            rtb.Document.Blocks.Add(tsrTable);

            //var dic = new Dictionary<int, string>()
            //{
            //    {1,"qqq" },{2,"www"},{3,"eee"}
            //};
            //var item = new TSRTableData(
            //    10, dic, 100, 20, "±", 0, 1
            //    );
            //var str1=item.DisplayValue(1);
            //var str2=item.DisplayCondition();
        }

        private void GetDataButton_Click(object sender, RoutedEventArgs e)
        {
            //if( rtb.Selection.Cells.Count()>0)
            //{
            //    var cell = rtb.Selection.Cells.First();
            //    if (cell is not TsrDataCell) return;

                
            //    var parent = cell.Parent;
                
            //    int counter1 = 0;
            //    foreach(var rw in parent.Parent.Children)
            //    {
            //        if (rw.Children.Count(x => x.GetType() == typeof(TsrDataCell)) > 0) 
            //            counter1++;
            //        if (rw.Index == parent.Index) break;
            //    };
                
            //    int counter =  parent.Children.OfType<TsrDataCell>().Count(x=>x.Index<= cell.Index);
            //    tb1.Text = $"Row:{counter1},Column:{counter}";
            //}
        }

        private void CriteriaButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ContainerDataGrid_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            if (ContainerDataGrid.SelectedItem == null) return;

            var item = ContainerDataGrid.SelectedItem as TableHeaderEntity;
        }
    }
}
