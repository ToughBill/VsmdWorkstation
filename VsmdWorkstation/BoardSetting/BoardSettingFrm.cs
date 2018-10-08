using System;
using System.ComponentModel;
using System.Windows.Forms;
using VsmdWorkstation.Controls;

namespace VsmdWorkstation
{
    public partial class BoardSettingFrm : Form
    {
        public enum FORM_MODE
        {
            Add,
            Update
        }
        private FORM_MODE m_mode;
        private BoardMeta m_curMeta;
        public BoardSettingFrm()
        {
            InitializeComponent();
        }

        private void BoardSettingFrm_Load(object sender, EventArgs e)
        {
            panelGrid.Top = panelSite.Top;
            m_curMeta = new BoardMeta();
            m_curMeta.Type = (int)BoardType.Site;
            FillData();
            m_mode = FORM_MODE.Add;

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
            //BoardMeta meta = GetTempBoardSetting();
            FormDatToMeta();
            bool ret = true;
            if(m_mode == FORM_MODE.Add)
            {
                ret = BoardSetting.GetInstance().AddNewBoard(m_curMeta);
                if (ret)
                {
                    ShowMessage(MessageType.Info, "添加成功！");
                    ResetData();
                }
                else
                {
                    ShowMessage(MessageType.Error, "添加失败！");
                }
            }
            else if(m_mode == FORM_MODE.Update)
            {
                //BoardSetting.GetInstance().AddNewBoard(m_curMeta);
                ret = BoardSetting.GetInstance().Save();
                if (ret)
                {
                    ShowMessage(MessageType.Info, "更新成功！");
                    ResetData();
                }
                else
                {
                    ShowMessage(MessageType.Error, "更新失败！");
                }
            }
        }

        private void ResetData()
        {
            int preBoardType = m_curMeta.Type;
            m_curMeta = new BoardMeta();
            m_curMeta.Type = preBoardType;
            m_mode = FORM_MODE.Add;
            FillData();
        }
        private BoardMeta GetTempBoardSetting()
        {
            BoardMeta temp = new BoardMeta();
            FormDatToMetaImpl(temp);

            return temp;
        }
        private void FormDatToMeta()
        {
            FormDatToMetaImpl(m_curMeta);
        }
        private void FormDatToMetaImpl(BoardMeta meta)
        {
            if(m_mode == FORM_MODE.Add)
            {
                meta.ID = BoardSetting.GetInstance().GetNextBoardNum();
            }
            meta.Name = txtName.Text.Trim();
            meta.Type = rbtSite.Checked ? 1 : 2;
            if (rbtSite.Checked)
            {
                meta.SiteCount = int.Parse(txtSiteCnt.Text);
                meta.RowCount = int.Parse(txtSiteRowCnt.Text);
                meta.ColumnCount = int.Parse(txtSiteColCnt.Text);

                meta.Site1FirstTubeX = int.Parse(txtSite1FTX.Text);
                meta.Site1FirstTubeY = int.Parse(txtSite1FTY.Text);
                meta.Site1LastTubeX = int.Parse(txtSite1LTX.Text);
                meta.Site1LastTubeY = int.Parse(txtSite1LTY.Text);
                meta.Site2FirstTubeX = int.Parse(txtSite2FTX.Text);
                meta.Site2FirstTubeY = int.Parse(txtSite2FTY.Text);
                meta.ZTravel = int.Parse(txtSiteZTravel.Text);
                meta.ZDispense = int.Parse(txtSiteZDispense.Text);
            }
            else
            {
                meta.GridCount = int.Parse(txtGridCnt.Text);
                meta.RowCount = int.Parse(txtGridRowCnt.Text);

                meta.GridFirstTubeX = int.Parse(txtGridFTX.Text);
                meta.GridFirstTubeY = int.Parse(txtGridFTY.Text);
                meta.GridLastTubeX = int.Parse(txtGridLTX.Text);
                meta.GridLastTubeY = int.Parse(txtGridLTY.Text);
                meta.ZTravel = int.Parse(txtGridZTravel.Text);
                meta.ZDispense = int.Parse(txtGridZDispense.Text);
            }
        }

