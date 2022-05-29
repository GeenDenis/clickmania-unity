using System.Collections.Generic;
using ClickMania.Colors;
using ClickMania.Core.Areas;
using ClickMania.Core.Areas.Search;
using ClickMania.Core.Blocks;
using ClickMania.View.Block;
using ClickMania.View.Data;
using ClickMania.View.Position;
using ClickMania.View.Spawn;

namespace ClickMania.View
{
    public class GameView : IGameView
    {
        private readonly IArea _area;
        private readonly ViewDataUpdater _viewDataUpdater;
        private readonly ISpawnBlock _blockViewSpawner;
        private readonly IConvertPosition _positionConverter;
        private readonly ColorPalette _colorPalette;
        
        public GameView(IArea area, ISpawnBlock blockViewSpawner, IConvertPosition positionConverter, IFindBlock blockFinder, ColorPalette colorPalette, ViewDataUpdater viewDataUpdater)
        {
            _area = area;
            _blockViewSpawner = blockViewSpawner;
            _positionConverter = positionConverter;
            _colorPalette = colorPalette;
            _viewDataUpdater = viewDataUpdater;
        }

        public void SpawnBlockViews()
        {
            var blocks = _area.GetAllBlocks();
            var blockViews = new List<IBlockView>();
            for (int i = 0; i < blocks.Length; i++)
            {
                var block = blocks[i];
                var spawnPosition = _positionConverter.ToVector2(block.Row, block.Column);
                var blockView = _blockViewSpawner.Spawn(blocks[i].ID, spawnPosition);
                blockView.SetColor(_colorPalette.GetColor(block.Color));
                blockViews.Add(blockView);
            }
            _viewDataUpdater.Init(blockViews.ToArray());
        }
        
        public void Update()
        {
            _viewDataUpdater.Update();
            var blockViews = _viewDataUpdater.GetAllViews();
            var viewsForDestroy = _viewDataUpdater.ViewsForDestroy;
            var viewsForMove = _viewDataUpdater.ViewsForMove;

            DestroyViews(viewsForDestroy);
            MoveViews(blockViews, viewsForMove);
        }
        
        public void Clear()
        {
            var blockViews = _viewDataUpdater.GetAllViews();
            for (int i = 0; i < blockViews.Length; i++)
            {
                blockViews[i].Destroy();
            }
        }

        private void DestroyViews(IBlockView[] blockViews)
        {
            for (int i = 0; i < blockViews.Length; i++)
            {
                blockViews[i].DestroyImmediate();
            }
        }
        
        private void MoveViews(IBlockView[] blockViews, Dictionary<IBlockView, IBlock> moveData)
        {
            for (int i = 0; i < blockViews.Length; i++)
            {
                var blockView = blockViews[i];
                if(moveData.ContainsKey(blockView) == false) continue;

                var block = moveData[blockView];
                var blockPosition = _positionConverter.ToVector2(block.Row, block.Column);
                blockView.SetPosition(blockPosition);
            }
        }
    }
}