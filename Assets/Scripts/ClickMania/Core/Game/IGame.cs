namespace ClickMania.Core.Game
{
    public interface IGame
    {
        GameState State { get; }

        void StartGame(int rowCount, int columnCount);
        void EndGame();
    }
}