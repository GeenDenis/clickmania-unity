using ClickMania.Score;
using TMPro;
using UnityEngine;

namespace ClickMania.View.Score
{
    public class ScoreMultiplierView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private IMultiplier _scoreMultiplier;
        
        public void Init(IMultiplier scoreMultiplier)
        {
            _scoreMultiplier = scoreMultiplier;
        }

        public void UpdateView()
        {
            gameObject.SetActive(_scoreMultiplier.Value != 1);
            _text.text = $"X{_scoreMultiplier.Value}";
        }
    }
}