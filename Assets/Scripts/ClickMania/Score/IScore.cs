namespace ClickMania.Score
{
    public interface IScore
    {
        int Value { get; }
        void Add(int blockCount);
        void Reset();
    }
}