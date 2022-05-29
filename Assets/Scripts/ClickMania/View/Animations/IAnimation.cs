using Cysharp.Threading.Tasks;

namespace ClickMania.View.Animations
{
    public interface IAnimation
    {
        UniTask Start();
    }
    
    public interface IAnimation<T>
    {
        UniTask Start(T data);
    }
}