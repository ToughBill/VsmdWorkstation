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
    public partial class VsmdSettingFrm : Form
    {
        private ChromiumWebBrowser m_browser;
        private BridgeObject m_externalObj;

        public VsmdSettingFrm()
        {
            InitializeComponent();
            InitBrowser();
        }
        private void InitBrowser()
        {
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            //Cef.Initialize(new CefSettings());
            string url = Application.StartupPath + @"\..\..\..\html\vsmdSetting.html";
            m_browser = new ChromiumWebBrowser(url);
            this.Controls.Add(m_browser);
            m_browser.Dock = DockStyle.Fill;
            BindingOptions opt = new BindingOptions();
            opt.CamelCaseJavascriptNames = false;
            m_browser.RegisterJsObject("externalObj", m_externalObj, opt);
        }

        private void VsmdSettingFrm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_browser.ShowDevTools();
        }
    }
}
