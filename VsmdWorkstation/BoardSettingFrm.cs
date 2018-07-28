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

        private void btnMove_Click(object sender, EventArgs e)
        {
            //vsmd = new Vsmd();
            //axisX = vsmd.createVsmdInfo(1);
            //bool ret = vsmd.openSerailPort("COM3", 9600);
            //axisX.flgAutoUpdate = false;
            //axisX.enable();
            //axisX.cfgSpd(128000);
            //axisX.cfgCur(1.6f, 1.4f, 0.8f);
        }

        private void BoardSettingFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                //ParameterizedThreadStart MyImpDelegate = new ParameterizedThreadStart(this, this.mythread);
                //Thread MyimpThread = new Thread(MyImpDelegate);
                //MyimpThread.IsBackground = true;
                //MyimpThread.Start(yourparam);
            }
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
            //SetTubePosiion frm = new SetTubePosiion(axisType, GetTempBoardSetting());
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    textbox.Text = frm.PositionVal.ToString();
            //}
            SetTubePositionFrm frm = new SetTubePositionFrm(axisType);
            frm.ShowDialog();
            textbox.Text = VsmdController.GetVsmdController().GetAxis(axisType).curPos.ToString();
        }
    }
}
