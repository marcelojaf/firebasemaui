using FirebaseMaui.Interfaces;
using FirebaseMaui.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace FirebaseMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .RegisterFirebase()
                .RegisterApp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterFirebase(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                events.AddAndroid(android => android.OnCreate((activity, bundle) =>
                {
                    Firebase.FirebaseApp.InitializeApp(activity);
                }));
#else
                events.AddiOS(iOS => iOS.FinishedLaunching((App, launchOptions) => {
                    Firebase.Core.App.Configure();
                    return false;
                }));
#endif
            });

            return builder;
        }

        private static MauiAppBuilder RegisterApp(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<IFirebaseAnalyticsService, FirebaseAnalyticsService>();
            mauiAppBuilder.Services.AddTransient<MainPage>();
            mauiAppBuilder.Services.AddSingleton<AppShell>();


            return mauiAppBuilder;
        }
    }
}
