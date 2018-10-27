using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace PigAppGame
{
    public class GameFragment : Fragment
    {



        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle bundle)
        {
            base.OnCreate(bundle);

            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.GameFragment, container, false);

            //return base.OnCreateView(inflater, container, bundle);
        }
    }
}