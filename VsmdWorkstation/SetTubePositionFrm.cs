using System;
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
    public partial class SetTubePositionFrm : Form
    {
        public enum PosType
        {
            Pos,
            Dist
        }
        private VsmdAxis m_axisType;
        private PosType m_posType;
        private float m_preAxisSpeed;
        private bool m_startSet;
        private bool m_inMoving;
        public SetTubePositionFrm(VsmdAxis axis, PosType posType)
        {
            InitializeComponent();
            m_axisType = axis;
            m_posType = posType;
            m_inMoving = false;
        }

        private void SetTubePositionFrm_Load(object sender, EventArgs e)
        {
            m_preAxisSpeed = VsmdController.GetVsmdController().GetSpeed(m_axisType);
            if (m_axisType == VsmdAxis.X)
            {
                lblTip.Text = "请通过键盘上\"←\"和、\"→\"来操作。";
            }
            else if (m_axisType == VsmdAxis.Y)
            {
                lblTip.Text = "请通过键盘上\"↑\"和、\"↓\"来操作。";
            }
            if(m_posType == PosType.Pos)
            {
                txtStartPos.Enabled = false;
                txtEndPos.Enabled = false;
                btnSetStart.Enabled = false;
            }
            this.KeyPreview = true;
            m_startSet = false;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtDist.Text.Trim()))
            {
                StatusBar.DisplayMessage(MessageType.Error, "距离值无效！");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down ||
                keyData == Keys.Left || keyData == Keys.Right)
                return false;
            else
                return base.ProcessDialogKey(keyData);
        }

        private async void SetTubePositionFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_inMoving)
            {
                return;
            }
            float moveSpd = GeneralSettings.GetInstance().MoveSpeed;
            if (m_axisType == VsmdAxis.X)
            {
                if (e.KeyCode == Keys.Right)
                {
                    await MoveAxis(moveSpd);
                }
                else if (e.KeyCode == Keys.Left)
                {
                    await MoveAxis(-moveSpd);
                }
            }
            else if (m_axisType == VsmdAxis.Y)
            {
                if (e.KeyCode == Keys.Up)
                {
                    await MoveAxis(moveSpd);
                }
                else if (e.KeyCode == Keys.Down)
                {
                    await MoveAxis(-moveSpd);
                }
            }
        }
        private async Task<bool> MoveAxis(float speed)
        {
            await VsmdController.GetVsmdController().SetSpeed(m_axisType, speed);
            VsmdController.GetVsmdController().GetAxis(m_axisType).SendCommand("mov");
            //await VsmdController.GetVsmdController().Move(m_axisType);
            m_inMoving = true;
            return true;
        }
        private async void SetTubePositionFrm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Down)
            {
                if (m_inMoving)
                {
                    await VsmdController.GetVsmdController().Stop(m_axisType);
                    m_inMoving = false;

                    if (m_posType == PosType.Pos)
                    {
                        txtDist.Text = VsmdController.GetVsmdController().GetAxis(m_axisType).curPos.ToString();
                    }
                    else if(m_posType == PosType.Dist)
                    {
                        if (m_startSet)
                        {
                            txtEndPos.Text = VsmdController.GetVsmdController().GetAxis(m_axisType).curPos.ToString();
                            if (!string.IsNullOrWhiteSpace(txtStartPos.Text))
                            {
                                txtDist.Text = Math.Abs((int.Parse(txtEndPos.Text) - int.Parse(txtStartPos.Text))).ToString();
                            }
                        }

                    }
                }
            }
        }

        private async void SetTubePositionFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await VsmdController.GetVsmdController().SetSpeed(m_axisType, m_preAxisSpeed);
        }

        private void btnSetStart_Click(object sender, EventArgs e)
        {
            txtStartPos.Text = VsmdController.GetVsmdController().GetAxis(m_axisType).curPos.ToString();
            m_startSet = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int GetDist()
        {
            if(m_posType == PosType.Dist)
            {
                int spos = int.Parse(txtStartPos.Text.Trim());
                int epos = int.Parse(txtEndPos.Text.Trim());
                return int.Parse(txtDist.Text.Trim());
            }
            else
            {
                return int.Parse(txtDist.Text.Trim());
            }
        }
    }
}
