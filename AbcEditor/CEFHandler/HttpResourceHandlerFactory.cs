using CefSharp;

namespace AbcEditor.CEFHandler
{
    public class HttpResourceHandlerFactory : IResourceHandlerFactory
    {
        bool IResourceHandlerFactory.HasHandlers
        {
            get { return true; }
        }

        IResourceHandler IResourceHandlerFactory.GetResourceHandler(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request)
        {
            if (request.Url.Contains("//hoge"))
            {
                return new HttpResourceHandler();
            }
            return null;
        }
    }
}
