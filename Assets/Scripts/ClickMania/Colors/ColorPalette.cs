using System.Collections.Generic;
using UnityEngine;

namespace ClickMania.Colors
{
    [CreateAssetMenu(fileName = "ColorPalette", menuName = "ClickMania/Colors/ColorPalette", order = 0)]
    public class ColorPalette : ScriptableObject
    {
        [SerializeField] private List<Color> pallet;

        public Color GetColor(int colorIndex)
        {
            return pallet[colorIndex];
        }
    }
}