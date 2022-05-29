using ClickMania.View.Block;

namespace ClickMania.Core.Blocks
{
    public interface IBlock
    {
        int ID { get; }
        int Row { get; }
        int Column { get; }
        int Color { get; }
        IBlock[] Group { get; }
        
        void Move(int row, int column);
        void Destroy();

        void SetGroup(IBlock[] group);
    }
}