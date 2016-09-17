using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Helpers.BaseClasses;
using Xamarin.Helpers.Services;
using Xamarin.Helpers.ViewModels;

namespace Xamarin.Helpers.Views
{
    public class HomePage : BaseContentPage<HomePageModel>
    {
        DownloadService downloader = null;

        public override void ViewCreated()
        {
            SetUseMasterPage(true);
        }

        public override View GetPage()
        {
            var t = this.ViewModel;

            var progress = new ProgressBar
            {
            };

            downloader = new DownloadService(progress);

            return new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    new Label {
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "Welcome to Xamarin Forms!"
                    },
                    progress
                }
            };
        }

        public override void ViewModelComplete()
        {
            // Loader gone at this point
        }

        public override void PageDisplayed()
        {
            // GetApplication.SetNextPage <...> ();
            // GetApplication.PushNextPage <...> ();

            Task.Run(async () =>
            {
                var cancellationToken = new CancellationTokenSource();
                await downloader.DownloadFileAsync(
                    "http://download.thinkbroadband.com/1MB.zip",
                    cancellationToken.Token,
                    (message) =>
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await this.DisplayAlert(
                            "Download Failed",
                            message,
                            "Ok");
                        });
                    },
                    () =>
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await this.DisplayAlert(
                            "Download Complete",
                            "Download has completed",
                            "Ok");
                        });
                    });
            });
        }
    }
}
