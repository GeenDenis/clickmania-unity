using System.Collections.Generic;
using ClickMania.Colors;
using ClickMania.Core.Areas;
using ClickMania.Core.Areas.Search;
using ClickMania.View.Block;
using ClickMania.View.Position;
using ClickMania.View.Spawn;

namespace ClickMania.View
{
    public class GameView : IGameView
    {
        private readonly IArea _area;
        private readonly ISpawnBlock _blockViewSpawner;
        private readonly IConvertPosition _positionConverter;
        private readonly IFindBlock _blockFinder;
        private readonly ColorPalette _colorPalette;

        private List<IBlockView> _blockViews;
        
        public GameView(IArea area, ISpawnBlock blockViewSpawner, IConvertPosition positionConverter, IFindBlock blockFinder, ColorPalette colorPalette)
        {
            _area = area;
            _blockViewSpawner = blockViewSpawner;
            _positionConverter = positionConverter;
            _colorPalette = colorPalette;
            _blockFinder = blockFinder;

            _blockViews = new List<IBlockView>();
        }

        public void SpawnBlockViews()
        {
            var blocks = _area.GetAllBlocks();
            for (int i = 0; i < blocks.Length; i++)
            {
                var block = blocks[i];
                var spawnPosition = _positionConverter.ToVector2(block.Row, block.Column);
                var blockView = _blockViewSpawner.Spawn(blocks[i].ID, spawnPosition);
                blockView.SetColor(_colorPalette.GetColor(block.Color));
                _blockViews.Add(blockView);
            }
        }
        
        public void Update()
        {
            var blockViews = _blockViews.ToArray();
            for (int i = 0; i < blockViews.Length; i++)
            {
                var blockView = blockViews[i];
                if (_blockFinder.TryFindBlockInArea(blockView.BlockID, out var block) == false)
                {
                    _blockViews.Remove(blockView);
                    blockView.Destroy();
                    continue;
                }

                var blockPosition = _positionConverter.ToVector2(block.Row, block.Column);
                blockView.SetPosition(blockPosition);
            }
        }
        
        public void Clear()
        {
            var blockViews = _blockViews.ToArray();
            for (int i = 0; i < blockViews.Length; i++)
            {
                blockViews[i].Destroy();
            }
            _blockViews.Clear();
        }
    }
}