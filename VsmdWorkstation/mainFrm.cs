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
        private BoardSetting m_curBoardSettings;

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
            panelGrid.Controls.Add(m_browser);
            m_browser.Dock = DockStyle.Fill;
            //m_browser.Left = 0;
            //m_browser.Width = this.Width;
            //m_browser.Top = toolStrip.Bottom;
            //m_browser.Height = statusBarEx.Top - toolStrip.Bottom;
            //m_browser.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);

            m_externalObj = new BridgeObject(m_browser);
            m_externalObj.onGridPageDomLoaded += OnGridPageDomLoaded;
            BindingOptions opt = new BindingOptions();
            opt.CamelCaseJavascriptNames = false;
            m_browser.RegisterJsObject("externalObj", m_externalObj, opt);
        }

        private void InitBoardSettings()
        {
            BoardMeta newBoard = new BoardMeta();
            newBoard = new BoardMeta();
            newBoard.Name = "3 X 8 X 12";
            newBoard.BlockCount = 3;
            newBoard.RowCount = 12;
            newBoard.ColumnCount = 8;
            newBoard.FirstTubeX = 300;
            newBoard.FirstTubeY = 300;
            newBoard.TubeDistanceX = 200;
            newBoard.TubeDistanceY = 200;
            newBoard.TubeDiameter = 200;
            BoardSetting.GetInstance().AddNewBoard(newBoard);

            cmbBoards.Items.Add(newBoard.Name);
            cmbBoards.SelectedIndex = 0;
        }
        private void InitVsmdController()
        {
            //bool ret = VsmdController.GetVsmdController().Init("COM3", 9600);
            //if (!ret)
            //{
            //    statusBarEx.DisplayMessage(MessageType.Error, "初始化控制器失败！");
            //}
        }
        private void OnGridPageDomLoaded()
        {
            m_externalObj.BuildGrid(BoardSetting.GetInstance().CurrentBoard);
        }
        private void InitTubeGrid()
        {
            //BoardSettings curBoard = BoardSettings.GetCurrentBoardSetting();
            //System.Threading.Thread.Sleep(2000);
            //m_externalObj.BuildGrid(curBoard);
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
            m_externalObj.Move();
        }
    }
}
