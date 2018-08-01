using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VsmdWorkstation
{
    public partial class SetTubePositionFrm : Form
    {
        private const float MOVE_SPEED = 500.0f;
        private VsmdAxis m_axisType;
        private float m_preAxisSpeed;
        private bool m_inMoving;
        public SetTubePositionFrm(VsmdAxis type)
        {
            InitializeComponent();
            m_axisType = type;
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
            this.KeyPreview = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
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

        private void SetTubePositionFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_inMoving)
            {
                return;
            }
            if (m_axisType == VsmdAxis.X)
            {
                if (e.KeyCode == Keys.Right)
                {
                    MoveAxis(MOVE_SPEED);
                }
                else if (e.KeyCode == Keys.Left)
                {
                    MoveAxis(-MOVE_SPEED);
                }
            }
            else if (m_axisType == VsmdAxis.Y)
            {
                if (e.KeyCode == Keys.Up)
                {
                    MoveAxis(MOVE_SPEED);
                }
                else if (e.KeyCode == Keys.Down)
                {
                    MoveAxis(-MOVE_SPEED);
                }
            }
        }
        private void MoveAxis(float speed)
        {
            VsmdController.GetVsmdController().SetSpeed(m_axisType, speed);
            VsmdController.GetVsmdController().Move(m_axisType);
            m_inMoving = true;
        }
        private void SetTubePositionFrm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Down)
                if (m_inMoving)
                {
                    VsmdController.GetVsmdController().Stop(m_axisType);
                    m_inMoving = false;
                }
        }

        private void SetTubePositionFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            VsmdController.GetVsmdController().SetSpeed(m_axisType, m_preAxisSpeed);
        }
    }
}
