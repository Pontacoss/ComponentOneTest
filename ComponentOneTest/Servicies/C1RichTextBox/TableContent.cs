namespace ComponentOneTest.Servicies.C1RichTextBox
{
    public sealed class TableContent
    {
        public string TableName { get; }
        public IEnumerable<ITsrHeader>? RowHeaders { get; }
        public IEnumerable<ITsrHeader> ColumnHeaders { get; }

        public TableContent(string tableName,
            IList<ITsrHeader>? rowHeaders,
            IList<ITsrHeader> columnHeaders)
        {
            TableName = tableName;
            RowHeaders = rowHeaders;
            ColumnHeaders = columnHeaders;
        }
    }
}
