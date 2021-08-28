using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SubLibrary
{
    public class WebHelper
    {
        public static async Task<Image> GetImageAsync(string url)
        {
            try
            {
                var req = CreateRequest(url);
                using (var response = await req.GetResponseAsync())
                using (var stream = response.GetResponseStream())
                {
                    //return Bitmap.FromStream(stream);
                    return Image.FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<string> DownloadFileAsync(string url, string fileName, string ext = ".zip", int retryCount = 0)
        {
            try
            {
                var req = CreateRequest(url);
                using (var response = (HttpWebResponse)await req.GetResponseAsync())
                {
                    var outputFile = Path.Combine(fileName + ext);

                    var original = outputFile;
                    int i = 1;
                    while (File.Exists(outputFile))
                    {
                        i++;
                        outputFile = original.Insert(original.Length - 4, $"_{i}");
                    }

                    using (var stream = response.GetResponseStream())
                    {
                        var file = new FileInfo(outputFile);
                        using (var output = file.Create())
                        {
                            int read = 0;
                            var buffer = new byte[4096];

                            while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                            {
                                await output.WriteAsync(buffer, 0, read);
                            }

                            return outputFile;
                        }
                    }
                }
            }
            catch (WebException webException)
            {
                if ((int)webException.Status != 409 && webException.Status != WebExceptionStatus.ProtocolError) // 409: conflict
                {
                    throw new WebException($"Downloading file from url: {url} failed.", webException);
                }

                if (retryCount >= 3)
                {
                    throw new WebException($"Downloading file from url: {url} failed after {retryCount} retries.", webException);
                }

                await Task.Delay(1000 * (retryCount + 1));
                return await DownloadFileAsync(url, fileName, ext,++retryCount);
            }
        }
        private static async Task<string> getResponseStringAsync(HttpWebRequest request)
        {
            using (var response = await request.GetResponseAsync())
            {
                return await getResponseStringAsync(response);
            }
        }
        public static async Task<string> DownloadStringAsync(string url, int retryCount = 0)
        {
            try
            {
                return await getResponseStringAsync(CreateRequest(url));
            }
            catch (WebException webException)
            {
                if ((int)webException.Status != 409 && webException.Status != WebExceptionStatus.ProtocolError) // 409: conflict
                {
                    throw new WebException($"Request to url: {url} failed.", webException);
                }

                if (retryCount >= 3)
                {
                    throw new WebException($"Request to url: {url} failed after {retryCount} retries.", webException);
                }

                await Task.Delay(1000 * (retryCount + 1));

                if (webException.Response != null)
                {
                    var responseStream = webException.Response.GetResponseStream();
                    var errorMessage = "";
                    if (responseStream != null)
                    {
                        using (var sr = new StreamReader(responseStream))
                        {
                            errorMessage = sr.ReadToEnd();
                            if (!string.IsNullOrEmpty(errorMessage))
                            {
                                if (errorMessage.ToLower().Contains("too many requests"))
                                {
                                    throw new WebException($"Request to url: {url} failed. Too many requests");
                                }
                                else
                                {
                                    //errorMessage
                                    throw new WebException($"Request to url: {url} failed." /*Server returned: {errorMessage}"*/, webException);
                                }

                            }
                        }
                    }
                }
                return await DownloadStringAsync(url, ++retryCount);
            }
        }

        private static async Task<string> getResponseStringAsync(WebResponse response)
        {
            using (var stream = response.GetResponseStream())
            using (var sr = new StreamReader(stream))
            {
                return await sr.ReadToEndAsync();
            }
        }
        public static HttpWebRequest CreateRequest(string url, string method = "GET")
        {
            Random random = new Random();
            int rand = random.Next(1, 100);
            var req = WebRequest.CreateHttp(url);
            req.Method = method;
            req.UserAgent = $"UA #{rand}";
            req.Timeout = req.ReadWriteTimeout = 10000;
            req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            return req;
        }
    }
}
