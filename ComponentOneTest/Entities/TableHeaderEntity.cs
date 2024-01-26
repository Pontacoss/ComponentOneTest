namespace ComponentOneTest.Entities
{
    public sealed class TableHeaderEntity
    {
        public int Id { get; }
        public int Parent { get; }
        public string? Value { get; }
        public string Title { get; } = "";
        public int Level { get; }
        public bool IsTitleVisible { get; }
        public bool IsMeasurementItem { get; }

        public TableHeaderEntity(
            TableHeaderEntity? parent, int id,  string? value)
        {
            Id = id;
            Value = value;
            Parent = parent != null ? parent.Id : 0;
            Level = parent != null ? parent.Level + 1 : 0;
        }
        public TableHeaderEntity(
            int id, string title, bool isTitleVisible,bool isMeasurementItem)
        {
            Id = id;
            Title = title;
            Parent = 0;
            Level = 0;
            IsTitleVisible = isTitleVisible;
            IsMeasurementItem = isMeasurementItem;
        }
    }
}
