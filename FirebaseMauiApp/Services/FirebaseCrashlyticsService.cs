using System;
using Firebase.Crashlytics;
#if IOS
using Foundation;
#endif

namespace FirebaseMauiApp.Services
{
    public class FirebaseCrashlyticsService : IFirebaseCrashlyticsService
    {
        public void Log(Exception ex)
        {
#if IOS
        var errorInfo = new Dictionary<object, object> {
                        { NSError.LocalizedDescriptionKey, NSBundle.MainBundle.GetLocalizedString(ex.Message, null) },
                        { NSError.LocalizedFailureReasonErrorKey, NSBundle.MainBundle.GetLocalizedString ("Managed Failure", null) },
                        { NSError.LocalizedRecoverySuggestionErrorKey, NSBundle.MainBundle.GetLocalizedString ("Recovery Suggestion", null) },
                        //{ "ProductId", "123456" }, you can append even custom information if needed
                    };

        var error = new NSError(new NSString("NonFatalError"),
                     -1001,
                     NSDictionary.FromObjectsAndKeys(errorInfo.Values.ToArray(), errorInfo.Keys.ToArray(), errorInfo.Keys.Count));

        Crashlytics.SharedInstance.RecordError(error);
#else
            FirebaseCrashlytics.Instance.RecordException(Java.Lang.Throwable.FromException(ex));
#endif
        }
    }
}

