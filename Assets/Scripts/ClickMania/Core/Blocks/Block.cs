using ClickMania.Core.Blocks.Destroy;
using ClickMania.Core.Blocks.Move;

namespace ClickMania.Core.Blocks
{
    public class Block : IBlock
    {
        private readonly IMoveBlock _blockMover;
        private readonly IDestroyBlock _blockDestroyer;

        public int ID { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }
        public int Color { get; }
        public IBlock[] Group { get; private set; }

        public Block(int id, IMoveBlock blockMover, IDestroyBlock blockDestroyer, int row, int column, int color)
        {
            ID = id;
            _blockMover = blockMover;
            _blockDestroyer = blockDestroyer;
            Row = row;
            Column = column;
            Color = color;
        }

        public void Move(int row, int column)
        {
            _blockMover.MoveBlock(Row, Column, row, column);
            Row = row;
            Column = column;
        }

        public void Destroy()
        {
            _blockDestroyer.DestroyBlock(Row, Column);
        }

        public void SetGroup(IBlock[] group)
        {
            Group = group;
        }
    }
}