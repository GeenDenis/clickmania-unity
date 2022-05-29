using ClickMania.Core.Areas;
using ClickMania.Core.Blocks;

namespace ClickMania.Core.Features.BlockFalling
{
    public class BlocksFallingFeature : IFallBlocks
    {
        private readonly IArea _area;

        public BlocksFallingFeature(IArea area)
        {
            _area = area;
        }
        
        public void Execute()
        {
            var blocks = _area.GetAllBlocks();
            for (int i = 0; i < blocks.Length; i++)
            {
                FallBlock(blocks[i]);
            }
        }

        private void FallBlock(IBlock block)
        {
            var fallToRow = block.Row;
            for(var row = fallToRow - 1; row >= 0; row--)
            {
                if (_area.Cells[row, block.Column] is null)
                {
                    fallToRow = row;
                    continue;
                }
                
                break;
            }

            if (fallToRow == block.Row)
            {
                return;
            }
            
            block.Move(fallToRow, block.Column);
        }
    }
}