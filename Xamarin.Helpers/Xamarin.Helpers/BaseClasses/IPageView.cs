using Xamarin.Forms;

namespace Xamarin.Helpers.BaseClasses
{
    public interface IPageView<TViewModel> : IPage
        where TViewModel : BaseViewModel
    {
        BaseApplication GetApplication { get; }
        TViewModel ViewModel { get; set; }

        View GetPage();
        void PageDisplayed();
        void ViewModelComplete();
        void ViewCreated();
    }
}