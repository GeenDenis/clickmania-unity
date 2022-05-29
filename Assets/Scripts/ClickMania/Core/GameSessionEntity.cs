using System.Threading;
using ClickMania.Core.Game;
using Cysharp.Threading.Tasks;

namespace ClickMania.Core
{
    public class GameSessionEntity : ISession
    {
        private IGame _game;

        private CancellationToken _token;

        public GameSessionEntity(IGame game, CancellationToken token)
        {
            _game = game;
            _token = token;
        }

        public void StartSession()
        {
            _game.StartGame(8, 8);
            SessionLoop();
        }

        private async void SessionLoop()
        {
            await UniTask.WaitWhile(() => _game.State == GameState.Started, cancellationToken: _token);

            if (_game.State == GameState.Win)
            {
                ContinueSession();
                return;
            }

            EndSession();
        }

        private void ContinueSession()
        {
            _game.StartGame(8, 8);
            SessionLoop();
        }

        private void EndSession()
        {
            
        }
    }
}