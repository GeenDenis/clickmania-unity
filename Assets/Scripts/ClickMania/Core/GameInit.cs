using System.Threading;
using ClickMania.Blocks.BlockCreation;
using ClickMania.Colors;
using ClickMania.Core.Areas;
using ClickMania.Core.Areas.Search;
using ClickMania.Core.Blocks.BlockCreation;
using ClickMania.Core.Blocks.BlockGroupUpdating;
using ClickMania.Core.Blocks.Destroy;
using ClickMania.Core.Blocks.Move;
using ClickMania.Core.Features.BlockFalling;
using ClickMania.Core.Features.CleanEmptyColumns;
using ClickMania.Core.Game;
using ClickMania.Score;
using ClickMania.View;
using ClickMania.View.Animations;
using ClickMania.View.Block;
using ClickMania.View.Position;
using ClickMania.View.Score;
using ClickMania.View.Spawn;
using SDK.CameraComponents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ClickMania.Core
{
    public class GameInit : MonoBehaviour
    {
        [SerializeField] private ScoreView _scoreView; //TODO: отрефакторить UI
        [SerializeField] private ScoreMultiplierView _scoreMultiplierView; //TODO: отрефакторить UI
        [SerializeField] private GameObject _mainScreen; //TODO: отрефакторить UI
        [SerializeField] private GameObject _gameScreen; //TODO: отрефакторить UI
        [SerializeField] private Camera _camera;
        [SerializeField] private BlockView _blockViewPrefab;
        [SerializeField] private ColorPalette _colorPalette;

        private ISession _gameSession;
        private GameView _gameView;

        private void Awake()
        {
            var token = new CancellationTokenSource();

            var scoreMultiplier = new ScoreMultiplier();
            _scoreMultiplierView.Init(scoreMultiplier);
            
            var score = new ScoreCounter(scoreMultiplier, 3);
            _scoreView.Init(score);

            var area = new BlocksArea();
            var blockMover = new BlockMover(area);
            var blockDestroyer = new BlockDestroyer(area);
            var blockCreator = new BlockCreator(area, blockMover, blockDestroyer);
            var blockSpawner = new BlockSpawner(area, blockCreator);
            var blocksFallEntity = new BlocksFallingFeature(area);
            var emptyColumnsCleaner = new CleaningEmptyColumnsFeature(area);
            var blockFinder = new BlockFinder(area);
            var blockGroupFinder = new BlockGroupFinder(area);
            var blockGroupUpdater = new BlockGroupUpdater(area, blockGroupFinder);
            var cameraWidthRegulator = new CameraWidthRegulator(_camera);
            var game = new GameEntity(
                area, 
                blockSpawner, 
                blockGroupUpdater, 
                cameraWidthRegulator);
            var endGameChecker = new EndGameConditions(area, game);
            var turn = new TurnEntity(
                blocksFallEntity, 
                emptyColumnsCleaner, 
                endGameChecker, 
                blockGroupUpdater, 
                blockFinder,
                score);
            var positionConverter = new PositionConverter(area);
            var blockViewSpawner = new BlockViewSpawner(_blockViewPrefab, turn);
            var destroyAnimation = new DestroyAnimation();
            var fallAnimation = new FallAnimation(area, positionConverter);
            var moveAnimation = new MoveAnimation(area, positionConverter);
            _gameView = new GameView(
                area, 
                blockViewSpawner, 
                positionConverter, 
                blockFinder, 
                _colorPalette,
                destroyAnimation,
                moveAnimation,
                fallAnimation);
            _gameSession = new GameSessionEntity(game, token.Token, score, scoreMultiplier, _mainScreen);

            game.OnStart += () =>
            {
                _gameView.SpawnBlockViews();
                _scoreView.UpdateView();
                _scoreMultiplierView.UpdateView();
            };
            turn.OnTurn += () =>
            {
                _gameView.Update();
                _scoreView.UpdateView();
            };
        }

        [Button]
        public void StartGame()
        {
            _mainScreen.SetActive(false);
            _gameScreen.SetActive(true);
            _gameView.Clear();
            _gameSession.StartSession();
        }
    }
}