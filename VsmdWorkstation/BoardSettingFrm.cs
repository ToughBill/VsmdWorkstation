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
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
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

            return temp;
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
    }
}
