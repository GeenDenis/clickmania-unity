using ClickMania.Core.Areas;
using UnityEngine;

namespace ClickMania.View.Position
{
    public class PositionConverter : IConvertPosition
    {
        private readonly IArea _area;

        public PositionConverter(IArea area)
        {
            _area = area;
        }

        public Vector2 ToVector2(int row, int collumn)
        {
            GetOffsets(out float rowOffset, out float collumnOffset);
            return new Vector2(collumn - collumnOffset, row - rowOffset);
        }

        public void ToPosition(Vector2 position, out int row, out int collumn)
        {
            GetOffsets(out float rowOffset, out float collumnOffset);
            row = Mathf.RoundToInt(position.y + rowOffset);
            collumn = Mathf.RoundToInt(position.x + collumnOffset);
        }

        private void GetOffsets(out float rowOffset, out float collumnOffset)
        {
            rowOffset = (_area.RowCount - 1) / 2f;
            collumnOffset = (_area.ColumnCount - 1) / 2f;
        }
    }
}