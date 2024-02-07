using C1.WPF.RichTextBox;
using ComponentOneTest.Serviceis.C1RichTextBox;
using ComponentOneTest.Entities;
using System.Windows;
using ComponentOneTest.Servicies.C1RichTextBox;
using System.Collections.ObjectModel;
using ComponentOneTest.ViewModelEntities;

namespace ComponentOneTest
{
    /// <summary>
    /// Window2.xaml の相互作用ロジック
    /// </summary>
    public partial class Window2 : Window
    {
        public ObservableCollection<TableHeaderVMEntity> ContainerList 
            = new ObservableCollection<TableHeaderVMEntity>();

        public ObservableCollection<TableHeaderVMEntity> CriteriaList
            = new ObservableCollection<TableHeaderVMEntity>();

        public List<TableHeaderVMEntity> _vmEntities=
            new List<TableHeaderVMEntity>();

        private TableHeaderVMEntity? SeekParent(List<TableHeaderVMEntity> entities,int parentId)
        {
            foreach(var entity in entities)
            {
                if(entity.Id == parentId)
                {
                    return entity;
                }
                else
                {
                    var result = SeekParent(entity.Children.ToList(), parentId);
                    if(result != null) return result;
                }
            }
            return null;
        }
        private List<TableHeaderVMEntity> ConvertToVMEntities(List<TableHeaderEntity> entities)
        {
            List<TableHeaderVMEntity> vmEntities = new();
            foreach (var entity in entities)
            {
                var parent = SeekParent(vmEntities,entity.Parent);
                if (parent == null)
                { 
                    vmEntities.Add(new TableHeaderVMEntity(entity)); 
                }
                else
                {
                    parent?.Add(new TableHeaderVMEntity(entity, parent));
                }
            }

            return vmEntities;
        }
        public Window2()
        {
            InitializeComponent();
            rtb.ViewMode = TextViewMode.Draft;
            rtb.Zoom = 1.5;

            CriteriaList.Add(new TableHeaderVMEntity(new TableHeaderEntity(111, "試験項目", false, true, true)));
            //var list=TableHeaderFake.GetData(1);
            //ConvertToVMEntities(list).ForEach(x => ContainerList.Add(x));

            //ContainerDataGrid.ItemsSource = ContainerList.ToList().FindAll(x => x.Parent == 0);

            //var list2 = TableHeaderFake.GetData(0);
            //ConvertToVMEntities(list2).ForEach(x => CriteriaList.Add(x));
            //CriteriaDataGrid.ItemsSource = CriteriaList[0].Children;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (CriteriaList[0].Children.Count==0) return;
            var entities = new List<TableHeaderEntity>();
            var rowList = ContainerList.ToList().FindAll(x => x.IsColumn == false);

            entities = RichTextBoxTools.GetEntities(rowList, null);
            var data1 = RichTextBoxTools.GetItemSource(entities);

            var columnList= ContainerList.ToList().FindAll(x => x.IsColumn == true);
            columnList.Add(CriteriaList[0]);
            
            entities = RichTextBoxTools.GetEntities(columnList, null);
            var data2 = RichTextBoxTools.GetItemSource(entities);

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

        

        private void ContainerDataGrid_SelectedCellsChanged(object sender, 
            System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            if (ContainerDataGrid.SelectedItem is not TableHeaderVMEntity item) return;
            tv1.ItemsSource = item.Children;
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

        private void HeaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (HeaderTextBox.Text is null) return;
            if (HeaderTextBox.Text == string.Empty) return;
            var container=ContainerDataGrid.SelectedItem as TableHeaderVMEntity;
            if (container == null) return;
            
            string[] hierarchy = HeaderTextBox.Text.Split('/');

            var input = HeaderTextBox.Text;
            var parent = container;
            if (hierarchy.Count() > 1)
            {
                if (hierarchy[0] == string.Empty)
                {
                    if (tv1.SelectedItem == null)
                    {
                        HeaderTextBox.Text = string.Empty;
                        return;
                    }
                    parent = tv1.SelectedItem as TableHeaderVMEntity;
                    if(parent == null) return;  
                }
                else
                {
                    parent = new TableHeaderVMEntity(hierarchy[0], container);
                    container.Add(parent);
                }
                input= hierarchy[1];
            }

            tv1.ItemsSource = null;
            string[] items= input.Split(',');
            foreach (var item in items)
            {
                parent.Add(new TableHeaderVMEntity(item, parent));
            }
            tv1.ItemsSource = container.Children;
            HeaderTextBox.Text = string.Empty;
        }

        private void ContainerButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContainerTextBox.Text is null) return;
            if (ContainerTextBox.Text == string.Empty) return;
            var id = ContainerList.Count + 1;
            ContainerList.Add(new TableHeaderVMEntity(new TableHeaderEntity(id,ContainerTextBox.Text)));
            ContainerDataGrid.ItemsSource = ContainerList.ToList().FindAll(x => x.Parent == 0);
            ContainerTextBox.Text=string.Empty;
        }
        private void CriteriaButton_Click(object sender, RoutedEventArgs e)
        {
            if (CriteriaTextBox.Text is null) return;
            if (CriteriaTextBox.Text == string.Empty) return;
            var id = CriteriaList[0].Children.Count*100 + 1000;
            ObservableCollection<TableHeaderVMEntity> parent = CriteriaList;
            var entity = new TableHeaderVMEntity(new TableHeaderEntity(id, CriteriaTextBox.Text, parent[0].Id, parent[0].Level));
            entity.Add(new TableHeaderVMEntity(new TableHeaderEntity(id+1, "基準値", entity.Id, entity.Level)));
            entity.Add(new TableHeaderVMEntity(new TableHeaderEntity(id + 2, "公差", entity.Id, entity.Level)));
            parent[0].Add(entity);
            CriteriaDataGrid.ItemsSource = parent[0].Children;
            CriteriaTextBox.Text=string.Empty;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CriteriaList.Clear();
            CriteriaList.Add(new TableHeaderVMEntity(new TableHeaderEntity(111, "試験項目", false, true, true)));
            ContainerList.Clear();
            CriteriaDataGrid.ItemsSource = null;
            ContainerDataGrid.ItemsSource = null;
            tv1.ItemsSource = null;
            rtb.Document = null;

        }
    }
}
