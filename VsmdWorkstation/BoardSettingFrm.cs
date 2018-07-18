using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VsmdLib;

namespace VsmdWorkstation
{
    public partial class BoardSettingFrm : Form
    {
        private ChromiumWebBrowser m_browser;
        private BridgeObject m_externalObj = new BridgeObject();
        Vsmd vsmd = null;
        VsmdInfo axisX = null;
        VsmdInfo axisY = null;
        VsmdInfo axisZ = null;

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

        private void btnMove_Click(object sender, EventArgs e)
        {
            vsmd = new Vsmd();
            axisX = vsmd.createVsmdInfo(1);
            bool ret = vsmd.openSerailPort("COM3", 9600);
            axisX.flgAutoUpdate = false;
            axisX.enable();
            axisX.cfgSpd(128000);
            axisX.cfgCur(1.6f, 1.4f, 0.8f);
        }

        private void BoardSettingFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                //ParameterizedThreadStart MyImpDelegate = new ParameterizedThreadStart(this, this.mythread);
                //Thread MyimpThread = new Thread(MyImpDelegate);
                //MyimpThread.IsBackground = true;
                //MyimpThread.Start(yourparam);
            }
        }                                                                                          
    }
}
