using ClickMania.Core.Areas;
using ClickMania.Core.Areas.Search;

namespace ClickMania.Core.Blocks.BlockGroupUpdating
{
    public class BlockGroupUpdater : IUpdateBlockGroup
    {
        private readonly IArea _area;
        private readonly IFindBlockGroup _blockGroupFinder;

        public BlockGroupUpdater(IArea area, IFindBlockGroup blockGroupFinder)
        {
            _area = area;
            _blockGroupFinder = blockGroupFinder;
        }

        public void UpdateGroups()
        {
            var blocks = _area.GetAllBlocks();
            ResetGroupsInBlocks(blocks);
            
            for (int i = 0; i < blocks.Length; i++)
            {
                if(blocks[i].Group.Length > 1) continue;
                var group = _blockGroupFinder.FindGroup(blocks[i].Row, blocks[i].Column);
                if(group.Length < 2) continue;
                UpdateGroupInBlocks(group, group);
            }
        }

        private void ResetGroupsInBlocks(IBlock[] blocks)
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i].SetGroup(new[] {blocks[i]});
            }
        }

        private void UpdateGroupInBlocks(IBlock[] blocks, IBlock[] group)
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i].SetGroup(group);
            }
        }
    }
}