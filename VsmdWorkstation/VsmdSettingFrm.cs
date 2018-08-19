using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VsmdLib;
using VsmdWorkstation.Controls;

namespace VsmdWorkstation
{
    public partial class VsmdSettingFrm : Form
    {

        public VsmdSettingFrm()
        {
            InitializeComponent();
        }

        private void VsmdSettingFrm_Load(object sender, EventArgs e)
        {
            InitAxises();
        }
        private  void InitAxises()
        {
            if (!VsmdController.GetVsmdController().IsInitialized())
            {
                StatusBar.DisplayMessage(MessageType.Error, "设备未连接！");
                DisableAllControls();
                return;
            }
            
            VsmdInfo ax = VsmdController.GetVsmdController().GetAxis(VsmdAxis.X);
            txtCidX.Text = ax.Cid.ToString();
            txtPosX.Text = ax.curPos.ToString();
            txtSpeedX.Text = ax.GetAttributeValue(VsmdAttribute.Spd).ToString();
            txtZsdX.Text = ax.GetAttributeValue(VsmdAttribute.Zsd).ToString();

            VsmdInfo ay = VsmdController.GetVsmdController().GetAxis(VsmdAxis.Y);
            txtCidY.Text = ay.Cid.ToString();
            txtPosY.Text = ay.curPos.ToString();
            txtSpeedY.Text = ay.GetAttributeValue(VsmdAttribute.Spd).ToString();
            txtZsdY.Text = ay.GetAttributeValue(VsmdAttribute.Zsd).ToString();
            
            VsmdInfo az = VsmdController.GetVsmdController().GetAxis(VsmdAxis.Z);
            txtCidZ.Text = az.Cid.ToString();
            txtPosZ.Text = az.curPos.ToString();
            txtSpeedZ.Text = az.GetAttributeValue(VsmdAttribute.Spd).ToString();
            txtZsdZ.Text = az.GetAttributeValue(VsmdAttribute.Zsd).ToString();

            //VsmdInfo axisX = VsmdController.GetVsmdController().GetAxis(VsmdAxis.X);
            //txtCidX.Text = axisX.Cid.ToString();
            //txtPosX.Text = axisX.curPos.ToString();
            //txtSpeedX.Text = axisX.curSpd.ToString();

            //VsmdInfo axisY = VsmdController.GetVsmdController().GetAxis(VsmdAxis.Y);
            //txtCidY.Text = axisY.Cid.ToString();
            //txtPosY.Text = axisY.curPos.ToString();
            //txtSpeedY.Text = axisY.curSpd.ToString();

            //VsmdInfo axisZ = VsmdController.GetVsmdController().GetAxis(VsmdAxis.Z);
            //txtCidZ.Text = axisZ.Cid.ToString();
            //txtPosZ.Text = axisZ.curPos.ToString();
            //txtSpeedZ.Text = axisZ.curSpd.ToString();
        }
        private void DisableAllControls()
        {
            foreach(Control ctrl in this.Controls)
            {
                if(ctrl is GroupBox)
                {
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        if (ctrl2 is Button ||
                            ctrl2 is TextBox ||
                            ctrl2 is CheckBox)
                        {
                            ctrl2.Enabled = false;
                        }
                    }
                }
                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
        }
        private bool CheckTextBoxValue(TextBox box, out int val)
        {
            return int.TryParse(box.Text.Trim(), out val);

        }
        private void btnPosX_Click(object sender, EventArgs e)
        {
            PosImpl(VsmdAxis.X, txtPosX);
        }
        private void btnPosY_Click(object sender, EventArgs e)
        {
            PosImpl(VsmdAxis.Y, txtPosY);
        }

        private void btnPosZ_Click(object sender, EventArgs e)
        {
            PosImpl(VsmdAxis.Z, txtPosZ);
        }
        private void PosImpl(VsmdAxis axis, TextBoxEx boxCtrl)
        {
            VsmdController.GetVsmdController().Pos(axis, int.Parse(boxCtrl.Text.Trim()));
        }

        private void btnEnaX_Click(object sender, EventArgs e)
        {
            EnaImpl(VsmdAxis.X);
        }

        private void btnEnaY_Click(object sender, EventArgs e)
        {
            EnaImpl(VsmdAxis.Y);
        }

