using System.Threading.Tasks;

namespace ClickMania.View.Animations
{
    public interface IAnimation
    {
        void Start();
        Task WaitForComplection();
    }
}