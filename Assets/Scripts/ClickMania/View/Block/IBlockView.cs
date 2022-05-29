using UnityEngine;

namespace ClickMania.View.Block
{
    public interface IBlockView
    {
        int BlockID { get; }
        Vector2 Position { get; }
        
        void Move(float xCoordinate);
        void Fall(float yCoordinate);
        void SetColor(Color color);
        void SetPosition(Vector2 position);
        void Show();
        void Hide();
        void Destroy();
    }
}