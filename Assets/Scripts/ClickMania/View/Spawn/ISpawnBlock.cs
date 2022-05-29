using ClickMania.View.Block;
using UnityEngine;

namespace ClickMania.View.Spawn
{
    public interface ISpawnBlock
    {
        IBlockView Spawn(int blockID, Vector2 position);
    }
}