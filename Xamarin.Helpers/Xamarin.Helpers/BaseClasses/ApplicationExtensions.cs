using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Helpers;
using Xamarin.Helpers.BaseClasses;

public static class ApplicationExtensions
{
    public static void SetNextPage<TPage>(this Application application, object modelData = null) where TPage : IPage, new()
    {
        TPage tmpPage = new TPage();
        IPage nextPage = tmpPage;

        if(modelData != null)
            nextPage.SetModelData(modelData);

        if (nextPage.UseMasterPage)
        {
            if (application.MainPage is MasterDetailPage)
            {
                (application.MainPage as MasterDetailPage).Detail = new NavigationPage((Page)nextPage);
            }
            else
            {
                application.MainPage = ((App)application).GetMasterPage((Page)nextPage);
            }
        }
        else
        {
            application.MainPage = new NavigationPage((Page)nextPage);
        }
    }

    public static void PushNextPage<TPage>(this Application application, object modelData = null) where TPage : IPage, new()
    {
        TPage tmpPage = new TPage();
        IPage nextPage = tmpPage;

        if (modelData != null)
            nextPage.SetModelData(modelData);

        application.MainPage.Navigation.PushAsync(new NavigationPage((Page)nextPage));
    }
}