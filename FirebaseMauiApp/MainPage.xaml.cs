using FirebaseMauiApp.Services;

namespace FirebaseMauiApp
{
    public partial class MainPage : ContentPage
    {
        private FirebaseAnalyticsService _firebaseAnalyticsService;

        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            _firebaseAnalyticsService = new FirebaseAnalyticsService();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";


            // Log the button click with Firebase Analytics
            _firebaseAnalyticsService.Log($"Counter button clicked {count} times", "button_click", "counter_button");


            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        void CrashBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            // Simulate an app crash by throwing an exception
            throw new Exception("Simulated crash");
        }
    }

}
