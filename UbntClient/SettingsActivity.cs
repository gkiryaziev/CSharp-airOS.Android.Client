using Android.App;
using Android.OS;

namespace UbntClient
{
    [Activity(Label = "Settings")]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
             SetContentView (Resource.Layout.Settings);
        }
    }
}