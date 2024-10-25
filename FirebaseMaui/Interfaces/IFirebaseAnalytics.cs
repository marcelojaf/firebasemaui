namespace FirebaseMaui.Interfaces
{
    public interface IFirebaseAnalytics
    {
        void LogEvent(string eventName, IDictionary<string, object> parameters);
    }
}
