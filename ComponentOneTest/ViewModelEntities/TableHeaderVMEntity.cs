using ComponentOneTest.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.ViewModelEntities
{
    public class TableHeaderVMEntity
    {
        private TableHeaderEntity _entity;
        public int Id =>_entity.Id;
        public int Parent =>_entity.Parent;
        public string? Name =>_entity.Name;
        public int Level
        {
            get => _entity.Level;
            set => _entity.Level = value;
        } 
        public int Span
        {
            get => _entity.Span;
            set => _entity.Span = value;
        }
        public bool IsTitleVisible
        {
            get => _entity.IsTitleVisible;
            set => _entity.IsTitleVisible = value;
        }
        public bool IsMeasurementItem => _entity.IsMeasurementItem;
        public bool IsRepeat
        {
            get => _entity.IsRepeat;
            set => _entity.IsRepeat = value;
        }
        public bool IsColumn
        {
            get => _entity.IsColumn;
            set => _entity.IsColumn = value;
        }
        public ObservableCollection<TableHeaderVMEntity> Children { get; private set; }
        = new ObservableCollection<TableHeaderVMEntity>();

        public TableHeaderVMEntity(TableHeaderEntity entity)
        {
            _entity = entity;
        }
        public TableHeaderVMEntity(TableHeaderEntity entity, TableHeaderVMEntity parent)
        {
            _entity = entity;
            _entity.Parent = parent.Id;
            _entity.Level = parent.Level + 1;
        }
        public TableHeaderVMEntity(string name, TableHeaderVMEntity parent)
        {
            var id = parent.Id * 100 + parent.Children.Count + 1;
            _entity=new TableHeaderEntity(id, name, parent.Id,parent.Level);
        }

        public void Add (TableHeaderVMEntity entity)
        {
            Children.Add(entity);
        }

        public TableHeaderEntity GetEntity()
        {
            return _entity;
        }
    }
}
