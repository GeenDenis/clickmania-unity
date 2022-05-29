using ClickMania.Core.Blocks;

namespace ClickMania.Core.Areas
{
    public interface IAreaEditor : IArea
    {
        void SetBlock(int row, int column, IBlock block);
    }
}