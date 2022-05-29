using ClickMania.Core.Areas;
using Random = UnityEngine.Random;

namespace ClickMania.Core.Blocks.BlockCreation
{
    public class BlockSpawner : ISpawnBlock
    {
        private readonly IArea _area;
        private readonly IBlockCreator _blockCreator;

        public BlockSpawner(IArea area, IBlockCreator blockCreator)
        {
            _area = area;
            _blockCreator = blockCreator;
        }

        public void SpawnBlocks(int colorCount)
        {
            for (int rowIndex = 0; rowIndex < _area.RowCount; rowIndex++)
            for (int columnIndex = 0; columnIndex < _area.ColumnCount; columnIndex++)
            {
                var colorId = Random.Range(0, colorCount);
                _blockCreator.CreateBlock(colorId, rowIndex, columnIndex);
            }
        }
    }
}