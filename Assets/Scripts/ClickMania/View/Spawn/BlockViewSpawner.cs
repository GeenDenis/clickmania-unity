using ClickMania.Core.Game;
using ClickMania.View.Block;
using UnityEngine;

namespace ClickMania.View.Spawn
{
    public class BlockViewSpawner : ISpawnBlock
    {
        private readonly BlockView _blockViewPrefab;
        private readonly TurnEntity _turnEntity;

        public BlockViewSpawner(BlockView blockViewPrefab, TurnEntity turnEntity)
        {
            _blockViewPrefab = blockViewPrefab;
            _turnEntity = turnEntity;
        }

        public IBlockView Spawn(int blockID, Vector2 position)
        {
            var blockView = Object.Instantiate(_blockViewPrefab);
            blockView.Init(_turnEntity);
            blockView.SetBlockID(blockID);
            blockView.SetPosition(position);
            return blockView;
        }
    }
}