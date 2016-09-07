using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Helpers.BaseClasses;

public static class IoC
{
    private static SimpleIoc container = null;
    
    public static SimpleIoc InitIoC(this object _)
    {
        if (container == null)
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            container = SimpleIoc.Default;
        }

        return container;
    }

    public static SimpleIoc GetContainer => container;

    public static ofType IoCGet<ofType>() => ServiceLocator.Current.GetInstance<ofType>();
    
    public static ViewModel GetViewModel<ViewModel>(this Page page)
        where ViewModel : BaseViewModel
    {
        var tmp = ServiceLocator.Current.GetInstance<ViewModel>();
        tmp.SetPage(page);
        return tmp;
    }
}