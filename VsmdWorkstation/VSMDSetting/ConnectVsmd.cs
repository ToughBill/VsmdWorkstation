using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace VsmdWorkstation
{
    public delegate void DelInitVsmdConnectionCB(InitResult initRet);
    public partial class ConnectVsmd : Form, IPerference
    {
        private DelInitVsmdConnectionCB m_initCB;
        private bool m_isConnecting;
        public bool IsClosed { get; set; }
        
        public ConnectVsmd(DelInitVsmdConnectionCB callback = null)
        {
            InitializeComponent();
            m_initCB = callback;
        }

        private void ConnectVsmd_Load(object sender, EventArgs e)
        {
            cmbPort.Items.AddRange(SerialPort.GetPortNames());
            cmbPumpPort.Items.AddRange(SerialPort.GetPortNames());
            if (VsmdController.GetVsmdController().IsInitialized())
            {
                //lblCurInfo.Text = VsmdController.GetVsmdController().GetPort() + ", " + VsmdController.GetVsmdController().GetBaudrate() + ", " + PumpController.GetPumpController().GetPort();
                int idx = cmbPort.Items.IndexOf(VsmdController.GetVsmdController().GetPort());
                if (idx > -1)
                {
                    cmbPort.SelectedIndex = idx;
                }
                idx = cmbBaudrate.Items.IndexOf(VsmdController.GetVsmdController().GetBaudrate().ToString());
                if (idx > -1)
                {
                    cmbBaudrate.SelectedIndex = idx;
                }
                idx = cmbPumpPort.Items.IndexOf(PumpController.GetPumpController().GetPort());
                if (idx > -1)
                {
                    cmbPumpPort.SelectedIndex = idx;
                }
                UpdateControlState();
                return;
            }
            else
            {
                UpdateControlState();
            }
            
            if (Preference.GetInstace().HasPreference)
            {
                int idx = cmbPort.Items.IndexOf(Preference.GetInstace().VsmdPort);
                if(idx > -1)
                {
                    cmbPort.SelectedIndex = idx;
                }
                idx = cmbBaudrate.Items.IndexOf(Preference.GetInstace().Baudrate.ToString());
                if (idx > -1)
                {
                    cmbBaudrate.SelectedIndex = idx;
                }
                idx = cmbPumpPort.Items.IndexOf(Preference.GetInstace().PumpPort);
                if (idx > -1)
                {
                    cmbPumpPort.SelectedIndex = idx;
                }
            }
            else
            {
                if (cmbPort.Items.Count > 0)
                {
                    cmbPort.SelectedIndex = 0;
                    cmbPumpPort.SelectedIndex = 0;
                }
                cmbBaudrate.SelectedIndex = 2;
            }
            IsClosed = false;

            if (GeneralSettings.GetInstance().AutoConnect)
            {
                btnConnect_Click(null, null);
            }
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (VsmdController.GetVsmdController().IsInitialized())
            {
                VsmdController.GetVsmdController().Dispose();
                PumpController.GetPumpController().Dispose();
                UpdateControlState();
                return;
            }

            if (m_isConnecting)
            {
                return;
            }
            
            if(cmbPort.SelectedItem == null)
            {
                StatusBar.DisplayMessage(VsmdWorkstation.Controls.MessageType.Error, "电机端口不能为空！");
                return;
            }
            if (cmbPumpPort.SelectedItem == null)
            {
                StatusBar.DisplayMessage(VsmdWorkstation.Controls.MessageType.Error, "蠕动泵端口不能为空！");
                return;
            }
            string vsmdPort = cmbPort.SelectedItem.ToString();
            string pumpPort = cmbPumpPort.SelectedItem.ToString();
            if (vsmdPort == pumpPort)
            {
                StatusBar.DisplayMessage(VsmdWorkstation.Controls.MessageType.Error, "电机端口与蠕动泵端口不能相同！");
                return;
            }
            int baudrate = int.Parse(cmbBaudrate.SelectedItem.ToString());
            if (vsmdPort == VsmdController.GetVsmdController().GetPort() &&
                baudrate == VsmdController.GetVsmdController().GetBaudrate() && 
                pumpPort == PumpController.GetPumpController().GetPort())
            {
                if (m_initCB != null)
                {
                    m_initCB(new InitResult() { Message = "设备连接成功!", IsSuccess = true });
                }
                this.Close();

            }

            m_isConnecting = true;
            InitResult vsmdRet = await VsmdController.GetVsmdController().Init(vsmdPort, baudrate);
            InitResult pumpRet = PumpController.GetPumpController().Init(pumpPort);
            if (m_initCB != null)
            {
                InitResult connectRet = new InitResult();
                connectRet.IsSuccess = vsmdRet.IsSuccess && pumpRet.IsSuccess;
                connectRet.Message = vsmdRet.IsSuccess ? pumpRet.Message : vsmdRet.Message;
                m_initCB(connectRet);
                m_isConnecting = false;
            }
            if (vsmdRet.IsSuccess && pumpRet.IsSuccess)
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConnectVsmd_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsClosed = true;
        }

        public void UpdateControlState()
        {
            if (VsmdController.GetVsmdController().IsInitialized())
            {
                cmbPort.Enabled = false;
                cmbBaudrate.Enabled = false;
                cmbPumpPort.Enabled = false;
                lblCurInfo.Text = "已连接";
                btnConnect.Text = "断开连接";
            }
            else
            {
                cmbPort.Enabled = true;
                cmbBaudrate.Enabled = true;
                cmbPumpPort.Enabled = true;
                lblCurInfo.Text = "未连接";
                btnConnect.Text = "连接";
            }
        }

        public bool SavePref()
        {
            bool ret = true;
            Preference perfInst = Preference.GetInstace();
            string curVsmdPort = cmbPort.SelectedItem.ToString();
            int curBaudrate = int.Parse(cmbBaudrate.SelectedItem.ToString());
            string pumpPort = cmbPumpPort.SelectedItem.ToString();
            if (perfInst.VsmdPort != curVsmdPort ||
                perfInst.Baudrate != curBaudrate ||
                perfInst.PumpPort != pumpPort)
            {
                perfInst.VsmdPort = curVsmdPort;
                perfInst.Baudrate = curBaudrate;
                perfInst.PumpPort = pumpPort;
                ret = perfInst.Save();
            }
            
            
            return ret;
        }

        private void ConnectVsmd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (VsmdController.GetVsmdController().IsInitialized())
                SavePref();
        }
    }
}
