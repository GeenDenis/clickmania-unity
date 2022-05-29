using ClickMania.Core.Blocks;

namespace ClickMania.Core.Areas.Search
{
    public interface IFindBlock
    {
        bool TryFindBlockInArea(int blockID, out IBlock foundBlock);
    }
}