        private void btnEnaZ_Click(object sender, EventArgs e)
        {
            EnaImpl(VsmdAxis.Z);
        }
        private void EnaImpl(VsmdAxis axis)
        {
            VsmdController.GetVsmdController().Ena(axis);
        }

        private void btnOffX_Click(object sender, EventArgs e)
        {
            OffImpl(VsmdAxis.X);
        }

        private void btnOffY_Click(object sender, EventArgs e)
        {
            OffImpl(VsmdAxis.Y);
        }

        private void btnOffZ_Click(object sender, EventArgs e)
        {
            OffImpl(VsmdAxis.Z);
        }
        private void OffImpl(VsmdAxis axis)
        {
            VsmdController.GetVsmdController().Off(axis);
        }

        private void btnMoveX_Click(object sender, EventArgs e)
        {
            MoveImpl(VsmdAxis.X, float.Parse(txtSpeedX.Text));
        }

        private void btnMoveY_Click(object sender, EventArgs e)
        {
            MoveImpl(VsmdAxis.Y, float.Parse(txtSpeedY.Text));
        }

        private void btnMoveZ_Click(object sender, EventArgs e)
        {
            MoveImpl(VsmdAxis.Z, float.Parse(txtSpeedZ.Text));
        }
        private void MoveImpl(VsmdAxis axis, float speed)
        {
            VsmdController.GetVsmdController().SetSpeed(axis, speed);
            VsmdController.GetVsmdController().Move(axis);
        }

        private void btnStopX_Click(object sender, EventArgs e)
        {
            StopImpl(VsmdAxis.X);
        }

        private void btnStopY_Click(object sender, EventArgs e)
        {
            StopImpl(VsmdAxis.Y);
        }

        private void btnStopZ_Click(object sender, EventArgs e)
        {
            StopImpl(VsmdAxis.Z);
        }
        private void StopImpl(VsmdAxis axis)
        {
            VsmdController.GetVsmdController().Stop(axis);
        }

        private void btnOrgX_Click(object sender, EventArgs e)
        {
            OrgImpl(VsmdAxis.X);
        }

        private void btnOrgY_Click(object sender, EventArgs e)
        {
            OrgImpl(VsmdAxis.Y);
        }

        private void btnOrgZ_Click(object sender, EventArgs e)
        {
            OrgImpl(VsmdAxis.Z);
        }
        private void OrgImpl(VsmdAxis axis)
        {
            VsmdController.GetVsmdController().Org(axis);
        }

        private void btnZeroStartX_Click(object sender, EventArgs e)
        {
            ZeroStartImpl(VsmdAxis.X, float.Parse(txtZsdX.Text));
        }

        private void btnZeroStartY_Click(object sender, EventArgs e)
        {
            ZeroStartImpl(VsmdAxis.Y, float.Parse(txtZsdY.Text));
        }

        private void btnZeroStartZ_Click(object sender, EventArgs e)
        {
            ZeroStartImpl(VsmdAxis.Z, float.Parse(txtZsdZ.Text));
        }
        private void ZeroStartImpl(VsmdAxis axis, float speed)
        {
            VsmdController.GetVsmdController().GetAxis(axis).addCommand("cfg zsd=" + speed.ToString() + "\n");
            VsmdController.GetVsmdController().ZeroStart(axis);
        }

        private void btnZeroStopX_Click(object sender, EventArgs e)
        {
            ZeroStopImpl(VsmdAxis.X, float.Parse(txtZsdX.Text));
        }

        private void btnZeroStopY_Click(object sender, EventArgs e)
        {
            ZeroStopImpl(VsmdAxis.Y, float.Parse(txtZsdY.Text));
        }

        private void btnZeroStopZ_Click(object sender, EventArgs e)
        {
            ZeroStopImpl(VsmdAxis.Z, float.Parse(txtZsdZ.Text));
        }
        private void ZeroStopImpl(VsmdAxis axis, float speed)
        {
            VsmdController.GetVsmdController().GetAxis(axis).addCommand("cfg zsd=" + speed.ToString() + "\n");
            VsmdController.GetVsmdController().ZeroStop(axis);
        }

