using System.Collections.Generic;
using ClickMania.View.Block;
using Cysharp.Threading.Tasks;

namespace ClickMania.View.Animations
{
    public class DestroyAnimation : IAnimation<IBlockView[]>
    {
        public UniTask Start(IBlockView[] blockViews)
        {
            var destroyAnimations = new List<UniTask>();
            for (int i = 0; i < blockViews.Length; i++)
            {
                destroyAnimations.Add(blockViews[i].Destroy());
            }

            return UniTask.WhenAll(destroyAnimations);
        }
    }
}