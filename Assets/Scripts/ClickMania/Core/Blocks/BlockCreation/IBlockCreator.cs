namespace ClickMania.Core.Blocks.BlockCreation
{
    public interface IBlockCreator
    {
        IBlock CreateBlock(int colorId, int rowIndex, int columnIndex);
    }
}