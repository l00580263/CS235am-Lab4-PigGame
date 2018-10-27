using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using PigGame;
using System.Xml.Serialization;
using System.IO;
using Android.Content.Res;
using Android.Content.PM;

namespace PigAppGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        
        
        
        
        #region Methods
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set view
            SetContentView(Resource.Layout.StartActivity);

            // use portrait
            if ((Application.ApplicationContext.Resources.Configuration.ScreenLayout & ScreenLayout.SizeMask) == ScreenLayout.SizeLarge)
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            }
        }       
        #endregion
    }
}