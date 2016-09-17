using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Helpers.Services
{

    public interface IFileAccess
    {
        Stream GetFileStream(string filename);
    }
}
