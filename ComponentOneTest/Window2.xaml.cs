﻿using C1.WPF.RichTextBox;
using ComponentOneTest.Serviceis.C1RichTextBox;
using ComponentOneTest.Entities;
using System.Windows;
using ComponentOneTest.Servicies.C1RichTextBox;
using System.Collections.ObjectModel;
using ComponentOneTest.ViewModelEntities;
using System.Text;

namespace ComponentOneTest
{
    /// <summary>
    /// Window2.xaml の相互作用ロジック
    /// </summary>
    public partial class Window2 : Window
    {
        public ObservableCollection<TableHeaderVMEntity> HeaderList 
            = new ObservableCollection<TableHeaderVMEntity>();

        public ObservableCollection<TableHeaderVMEntity> CriteriaList
             = new ObservableCollection<TableHeaderVMEntity>();

        private TsrDataCell? targetCell;

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
                    parent.Add(new TableHeaderVMEntity(entity, parent));
                }
            }
            return vmEntities;
        }
        public Window2()
        {
            InitializeComponent();
            rtb.ViewMode = TextViewMode.Draft;
            rtb.Zoom = 1.5;

            SpecSheetRadioButton.IsChecked = true;

            var list = TableHeaderFake.GetData(1);
            ConvertToVMEntities(list).ForEach(x => HeaderList.Add(x));

            ContainerDataGrid.ItemsSource = HeaderList.ToList().FindAll(x => x.Parent == 0);

            var list2 = TableHeaderFake.GetData(0);
            ConvertToVMEntities(list2).ForEach(x => CriteriaList.Add(x));
            CriteriaDataGrid.ItemsSource = CriteriaList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = null;
            targetCell = null;

            var tsrTable = RichTextBoxTools.CreateTable(
                HeaderList.ToList(),
                CriteriaList.ToList(),
                SpecSheetRadioButton.IsChecked);

            rtb.Document.Blocks.Clear();
            rtb.Document.Blocks.Add(tsrTable);
        }

        private void ContainerDataGrid_SelectedCellsChanged(object sender, 
            System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            if (ContainerDataGrid.SelectedItem is not TableHeaderVMEntity item) return;
            tv1.ItemsSource = item.Children;
        }

        private void GetDataButton_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = string.Empty;
            if(targetCell is not null ) targetCell.Background = null;
            if (rtb.Selection.Cells.Count() > 0)
            {
                if (rtb.Selection.Cells.First() is TsrDataCell cell)
                {
                    tb1.Text = cell.Conditions;
                    cell.Background = System.Windows.Media.Brushes.Red;
                    targetCell = cell;
                }
                else
                {
                    tb1.Text = rtb.Selection.Cells.First().GetType().ToString();
                }
            }
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
            var id = HeaderList.Count + 1;
            HeaderList.Add(new TableHeaderVMEntity(new TableHeaderEntity(id,ContainerTextBox.Text)));
            ContainerDataGrid.ItemsSource = HeaderList.ToList().FindAll(x => x.Parent == 0);
            ContainerTextBox.Text=string.Empty;
        }
        private void CriteriaButton_Click(object sender, RoutedEventArgs e)
        {
            if (CriteriaTextBox.Text is null) return;
            if (CriteriaTextBox.Text == string.Empty) return;

            // todo Criteria Sub Container の作り方検討
            var id = CriteriaList.Count * 100 + 1001;
            CriteriaList.Add( new TableHeaderVMEntity(
                new TableHeaderEntity(id, CriteriaTextBox.Text, 1000, 1)));
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            CriteriaDataGrid.ItemsSource = CriteriaList;
            CriteriaTextBox.Text=string.Empty;
        }

        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CriteriaList.Clear();
            HeaderList.Clear();
            CriteriaDataGrid.ItemsSource = null;
            ContainerDataGrid.ItemsSource = null;
            tv1.ItemsSource = null;
            rtb.Document = null;
            tb1.Text = null;
            targetCell=null;
        }
    }
}
