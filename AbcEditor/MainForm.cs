using AbcEditor.CEFHandler;
using CefSharp;
using CefSharp.WinForms;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AbcEditor
{
    public partial class MainForm : Form
    {
        public ChromiumWebBrowser CEFEngine { get; private set; }

        private BrowserSettings BrowserSettings => new BrowserSettings
        {
            DefaultEncoding = "UTF-8",
            JavascriptAccessClipboard = CefState.Enabled,
            JavascriptOpenWindows = CefState.Disabled,
            JavascriptCloseWindows = CefState.Disabled,
            JavascriptDomPaste = CefState.Disabled,
            OffScreenTransparentBackground = true,
            SansSerifFontFamily = "Meiryo"
        };

        public MainForm()
        {
            InitializeComponent();
            InitializeCefSharp();
        }

        public void InitializeCefSharp()
        {
            var settings = new CefSettings();
            var current = AppDomain.CurrentDomain.BaseDirectory;
            var flash = $@"{current}plugins\pepper flash\23.0.0.207\pepflashplayer.dll";
            if (File.Exists(flash))
                settings.CefCommandLineArgs.Add("ppapi-flash-path", flash);
            settings.CachePath = $@"{current}data";
            Cef.Initialize(settings);
            CEFEngine = new ChromiumWebBrowser("http://www.nyafuri.com/TypeShoot/jp.html");
            var lifeSpanHandler = new LifeSpanHandler();
            lifeSpanHandler.PopupRequest += x => Process.Start(x);
            CEFEngine.LifeSpanHandler = lifeSpanHandler;
            CEFEngine.ResourceHandlerFactory = new HttpResourceHandlerFactory();
            CEFEngine.BrowserSettings = BrowserSettings;
            CEFEngine.Dock = DockStyle.Fill;
            CefSharpPanel.Controls.Add(CEFEngine);
        }
    }
}
