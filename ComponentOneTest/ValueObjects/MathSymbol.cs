using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.ValueObjects
{
    public sealed class MathSymbol : ValueObject<MathSymbol>
    {
        public static MathSymbol PlusMinus = new MathSymbol(0);
        public static MathSymbol Plus = new MathSymbol(1);
        public static MathSymbol Minus = new MathSymbol(2);

        public MathSymbol(int value)
        {
            //Value = value;
        }
        public string Value { get; }
        public override string ToString() => Value.ToString();
        public string DisplayValue => Value.ToString().PadLeft(6, '0');
        protected override bool EqualsCore(MathSymbol other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
