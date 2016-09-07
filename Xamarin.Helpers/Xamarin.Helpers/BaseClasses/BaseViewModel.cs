using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.Helpers.BaseClasses
{
    /// <summary>
    /// Lifecycle : 1. SetModelData, 2. Init, 3. Start
    /// </summary>
    public abstract class BaseViewModel : ViewModelBase
    {
        protected Page page;

        public App GetApplication => (App)App.Current;

        public BaseViewModel SetPage(Page page)
        {
            this.page = page;
            return this;
        }

        /// <summary>
        /// Executed on View Model creation
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Executed by the BaseContentPage to start up the ViewModel
        /// </summary>
        /// <returns></returns>
        public abstract Task Start();

        public virtual void SetModelData(object modelData) { }

        private bool isLoading = false;
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                isLoading = value;

                if (value)
                {
                    if (Device.OS != TargetPlatform.Windows && Device.OS != TargetPlatform.WinPhone)
                        GetApplication.ShowLoading();
                    else
                        page.IsBusy = true;
                }
                else
                {
                    if (Device.OS != TargetPlatform.Windows && Device.OS != TargetPlatform.WinPhone)
                        GetApplication.HideLoading();
                    else
                        page.IsBusy = false;
                }
            }
        }
    }
}
