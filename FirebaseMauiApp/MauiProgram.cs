using FirebaseMauiApp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace FirebaseMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .RegisterFirebase()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IFirebaseAnalyticsService, FirebaseAnalyticsService>();


            return builder.Build();
        }

        public static MauiAppBuilder RegisterFirebase(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                events.AddAndroid(android => android
                .OnCreate((activity, bundle) => Firebase.FirebaseApp.InitializeApp(activity)));
#else
                events.AddiOS(iOS => iOS.FinishedLaunching((app, launchOptions) => {
                    Firebase.Core.App.Configure();
                    Firebase.Crashlytics.Crashlytics.SharedInstance.Init();
                    Firebase.Crashlytics.Crashlytics.SharedInstance.SetCrashlyticsCollectionEnabled(true);
                    Firebase.Crashlytics.Crashlytics.SharedInstance.SendUnsentReports();
                    return false;
                }));
#endif
            });

            return builder;
        }
    }
}
