using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VsmdWorkstation
{
    public partial class BoardSettingFrm : Form
    {
        private ChromiumWebBrowser m_browser;
        private BridgeObject m_externalObj = new BridgeObject();

        public BoardSettingFrm()
        {
            InitializeComponent();
        }

        private void InitBrowser()
        {
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            Cef.Initialize(new CefSettings());
            string url = Application.StartupPath + @"\..\..\..\html\boardSetting.html";
            m_browser = new ChromiumWebBrowser(url);
            this.Controls.Add(m_browser);
            m_browser.Dock = DockStyle.Fill;
            //m_browser.Left = 0;
            //m_browser.Width = this.Width;
            //m_browser.Top = toolStrip.Bottom;
            //m_browser.Height = statusStrip.Top - toolStrip.Bottom;
            //m_browser.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            BindingOptions opt = new BindingOptions();
            opt.CamelCaseJavascriptNames = false;
            m_browser.RegisterJsObject("externalObj", m_externalObj, opt);
        }
    }
}
