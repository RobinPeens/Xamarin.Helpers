using System.Collections.Generic;

namespace Xamarin.Helpers.Services
{
    public interface ISettingsService
    {
        void Init(object context);
        List<string> Tags { get; set; }
    }
}
