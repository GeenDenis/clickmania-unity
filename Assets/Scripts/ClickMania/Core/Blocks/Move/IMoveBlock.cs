namespace ClickMania.Core.Blocks.Move
{
    public interface IMoveBlock
    {
        void MoveBlock(int fromRow, int fromColumn, int toRow, int toColumn);
    }
}