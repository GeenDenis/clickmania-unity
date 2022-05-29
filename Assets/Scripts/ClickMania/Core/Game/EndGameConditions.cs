using ClickMania.Core.Areas;

namespace ClickMania.Core.Game
{
    public class EndGameConditions : IEndGameConditions
    {
        private readonly IArea _area;
        private readonly IGame _game;

        public EndGameConditions(IArea area, IGame game)
        {
            _area = area;
            _game = game;
        }

        public void Check()
        {
            if (CheckEndGame() == false) return;
            
            _game.EndGame();
        }

        private bool CheckEndGame()
        {
            var blocks = _area.GetAllBlocks();
            for (int i = 0; i < blocks.Length; i++)
            {
                if (blocks[i].Group.Length > 1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}