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
using VsmdWorkstation.Drip;
using VsmdWorkstation.Record;
using VsmdWorkstation.Utils;

namespace VsmdWorkstation
{
    public enum PipettingStatus
    {
        Idle,
        Moving,
        PauseMove
    }
    public partial class DripFrm : Form, IPerference
    {
        private ChromiumWebBrowser m_browser;
        private BridgeObject m_externalObj;
        private PipettingStatus m_pipettingStatus = PipettingStatus.Idle;
        private bool m_delayToBuildGrid = false;
        private bool m_isOpened = true;
        private bool m_manuallySelect = false;
        RunInfoModel runInfoModel = new RunInfoModel();
        public DripFrm()
        {
            InitializeComponent();
            InitBrowser();
            GlobalVars.Instance.onPrjectInfoChanged += Instance_onPrjectInfoChanged;
        }


        public PipettingStatus PipettingStatus
        {
            get
            {
                return m_pipettingStatus;
            }

        }

        private void Instance_onPrjectInfoChanged(object sender, bool e)
        {
            cmbProjectName.Items.Clear();
            foreach (var prjInfo in GlobalVars.Instance.PrjInfoCollection.ProjectInfos)
            {
                cmbProjectName.Items.Add(prjInfo.Name);
            }
        }

      

        public bool IsOpened
        {
            get
            {
                return m_isOpened;
            }
        }

        private void DripFrm_Load(object sender, EventArgs e)
        {
#if DEBUG
            btnDevTools.Visible = true;
#endif
            
            foreach(var prjInfo in GlobalVars.Instance.PrjInfoCollection.ProjectInfos)
            {
                cmbProjectName.Items.Add(prjInfo.Name);
            }
            InitBoardSettings();
        }
        private void InitBrowser()
        {
            BridgeObject.InitializeCef();
#if DEBUG
            string url = Application.StartupPath + @"\..\..\..\html\tubeGrid.html";
#else
            string url = Application.StartupPath + @"\html\tubeGrid.html";
#endif

            m_browser = new ChromiumWebBrowser(url);
            panelGrid.Controls.Add(m_browser);
            m_browser.Dock = DockStyle.Fill;

            m_externalObj = new BridgeObject(m_browser);
            m_externalObj.onGridPageDomLoaded += OnGridPageDomLoaded;
            m_externalObj.onPipettingFinished += OnPipettingFinished;
            m_externalObj.onGridTubeSelected += OnGridTubeSelected;
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
            cmbBoards.Items.Clear();
            txtDelaySeconds.Text = Preference.GetInstace().DelaySeconds.ToString();
            var allMetas = BoardSetting.GetInstance().GetAllBoardMetaes();
            List<BoardMeta> sortedMetas = new List<BoardMeta>();
            if(BoardSetting.GetInstance().NamesOrder.Count > 0)
            {
                foreach(var boardName in BoardSetting.GetInstance().NamesOrder)
                {
                    var validNameMetas= allMetas.Where(x => x.Name == boardName);
                    if(validNameMetas.Count() > 0)
                    {
                        sortedMetas.Add(validNameMetas.First());
                        //allMetas.Remove(validNameMetas.First());
                    }
                }
            }

            foreach(var meta in allMetas) //add remaining
            {
                if(!sortedMetas.Contains(meta))
                    sortedMetas.Add(meta);
            }
            sortedMetas.ForEach(x => cmbBoards.Items.Add(x));

            Preference perfInst = Preference.GetInstace();
            if (cmbBoards.Items.Count > 0)
            {
                if (perfInst.HasPreference)
                {
                    BoardMeta findBoardMeta = BoardSetting.GetInstance().GetAllBoardMetaes().Find((meta) =>
                    {
                        return meta.ID == perfInst.BoardID;
                    });
                    if (findBoardMeta != null)
                    {
                        cmbBoards.SelectedItem = findBoardMeta;
                    }
                }
                else
                {
                    cmbBoards.SelectedIndex = 0;
                }
            }
        }

        private void OnGridPageDomLoaded()
        {
            if(BoardSetting.GetInstance().CurrentBoard != null)
            {
                m_externalObj.BuildGrid(BoardSetting.GetInstance().CurrentBoard);
            }
        }
        private void OnPipettingFinished()
        {
            m_pipettingStatus = PipettingStatus.Idle;
            this.Invoke(new DelDripFinished(delegate
            {
                UpdateButtons();
            }));
        }

