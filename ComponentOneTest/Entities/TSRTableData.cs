using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentOneTest.Entities
{
    public class TSRTableData
    {
        public int Id { get; }
        public Dictionary<int, string> Conditions { get; }
        = new Dictionary<int, string>();
        public double Value { get; }
        public double Tolereance { get; }
        public string MathSymbol { get; }
        public int TolereanceType { get; }
        public int TableId { get; }
        public TSRTableData(
            int id,
            Dictionary<int,string> conditions,
            double value,
            double tolerance,
            string mathSymbol,
            int toleranceType,
            int tableId)
        {
            Id = id;
            Conditions = conditions;
            Value = value;
            Tolereance = tolerance;
            MathSymbol = mathSymbol;
            TolereanceType = toleranceType;
            TableId = tableId;
        }
        public string DisplayValue(int i)
        {
            if (i == 0)
            {
                return string.Format(Value + MathSymbol + Tolereance + TolereanceType);
            }
            else
            {
                if (TolereanceType == 0)
                    return string.Format(
                        Value * (1 - Tolereance / 100) + "～" + Value + "～" + Value * (1 + Tolereance / 100));
                else
                    return string.Format(
                        Value * (1 - Tolereance) + "～" + Value + "～" + Value * (1 + Tolereance));
            }
        }

        public string DisplayCondition()
        {
            return string.Join(",", Conditions.Select(x => x.Key + ":" + x.Value));
        }

    }
}
