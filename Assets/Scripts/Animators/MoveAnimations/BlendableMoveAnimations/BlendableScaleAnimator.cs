using DG.Tweening;
using UnityEngine;

namespace Animators.MoveAnimations.BlendableMoveAnimations
{
    [CreateAssetMenu(fileName = "BlendableScaleAnimator", menuName = "Animations/Tweens/Transform/BlendableScale", order = 0)]
    public class BlendableScaleAnimator : ScriptableObject, IAnimator<Transform, Vector3>
    {
        [SerializeField] private float duration;
        [SerializeField] private Ease type;

        public Tween StartAnimation(Transform transform, Vector3 scale)
        {
            return transform.DOBlendableScaleBy(scale, duration).SetEase(type);
        }
    }
}