namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public sealed class TableContent
    {
        public string TableName { get; }
        public IEnumerable<ITableHeader> RowHeaders { get; }
        public IEnumerable<ITableHeader> ColumnHeaders { get; }

        public TableContent(string tableName,
            IList<ITableHeader> rowHeaders,
            IList<ITableHeader> columnHeaders)
        {
            TableName = tableName;
            RowHeaders = rowHeaders;
            ColumnHeaders = columnHeaders;
        }
    }
}
