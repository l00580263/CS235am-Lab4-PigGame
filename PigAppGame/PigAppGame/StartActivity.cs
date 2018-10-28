using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PigAppGame
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class StartActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.StartActivity);

            // use portrait
            RequestedOrientation = ScreenOrientation.Portrait;

            // is large
            bool large = FindViewById<FrameLayout>(Resource.Id.frameBackgroundImage) != null;

            if (large)
            {
                var intent = new Intent(this, typeof(GameActivity));
                intent.PutExtra("large", large);
                StartActivity(intent);
            }
            else
            {
                // set up button
                FindViewById<Button>(Resource.Id.startButton).Click += (sender, e) => {

                    var intent = new Intent(this, typeof(GameActivity));
                    // add names to intent
                    intent.PutExtra("player1Name", FindViewById<EditText>(Resource.Id.Player1EditText).Text);
                    intent.PutExtra("player2Name", FindViewById<EditText>(Resource.Id.Player2EditText).Text);
                    // start game
                    StartActivity(intent);
                };
            }
        }
    }
}