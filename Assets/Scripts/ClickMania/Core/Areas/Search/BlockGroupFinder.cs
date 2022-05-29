using System.Collections.Generic;
using System.Linq;
using ClickMania.Core.Blocks;
using Extensions;

namespace ClickMania.Core.Areas.Search
{
    public class BlockGroupFinder : IFindBlockGroup
    {
        private readonly IArea _area;

        private readonly Queue<IBlock> _toCheckBuffer;
        private int _searchColorID;
        
        public BlockGroupFinder(IArea area)
        {
            _toCheckBuffer = new Queue<IBlock>();
            _area = area;
        }

        public IBlock[] FindGroup(int row, int column)
        {
            var foundGroup = new List<IBlock>();
            _toCheckBuffer.Clear();
            
            SearchInit(row, column);
            while (_toCheckBuffer.Count > 0)
            {
                var checkingBlock = _toCheckBuffer.Dequeue();
                foundGroup.Add(checkingBlock);
                var identicalNeighboringBlocks = GetIdenticalNeighboringBlocks(checkingBlock.Row, checkingBlock.Column);
                var nonRecurringIdenticalNeighboringBlocks = identicalNeighboringBlocks
                    .RemoveList(foundGroup)
                    .RemoveList(_toCheckBuffer.ToList());
                AddBlocksToCheck(nonRecurringIdenticalNeighboringBlocks);
            }

            return foundGroup.ToArray();
        }

        private void SearchInit(int row, int column)
        {
            var firstBlock = _area.Cells[row, column];
            _searchColorID = firstBlock.Color;
            _toCheckBuffer.Enqueue(firstBlock);
        }

        private List<IBlock> GetIdenticalNeighboringBlocks(int row, int column)
        {
            var neighboringBlocks = new List<IBlock>(4);
            if (TryGetIdenticalBlock(row + 1, column, out var upBlock)) neighboringBlocks.Add(upBlock);
            if (TryGetIdenticalBlock(row - 1, column, out var downBlock)) neighboringBlocks.Add(downBlock);
            if (TryGetIdenticalBlock(row, column + 1, out var rightBlock)) neighboringBlocks.Add(rightBlock);
            if (TryGetIdenticalBlock(row, column - 1, out var leftBlock)) neighboringBlocks.Add(leftBlock);
            return neighboringBlocks;
        }

        private bool TryGetIdenticalBlock(int row, int column, out IBlock block)
        {
            block = default;

            if (!_area.InsideAreaCheck(row, column) || _area.Cells[row, column] is null || _area.Cells[row, column].Color != _searchColorID)
            {
                return false;
            }
            
            block = _area.Cells[row, column];
            return true;
        }

        private void AddBlocksToCheck(List<IBlock> blocks)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                _toCheckBuffer.Enqueue(blocks[i]);
            }
        }
    }
}