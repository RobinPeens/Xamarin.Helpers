using Xamarin.Forms;

namespace Xamarin.Helpers.BaseClasses
{
    public interface IPageView<TViewModel> : IPage
        where TViewModel : BaseViewModel
    {
        App GetApplication { get; }
        TViewModel ViewModel { get; set; }

        View GetPage();
        void PageDisplayed();
        void ViewModelComplete();
        void ViewCreated();
    }
}