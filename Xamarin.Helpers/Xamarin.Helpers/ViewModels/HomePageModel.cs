using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Helpers.BaseClasses;

namespace Xamarin.Helpers.ViewModels
{
    public class TestModelData
    {
        public string Text => "Test Model Data";
    }

    public class HomePageModel : BaseViewModel
    {
        TestModelData data = null;
            
        public override void SetModelData(object modelData)
        {
            data = (TestModelData) modelData;
        }

        public override void Init()
        {
        }

        public override async Task Start()
        {
            await Task.Delay(1000);
        }
    }
}
