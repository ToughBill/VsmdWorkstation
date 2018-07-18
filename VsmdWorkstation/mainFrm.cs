using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VsmdWorkstation
{
    public partial class MainFrm : Form
    {
        private ChromiumWebBrowser m_browser;
        private BridgeObject m_externalObj = new BridgeObject();
        private BoardSettings m_curBoardSettings;

        public MainFrm()
        {
            InitializeComponent();
            InitBrowser();
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            InitBoardSettings();
        }
        private void InitBrowser()
        {
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            Cef.Initialize(new CefSettings());
            string url = Application.StartupPath + @"\..\..\..\html\main.html";
            m_browser = new ChromiumWebBrowser(url);
            this.Controls.Add(m_browser);

            m_browser.Left = 0;
            m_browser.Width = this.Width;
            m_browser.Top = toolStrip.Bottom;
            m_browser.Height = statusStrip.Top - toolStrip.Bottom;
            m_browser.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            BindingOptions opt = new BindingOptions();
            opt.CamelCaseJavascriptNames = false;
            m_browser.RegisterJsObject("externalObj", m_externalObj, opt);
        }

        private void InitBoardSettings()
        {
            m_curBoardSettings = new BoardSettings();
        }

        private void tsmBoardSetting_Click(object sender, EventArgs e)
        {
            BoardSettingFrm frm = new BoardSettingFrm();
            frm.ShowDialog();
        }
    }
}
