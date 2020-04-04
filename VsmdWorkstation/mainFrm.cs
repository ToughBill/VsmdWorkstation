using System;
using System.Windows.Forms;
using VsmdWorkstation.Controls;
using VsmdWorkstation.Record;

namespace VsmdWorkstation
{
    public partial class MainFrm : Form
    {
        private ConnectVsmd m_connectForm;
        private DripFrm m_dripForm;
        public MainFrm()
        {
            InitializeComponent();
            this.KeyDown += MainFrm_KeyDown;
        }

        private void MainFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                AboutBox aboutForm = new AboutBox();
                aboutForm.ShowDialog();
            }
        }

        private void tsmVsmdSetting_Click(object sender, EventArgs e)
        {
            SetAllSubFormState();
            VsmdSettingFrm frm = new VsmdSettingFrm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            InitStatusBar();
            Preference.GetInstace().Load();
            GeneralSettings.GetInstance().LoadGeneralSettings();
            BoardSetting.GetInstance().LoadBoardSettings();
            InitVsmdConnection();
        }
        private void InitStatusBar()
        {
            StatusBar.Init(statusBarEx);
        }
        private void InitVsmdConnection()
        {
            ConnectVsmd();
        }
        private void InitVsmdConnectionCB(InitResult initRet)
        {
            if (initRet.IsSuccess)
            {
                StatusBar.DisplayMessage(MessageType.Info, "设备连接成功！");
                if (m_dripForm == null)
                {
                    m_dripForm = new DripFrm();
                    m_dripForm.MdiParent = this;
                    m_dripForm.Show();
                    m_connectForm.Close();
                    m_connectForm = null;
                }
            }
            else
            {
                StatusBar.DisplayMessage(MessageType.Error, initRet.Message);
            }
        }

        private void tsmBoardSetting_Click(object sender, EventArgs e)
        {
            SetAllSubFormState();
            BoardSettingFrm frm = new BoardSettingFrm();
            frm.MdiParent = this;
            frm.Show();
        }
        private void ConnectVsmd()
        {
            if(m_connectForm == null || m_connectForm.IsClosed)
            {
                m_connectForm = new ConnectVsmd(InitVsmdConnectionCB);
                m_connectForm.MdiParent = this;
                m_connectForm.Show();
            }
            else
            {
                m_connectForm.Focus();
            }
        }
        private void tsmConnectVsmd_Click(object sender, EventArgs e)
        {
            SetAllSubFormState();
            ConnectVsmd();
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            VsmdController.GetVsmdController().Dispose();
        }

        private void tsmGenaralSetting_Click(object sender, EventArgs e)
        {
            SetAllSubFormState();
            GeneralSettingFrm frm = new GeneralSettingFrm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tsmDrip_Click(object sender, EventArgs e)
        {
            SetAllSubFormState();
            if (VsmdController.GetVsmdController().IsInitialized())
            {
                if (m_dripForm == null || !m_dripForm.IsOpened)
                {
                    m_dripForm = new DripFrm();
                    m_dripForm.MdiParent = this;
                    //m_dripForm.WindowState = FormWindowState.Maximized;
                    m_dripForm.Show();
                }
                else
                {
                    m_dripForm.BringToFront();
                }
            }
            else
            {
                StatusBar.DisplayMessage(MessageType.Error, "设备未连接！");
            }
        }

        private void SetAllSubFormState()
        {
            foreach(Form frm in this.MdiChildren)
            {
                if(frm.WindowState == FormWindowState.Maximized)
                {
                    frm.WindowState = FormWindowState.Normal;
                }
            }
        }

        private void tsmSetProjectNames_Click(object sender, EventArgs e)
        {
            SetProject setProjectWindow = new SetProject();
            setProjectWindow.ShowDialog();
        }

        private void tsmStatistic_Click(object sender, EventArgs e)
        {
            RunHistory runHistory = new RunHistory();
            runHistory.ShowDialog();
        }
    }
}
