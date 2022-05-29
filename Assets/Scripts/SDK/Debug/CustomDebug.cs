namespace SDK.Debug
{
    public static class CustomDebug
    {
        private static bool _activeLog;

        public static void SetActiveDebugLog(bool active)
        {
            _activeLog = active;
        }

        public static void LogError(string message)
        {
            if(!_activeLog) return;
            UnityEngine.Debug.LogError(message);
        }
    }
}