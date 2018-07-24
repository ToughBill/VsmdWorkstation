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
        private BridgeObject m_externalObj;
        private BoardSettings m_curBoardSettings;

        public MainFrm()
        {
            InitializeComponent();
            InitBrowser();
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            InitBoardSettings();
            //InitVsmdController();
            
            StatusBar.Init(statusBarEx);
            InitTubeGrid();
        }
        private void InitBrowser()
        {
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            CefSettings setting = new CefSettings();
            setting.RemoteDebuggingPort = 7073;
            Cef.Initialize(setting);
            string url = Application.StartupPath + @"\..\..\..\html\tubeGrid.html";
            m_browser = new ChromiumWebBrowser(url);
            this.Controls.Add(m_browser);

            m_browser.Left = 0;
            m_browser.Width = this.Width;
            m_browser.Top = toolStrip.Bottom;
            m_browser.Height = statusBarEx.Top - toolStrip.Bottom;
            m_browser.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);

            m_externalObj = new BridgeObject(m_browser);

            BindingOptions opt = new BindingOptions();
            opt.CamelCaseJavascriptNames = false;
            m_browser.RegisterJsObject("externalObj", m_externalObj, opt);
        }

        private void InitBoardSettings()
        {
            m_curBoardSettings = new BoardSettings();
            m_curBoardSettings.Name = "3 X 8 X 12";
            m_curBoardSettings.BlockCount = 3;
            m_curBoardSettings.RowCount = 12;
            m_curBoardSettings.ColumnCount = 8;
            m_curBoardSettings.FirstTubeX = 300;
            m_curBoardSettings.FirstTubeY = 300;
            m_curBoardSettings.TubeDistanceX = 200;
            m_curBoardSettings.TubeDistanceY = 200;
            m_curBoardSettings.TubeDiameter = 200;
            BoardSettings.SetCurrentBoardSetting(m_curBoardSettings);

            cmbBoards.Items.Add(m_curBoardSettings.Name);
        }
        private void InitVsmdController()
        {
            bool ret = VsmdController.GetVsmdController().Init("COM3", 9600);
            if (!ret)
            {
                statusBarEx.DisplayMessage(MessageType.Error, "初始化控制器失败！");
            }
        }
        private void InitTubeGrid()
        {
            BoardSettings curBoard = BoardSettings.GetCurrentBoardSetting();
            m_externalObj.BuildGrid(curBoard);
        }
        private void tsmBoardSetting_Click(object sender, EventArgs e)
        {
            BoardSettingFrm frm = new BoardSettingFrm();
            frm.ShowDialog();
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            VsmdController.GetVsmdController().Dispose();
        }

        private void tsmDevTools_Click(object sender, EventArgs e)
        {
            m_browser.ShowDevTools();
        }

        private void tsmVsmdSetting_Click(object sender, EventArgs e)
        {
            VsmdSettingFrm frm = new VsmdSettingFrm();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }
    }
}
