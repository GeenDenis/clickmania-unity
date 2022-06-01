namespace ClickMania.Score
{
    public class ScoreMultiplier : IMultiplier
    {
        public int Value { get; private set; }
        
        public void Set(int value)
        {
            Value = value;
        }
    }
}