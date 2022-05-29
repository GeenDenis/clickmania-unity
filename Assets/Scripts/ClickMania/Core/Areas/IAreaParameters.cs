namespace ClickMania.Core.Areas
{
    public interface IAreaParameters : IArea
    {
        void SetSize(int rowCount, int columnCount);
    }
}