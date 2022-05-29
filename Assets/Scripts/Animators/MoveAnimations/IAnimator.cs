using DG.Tweening;

namespace Animators.MoveAnimations
{
    public interface IAnimator<T1, T2>
    {
        Tween StartAnimation(T1 obj, T2 parameter);
    }
}