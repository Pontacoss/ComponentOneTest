using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.ComponentOne.RichTextBox
{
    internal static class HeaderFake
    {
        internal static List<HeaderEntity> GetData()
        {
            var result = new List<HeaderEntity>();

            var entity = new HeaderEntity(null, 10, "1");
            result.Add(entity);
            result.Add(new HeaderEntity(entity, 11, "1-1"));
            result.Add(new HeaderEntity(entity, 12, "1-2"));

            var entity2 = new HeaderEntity(null, 20, "2");
            result.Add(entity2);

            var entity3 = new HeaderEntity(entity2, 21, "2-1");
            result.Add(entity3);

            result.Add(new HeaderEntity(entity3, 211, "2-1-1"));
            result.Add(new HeaderEntity(entity3, 212, "2-1-2"));


            result.Add(new HeaderEntity(entity2, 22, "2-2"));

            return result;
        }

        internal static List<HeaderEntity> GetData2()
        {
            var result = new List<HeaderEntity>();

            var entity = new HeaderEntity(null, 10, "1");
            result.Add(entity);

            var entity2 = new HeaderEntity(entity, 11, "1-1");
            result.Add(entity2);

            result.Add(new HeaderEntity(entity2, 111, "1-1-1"));
            result.Add(new HeaderEntity(entity2, 112, "1-1-2"));

            result.Add(new HeaderEntity(entity, 12, "1-2"));
            result.Add(new HeaderEntity(entity, 13, "1-3"));
            result.Add(new HeaderEntity(entity, 14, "1-4"));


            return result;
        }
    }
}
