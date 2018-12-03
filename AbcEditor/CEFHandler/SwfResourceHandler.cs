using CefSharp;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AbcEditor.CEFHandler
{
    public class SwfResourceHandler : HttpResourceHandler
    {
        public Func<byte[], byte[]> Filtering = null;

        public SwfResourceHandler(Func<byte[], byte[]> filter)
        {
            Filtering = filter;
        }

        public override bool ProcessRequestAsync(IRequest request, ICallback callback)
        {
            var url = request.Url;

            Task.Run(() =>
            {
                using (callback)
                {
                    var data = Filtering?
                        .Invoke(Download(url));

                    var stream = new MemoryStream(data);

                    ResponseLength = stream.Length;
                    StatusCode = (int)HttpStatusCode.OK;
                    Stream = stream;

                    callback.Continue();
                }
            });

            return true;
        }
    }
}