        private void OnGridTubeSelected(int count)
        {
            this.Invoke(new Action(delegate() {
                txtSampleCnt.Text = count.ToString();
                txtSampleCnt.BackColor = Color.FromArgb(0x99, 0xCC, 0xFF);
                m_manuallySelect = true;
            }));
        }
        private void tsmBoardSetting_Click(object sender, EventArgs e)
        {
            BoardSettingFrm frm = new BoardSettingFrm();
            frm.ShowDialog();
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(m_pipettingStatus == PipettingStatus.Moving)
            {
                e.Cancel = true;
            }
            else
            {
                SavePref();
                m_isOpened = false;
            }
      
        }
        public bool SavePref()
        {
            bool ret = true;
            Preference perfInst = Preference.GetInstace();
            BoardMeta selectedBoardMeta = cmbBoards.SelectedItem as BoardMeta;
            if(selectedBoardMeta != null && selectedBoardMeta.ID != perfInst.BoardID)
            {
                perfInst.BoardID = selectedBoardMeta.ID;
                ret = perfInst.Save();
            }
            
            return ret;
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
            double val = 0;
            
            bool bok = double.TryParse(txtDelaySeconds.Text, out val);
            if(!bok)
            {
                MessageBox.Show("体积必须为数字！");
                return;
            }
            if(val == 0)
            {
                MessageBox.Show("体积必须大于0！");
                return;
            }

            Preference.GetInstace().DelaySeconds = val;

            int cnt = 0;
            bok = int.TryParse(txtSampleCnt.Text, out cnt);
            if (!bok)
            {
                MessageBox.Show("样本数必须为数字！");
                return;
            }
            if (val == 0)
            {
                MessageBox.Show("样本数必须大于0！");
                return;
            }
            if (!m_manuallySelect)
            {
                m_externalObj.ResetBoard();
                m_externalObj.SelectTubes(cnt);
            }
            Preference.GetInstace().Save();
            //Logger.Instance.Write("about to start pipetting");
            m_externalObj.Move();
            m_pipettingStatus = PipettingStatus.Moving;
            runInfoModel.AddRunInfo(cmbProjectName.SelectedItem.ToString(), int.Parse(txtSampleCnt.Text));
            UpdateButtons();
     
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            m_externalObj.StopMove();
            m_pipettingStatus = PipettingStatus.Idle;
            UpdateButtons();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (m_pipettingStatus == PipettingStatus.Moving)
            {
                m_externalObj.PauseMove();
                m_pipettingStatus = PipettingStatus.PauseMove;
                btnPause.Text = "继续滴液";
            }
            else if(m_pipettingStatus == PipettingStatus.PauseMove)
            {
                m_externalObj.ResumeMove();
                m_pipettingStatus = PipettingStatus.Moving;
                btnPause.Text = "暂停滴液";
            }
            UpdateButtons();
        }
        private void UpdateButtons()
        {
            bool isIdel = m_pipettingStatus == PipettingStatus.Idle;
            btnStart.Enabled = isIdel;
            btnWash.Enabled = isIdel;
            btnPause.Enabled = !isIdel;
            btnStop.Enabled = !isIdel;
            cmbProjectName.Enabled = isIdel;
            txtSampleCnt.Enabled = isIdel;
            txtDelaySeconds.Enabled = isIdel;
            btnSelectAll.Enabled = isIdel;
            btnClearAll.Enabled = isIdel;
            btnMoveFront.Enabled = isIdel;
            btnMoveToBack.Enabled = isIdel;
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

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            m_externalObj.SelectAllTubes();
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            m_externalObj.ReverseSelect();
        }

        private void txtSampleCnt_TextChanged(object sender, EventArgs e)
        {
            txtSampleCnt.BackColor = Color.Yellow;
            m_manuallySelect = false;
        }

        private void cmbProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool prjSelected = cmbProjectName.SelectedIndex != -1;
            btnWash.Enabled = prjSelected;
            

        }

       

        private async void btnWash_Click(object sender, EventArgs e)
        {
            await  DoWash();
            btnStart.Enabled = true;
        }

        private async Task<bool> DoWash()
        {
            await WashController.DoWash();
            StatusBar.DisplayMessage(VsmdWorkstation.Controls.MessageType.Info, "冲洗完成!");
            return true;
        }

        private async void btnMoveFront_Click(object sender, EventArgs e)
        {

            bool bok = await VsmdController.GetVsmdController().MoveTo(VsmdAxis.Y, 100);
            if(bok)
            {
                StatusBar.DisplayMessage(VsmdWorkstation.Controls.MessageType.Info, "移动到最前端!");
            }
            else
            {
                StatusBar.DisplayMessage(VsmdWorkstation.Controls.MessageType.Error, "未能移动到最前端!");
            }
        }

        private async void btnMoveToBack_Click(object sender, EventArgs e)
        {
            BoardSetting curBoardSetting = BoardSetting.GetInstance();
            bool bok = await VsmdController.GetVsmdController().MoveTo(VsmdAxis.Y, 30500);
            if (bok)
            {
                StatusBar.DisplayMessage(VsmdWorkstation.Controls.MessageType.Info, "移动到后端!");
            }
            else
            {
                StatusBar.DisplayMessage(VsmdWorkstation.Controls.MessageType.Error, "未能移动到后端!");
            }
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
