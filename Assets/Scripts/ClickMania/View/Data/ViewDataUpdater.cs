using System.Collections.Generic;
using ClickMania.Core.Areas;
using ClickMania.Core.Areas.Search;
using ClickMania.Core.Blocks;
using ClickMania.View.Block;
using ClickMania.View.Position;

namespace ClickMania.View.Data
{
    public class ViewDataUpdater
    {
        private readonly IArea _area;
        private readonly IFindBlock _blockFinder;
        private readonly IConvertPosition _positionConverter;
        private List<IBlockView> _blockViews;

        public Dictionary<IBlockView, IBlock> ViewsForMove  { get; private set; }
        public IBlockView[] ViewsForDestroy { get; private set; }

        public ViewDataUpdater(IArea area, IFindBlock blockFinder, IConvertPosition positionConverter)
        {
            _area = area;
            _blockFinder = blockFinder;
            _positionConverter = positionConverter;
            _blockViews = new List<IBlockView>();
            ViewsForMove = new Dictionary<IBlockView, IBlock>();
            ViewsForDestroy = new IBlockView[0];
        }

        public void Init(IBlockView[] blockViews)
        {
            _blockViews = new List<IBlockView>(blockViews);
        }

        public void Update()
        {
            ViewsForMove.Clear();
            var viewsForDestroy = new List<IBlockView>();

            var blockViews = _blockViews.ToArray();
            for (int i = 0; i < blockViews.Length; i++)
            {
                var blockView = blockViews[i];
                
                if (_blockFinder.TryFindBlockInArea(blockView.ID, out var block) == false)
                {
                    _blockViews.Remove(blockView);
                    viewsForDestroy.Add(blockView);
                    continue;
                }

                var blockPosition = _positionConverter.ToVector2(block.Row, block.Column);
                blockView.SetPosition(blockPosition);
            }
            
            ViewsForDestroy = viewsForDestroy.ToArray();
        }

        public IBlockView[] GetAllViews()
        {
            return _blockViews.ToArray();
        }
    }
}