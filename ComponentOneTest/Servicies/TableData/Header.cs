using ComponentOneTest.Entities;

namespace ComponentOneTest.Servicies.TableData
{
    public sealed class Header : HeaderBase
    {
        public Header(TableHeaderEntity headerEntity) : base(headerEntity) { }

        public override string DisplayName()
        {
            return "[" + Name + "]";
        }
    }
}
