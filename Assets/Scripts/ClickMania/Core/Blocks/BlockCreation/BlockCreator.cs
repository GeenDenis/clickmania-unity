using ClickMania.Core.Areas;
using ClickMania.Core.Blocks;
using ClickMania.Core.Blocks.BlockCreation;
using ClickMania.Core.Blocks.Destroy;
using ClickMania.Core.Blocks.Move;

namespace ClickMania.Blocks.BlockCreation
{
    public class BlockCreator : IBlockCreator
    {
        private readonly IAreaEditor _area;
        private readonly IMoveBlock _blockMover;
        private readonly IDestroyBlock _blockDestroyer;

        private int _lastBlockId;
        
        public BlockCreator(IAreaEditor area, IMoveBlock blockMover, IDestroyBlock blockDestroyer)
        {
            _area = area;
            _blockMover = blockMover;
            _blockDestroyer = blockDestroyer;
        }

        public IBlock CreateBlock(int colorId, int rowIndex, int columnIndex)
        {
            var block = new Block(_lastBlockId, _blockMover, _blockDestroyer, rowIndex, columnIndex, colorId);
            _area.SetBlock(rowIndex, columnIndex, block);
            _lastBlockId++;
            return block;
        }
    }
}