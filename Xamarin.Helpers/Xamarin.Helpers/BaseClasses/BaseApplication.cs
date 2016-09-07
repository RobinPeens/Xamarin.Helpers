using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.Helpers.BaseClasses
{
    public abstract class BaseApplication : Application
    {
        public BaseApplication()
        {
            this.InitIoC();
        }

        public abstract MasterDetailPage GetMasterPage(Page detailPage);

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
