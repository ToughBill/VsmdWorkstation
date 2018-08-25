﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VsmdWorkstation.Controls;

namespace VsmdWorkstation
{
    public partial class GeneralSettingFrm : Form
    {
        public GeneralSettingFrm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            GeneralSettingMeta meta = GeneralSettings.GetInstance().GetSettingMeta();
            meta.DripInterval = (int)numDripInter.Value;
            meta.MoveSpeed = float.Parse(txtMoveSpd.Text.Trim());
            meta.OutputCommandLog = ckbEnableCmdLog.Checked;
            bool retVal = GeneralSettings.GetInstance().Save();
            if (retVal)
            {
                StatusBar.DisplayMessage(MessageType.Info, "设置成功！");
                this.Close();
            }
            else
            {
                StatusBar.DisplayMessage(MessageType.Error, "设置失败！");
            }
        }

        private void GeneralSettingFrm_Load(object sender, EventArgs e)
        {
            InitFormData();
        }

        private void InitFormData()
        {
            GeneralSettingMeta meta = GeneralSettings.GetInstance().GetSettingMeta();
            numDripInter.Value = (decimal)meta.DripInterval;
            txtMoveSpd.Text = meta.MoveSpeed.ToString();
            ckbEnableCmdLog.Checked = meta.OutputCommandLog;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}