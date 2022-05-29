using ClickMania.Core.Areas;

namespace ClickMania.Core.Features.CleanEmptyColumns
{
    public class CleaningEmptyColumnsFeature : ICleanEmptyCollumns
    {
        private readonly IArea _area;

        public CleaningEmptyColumnsFeature(IArea area)
        {
            _area = area;
        }

        public void Execute()
        {
            var toMoveFlag = 0;
            for (int i = 0; i < _area.ColumnCount; i++)
            {
                if (ColumnIsEmpty(i)) continue;
                if (i != toMoveFlag) MoveColumn(i, toMoveFlag);
                toMoveFlag++;
            }
        }

        private void MoveColumn(int from, int to)
        {
            for (int i = 0; i < _area.RowCount; i++)
            {
                if(_area.Cells[i, from] is null) continue;
                _area.Cells[i, from].Move(i, to);
            }
        }

        private bool ColumnIsEmpty(int columnIndex)
        {
            if (0 > columnIndex || columnIndex >= _area.ColumnCount) return true;
            
            for (int i = 0; i < _area.RowCount; i++)
            {
                if(_area.Cells[i, columnIndex] is null) continue;
                return false;
            }

            return true;
        }
    }
}