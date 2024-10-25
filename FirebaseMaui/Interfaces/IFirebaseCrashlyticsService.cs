namespace FirebaseMaui.Interfaces
{
    public interface IFirebaseCrashlyticsService
    {
        void RecordException(Exception exception);
    }
}
