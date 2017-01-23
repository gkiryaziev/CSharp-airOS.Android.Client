using Android.App;
using Android.Content.PM;
using Android.OS;

namespace UbntClient
{
    [Activity(
        Label = "@string/settingsActivity",
        Icon = "@drawable/WiFi",
        Theme = "@style/MyCustomTheme",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Settings);
        }
    }
}