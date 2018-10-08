using System;
using System.Collections.Generic;
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
            //meta.DispenseInterval = (int)numDripInter.Value;
            meta.MoveSpeed = float.Parse(txtMoveSpd.Text.Trim());
            meta.AutoConnect = ckbAutoConnect.Checked;
            meta.OutputCommandLog = ckbEnableCmdLog.Checked;
            meta.OutputStsCommandLog = ckbEnableStsCmdLog.Checked;
            meta.VolumeDelay = ConvertToDict(lvVolumeDelayDict.Items);
            bool retVal = GeneralSettings.GetInstance().Save();
            if (retVal)
            {
                StatusBar.DisplayMessage(MessageType.Info, "设置成功！");
                if (VsmdController.GetVsmdController().IsInitialized())
                {
                    VsmdController.GetVsmdController().SetOutputCommandLogFlag(meta.OutputCommandLog);
                }
                
                this.Close();
            }
            else
            {
                StatusBar.DisplayMessage(MessageType.Error, "设置失败！");
            }
        }

        private Dictionary<string, string> ConvertToDict(ListView.ListViewItemCollection items)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i< items.Count; i++)
            {
                dict.Add(items[i].Text, items[i].SubItems[1].Text);
            }
            return dict;
        }

        private void GeneralSettingFrm_Load(object sender, EventArgs e)
        {
            InitFormData();
#if DEBUG
            ckbEnableCmdLog.Visible = true;
            ckbEnableStsCmdLog.Visible = true;
#endif
        }

        private void InitFormData()
        {
            GeneralSettingMeta meta = GeneralSettings.GetInstance().GetSettingMeta();
            //numDripInter.Value = (decimal)meta.DispenseInterval;
            foreach(var pair in meta.VolumeDelay)
            {
                var lvi = new ListViewItem(pair.Key);
                lvi.SubItems.Add( pair.Value.ToString());
                lvVolumeDelayDict.Items.Add(lvi);
            }
                
            txtMoveSpd.Text = meta.MoveSpeed.ToString();
            ckbAutoConnect.Checked = meta.AutoConnect;
            ckbEnableCmdLog.Checked = meta.OutputCommandLog;
            ckbEnableStsCmdLog.Checked = meta.OutputStsCommandLog;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = new ListViewItem();
            int vol = 0;
            int delayMs = 0;
            bool bok = int.TryParse(txtVolumeML.Text, out vol);
            if(!bok)
            {
                MessageBox.Show("体积必须为数字！");
                return;
            }
            bok = int.TryParse(txtDelayMs.Text, out delayMs);
            if (!bok)
            {
                MessageBox.Show("延时必须为数字！");
                return;
            }
            lvi.Text = vol.ToString();
            lvi.SubItems.Add( delayMs.ToString() );
            //if(lvVolumeDelayDict.Items)
            for(int i = 0; i<lvVolumeDelayDict.Items.Count; i++)
            {
                if( lvVolumeDelayDict.Items[i].Text == vol.ToString())
                {
                    MessageBox.Show("该体积已经存在！");
                    return;
                }
            }
            lvVolumeDelayDict.Items.Add(lvi);
            
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvVolumeDelayDict.SelectedItems.Count == 0)
                return;
            lvVolumeDelayDict.Items.Remove(lvVolumeDelayDict.SelectedItems[0]);

        }
    }
}
