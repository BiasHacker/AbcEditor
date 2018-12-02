using CefSharp;
using System.IO;
using System.Net;
using System.Net.Cache;

namespace AbcEditor.CEFHandler
{
    public class HttpResourceHandler : ResourceHandler
    {
        public string UserAgent = "Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36";

        public HttpRequestCacheLevel CachePolicy = HttpRequestCacheLevel.BypassCache;

        public byte[] Download(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.CachePolicy = new HttpRequestCachePolicy(CachePolicy);
            request.Proxy = null;
            request.Method = "GET";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.UserAgent = UserAgent;
            request.Headers["Accept-Language"] = "ja,en-US;q=0.8,en;q=0.6";
            request.Headers["Accept-Encoding"] = "gzip, deflate, sdch";
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var memoryStream = new MemoryStream())
            {
                MimeType = response.ContentType;
                responseStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
