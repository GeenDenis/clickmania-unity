using System;
using ClickMania.Core.Areas;
using ClickMania.Score;

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
                throw new Exception($"Block at position [{row}, {column}] does not exist");
            }
            
            _area.SetBlock(row, column, null);
        }
    }
}