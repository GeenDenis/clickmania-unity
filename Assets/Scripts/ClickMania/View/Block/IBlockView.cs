using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ClickMania.View.Block
{
    public interface IBlockView
    {
        int BlockID { get; }
        Vector2 Position { get; }
        
        void SetColor(Color color);
        void SetPosition(Vector2 position);
        void DestroyImmediate();
        
        UniTask Move(float xCoordinate);
        UniTask Fall(float yCoordinate);
        UniTask Show();
        UniTask Hide();
        UniTask Destroy();
    }
}