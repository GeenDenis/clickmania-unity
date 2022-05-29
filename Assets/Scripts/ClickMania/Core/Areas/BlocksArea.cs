using System.Collections.Generic;
using ClickMania.Core.Blocks;

namespace ClickMania.Core.Areas
{
    public class BlocksArea : IAreaEditor, IAreaParameters
    {
        public IBlock[,] Cells { get; private set; }
        public int RowCount { get; private set; }
        public int ColumnCount { get; private set; }

        public BlocksArea()
        {
            SetSize(0, 0);
        }
        
        public IBlock[] GetAllBlocks()
        {
            var blocks = new List<IBlock>();
            for (var row = 0; row < RowCount; row++)
            {
                for (var column = 0; column < RowCount; column++)
                {
                    if(Cells[row, column] is null) continue;
                    blocks.Add(Cells[row, column]);
                }
            }
            return blocks.ToArray();
        }

        public void SetBlock(int row, int column, IBlock block)
        {
            Cells[row, column] = block;
        }

        public void SetSize(int rowCount, int columnCount)
        {
            Cells = new IBlock[rowCount, columnCount];
            RowCount = rowCount;
            ColumnCount = columnCount;
        }
        
        public bool InsideAreaCheck(int row, int column)
        {
            return RowCount > row
                   && 0 <= row
                   && ColumnCount > column
                   && 0 <= column;
        }
    }
}