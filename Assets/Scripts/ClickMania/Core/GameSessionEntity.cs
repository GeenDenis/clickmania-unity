using System.Threading;
using ClickMania.Core.Game;
using ClickMania.Score;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ClickMania.Core
{
    public class GameSessionEntity : ISession
    {
        private readonly GameObject _mainMenu;
        private readonly IGame _game;
        private readonly IScore _score;
        private readonly IMultiplier _multiplier;

        private CancellationToken _token;

        public GameSessionEntity(IGame game, CancellationToken token, IScore score, IMultiplier multiplier, GameObject mainMenu)
        {
            _game = game;
            _token = token;
            _score = score;
            _multiplier = multiplier;
            _mainMenu = mainMenu;
        }

        public void StartSession()
        {
            _score.Reset();
            _multiplier.Set(1);
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
            _multiplier.Set(_multiplier.Value + 1);
            _game.StartGame(8, 8);
            SessionLoop();
        }

        private void EndSession()
        {
            _mainMenu.SetActive(true);
        }
    }
}