using FirebaseMaui.Interfaces;
using FirebaseMaui.Services;
using System.Diagnostics;

namespace FirebaseMaui
{
    public partial class MainPage : ContentPage
    {
        private int counter = 0;
        private readonly IFirebaseAnalyticsService _analytics;

        public MainPage(IFirebaseAnalyticsService analytics)
        {
            InitializeComponent();
            _analytics = analytics;

            LogPageView();
        }

        private void LogPageView()
        {
            try
            {
                _analytics.Log("MainPage");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to log page view: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles the counter increment and logs the event
        /// </summary>
        private void OnCounterClicked(object sender, EventArgs e)
        {
            counter++;
            CounterLabel.Text = $"Counter: {counter}";

            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "counter_value", counter.ToString() },
                    { "timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() }
                };

                _analytics.Log("counter_increment", parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to log counter event: {ex.Message}");
            }
        }

        /// <summary>
        /// Generates and logs a controlled exception
        /// </summary>
        private void OnControlledExceptionClicked(object sender, EventArgs e)
        {
            try
            {
                throw new Exception("Controlled test exception");
            }
            catch (Exception ex)
            {
                // Log to Crashlytics
                //_crashlytics?.RecordException(ex);

                // Log to Analytics
                try
                {
                    var parameters = new Dictionary<string, string>
                    {
                        { "error_description", ex.Message },
                        { "error_code", "-1" },
                        { "timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() }
                    };

                    _analytics?.Log("controlled_exception", parameters);
                }
                catch (Exception analyticsEx)
                {
                    Debug.WriteLine($"Failed to log controlled exception: {analyticsEx.Message}");
                }
            }
        }

        /// <summary>
        /// Forces an app crash for testing Crashlytics
        /// </summary>
        private void OnForceCrashClicked(object sender, EventArgs e)
        {
            throw new Exception("Intentional app crash");
        }

    }

}
