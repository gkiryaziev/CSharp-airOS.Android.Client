using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Timers;

namespace UbntClient
{
    [Activity(Label = "UbntClient", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        TextView lblCounter;
        Button btnStart, btnStop, btnSettings;
        Timer timerMain;
        int counter = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            lblCounter = FindViewById<TextView>(Resource.Id.lblCounter);
            btnStart = FindViewById<Button>(Resource.Id.btnStart);
            btnStop = FindViewById<Button>(Resource.Id.btnStop);
            btnSettings = FindViewById<Button>(Resource.Id.btnSettings);

            btnStart.Click += BtnStart_Click;
            btnStop.Click += BtnStop_Click;
            btnSettings.Click += BtnSettings_Click;
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(SettingsActivity));
            StartActivity(i);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (timerMain == null)
            {
                timerMain = new Timer();
                timerMain.Interval = 1000;
                // timerMain.Enabled = true;
                // timerMain.AutoReset = false;
                timerMain.Elapsed += TimerMain_Elapsed;
                timerMain.Start();
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (timerMain != null)
            {
                timerMain.Dispose();
                timerMain = null;
                counter = 0;
                lblCounter.Text = "0";
            }
        }

        private void TimerMain_Elapsed(object sender, ElapsedEventArgs e)
        {
            counter++;
            RunOnUiThread(() => { lblCounter.Text = counter.ToString(); } );            
        }
    }
}

