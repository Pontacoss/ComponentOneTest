﻿using ComponentOneTest.Entities;

namespace ComponentOneTest.Servicies.TableData
{
    public class BlockContainer(TableHeaderEntity headerEntity) : ContainerBase(headerEntity), IContainer
    {
        public SpanCounter GetHeaderWidth(SpanCounter spanCounter)
        {
            return new SpanCounter
            {
                BlockSpan = Math.Max(spanCounter.BlockSpan, GetSpanSum()),
                RepeatSpan = spanCounter.RepeatSpan
            };
        }
        public int SetUnitSize(SpanCounter spanCounter, int repaetCellHeight)
        {
            _unitSize = spanCounter.RepeatSpan;
            return repaetCellHeight;
        }

        public int SetRepeat(int repeat)
        {
            _repeat = 1;
            return repeat;
        }
        public string GetConditionStringByContainer(int Index)
        {
            return GetConditionStringRecursive(Index, _unitSize);
        }

        public override string DisplayName()
        {
            return "[" + Name?.Replace("\n", "-") + "] : ";
        }

        public int CreateColumnHeaders(List<CellEntity> list, int rowIndex,int columnIndex)
        {
            throw new NotImplementedException();
        }

        public int CreateColumnContainerTitles(List<CellEntity> list, int rowIndex,int columnIndex)
        {
            return 0;
        }
    }
}
