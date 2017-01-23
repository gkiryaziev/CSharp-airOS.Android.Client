using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Timers;
using ConnectionManager;
using Android.Content.PM;
using Android.Views;

namespace UbntClient
{
    [Activity(
        Label = "@string/appName",
        MainLauncher = true,
        Icon = "@drawable/WiFi",
        Theme = "@style/MyCustomTheme",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        TextView lblBaseSSID, lblApMac, lblWlanIPAddress, lblFrequency, lblChannel,
            lblACKTimeout, lblTxRate, lblRxRate, lblUptime,
            lblSignal, lblNoise, lblCCQ;

        Switch swSignal, swNoise, swCCQ;

        Button btnConnect, btnDisconnect, btnSettings;

        Timer timerMain;

        // client
        SSHClient sshClient = new SSHClient();
        ConnectionManager.UbntClient ***REMOVED***Client = new ConnectionManager.UbntClient();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // keep screen on
            this.Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);

            // TextView
            lblBaseSSID = FindViewById<TextView>(Resource.Id.lblBaseSSID);
            lblApMac = FindViewById<TextView>(Resource.Id.lblApMac);
            lblWlanIPAddress = FindViewById<TextView>(Resource.Id.lblWlanIPAddress);
            lblFrequency = FindViewById<TextView>(Resource.Id.lblFrequency);
            lblChannel = FindViewById<TextView>(Resource.Id.lblChannel);
            lblACKTimeout = FindViewById<TextView>(Resource.Id.lblACKTimeout);
            lblTxRate = FindViewById<TextView>(Resource.Id.lblTxRate);
            lblRxRate = FindViewById<TextView>(Resource.Id.lblRxRate);
            lblUptime = FindViewById<TextView>(Resource.Id.lblUptime);
            lblSignal = FindViewById<TextView>(Resource.Id.lblSignal);
            lblNoise = FindViewById<TextView>(Resource.Id.lblNoise);
            lblCCQ = FindViewById<TextView>(Resource.Id.lblCCQ);

            // Switch
            swSignal = FindViewById<Switch>(Resource.Id.swSignal);
            swNoise = FindViewById<Switch>(Resource.Id.swNoise);
            swCCQ = FindViewById<Switch>(Resource.Id.swCCQ);
            swSignal.Checked = true;
            swNoise.Checked = false;
            swCCQ.Checked = false;

            // Button
            btnConnect = FindViewById<Button>(Resource.Id.btnConnect);
            btnDisconnect = FindViewById<Button>(Resource.Id.btnDisconnect);
            btnSettings = FindViewById<Button>(Resource.Id.btnSettings);

            btnConnect.Click += BtnConnect_Click;
            btnDisconnect.Click += BtnDisconnect_Click;
            btnSettings.Click += BtnSettings_Click;
        }

        //---------------------------------
        // OnStop
        //---------------------------------
        protected override void OnStop()
        {
            base.OnStop();

            if (timerMain != null)
            {
                timerMain.Dispose();
                timerMain = null;
            }

            sshClient.Close();
        }

        //---------------------------------
        // Button Connect Event
        //---------------------------------
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (sshClient.Open("***REMOVED***", 22, "***REMOVED***", "***REMOVED***"))
                {
                    ***REMOVED***Client.SetSSHClient(sshClient);
                    GetStatus();
                    if (timerMain == null)
                    {
                        timerMain = new Timer();
                        timerMain.Interval = 2000;
                        timerMain.Elapsed += TimerMain_Elapsed;
                        timerMain.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Check WIFI connection.", ToastLength.Short).Show();
                return;
            }
        }

        //---------------------------------
        // Button Disconnect Event
        //---------------------------------
        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            if (timerMain != null)
            {
                timerMain.Dispose();
                timerMain = null;
            }

            sshClient.Close();
        }

        //---------------------------------
        // Button Settings Event
        //---------------------------------
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(SettingsActivity));
            StartActivity(i);
        }

        //---------------------------------
        // Main Timer Event
        //---------------------------------
        private void TimerMain_Elapsed(object sender, ElapsedEventArgs e)
        {
            RunOnUiThread(() =>
                {
                    if (swSignal.Checked)
                    {
                        int signal = ***REMOVED***Client.GetSignal();
                        //cirPbSignal.Value = signal + 100;
                        lblSignal.Text = signal.ToString();
                    }

                    if (swNoise.Checked)
                    {
                        int noise = ***REMOVED***Client.GetNoiseFloor();
                        //cirPbNoise.Value = noise + 100;
                        lblNoise.Text = noise.ToString();
                    }

                    if (swCCQ.Checked)
                    {
                        int ccq = ***REMOVED***Client.GetTransmitCCQ();
                        //cirPbCCQ.Value = ccq;
                        lblCCQ.Text = ccq.ToString();
                    }
                }
            );
        }

        //---------------------------------
        // Get main status
        //---------------------------------
        private void GetStatus()
        {
            lblBaseSSID.Text = ***REMOVED***Client.GetBaseSSID();
            lblApMac.Text = ***REMOVED***Client.GetApMAC();
            lblWlanIPAddress.Text = ***REMOVED***Client.GetWlanIpAddress();
            lblFrequency.Text = ***REMOVED***Client.GetFrequency() + " MHz";
            lblChannel.Text = ***REMOVED***Client.GetChannel();
            lblACKTimeout.Text = ***REMOVED***Client.GetAckTimeout();
            lblTxRate.Text = ***REMOVED***Client.GetTxRate() + " Mbps";
            lblRxRate.Text = ***REMOVED***Client.GetRxRate() + " Mbps";
            lblUptime.Text = ***REMOVED***Client.GetUptimeFormatted();
        }
    }
}

