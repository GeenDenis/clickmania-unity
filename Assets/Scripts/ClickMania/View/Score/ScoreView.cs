using ClickMania.Score;
using TMPro;
using UnityEngine;

namespace ClickMania.View.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private IScore _score;
        
        public void Init(IScore score)
        {
            _score = score;
        }

        public void UpdateView()
        {
            _text.text = _score.Value.ToString();
        }
    }
}