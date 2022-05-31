using System.Collections.Generic;
using ClickMania.View.Block;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace ClickMania.View.Animations
{
    public class DestroyAnimation : IAnimation<IBlockView[]>
    {
        public UniTask Start(IBlockView[] blockViews)
        {
            var destroyAnimations = new List<UniTask>();
            for (int i = 0; i < blockViews.Length; i++)
            {
                var tween = blockViews[i].Hide();
                tween.OnComplete(blockViews[i].Destroy);
                destroyAnimations.Add(tween.AsyncWaitForCompletion().AsUniTask());
            }

            return UniTask.WhenAll(destroyAnimations);
        }
    }
}