        private void btnStsX_Click(object sender, EventArgs e)
        {
            StsImpl(VsmdAxis.X);
        }

        private void btnStsY_Click(object sender, EventArgs e)
        {
            StsImpl(VsmdAxis.Y);
        }

        private void btnStsZ_Click(object sender, EventArgs e)
        {
            StsImpl(VsmdAxis.Z);
        }
        private void StsImpl(VsmdAxis axis)
        {
            VsmdController.GetVsmdController().Sts(axis);
            SetTimeout(100, new Action(delegate ()
            {
                Action action = delegate ()
                {
                    string newPos = VsmdController.GetVsmdController().GetAxis(axis).curPos.ToString();
                    string newSpeed = VsmdController.GetVsmdController().GetAxis(axis).curSpd.ToString();
                    switch (axis)
                    {
                        case VsmdAxis.X:
                            txtPosX.Text = newPos;
                            txtSpeedX.Text = newSpeed;
                            break;
                        case VsmdAxis.Y:
                            txtPosY.Text = newPos;
                            txtSpeedY.Text = newSpeed;
                            break;
                        case VsmdAxis.Z:
                            txtPosZ.Text = newPos;
                            txtSpeedZ.Text = newSpeed;
                            break;
                        default:
                            break;
                    }
                };
                this.Invoke(action);
            })
            );
        }
        public void SetTimeout(double interval, Action action)
        {
            System.Timers.Timer timer = new System.Timers.Timer(interval);
            timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                timer.Enabled = false;
                action();
            };
            timer.Enabled = true;
        }

        private void btnSaveX_Click(object sender, EventArgs e)
        {
            SaveImpl(VsmdAxis.X);
        }
        private void btnSaveY_Click(object sender, EventArgs e)
        {
            SaveImpl(VsmdAxis.Y);
        }

        private void btnSaveZ_Click(object sender, EventArgs e)
        {
            SaveImpl(VsmdAxis.Z);
        }
        private void SaveImpl(VsmdAxis axis)
        {
            switch (axis)
            {
                case VsmdAxis.X:
                    if (!string.IsNullOrWhiteSpace(txtSpeedX.Text))
                    {
                        VsmdController.GetVsmdController().SetSpeed(axis, float.Parse(txtSpeedX.Text));
                    }
                    if (!string.IsNullOrWhiteSpace(txtZsdX.Text))
                    {
                        VsmdController.GetVsmdController().SetZsd(axis, float.Parse(txtZsdX.Text));
                    }
                    break;
                case VsmdAxis.Y:
                    if (!string.IsNullOrWhiteSpace(txtSpeedY.Text))
                    {
                        VsmdController.GetVsmdController().SetSpeed(axis, float.Parse(txtSpeedY.Text));
                    }
                    if (!string.IsNullOrWhiteSpace(txtZsdY.Text))
                    {
                        VsmdController.GetVsmdController().SetZsd(axis, float.Parse(txtZsdY.Text));
                    }
                    break;
                case VsmdAxis.Z:
                    if (!string.IsNullOrWhiteSpace(txtSpeedZ.Text))
                    {
                        VsmdController.GetVsmdController().SetSpeed(axis, float.Parse(txtSpeedZ.Text));
                    }
                    if (!string.IsNullOrWhiteSpace(txtZsdX.Text))
                    {
                        VsmdController.GetVsmdController().SetZsd(axis, float.Parse(txtZsdZ.Text));
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnS3Input_Click(object sender, EventArgs e)
        {
            VsmdController.GetVsmdController().SetS3Mode(VsmdAxis.Z, 0);
        }

        private void btnS3Output_Click(object sender, EventArgs e)
        {
            VsmdController.GetVsmdController().SetS3Mode(VsmdAxis.Z, 1);
        }

        private void btnS3On_Click(object sender, EventArgs e)
        {
            VsmdController.GetVsmdController().S3On(VsmdAxis.Z);
        }

        private void btnS3Off_Click(object sender, EventArgs e)
        {
            VsmdController.GetVsmdController().S3Off(VsmdAxis.Z);
        }

        private void btnAddCmd_Click(object sender, EventArgs e)
        {
            VsmdController.GetVsmdController().GetAxis(VsmdAxis.Y).addCommand("cfg\n");
        }
    }
}
