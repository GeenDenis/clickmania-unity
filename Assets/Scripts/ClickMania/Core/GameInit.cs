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
using ClickMania.View;
using ClickMania.View.Animations;
using ClickMania.View.Block;
using ClickMania.View.Data;
using ClickMania.View.Position;
using ClickMania.View.Spawn;
using SDK.CameraComponents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ClickMania.Core
{
    public class GameInit : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private BlockView _blockViewPrefab;
        [SerializeField] private ColorPalette _colorPalette;

        private ISession _gameSession;
        private GameView _gameView;

        private void Awake()
        {
            var token = new CancellationTokenSource();


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
                blockFinder);
            var positionConverter = new PositionConverter(area);
            var blockViewSpawner = new BlockViewSpawner(_blockViewPrefab, turn);
            var destroyAnimation = new DestroyAnimation();
            var fallAnimation = new FallAnimation(area, positionConverter);
            var moveAnimation = new DestroyAnimation();
            _gameView = new GameView(
                area, 
                blockViewSpawner, 
                positionConverter, 
                blockFinder, 
                _colorPalette,
                destroyAnimation,
                moveAnimation,
                fallAnimation);
            _gameSession = new GameSessionEntity(game, token.Token);

            game.OnStart += _gameView.SpawnBlockViews;
            turn.OnTurn += _gameView.Update;
        }

        [Button]
        public void StartGame()
        {
            _gameView.Clear();
            _gameSession.StartSession();
        }
    }
}