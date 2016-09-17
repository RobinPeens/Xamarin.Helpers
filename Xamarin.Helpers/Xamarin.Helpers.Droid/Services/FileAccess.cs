using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Helpers.Droid;
using Xamarin.Helpers.Services;

[assembly: Xamarin.Forms.Dependency(typeof(Xamarin.Helpers.Droid.FileAccess))]
namespace Xamarin.Helpers.Droid
{
    public class FileAccess : IFileAccess
    {
        public Stream GetFileStream(string filename)
        {
            //string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // .Personal
            string documentsPath = "/sdcard/testapp";

            if (!Directory.Exists(documentsPath))
            {
                Directory.CreateDirectory(documentsPath);
            }

            var path = Path.Combine(documentsPath, filename);

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                return new FileStream(path, FileMode.CreateNew);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}