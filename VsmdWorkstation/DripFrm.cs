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
    public partial class DripFrm : Form
    {
        public enum DripStatus
        {
            Idle,
            Moving,
            PauseMove
        }
        private ChromiumWebBrowser m_browser;
        private BridgeObject m_externalObj;
        private DripStatus m_dripStatus = DripStatus.Idle;
        public DripFrm()
        {
            InitializeComponent();
            InitBrowser();
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
#if DEBUG
            btnDevTools.Visible = true;
#endif

            InitBoardSettings();
            //InitVsmdController();
            
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
            m_externalObj.onDripFinished += OnDripFinished;
            BindingOptions opt = new BindingOptions();
            opt.CamelCaseJavascriptNames = false;
            m_browser.RegisterJsObject("externalObj", m_externalObj, opt);
        }

        private void InitBoardSettings()
        {
            //BoardMeta newBoard = new BoardMeta();
            //newBoard = new BoardMeta();
            //newBoard.Name = "3 X 8 X 12";
            //newBoard.BlockCount = 3;
            //newBoard.RowCount = 12;
            //newBoard.ColumnCount = 8;
            //newBoard.FirstTubeX = 300;
            //newBoard.FirstTubeY = 300;
            //newBoard.TubeDistanceX = 200;
            //newBoard.TubeDistanceY = 200;
            //newBoard.TubeDiameter = 200;
            //BoardSetting.GetInstance().AddNewBoard(newBoard);
            //BoardSetting.GetInstance().CurrentBoard = newBoard;

            //BoardSetting.GetInstance().LoadBoardSettings();
            //cmbBoards.Items.Add(newBoard.Name);
            BoardSetting.GetInstance().GetAllBoardMetaes().ForEach((meta) =>
                {
                    cmbBoards.Items.Add(meta);
                }
            );
            
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
        private void OnDripFinished()
        {
            m_dripStatus = DripStatus.Idle;
            this.Invoke(new DelDripFinished(delegate
            {
                UpdateButtons();
            }));
            //
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
            m_dripStatus = DripStatus.Moving;
            UpdateButtons();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            m_externalObj.StopMove();
            m_dripStatus = DripStatus.Idle;
            UpdateButtons();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (m_dripStatus == DripStatus.Moving)
            {
                m_externalObj.ResumeMove();
                m_dripStatus = DripStatus.PauseMove;
                btnPause.Text = "暂停滴液";
            }
            else if(m_dripStatus == DripStatus.PauseMove)
            {
                m_externalObj.PauseMove();
                m_dripStatus = DripStatus.Moving;
                btnPause.Text = "继续滴液";
            }
            UpdateButtons();
        }
        private void UpdateButtons()
        {
            btnStart.Enabled = (m_dripStatus == DripStatus.Idle);
            btnStop.Enabled = (m_dripStatus != DripStatus.Idle);
            btnPause.Enabled = (m_dripStatus == DripStatus.Moving || m_dripStatus == DripStatus.PauseMove);
            btnRestGrid.Enabled = (m_dripStatus == DripStatus.Idle);
        }

        private void cmbBoards_SelectedIndexChanged(object sender, EventArgs e)
        {
            BoardSetting.GetInstance().CurrentBoard = (BoardMeta)cmbBoards.Items[cmbBoards.SelectedIndex];
            m_externalObj.BuildGrid(BoardSetting.GetInstance().CurrentBoard);
        }

        private void btnDevTools_Click(object sender, EventArgs e)
        {
            m_browser.ShowDevTools();
        }

        private void btnRestGrid_Click(object sender, EventArgs e)
        {
            m_externalObj.ResetBoard();
        }
    }
}
