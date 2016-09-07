using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Xamarin.Forms;
using Xamarin.Helpers.BaseClasses;
using Xamarin.Helpers.ViewModels;

namespace Xamarin.Helpers
{
    public class App : Application
    {
        public App()
        {
            this.InitIoC();
            this.SetNextPage<HomePage>(new TestModelData());
        }

        public MasterDetailPage GetMasterPage(Page detailPage)
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

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void ShowLoading()
        {
            UserDialogs.Instance.Loading().Show();
        }

        public void HideLoading()
        {
            UserDialogs.Instance.Loading().Hide();
        } 
    }
}
