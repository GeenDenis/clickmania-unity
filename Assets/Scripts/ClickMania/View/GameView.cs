using System.Collections.Generic;
using ClickMania.Colors;
using ClickMania.Core.Areas;
using ClickMania.Core.Areas.Search;
using ClickMania.View.Animations;
using ClickMania.View.Block;
using ClickMania.View.Position;
using ClickMania.View.Spawn;
using Cysharp.Threading.Tasks;
using Extensions;

namespace ClickMania.View
{
    public class GameView : IGameView
    {
        private readonly IArea _area;
        private readonly ISpawnBlock _blockViewSpawner;
        private readonly IConvertPosition _positionConverter;
        private readonly IFindBlock _blockFinder;
        private readonly IAnimation<IBlockView[]> _destroyAnimation;
        private readonly IAnimation<IBlockView[]> _moveAnimation;
        private readonly IAnimation<IBlockView[]> _fallAnimation;
        private readonly ColorPalette _colorPalette;

        private List<IBlockView> _blockViews;
        
        public GameView(IArea area, ISpawnBlock blockViewSpawner, IConvertPosition positionConverter, IFindBlock blockFinder, ColorPalette colorPalette, IAnimation<IBlockView[]> destroyAnimation, IAnimation<IBlockView[]> moveAnimation, IAnimation<IBlockView[]> fallAnimation)
        {
            _blockViews = new List<IBlockView>();
            _area = area;
            _blockViewSpawner = blockViewSpawner;
            _positionConverter = positionConverter;
            _colorPalette = colorPalette;
            _destroyAnimation = destroyAnimation;
            _moveAnimation = moveAnimation;
            _fallAnimation = fallAnimation;
            _blockFinder = blockFinder;
        }

        public void SpawnBlockViews()
        {
            var blocks = _area.GetAllBlocks();
            _blockViews = new List<IBlockView>();
            for (int i = 0; i < blocks.Length; i++)
            {
                var block = blocks[i];
                var spawnPosition = _positionConverter.ToVector2(block.Row, block.Column);
                var blockView = _blockViewSpawner.Spawn(blocks[i].ID, spawnPosition);
                blockView.SetColor(_colorPalette.GetColor(block.Color));
                _blockViews.Add(blockView);
            }
        }
        
        public async void Update()
        {
            var viewsForDestroy = GetViewsForDestroy();
            _blockViews.RemoveList(viewsForDestroy);
            
            await _destroyAnimation.Start(viewsForDestroy.ToArray()).AsTask();
            _fallAnimation.Start(_blockViews.ToArray());
            //_moveAnimation.Start(_blockViews.ToArray());
        }
        
        public void Clear()
        {
            for (int i = 0; i < _blockViews.Count; i++)
            {
                _blockViews[i].Destroy();
            }
        }

        private List<IBlockView> GetViewsForDestroy()
        {
            var viewsForDestroy = new List<IBlockView>();
            for (int i = 0; i < _blockViews.Count; i++)
            {
                if(_blockFinder.TryFindBlockInArea(_blockViews[i].BlockID, out var block)) continue;
                viewsForDestroy.Add(_blockViews[i]);
            }
            return viewsForDestroy;
        }
    }
}