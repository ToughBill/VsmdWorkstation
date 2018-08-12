using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using VsmdLib;

namespace VsmdWorkstation
{
    public delegate void DelInitVsmdConnectionCB(InitResult initRet);
    public partial class ConnectVsmd : Form
    {
        public bool IsConnected { get; set; }
        private DelInitVsmdConnectionCB m_initCB;
        public bool IsClosed { get; set; }
        public ConnectVsmd(DelInitVsmdConnectionCB callback = null)
        {
            InitializeComponent();
            m_initCB = callback;
        }

        private void ConnectVsmd_Load(object sender, EventArgs e)
        {
            if (VsmdController.GetVsmdController().IsInitialized())
            {
                lblCurConn.Visible = true;
                lblCurInfo.Text = VsmdController.GetVsmdController().GetPort() + ", " + VsmdController.GetVsmdController().GetBaudrate();
                lblCurInfo.Visible = true;
            }
            else
            {
                lblCurConn.Visible = false;
                lblCurInfo.Visible = false;
            }
            cmbPort.Items.AddRange(SerialPort.GetPortNames());
            cmbPort.SelectedIndex = 0;
            cmbBaudrate.SelectedIndex = 2;
            IsClosed = false;
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            InitResult initRet = await VsmdController.GetVsmdController().Init(cmbPort.SelectedItem.ToString(), int.Parse(cmbBaudrate.SelectedItem.ToString()));
            if(m_initCB != null)
            {
                m_initCB(initRet);
            }
            if (initRet.IsSuccess)
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
    }
}
