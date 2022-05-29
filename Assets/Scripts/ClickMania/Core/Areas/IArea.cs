using ClickMania.Core.Blocks;

namespace ClickMania.Core.Areas
{
    public interface IArea
    {
        IBlock[,] Cells { get; }
        int RowCount { get; }
        int ColumnCount { get; }

        IBlock[] GetAllBlocks();
        bool InsideAreaCheck(int row, int column);
    }
}