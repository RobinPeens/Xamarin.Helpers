﻿using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Xamarin.Helpers.Droid
{
    [Activity(Label = "Xamarin.Helpers", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            //ActionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid()));
            LoadApplication(new App());
        }
    }
}
