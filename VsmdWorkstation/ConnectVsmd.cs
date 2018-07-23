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
            if(VsmdController.GetVsmdController().Init(cmbPort.SelectedItem.ToString(), int.Parse(cmbBaudrate.SelectedItem.ToString())))
            {
                this.IsConnected = true;
                this.DialogResult = DialogResult.OK;
            } 
            else
            {
                this.DialogResult = DialogResult.Retry;
            }

        }
    }
}