        private bool ValidFormData()
        {
            if(string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowMessage(MessageType.Error, "名称不能为空！");
                txtName.Focus();
                return false;
            }
            if (rbtSite.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtSiteCnt.Text))
                {
                    ShowMessage(MessageType.Error, "Site数量不能为空！");
                    txtSiteCnt.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtSiteRowCnt.Text))
                {
                    ShowMessage(MessageType.Error, "行数量不能为空！");
                    txtSiteRowCnt.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtSiteColCnt.Text))
                {
                    ShowMessage(MessageType.Error, "列数量不能为空！");
                    txtSiteColCnt.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtSite1FTX.Text))
                {
                    ShowMessage(MessageType.Error, "Site1首孔位置不能为空！");
                    txtSite1FTX.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtSite1FTY.Text))
                {
                    ShowMessage(MessageType.Error, "Site1首孔位置不能为空！");
                    txtSite1FTY.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtSite1LTX.Text))
                {
                    ShowMessage(MessageType.Error, "Site1尾孔位置不能为空！");
                    txtSite1LTX.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtSite1LTY.Text))
                {
                    ShowMessage(MessageType.Error, "Site1尾孔位置不能为空！");
                    txtSite1LTY.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtSite2FTX.Text))
                {
                    ShowMessage(MessageType.Error, "Site2首孔位置不能为空！");
                    txtSite2FTX.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtSite2FTY.Text))
                {
                    ShowMessage(MessageType.Error, "Site2首孔位置不能为空！");
                    txtSite2FTY.Focus();
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtGridCnt.Text))
                {
                    ShowMessage(MessageType.Error, "Grid数量不能为空！");
                    txtGridCnt.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtGridRowCnt.Text))
                {
                    ShowMessage(MessageType.Error, "行数量不能为空！");
                    txtGridRowCnt.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtGridFTX.Text))
                {
                    ShowMessage(MessageType.Error, "首孔位置不能为空！");
                    txtGridFTX.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtGridFTY.Text))
                {
                    ShowMessage(MessageType.Error, "首孔位置不能为空！");
                    txtGridFTY.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtGridLTX.Text))
                {
                    ShowMessage(MessageType.Error, "尾孔位置不能为空！");
                    txtGridLTX.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtGridLTY.Text))
                {
                    ShowMessage(MessageType.Error, "尾孔位置不能为空！");
                    txtGridLTY.Focus();
                    return false;
                }
            }

            return true;
        }
        private void ShowMessage(MessageType type, string str)
        {
            StatusBar.DisplayMessage(type, str);
        }
        private void btnSetFX_Click(object sender, EventArgs e)
        {
            //ShowSetDlg(txtFirstTubePosX, VsmdAxis.X, SetTubePositionFrm.PosType.Pos);
        }
        private void btnSetFY_Click(object sender, EventArgs e)
        {
            //ShowSetDlg(txtFirstTubePosY, VsmdAxis.Y, SetTubePositionFrm.PosType.Pos);
        }
        private void btnSetTX_Click(object sender, EventArgs e)
        {
            //ShowSetDlg(txtTubeDistX, VsmdAxis.X, SetTubePositionFrm.PosType.Dist);
        }
        private void btnSetTY_Click(object sender, EventArgs e)
        {
            //ShowSetDlg(txtTubeDistY, VsmdAxis.Y, SetTubePositionFrm.PosType.Dist);
        }
        private void ShowSetDlg(TextBox textbox, VsmdAxis axisType)
        {
            SetTubePositionFrm frm = new SetTubePositionFrm(axisType);
            if(frm.ShowDialog() == DialogResult.OK)
            {
                textbox.Text = frm.GetPos().ToString();
            }
            
        }
        private void DisableControls() {
            btnGridFTX.Enabled = false;
            btnGridFTY.Enabled = false;
            btnGridLTX.Enabled = false;
            btnGridLTY.Enabled = false;

            btnSite1FTX.Enabled = false;
            btnSite1FTY.Enabled = false;
            btnSite1LTX.Enabled = false;
            btnSite1LTY.Enabled = false;
            btnSite2FTX.Enabled = false;
            btnSite2FTY.Enabled = false;
        }
        private void btnChoose_Click(object sender, EventArgs e)
        {
            ItemListFrm frm = new ItemListFrm();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                m_curMeta = (BoardMeta)frm.SelectedObject;
                FillData();
                m_mode = FORM_MODE.Update;
            }
        }
        private void FillData()
        {
            txtName.Text = m_curMeta.Name;
            rbtGrid.Checked = m_curMeta.Type == (int)BoardType.Grid;
            rbtSite.Checked = m_curMeta.Type == (int)BoardType.Site;
            if (m_curMeta.Type == (int)BoardType.Grid)
            {
                txtGridCnt.Text = m_curMeta.GridCount.ToString();
                txtGridRowCnt.Text = m_curMeta.RowCount.ToString();
                txtGridFTX.Text = m_curMeta.GridFirstTubeX.ToString();
                txtGridFTY.Text = m_curMeta.GridFirstTubeY.ToString();
                txtGridLTX.Text = m_curMeta.GridLastTubeX.ToString();
                txtGridLTY.Text = m_curMeta.GridLastTubeY.ToString();
                txtGridZTravel.Text = m_curMeta.ZTravel.ToString();
                txtGridZDispense.Text = m_curMeta.ZDispense.ToString();
            }
            else
            {
                txtSiteCnt.Text = m_curMeta.SiteCount.ToString();
                txtSiteRowCnt.Text = m_curMeta.RowCount.ToString();
                txtSiteColCnt.Text = m_curMeta.ColumnCount.ToString();
                txtSite1FTX.Text = m_curMeta.Site1FirstTubeX.ToString();
                txtSite1FTY.Text = m_curMeta.Site1FirstTubeY.ToString();
                txtSite1LTX.Text = m_curMeta.Site1LastTubeX.ToString();
                txtSite1LTY.Text = m_curMeta.Site1LastTubeY.ToString();
                txtSite2FTX.Text = m_curMeta.Site2FirstTubeX.ToString();
                txtSite2FTY.Text = m_curMeta.Site2FirstTubeY.ToString();
                txtSiteZTravel.Text = m_curMeta.ZTravel.ToString();
                txtSiteZDispense.Text = m_curMeta.ZDispense.ToString();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            bool ret = BoardSetting.GetInstance().DeleteBoard(m_curMeta);
            if (ret)
            {
                ShowMessage(MessageType.Info, "删除成功！");
                ResetData();
            }
            else
            {
                ShowMessage(MessageType.Error, "删除失败！");
            }
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            tsmiDelete.Enabled = (m_mode == FORM_MODE.Update);
        }

        private void rbtGrid_CheckedChanged(object sender, EventArgs e)
        {
            SwitchPannel();
        }

        private void rbtSite_CheckedChanged(object sender, EventArgs e)
        {
            SwitchPannel();
        }
        private void SwitchPannel()
        {
            panelSite.Visible = rbtSite.Checked;
            panelGrid.Visible = rbtGrid.Checked;
        }

        private void tsmNew_Click(object sender, EventArgs e)
        {
            m_curMeta = new BoardMeta();
            m_curMeta.ID = BoardSetting.GetInstance().GetNextBoardNo();
            m_curMeta.Type = (int)BoardType.Site;
            FillData();
            m_mode = FORM_MODE.Add;
        }

        private void btnSite1FTX_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtSite1FTX, VsmdAxis.X);
        }

        private void btnSite1FTY_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtSite1FTY, VsmdAxis.Y);
        }

        private void btnSite1LTX_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtSite1LTX, VsmdAxis.X);
        }

        private void btnSite1LTY_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtSite1LTY, VsmdAxis.Y);
        }

        private void btnSite2FTX_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtSite2FTX, VsmdAxis.X);
        }

        private void btnSite2FTY_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtSite2FTY, VsmdAxis.Y);
        }

        private void btnGridFTX_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtGridFTX, VsmdAxis.X);
        }

        private void btnGridFTY_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtGridFTY, VsmdAxis.Y);
        }

        private void btnGridLTX_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtGridLTX, VsmdAxis.X);
        }

        private void btnGridLTY_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtGridLTY, VsmdAxis.Y);
        }

        private void btnZTravelLTY_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtGridZTravel, VsmdAxis.Z);
        }

        private void btnZDispenseLTY_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtGridZDispense, VsmdAxis.Z);
        }

        private void btnZTravel_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtSiteZTravel, VsmdAxis.Z);
        }

        private void btnZDispense_Click(object sender, EventArgs e)
        {
            ShowSetDlg(txtSiteZDispense, VsmdAxis.Z);
        }
    }
}
