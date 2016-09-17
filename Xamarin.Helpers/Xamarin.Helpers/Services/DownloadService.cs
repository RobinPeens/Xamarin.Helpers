using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.Helpers.Services
{
    public class DownloadService
    {
        private HttpClient client;
        ProgressBar progress;

        public DownloadService(ProgressBar progress)
        {
            this.client = new HttpClient();
            this.progress = progress;
        }

        public async Task DownloadFileAsync(string url, CancellationToken token, Action<string> onFail, Action onComplete)
        {
            try
            {
                var fileAccess = DependencyService.Get<IFileAccess>();

                Uri uri = new Uri(url);
                string filename = System.IO.Path.GetFileName(uri.LocalPath);
                Stream fileStream = null;

                try
                {
                    fileStream = fileAccess.GetFileStream(filename);
                }
                catch
                {
                    onFail($"Failed to get file writer");
                    return;
                }

                var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token);

                if (!response.IsSuccessStatusCode)
                {
                    onFail($"Failed to download file, Status code : {response.StatusCode}");
                    return;
                }

                var total = response.Content.Headers.ContentLength.HasValue ? response.Content.Headers.ContentLength.Value : -1L;
                var canReportProgress = total != -1 && progress != null;

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var totalRead = 0L;
                    var buffer = new byte[4096];
                    var isMoreToRead = true;

                    do
                    {
                        token.ThrowIfCancellationRequested();

                        var read = await stream.ReadAsync(buffer, 0, buffer.Length, token);

                        if (read == 0)
                        {
                            isMoreToRead = false;
                        }
                        else
                        {
                            var data = new byte[read];
                            buffer.ToList().CopyTo(0, data, 0, read);

                            fileStream.Write(data, 0, read);

                            // TODO: put here the code to write the file to disk

                            totalRead += read;

                            if (canReportProgress)
                            {
                                progress.Progress = (totalRead * 1d) / (total * 1d) * 100;
                            }
                        }
                    } while (isMoreToRead);

                    onComplete();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to download file : {ex.Message}");
                onFail($"Failed to download file : {ex.Message}");
            }
        }
    }
}
