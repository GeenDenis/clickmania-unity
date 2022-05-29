namespace SDK.GameCore.Interfaces
{
    public interface IEditorInitable
    {
#if UNITY_EDITOR
        void E_Init();
#endif
    }
}