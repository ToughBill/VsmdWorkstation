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
        private VsmdAxis m_axisType;
        private float m_preAxisSpeed;
        private bool m_inMoving;
        public SetTubePositionFrm(VsmdAxis axis)
        {
            InitializeComponent();
            m_axisType = axis;
            m_inMoving = false;
        }

        private void SetTubePositionFrm_Load(object sender, EventArgs e)
        {
            m_preAxisSpeed = VsmdController.GetVsmdController().GetAxis(m_axisType).GetAttributeValue(VsmdLib.VsmdAttribute.Spd);
            if (m_axisType == VsmdAxis.X)
            {
                lblTip.Text = "请通过键盘上\"←\"和、\"→\"来操作。";
            }
            else if (m_axisType == VsmdAxis.Y)
            {
                lblTip.Text = "请通过键盘上\"↑\"和、\"↓\"来操作。";
            }
            else
            {
                lblTip.Text = "请通过键盘上\"W\"和、\"S\"来操作。";
            }
            this.KeyPreview = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtPos.Text.Trim()))
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
                keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.W || keyData == Keys.S)
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
            else
            {
                if (e.KeyCode == Keys.W)
                {
                    await MoveAxis(-moveSpd);
                }
                else if (e.KeyCode == Keys.S)
                {
                    await MoveAxis(moveSpd);
                }
            }
        }
        private async Task<bool> MoveAxis(float speed)
        {
            await VsmdController.GetVsmdController().SetSpeed(m_axisType, speed);
            VsmdController.GetVsmdController().GetAxis(m_axisType).SendCommand("mov");
            m_inMoving = true;
            return true;
        }
        private async void SetTubePositionFrm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Down || e.KeyCode == Keys.W || e.KeyCode == Keys.S)
            {
                if (m_inMoving)
                {
                    await VsmdController.GetVsmdController().Stop(m_axisType);
                    m_inMoving = false;
                    txtPos.Text = VsmdController.GetVsmdController().GetAxis(m_axisType).curPos.ToString();
                }
            }
        }

        private async void SetTubePositionFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await VsmdController.GetVsmdController().SetSpeed(m_axisType, m_preAxisSpeed);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int GetPos()
        {
            return int.Parse(txtPos.Text.Trim());
        }

        private async void btnMove_Click(object sender, EventArgs e)
        {
            await VsmdController.GetVsmdController().SetSpeed(m_axisType, m_preAxisSpeed);
            await VsmdController.GetVsmdController().MoveTo(m_axisType, int.Parse(txtPos.Text));
        }
    }
}
