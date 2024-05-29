using System;
namespace FirebaseMauiApp.Services
{
	public interface IFirebaseCrashlyticsService
	{
        void Log(Exception ex);
    }
}

