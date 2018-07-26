﻿using System;
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

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            //Vsmd m_vsmd = new Vsmd();
            //bool ret = m_vsmd.openSerailPort("COM1", 9600);
            //m_vsmd.closeSerailPort();
            //Vsmd m_vsmd2 = new Vsmd();
            //m_vsmd2.openSerailPort("COM1", 9600);
            InitResult initRet = await VsmdController.GetVsmdController().Init(cmbPort.SelectedItem.ToString(), int.Parse(cmbBaudrate.SelectedItem.ToString()));
            if (initRet.IsSuccess)
            {
                this.IsConnected = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(initRet.ErrorMsg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}