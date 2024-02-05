namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public sealed class TableContent
    {
        public string TableName { get; }
        public IEnumerable<HeaderBase>? RowHeaders { get; }
        public IEnumerable<HeaderBase> ColumnHeaders { get; }

        public TableContent(string tableName,
            IList<HeaderBase>? rowHeaders,
            IList<HeaderBase> columnHeaders)
        {
            TableName = tableName;
            RowHeaders = rowHeaders;
            ColumnHeaders = columnHeaders;
        }
    }
}
