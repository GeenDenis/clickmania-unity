using DG.Tweening;
using UnityEngine;

namespace ClickMania.View.Block
{
    public interface IBlockView
    {
        int BlockID { get; }
        Vector2 Position { get; }
        
        void SetColor(Color color);
        void SetPosition(Vector2 position);
        
        Tween Move(float xCoordinate);
        Tween Fall(float yCoordinate);
        Tween Show();
        Tween Hide();
        void Destroy();
    }
}