namespace ClickMania.Score
{
    public interface IMultiplier
    {
        int Value { get; }
        void Set(int value);
    }
}