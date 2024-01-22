namespace ComponentOneTest.Entities
{
    public sealed class HeaderEntity
    {
        public int Id { get; }
        public int Parent { get; }
        public string Value { get; }
        public int Level { get; }

        public HeaderEntity(HeaderEntity? parent, int id, string value)
        {
            Id = id;
            Value = value;
            Parent = parent != null ? parent.Id : 0;
            Level = parent != null ? parent.Level + 1 : 0;
        }
    }
}
