using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Xamarin.Helpers.Droid
{
    /// <summary>
    /// Define the Constants.
    /// </summary>
    public class Constants
    {
        // ------- Google API Project Number
        // This is used in all Xamarin Android Projects
        public const string SenderID = "322124011702";

        // -------  Azure app specific connection string and hub name
        // This is only used in Case 1 and Case 2
        public const string HubName = "HorecaApp";

        // This is only used in Case 1
        public const string ConnectionString = "Endpoint=sb://horeca.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=urD9MIbVDC5f7lLu0k68nn+/ZhfCU4skg1mRbWPZ52o=";
    }
}