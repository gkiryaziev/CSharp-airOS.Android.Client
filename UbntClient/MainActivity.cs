using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Timers;
using ConnectionManager;

namespace UbntClient
{
    [Activity(Label = "Ubnt Client", MainLauncher = true, Icon = "@drawable/WiFi")]
    public class MainActivity : Activity
    {
        TextView lblBaseSSID, lblApMac, lblWlanIPAddress, lblFrequency, lblChannel;
        Button btnConnect, btnDisconnect, btnSettings;
        Timer timerMain;

        // client
        SSHClient sshClient = new SSHClient();
        ConnectionManager.UbntClient ***REMOVED***Client = new ConnectionManager.UbntClient();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // TextView
            lblBaseSSID = FindViewById<TextView>(Resource.Id.lblBaseSSID);
            lblApMac = FindViewById<TextView>(Resource.Id.lblApMac);
            lblWlanIPAddress = FindViewById<TextView>(Resource.Id.lblWlanIPAddress);
            lblFrequency = FindViewById<TextView>(Resource.Id.lblFrequency);
            lblChannel = FindViewById<TextView>(Resource.Id.lblChannel);

            // Button
            btnConnect = FindViewById<Button>(Resource.Id.btnConnect);
            btnDisconnect = FindViewById<Button>(Resource.Id.btnDisconnect);
            btnSettings = FindViewById<Button>(Resource.Id.btnSettings);

            btnConnect.Click += BtnConnect_Click;
            btnDisconnect.Click += BtnDisconnect_Click;
            btnSettings.Click += BtnSettings_Click;
        }

        protected override void OnStop()
        {
            base.OnStop();
            sshClient.Close();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
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

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            if (timerMain != null)
            {
                sshClient.Close();
                timerMain.Dispose();
                timerMain = null;
            }
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(SettingsActivity));
            StartActivity(i);
        }

        private void TimerMain_Elapsed(object sender, ElapsedEventArgs e)
        {
            //RunOnUiThread(() =>
            //    {
            //        if (chkBoxSignal.Checked)
            //        {
            //            int signal = ***REMOVED***Client.GetSignal();
            //            cirPbSignal.Value = signal + 100;
            //            cirPbSignal.Text = signal.ToString();
            //        }

            //        if (chkBoxNoise.Checked)
            //        {
            //            int noise = ***REMOVED***Client.GetNoiseFloor();
            //            cirPbNoise.Value = noise + 100;
            //            cirPbNoise.Text = noise.ToString();
            //        }

            //        if (chkBoxCCQ.Checked)
            //        {
            //            int ccq = ***REMOVED***Client.GetTransmitCCQ();
            //            cirPbCCQ.Value = ccq;
            //            cirPbCCQ.Text = ccq.ToString();
            //        }
            //    }
            //);
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
            //lblACKTimeout.Text = ***REMOVED***Client.GetAckTimeout();
            //lblTxRate.Text = ***REMOVED***Client.GetTxRate() + " Mbps";
            //lblRxRate.Text = ***REMOVED***Client.GetRxRate() + " Mbps";
            //lblUptime.Text = ***REMOVED***Client.GetUptimeFormatted();
        }
    }
}

