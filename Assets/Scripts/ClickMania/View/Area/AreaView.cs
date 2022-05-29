using UnityEngine;

namespace ClickMania.Area.View
{
    public class AreaView : IAreaView
    {
        private readonly SpriteRenderer _sprite;

        public AreaView(SpriteRenderer sprite)
        {
            _sprite = sprite;
        }
        
        public void SetSize(int rowCount, int columnCount)
        {
            var height = rowCount + 0.5f;
            var width = columnCount + 0.5f;
            _sprite.size = new Vector2(width, height);
        }
    }
}