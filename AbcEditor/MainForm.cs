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
            CEFEngine = new ChromiumWebBrowser("http://google.com/");
            var lifeSpanHandler = new LifeSpanHandler();
            lifeSpanHandler.PopupRequest += x => Process.Start(x);
            CEFEngine.LifeSpanHandler = lifeSpanHandler;
            CEFEngine.Dock = DockStyle.Fill;
            CefSharpPanel.Controls.Add(CEFEngine);
        }
    }
}
