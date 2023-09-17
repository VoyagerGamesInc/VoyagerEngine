namespace VoyagerEngine
{
    public static class VoyagerUtility_Log
    {
        public static string LogCyan(string message)
        {
            return $"<color=#00FFFFFF>{message}</color>";
        }
        public static string LogMagenta(string message)
        {
            return $"<color=#FF00FFFF>{message}</color>";
        }
        public static string LogYellow(string message)
        {
            return $"<color=#FFFF00FF>{message}</color>";
        }
        public static string LogRed(string message)
        {
            return $"<color=#FF0000FF>{message}</color>";
        }
        public static string LogGreen(string message)
        {
            return $"<color=#00FF00FF>{message}</color>";
        }
        public static string LogBlue(string message)
        {
            return $"<color=#0000FFFF>{message}</color>";
        }
        public static T GetLast<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }
        public static T GetFirst<T>(this List<T> list)
        {
            return list[0];
        }
    }
}