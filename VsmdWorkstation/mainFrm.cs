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
using VsmdWorkstation.Controls;

namespace VsmdWorkstation
{
    public partial class MainFrm : Form
    {
        private ConnectVsmd m_connectForm;
        public MainFrm()
        {
            InitializeComponent();
        }

        private void tsmVsmdSetting_Click(object sender, EventArgs e)
        {
            VsmdSettingFrm frm = new VsmdSettingFrm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            InitStatusBar();
            InitVsmdConnection();
            BoardSetting.GetInstance().LoadBoardSettings();
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
                DripFrm frm = new DripFrm();
                frm.MdiParent = this;
                //frm.WindowState = FormWindowState.Maximized;
                frm.Dock = DockStyle.Fill;
                frm.Show();
                m_connectForm.Dispose();
                m_connectForm = null;
            }
            else
            {
                StatusBar.DisplayMessage(MessageType.Error, "设备连接失败！");
            }
        }

        private void tsmBoardSetting_Click(object sender, EventArgs e)
        {
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
            ConnectVsmd();
        }
    }
}
