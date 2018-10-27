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
        string player1Name;
        string player2Name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.StartActivity);

            // use portrait
            RequestedOrientation = ScreenOrientation.Portrait;

            // is large
            bool large = FindViewById<FrameLayout>(Resource.Id.frameBackgroundImage) != null;

            // set up button
            FindViewById<Button>(Resource.Id.startButton).Click += (sender, e) => {

                if (large)
                {
                    // freeze start ui
                    FindViewById<Button>(Resource.Id.startButton).Enabled = false;
                    FindViewById<EditText>(Resource.Id.Player1EditText).Enabled = false;
                    FindViewById<EditText>(Resource.Id.Player2EditText).Enabled = false;

                    // new game                   
                }

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