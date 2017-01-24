using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using System;

namespace UbntClient
{
    [Activity(
        Label = "@string/settingsActivity",
        Icon = "@drawable/Settings",
        Theme = "@style/MyCustomTheme",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SettingsActivity : Activity
    {
        // setting
        ISharedPreferences settings;

        // controls
        EditText txtHost, txtPort, txtLogin, txtPassword, txtInterval;
        Button btnSave;

        //---------------------------------
        // OnCreate
        //---------------------------------
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Settings);

            // read settings
            settings = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);

            // controls
            txtHost = FindViewById<EditText>(Resource.Id.txtHost);
            txtPort = FindViewById<EditText>(Resource.Id.txtPort);
            txtLogin = FindViewById<EditText>(Resource.Id.txtLogin);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            txtInterval = FindViewById<EditText>(Resource.Id.txtInterval);
            btnSave = FindViewById<Button>(Resource.Id.btnSave);

            btnSave.Click += BtnSave_Click;

            // set settings to controls
            txtHost.Text = settings.GetString("Host", "192.168.1.1");
            txtPort.Text = settings.GetInt("Port", 22).ToString();
            txtLogin.Text = settings.GetString("Login", "admin");
            txtPassword.Text = settings.GetString("Password", "admin");
            txtInterval.Text = settings.GetInt("Interval", 2000).ToString();
        }

        //---------------------------------
        // Button Save Settings Event
        //---------------------------------
        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            ISharedPreferencesEditor editor = settings.Edit();
            editor.PutString("Host", txtHost.Text);
            editor.PutInt("Port", Convert.ToInt32(txtPort.Text));
            editor.PutString("Login", txtLogin.Text);
            editor.PutString("Password", txtPassword.Text);
            editor.PutInt("Interval", Convert.ToInt32(txtInterval.Text));
            editor.Apply();
            Toast.MakeText(this, "Saved.", ToastLength.Short).Show();
        }
    }
}