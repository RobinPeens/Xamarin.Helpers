using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Helpers.BaseClasses;
using Xamarin.Helpers.ViewModels;

namespace Xamarin.Helpers
{
    public class HomePage : BaseContentPage<HomePageModel>
    {
        public override void ViewCreated()
        {
            SetUseMasterPage(true);
            SetDisplayPageFirst(false);
        }

        public override View GetPage()
        {
            var t = this.ViewModel;

            return new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    new Label {
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "Welcome to Xamarin Forms!"
                    }
                }
            };
        }

        public override void PageDisplayed()
        {
            // GetApplication.SetNextPage <...> ();
            // GetApplication.PushNextPage <...> ();
        }

        public override void ViewModelComplete()
        {
        }
    }
}
