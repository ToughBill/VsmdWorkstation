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

namespace VsmdWorkstation
{
    public partial class ConnectVsmd : Form
    {
        public bool IsConnected { get; set; }
        public ConnectVsmd()
        {
            InitializeComponent();
        }

        private void ConnectVsmd_Load(object sender, EventArgs e)
        {
            cmbPort.Items.AddRange(SerialPort.GetPortNames());
            cmbPort.SelectedIndex = 0;
            cmbBaudrate.SelectedIndex = 2;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if(!VsmdController.GetVsmdController().Init(cmbPort.SelectedItem.ToString(), int.Parse(cmbBaudrate.SelectedItem.ToString()), VsmdInitCB))
            {
                MessageBox.Show("打开串口失败", "连接控制器", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.DialogResult = DialogResult.No;
            }
        }
        private void VsmdInitCB(bool isOnline, List<string> errAxis)
        {
            if (isOnline)
            {
                this.IsConnected = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if(errAxis.Count > 0)
            {
                string errMsg = "驱动器 ";
                for(int i = 0; i < errAxis.Count; i++)
                {
                    if(i > 0)
                    {
                        errMsg += ", ";
                    }
                    errMsg += errAxis[i];
                }
                errMsg += "连接失败！";
                MessageBox.Show(errMsg, "连接控制器", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.DialogResult = DialogResult.No;
            }
        }
    }
}
