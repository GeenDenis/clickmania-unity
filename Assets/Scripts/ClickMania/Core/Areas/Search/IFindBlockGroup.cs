using ClickMania.Core.Blocks;

namespace ClickMania.Core.Areas.Search
{
    public interface IFindBlockGroup
    {
        IBlock[] FindGroup(int row, int column);
    }
}