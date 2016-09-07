using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Xamarin.Forms;
using Xamarin.Helpers.BaseClasses;
using Xamarin.Helpers.ViewModels;
using Xamarin.Helpers.Views;

namespace Xamarin.Helpers
{
    public class App : BaseApplication
    {
        public App()
        {
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

            listView.ItemSelected += (sender, args) =>
            {
                if (args.SelectedItem != null)
                {
                    listView.SelectedItem = null;
                }
                // Show the detail page.
                mp.IsPresented = false;
            };

            return mp;
        }
    }
}
