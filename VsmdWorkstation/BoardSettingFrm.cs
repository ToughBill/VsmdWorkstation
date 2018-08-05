using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VsmdLib;
using VsmdWorkstation.Controls;

namespace VsmdWorkstation
{
    public partial class BoardSettingFrm : Form
    {
        public BoardSettingFrm()
        {
            InitializeComponent();
        }

        private void BoardSettingFrm_Load(object sender, EventArgs e)
        {
            if (!VsmdController.GetVsmdController().IsInitialized())
            {
                StatusBar.DisplayMessage(MessageType.Error, "设备未连接！");
                DisableControls();
                return;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidFormData())
            {
                return;
            }
            BoardMeta meta = GetTempBoardSetting();
            
            BoardSetting.GetInstance().AddNewBoard(meta);
            BoardSetting.GetInstance().Save();
        }
        private BoardMeta GetTempBoardSetting()
        {
            BoardMeta temp = new BoardMeta();
            temp.Name = txtName.Text.Trim();
            temp.BlockCount = int.Parse(txtBlockCnt.Text.Trim());
            temp.RowCount = int.Parse(txtRowCnt.Text.Trim());
            temp.ColumnCount = int.Parse(txtColCnt.Text.Trim());
            int val = 0;
            int.TryParse(txtFirstTubePosX.Text.Trim(), out val);
            temp.FirstTubeX = val;

            val = 0;
            int.TryParse(txtFirstTubePosY.Text.Trim(), out val);
        
            val = 0;
            int.TryParse(txtTubeDistX.Text.Trim(), out val);
            temp.TubeDistanceX = val;

            val = 0;
            int.TryParse(txtTubeDistY.Text.Trim(), out val);
            temp.TubeDistanceX = val;

            val = 0;
            int.TryParse(txtBlockDist.Text.Trim(), out val);
            temp.BlockDistanceX = val;

            return temp;
        }
        private bool ValidFormData()
        {
            if(string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowMessage(MessageType.Error, "名称不能为空！");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtBlockCnt.Text))
            {
                ShowMessage(MessageType.Error, "规格-组不能为空！");
                txtBlockCnt.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtRowCnt.Text))
            {
                ShowMessage(MessageType.Error, "规格-行不能为空！");
                txtRowCnt.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtColCnt.Text))
            {
                ShowMessage(MessageType.Error, "规格-列不能为空！");
                txtColCnt.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtBlockDist.Text))
            {
                ShowMessage(MessageType.Error, "组件不能为空！");
                txtBlockDist.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtFirstTubePosX.Text))
            {
                ShowMessage(MessageType.Error, "首孔位置不能为空！");
                txtFirstTubePosX.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtFirstTubePosY.Text))
            {
                ShowMessage(MessageType.Error, "首孔位置不能为空！");
                txtFirstTubePosY.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTubeDistX.Text))
            {
                ShowMessage(MessageType.Error, "孔距不能为空！");
                txtTubeDistX.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTubeDistY.Text))
            {
                ShowMessage(MessageType.Error, "孔距不能为空！");
                txtTubeDistY.Focus();
                return false;
            }

            return true;
        }
        private void ShowMessage(MessageType type, string str)
        {
            StatusBar.DisplayMessage(type, str);
        }
        private void btnSetFX_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtFirstTubePosX, VsmdAxis.X);
        }
        private void btnSetFY_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtFirstTubePosY, VsmdAxis.Y);
        }
        private void btnSetTX_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtTubeDistX, VsmdAxis.X);
        }
        private void btnSetTY_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtTubeDistY, VsmdAxis.Y);
        }
        private void ShowSetDlg(TextBox textbox, VsmdAxis axisType)
        {
            SetTubePositionFrm frm = new SetTubePositionFrm(axisType);
            frm.ShowDialog();
            textbox.Text = VsmdController.GetVsmdController().GetAxis(axisType).curPos.ToString();
        }
        private void DisableControls() {
            btnSetBlockDist.Enabled = false;
            btnSetFX.Enabled = false;
            btnSetFY.Enabled = false;
            btnSetTX.Enabled = false;
            btnSetTY.Enabled = false;
        }
        private void btnChoose_Click(object sender, EventArgs e)
        {
            ItemListFrm frm = new ItemListFrm();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                BoardMeta meta = (BoardMeta)frm.SelectedObject;
                FillData(meta);
            }
        }
        private void FillData(BoardMeta meta)
        {
            txtName.Text = meta.Name;
            txtBlockCnt.Text = meta.BlockCount.ToString();
            txtRowCnt.Text = meta.RowCount.ToString();
            txtColCnt.Text = meta.ColumnCount.ToString();
            txtFirstTubePosX.Text = meta.FirstTubeX.ToString();
            txtFirstTubePosY.Text = meta.FirstTubeY.ToString();
            txtTubeDistX.Text = meta.TubeDistanceX.ToString();
            txtTubeDistY.Text = meta.TubeDistanceY.ToString();
        }

        private void btnSetBlockDist_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtBlockDist, VsmdAxis.X);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
