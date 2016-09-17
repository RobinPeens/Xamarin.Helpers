using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Acr.UserDialogs;
using Xamarin.Helpers.Droid.Services;

namespace Xamarin.Helpers.Droid
{
    [Activity(Label = "Xamarin.Helpers", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            var settingsService = new SettingsService();
            settingsService.Init(this);

            var notificationHubsService = new NotificationHubsService();
            notificationHubsService.Init(this);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            UserDialogs.Init(this);
            LoadApplication(new App(settingsService, notificationHubsService));
            notificationHubsService.RegisterOrUpdate();
        }
    }
}

