using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.Helpers.BaseClasses
{
    /// <summary>
    /// Lifecycle - 1. ViewCreated, 2. PageDisplayed 3. ViewModelComplete
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public abstract class BaseContentPage<TViewModel> : ContentPage, IPageView<TViewModel> where TViewModel : BaseViewModel
    {
        public TViewModel ViewModel { get; set; }

        public App GetApplication => (App)App.Current;

        public bool UseMasterPage { get; private set; } = false;
        private bool PageFirst = false;

        /// <summary>
        /// Default false
        /// </summary>
        /// <param name="value"></param>
        public void SetUseMasterPage(bool value)
        {
            UseMasterPage = value;
        }

        /// <summary>
        /// Default false
        /// true = Start Model and Display Page at the same time, false = Start Model then provide Page after ViewModelReady()
        /// </summary>
        public void SetDisplayPageFirst(bool value)
        {
            PageFirst = value;
        }

        public BaseContentPage()
        {
            this.IsLoading = true;

            try
            {
                if (!SimpleIoc.Default.IsRegistered<TViewModel>())
                    IoC.GetContainer.Register<TViewModel>();

                this.ViewModel = this.GetViewModel<TViewModel>();
                this.BindingContext = this.ViewModel;

                this.ViewCreated();

                this.Appearing += async (s, e) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        try
                        {
                            this.PageDisplayed();
                        }
                        catch
                        {
                            this.IsLoading = false;
                            throw;
                        }
                    });

                    if (PageFirst)
                    {
                        await Task.Run(async () =>
                        {
                            try
                            {
                                await this.ViewModel.Start();

                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    this.ViewModelComplete();
                                    this.IsLoading = false;
                                });
                            }
                            catch
                            {
                                this.IsLoading = false;
                                throw;
                            }
                        });
                    }
                };

                if (PageFirst)
                    Content = GetPage();
                else
                {
                    Task.Run(async () =>
                    {
                        try
                        {
                            await this.ViewModel.Start();

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                this.ViewModelComplete();
                                this.IsLoading = false;

                                Content = GetPage();
                            });
                        }
                        catch
                        {
                            this.IsLoading = false;
                            throw;
                        }
                    });
                }
            }
            catch
            {
                this.IsLoading = false;
                throw;
            }
        }

        public abstract View GetPage();

        /// <summary>
        /// Executed on UI Thread, ViewModel Should exist but not started at this point
        /// </summary>
        public abstract void ViewCreated();

        /// <summary>
        /// Executed on UI Thread, Page has been displayed, ViewModel about to start
        /// </summary>
        public abstract void PageDisplayed();

        /// <summary>
        /// Executed on UI Thread, ViewModel loaded and ready
        /// </summary>
        public abstract void ViewModelComplete();

        public void SetModelData(object modelData)
        {
            this.ViewModel.SetModelData(modelData);
        }

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
                        IsBusy = true;
                }
                else
                {
                    if (Device.OS != TargetPlatform.Windows && Device.OS != TargetPlatform.WinPhone)
                        GetApplication.HideLoading();
                    else
                        IsBusy = false;
                }
            }
        }
    }
}
