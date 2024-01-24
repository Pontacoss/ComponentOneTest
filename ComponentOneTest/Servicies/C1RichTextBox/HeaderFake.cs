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
                var entity = new TableHeaderEntity(10, "title 1", true);
                result.Add(entity);
                result.Add(new TableHeaderEntity(entity, 11, "1-1"));
                result.Add(new TableHeaderEntity(entity, 12, "1-2"));
            }
            else if (selector == 1)
            {
                var entity2 = new TableHeaderEntity(20, "title 2", true);
                result.Add(entity2);

                var entity3 = new TableHeaderEntity(entity2, 21, "2-1");
                result.Add(entity3);

                result.Add(new TableHeaderEntity(entity3, 211, "2-1-1"));
                result.Add(new TableHeaderEntity(entity3, 212, "2-1-2"));

                result.Add(new TableHeaderEntity(entity2, 22, "2-2"));
            }
            else if (selector == 2)
            {
                var entity = new TableHeaderEntity(10, "title 3", false);
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
                var entity = new TableHeaderEntity(10, "title 1", true);
                result.Add(entity);

                var entity3 = new TableHeaderEntity(entity, 11, "1-1");
                result.Add(entity3);
                result.Add(new TableHeaderEntity(entity3, 111, "1-1-1"));
                result.Add(new TableHeaderEntity(entity3, 112, "1-1-2"));
                result.Add(new TableHeaderEntity(entity, 12, "1-2"));

                var entity2 = new TableHeaderEntity(20, "title 2", false);
                result.Add(entity2);
                result.Add(new TableHeaderEntity(entity2, 21, "2-1"));
                result.Add(new TableHeaderEntity(entity2, 22, "2-2"));
            }
            else
            {
                var entity = new TableHeaderEntity(10, "title 1", true);
                result.Add(entity);

                var entity3 = new TableHeaderEntity(entity, 11, "1-1");
                result.Add(entity3);
                result.Add(new TableHeaderEntity(entity3, 111, "1-1-1"));
                result.Add(new TableHeaderEntity(entity3, 112, "1-1-2"));
                result.Add(new TableHeaderEntity(entity, 12, "1-2"));

                var entity2 = new TableHeaderEntity(20, "title 2", false);
                result.Add(entity2);
                result.Add(new TableHeaderEntity(entity2, 21, "2-1"));
                result.Add(new TableHeaderEntity(entity2, 22, "2-2"));

                var entity4 = new TableHeaderEntity(40, "title 4", true);
                result.Add(entity4);
                result.Add(new TableHeaderEntity(entity4, 41, "4-1"));
                result.Add(new TableHeaderEntity(entity4, 42, "4-2"));
            }
            return result;
        }
    }
}
