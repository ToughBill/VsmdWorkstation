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

namespace VsmdWorkstation
{
    public partial class MainFrm : Form
    {
        private ChromiumWebBrowser m_browser;

        public MainFrm()
        {
            InitializeComponent();
            InitBrowser();
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {

        }
        private void InitBrowser()
        {
            Cef.Initialize(new CefSettings());
            string url = Application.StartupPath + @"\..\..\..\html\main.html";
            m_browser = new ChromiumWebBrowser(url);
            this.Controls.Add(m_browser);

            m_browser.Left = 0;
            m_browser.Width = this.Width;
            m_browser.Top = toolStrip.Bottom;
            m_browser.Height = statusStrip.Top - toolStrip.Bottom;
            m_browser.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);

        }
    }
}
