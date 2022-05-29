using ClickMania.Core.Areas;
using SDK.Debug;

namespace ClickMania.Core.Blocks.Destroy
{
    public class BlockDestroyer : IDestroyBlock
    {
        private readonly IAreaEditor _area;

        public BlockDestroyer(IAreaEditor area)
        {
            _area = area;
        }

        public void DestroyBlock(int row, int column)
        {
            if (_area.Cells[row, column] is null)
            {
                CustomDebug.LogError($"Block at position [{row}, {column}] does not exist");
                return;
            }
            
            _area.SetBlock(row, column, null);
        }
    }
}