using ClickMania.Core.Blocks;

namespace ClickMania.Core.Areas.Search
{
    public class BlockFinder : IFindBlock
    {
        private readonly IArea _area;

        public BlockFinder(IArea area)
        {
            _area = area;
        }

        public bool TryFindBlockInArea(int blockID, out IBlock foundBlock)
        {
            foundBlock = default;
            var blocks = _area.Cells;
            var rowCount = _area.RowCount;
            var columnCount = _area.ColumnCount;
            for (int i = 0; i < rowCount; i++)
            for (int j = 0; j < columnCount; j++)
            {
                var block = blocks[i, j];
                if(block is null) continue;
                if(block.ID != blockID) continue;
                foundBlock = block;
                return true;
            }

            return false;
        }
    }
}