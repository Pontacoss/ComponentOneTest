using ComponentOneTest.Entities;

namespace ComponentOneTest.Serviceis.C1RichTextBox
{
    internal static class HeaderFake
    {
        internal static List<TableHeaderEntity> GetData(int selector)
        {
            var result = new List<TableHeaderEntity>();
            if (selector == 0)
            {
               

                var entity = new TableHeaderEntity(10, "試験項目\nトルクパターン", true, true);
                result.Add(entity);
                result.Add(new TableHeaderEntity(entity, 11, "基準値"));
                result.Add(new TableHeaderEntity(entity, 12, "公差"));

                //var entity1 = new TableHeaderEntity(20, "試験項目\nトルクパターン", true, true);
                //result.Add(entity1);
                result.Add(new TableHeaderEntity(entity, 21, "基準値"));
                result.Add(new TableHeaderEntity(entity, 22, "公差"));

            }
            else if (selector == 1)
            {
                var entity2 = new TableHeaderEntity(20, "試験条件1\n応荷重", true, true);
                result.Add(entity2);
                var entity3 = new TableHeaderEntity(entity2, 21, "AW0");
                result.Add(entity3);
                result.Add(new TableHeaderEntity(entity3, 211, "45%"));
                var entity31 = new TableHeaderEntity(entity2, 22, "AW3");
                result.Add(entity31);
                result.Add(new TableHeaderEntity(entity31, 221, "45%"));
                result.Add(new TableHeaderEntity(entity31, 231, "75%"));

                var entity4 = new TableHeaderEntity(30, "試験条件2\n車輪径", true, true);
                result.Add(entity4);
                result.Add(new TableHeaderEntity(entity4, 311, "820"));
                result.Add(new TableHeaderEntity(entity4, 312, "860"));

                var entity5 = new TableHeaderEntity(40, "試験条件3\nFM(Hz)", true, true);
                result.Add(entity5);
                result.Add(new TableHeaderEntity(entity5, 411, "10"));
                result.Add(new TableHeaderEntity(entity5, 412, "20"));
            }
            else if (selector == 2)
            {
                var entity = new TableHeaderEntity(10, "title 3", false, true);
                result.Add(entity);

                var entity2 = new TableHeaderEntity(entity, 11, "1-1");
                result.Add(entity2);

                result.Add(new TableHeaderEntity(entity2, 111, "1-1-1"));
                result.Add(new TableHeaderEntity(entity2, 112, "1-1-2"));

                result.Add(new TableHeaderEntity(entity, 12, "1-2"));
                result.Add(new TableHeaderEntity(entity, 13, "1-3"));
                result.Add(new TableHeaderEntity(entity, 14, "1-4"));
            }
            else if (selector == 3)
            {
                var entity = new TableHeaderEntity(10, "title 1", true, false);
                result.Add(entity);

                var entity3 = new TableHeaderEntity(entity, 11, "1-1");
                result.Add(entity3);
                result.Add(new TableHeaderEntity(entity3, 111, "1-1-1"));
                result.Add(new TableHeaderEntity(entity3, 112, "1-1-2"));
                result.Add(new TableHeaderEntity(entity, 12, "1-2"));

                var entity2 = new TableHeaderEntity(20, "title 2", false, true);
                result.Add(entity2);
                result.Add(new TableHeaderEntity(entity2, 21, "2-1"));
                result.Add(new TableHeaderEntity(entity2, 22, "2-2"));
            }
            else
            {
                var entity = new TableHeaderEntity(10, "title 1", true, false);
                result.Add(entity);

                var entity3 = new TableHeaderEntity(entity, 11, "1-1");
                result.Add(entity3);
                result.Add(new TableHeaderEntity(entity3, 111, "1-1-1"));
                result.Add(new TableHeaderEntity(entity3, 112, "1-1-2"));
                result.Add(new TableHeaderEntity(entity, 12, "1-2"));

                var entity2 = new TableHeaderEntity(20, "title 2", true, false);
                result.Add(entity2);
                result.Add(new TableHeaderEntity(entity2, 21, "2-1"));
                result.Add(new TableHeaderEntity(entity2, 22, "2-2"));

                var entity4 = new TableHeaderEntity(40, "title 4", true, true);
                result.Add(entity4);
                result.Add(new TableHeaderEntity(entity4, 41, "4-1"));
                result.Add(new TableHeaderEntity(entity4, 42, "4-2"));
            }
            return result;
        }
    }
}
