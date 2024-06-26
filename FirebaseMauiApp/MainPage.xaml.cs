﻿using System.Diagnostics;
using FirebaseMauiApp.Services;

namespace FirebaseMauiApp
{
    public partial class MainPage : ContentPage
    {
        private readonly FirebaseAnalyticsService _firebaseAnalyticsService;
        private readonly FirebaseCrashlyticsService _firebaseCrashlyticsService;

        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            _firebaseAnalyticsService = new FirebaseAnalyticsService();
            _firebaseCrashlyticsService = new FirebaseCrashlyticsService();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";


            // Log the button click with Firebase Analytics
            _firebaseAnalyticsService.Log("select_content", "button_click", "counter_button");


            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        void CrashBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            throw new Exception(">>>>>> NOT CATCHED EXCEPTION!!!!!");
        }

        void CatchCrashBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                // Simulate an app crash by throwing an exception
                throw new Exception(">>>>>> SIMULATED CRASH!!!!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                _firebaseCrashlyticsService.Log(ex);
            }
        }
    }

}
