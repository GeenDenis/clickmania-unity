using System.Threading.Tasks;
using ClickMania.Core.Areas;

namespace ClickMania.View.Animations
{
    public class DestroyAnimation : IAnimation
    {
        private readonly IArea _area;

        public DestroyAnimation(IArea area)
        {
            _area = area;
        }

        public void Start()
        {
            for (int i = 0; i < _area.ColumnCount; i++)
            for (int j = 0; j < _area.RowCount; j++)
            {
                
            }
        }

        public Task WaitForComplection()
        {
            throw new System.NotImplementedException();
        }
    }
}