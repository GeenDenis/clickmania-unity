using System;
using ClickMania.Core.Areas;
using ClickMania.Core.Blocks.BlockCreation;
using ClickMania.Core.Blocks.BlockGroupUpdating;
using SDK.CameraComponents;

namespace ClickMania.Core.Game
{
    public class GameEntity : IGame
    {
        private IAreaParameters _area;
        private ISpawnBlock _blockSpawner;
        private IUpdateBlockGroup _blockGroupUpdater;
        private ICameraWidthRegulation _cameraWidthRegulator;

        public GameState State { get; private set; }

        public event Action OnStart = delegate { }; //TODO: Убрать после теста вьюва
        
        public GameEntity(IAreaParameters area, ISpawnBlock blockSpawner, IUpdateBlockGroup blockGroupUpdater, ICameraWidthRegulation cameraWidthRegulator)
        {
            _area = area;
            _blockSpawner = blockSpawner;
            _blockGroupUpdater = blockGroupUpdater;
            _cameraWidthRegulator = cameraWidthRegulator;

            State = GameState.NotStarted;
        }

        public void StartGame(int rowCount, int columnCount)
        {
            _cameraWidthRegulator.SetWidth(columnCount + 3);
            _area.SetSize(rowCount, columnCount);
            _blockSpawner.SpawnBlocks(3);
            _blockGroupUpdater.UpdateGroups();
            State = GameState.Started;
            
            OnStart.Invoke(); //TODO: Убрать после теста вьюва
        }

        public void StopGame()
        {
            State = GameState.NotStarted;
        }
        
        public void EndGame()
        {
            State = _area.GetAllBlocks().Length == 0
                ? GameState.Win
                : GameState.Lose;
        }
    }
}