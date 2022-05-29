using UnityEngine;

namespace ClickMania.View.Position
{
    public interface IConvertPosition
    {
        Vector2 ToVector2(int row, int collumn);
        void ToPosition(Vector2 position, out int row, out int collumn);
    }
}