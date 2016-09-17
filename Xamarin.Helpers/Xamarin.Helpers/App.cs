using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Xamarin.Forms;
using Xamarin.Helpers.BaseClasses;
using Xamarin.Helpers.Services;
using Xamarin.Helpers.ViewModels;
using Xamarin.Helpers.Views;

namespace Xamarin.Helpers
{
    public class App : BaseApplication
    {
        public App(ISettingsService settingsService, INotificationHubsService notificationHubsService)
        {
            this.InitIoC();

            if(!IoC.GetContainer.IsRegistered<ISettingsService>())
                IoC.GetContainer.Register(() => settingsService);

            if (!IoC.GetContainer.IsRegistered<INotificationHubsService>())
                IoC.GetContainer.Register(() => notificationHubsService);


            var ss = IoC.IoCGet<ISettingsService>();
            var nh = IoC.IoCGet<INotificationHubsService>();

            List<string> tags = new List<string>()
            {
                "TestTag",
                "RobinTest",
                "QWERTY"
            };

            ss.Tags = tags;
            nh.RegisterOrUpdate();

            this.SetNextPage<HomePage>(new TestModelData());
        }

        public override MasterDetailPage GetMasterPage(Page detailPage)
        {
            var mp = new MasterDetailPage();
            
            string[] menuItems =
                {
                    "Red",
                    "Silver",
                    "Teal",
                    "White",
                    "Yellow",
                };

            // Create ListView for the master page.
            ListView listView = new ListView
            {
                ItemsSource = menuItems
            };

            // Create the master page with the ListView.
            mp.Master = new ContentPage
            {
                Title = "MasterDetailPage",
                Content = new StackLayout
                {
                    Children =
                    {
                        listView
                    }
                }
            };

            mp.Detail = new NavigationPage(detailPage);

            if (Device.OS == TargetPlatform.WinPhone)
            {
                (mp.Detail as ContentPage).Content.GestureRecognizers.Add(
                    new TapGestureRecognizer((view) =>
                    {
                        mp.IsPresented = true;
                    }));
            }

            listView.ItemSelected += async (sender, args) =>
            {
                if (args.SelectedItem != null)
                {
                    listView.SelectedItem = null;
                    await mp.Detail.DisplayAlert("Click", (string)listView.SelectedItem, "Mkay");
                }
                // Show the detail page.
                mp.IsPresented = false;
            };

            return mp;
        }
    }
}
