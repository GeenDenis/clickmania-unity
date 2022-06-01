﻿using System;
using System.Collections.Generic;
using ClickMania.Core.Areas;
using ClickMania.Core.Blocks;
using ClickMania.View.Block;
using ClickMania.View.Position;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace ClickMania.View.Animations
{
    public class FallAnimation : IAnimation<IBlockView[]>
    {
        private readonly IArea _area;
        private readonly IConvertPosition _positionConverter;

        private float betweenFallDelay = 0.05f;
        
        public FallAnimation(IArea area, IConvertPosition positionConverter)
        {
            _area = area;
            _positionConverter = positionConverter;
        }

        public UniTask Start(IBlockView[] data)
        {
            var fallAnimations = new List<UniTask>();
            
            for (int columnIndex = 0; columnIndex < _area.ColumnCount; columnIndex++)
            {
                var fallDelay = 0f;
                for (int rowIndex = 0; rowIndex < _area.RowCount; rowIndex++)
                {
                    var block = _area.Cells[rowIndex, columnIndex];
                    
                    if(block is null) continue;
                    
                    if(TryFindView(data, block.ID, out var blockView) == false) continue;
                    
                    if(TryAnimateBlock(block, blockView, out Tween animation) == false) continue;
                    
                    animation.SetDelay(fallDelay);
                    fallAnimations.Add(animation.AsyncWaitForCompletion().AsUniTask());
                    fallDelay += betweenFallDelay;
                }
            }

            return UniTask.WhenAll(fallAnimations);
        }

        private bool TryAnimateBlock(IBlock block, IBlockView view, out Tween animation)
        {
            animation = default;
            if (block.ID != view.ID) return false;
            var blockPos = _positionConverter.ToVector2(block.Row, block.Column);
            if (Math.Abs(view.Position.y - blockPos.y) < 0.05f) return false;
            animation = view.Fall(blockPos.y);
            return true;
        }

        private bool TryFindView(IBlockView[] views, int id, out IBlockView view)
        {
            view = default;
            for (int i = 0; i < views.Length; i++)
            {
                if(views[i].ID != id) continue;
                view = views[i];
                return true;
            }
            return false;
        }
    }
}