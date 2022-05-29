using ClickMania.Core.Areas;
using SDK.Debug;

namespace ClickMania.Core.Blocks.Move
{
    public class BlockMover : IMoveBlock
    {
        private readonly IAreaEditor _area;

        public BlockMover(IAreaEditor area)
        {
            _area = area;
        }

        public void MoveBlock(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (_area.Cells[fromRow, fromColumn] is null)
            {
                CustomDebug.LogError($"Block at position [{fromRow}, {fromColumn}] does not exist");
                return;
            }

            if (_area.Cells[toRow, toColumn] is null == false)
            {
                CustomDebug.LogError($"Position [{fromRow}, {fromColumn}] is occupied by another block.");
                return;
            }
            
            var block = _area.Cells[fromRow, fromColumn];
            _area.SetBlock(fromRow, fromColumn, null);
            _area.SetBlock(toRow, toColumn, block);
        }
    }
}