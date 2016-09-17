namespace Xamarin.Helpers.Services
{
    public interface INotificationHubsService
    {
        string PnsHandler { get; set; }

        void Init(object context);
        void RegisterOrUpdate();
    }
}
