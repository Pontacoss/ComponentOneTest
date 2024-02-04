﻿namespace ComponentOneTest.Entities
{
    public sealed class TableHeaderEntity
    {
        public int Id { get; }
        public int Parent { get; }
        public string? Name { get; }
        public int Level { get; }
        public bool IsTitleVisible { get; }
        public bool IsMeasurementItem { get; }
        public bool IsRepeat { get; }

        /// <summary>
        /// Headerクラス用のコンストラクタ
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public TableHeaderEntity(
            TableHeaderEntity? parent, int id, string? name)
        {
            Id = id;
            Name = name;
            Parent = parent != null ? parent.Id : 0;
            Level = parent != null ? parent.Level + 1 : 0;
        }

        /// <summary>
        /// HeaderContainerクラス用のコンストラクタ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="isTitleVisible"></param>
        /// <param name="isMeasurementItem"></param>
        public TableHeaderEntity(
            int id, string name, bool isTitleVisible, bool isMeasurementItem,bool isRepeart)
        {
            Id = id;
            Name = name;
            Parent = 0;
            Level = 0;
            IsTitleVisible = isTitleVisible;
            IsMeasurementItem = isMeasurementItem;
            IsRepeat = isRepeart;
        }
    }
}
