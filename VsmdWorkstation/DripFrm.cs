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
    public enum DripStatus
    {
        Idle,
        Moving,
        PauseMove
    }
    public partial class DripFrm : Form
    {
        private ChromiumWebBrowser m_browser;
        private BridgeObject m_externalObj;
        private DripStatus m_dripStatus = DripStatus.Idle;
        private bool m_delayToBuildGrid = false;
        private bool m_isOpened = true;
        public DripFrm()
        {
            InitializeComponent();
            InitBrowser();
        }

        public bool IsOpened
        {
            get
            {
                return m_isOpened;
            }
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
#if DEBUG
            btnDevTools.Visible = true;
#endif

            InitBoardSettings();
        }
        private void InitBrowser()
        {
            BridgeObject.InitializeCef();
            string url = Application.StartupPath + @"\html\tubeGrid.html";
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
            m_browser.IsBrowserInitializedChanged += IsBrowserInitializedChanged;
            m_browser.MenuHandler = new CEFMenuHandler();
        }

        private void IsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            if(e.IsBrowserInitialized && m_delayToBuildGrid)
            {
                m_externalObj.BuildGrid(BoardSetting.GetInstance().CurrentBoard);
            }
        }

        private void InitBoardSettings()
        {
            BoardSetting.GetInstance().GetAllBoardMetaes().ForEach((meta) =>
                {
                    cmbBoards.Items.Add(meta);
                }
            );
            if(cmbBoards.Items.Count > 0)
            {
                cmbBoards.SelectedIndex = 0;
            }
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
            if(BoardSetting.GetInstance().CurrentBoard != null)
            {
                m_externalObj.BuildGrid(BoardSetting.GetInstance().CurrentBoard);
            }
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
            if(m_dripStatus == DripStatus.Moving)
            {
                e.Cancel = true;
            }
            m_isOpened = false;
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
                m_externalObj.PauseMove();
                m_dripStatus = DripStatus.PauseMove;
                btnPause.Text = "继续滴液";
            }
            else if(m_dripStatus == DripStatus.PauseMove)
            {
                m_externalObj.ResumeMove();
                m_dripStatus = DripStatus.Moving;
                btnPause.Text = "暂停滴液";
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
            if (m_browser.IsBrowserInitialized)
            {
                m_externalObj.BuildGrid(BoardSetting.GetInstance().CurrentBoard);
            }
            else
            {
                m_delayToBuildGrid = true;
            }
        }

        private void btnDevTools_Click(object sender, EventArgs e)
        {
            m_browser.ShowDevTools();
        }

        private void btnRestGrid_Click(object sender, EventArgs e)
        {
            m_externalObj.ResetBoard();
        }

        private async void btnResetControler_Click(object sender, EventArgs e)
        {
            await VsmdController.GetVsmdController().ResetVsmdController();
        }
    }

    public class CEFMenuHandler : CefSharp.IContextMenuHandler
    {
        void CefSharp.IContextMenuHandler.OnBeforeContextMenu(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.IMenuModel model)
        {
            model.Clear();
        }
        bool CefSharp.IContextMenuHandler.OnContextMenuCommand(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.CefMenuCommand commandId, CefSharp.CefEventFlags eventFlags)
        {
            //throw new NotImplementedException();
            return false;
        }
        void CefSharp.IContextMenuHandler.OnContextMenuDismissed(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame)
        {
            //throw new NotImplementedException();
        }
        bool CefSharp.IContextMenuHandler.RunContextMenu(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.IMenuModel model, CefSharp.IRunContextMenuCallback callback)
        {
            return false;
        }
    }
}
