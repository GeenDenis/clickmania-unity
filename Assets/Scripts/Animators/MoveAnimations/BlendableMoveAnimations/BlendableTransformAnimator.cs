using DG.Tweening;
using UnityEngine;

namespace Animators.MoveAnimations.BlendableMoveAnimations
{
    [CreateAssetMenu(fileName = "BlendableMoveTransformAnimator", menuName = "Animations/Tweens/Transform/BlendableMove", order = 0)]
    public class BlendableTransformAnimator : ScriptableObject, IAnimator<Transform, Vector2>
    {
        [SerializeField] private float duration;
        [SerializeField] private Ease type;

        public Tween StartAnimation(Transform transform, Vector2 toPosition)
        {
            return transform.DOBlendableMoveBy(toPosition, duration).SetEase(type);
        }
    }
}