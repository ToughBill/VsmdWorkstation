using System;
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

            VsmdInfoSync ax = VsmdController.GetVsmdController().GetAxis(VsmdAxis.X);
            txtCidX.Text = ax.Cid.ToString();
            txtPosX.Text = ax.curPos.ToString();
            txtSpeedX.Text = ax.GetAttributeValue(VsmdAttribute.Spd).ToString();
            txtZsdX.Text = ax.GetAttributeValue(VsmdAttribute.Zsd).ToString();

            VsmdInfoSync ay = VsmdController.GetVsmdController().GetAxis(VsmdAxis.Y);
            txtCidY.Text = ay.Cid.ToString();
            txtPosY.Text = ay.curPos.ToString();
            txtSpeedY.Text = ay.GetAttributeValue(VsmdAttribute.Spd).ToString();
            txtZsdY.Text = ay.GetAttributeValue(VsmdAttribute.Zsd).ToString();

            VsmdInfoSync az = VsmdController.GetVsmdController().GetAxis(VsmdAxis.Z);
            txtCidZ.Text = az.Cid.ToString();
            txtPosZ.Text = az.curPos.ToString();
            txtSpeedZ.Text = az.GetAttributeValue(VsmdAttribute.Spd).ToString();
            txtZsdZ.Text = az.GetAttributeValue(VsmdAttribute.Zsd).ToString();
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
        private async void btnPosX_Click(object sender, EventArgs e)
        {
            await PosImpl(VsmdAxis.X, txtPosX);
        }
        private async void btnPosY_Click(object sender, EventArgs e)
        {
            await PosImpl(VsmdAxis.Y, txtPosY);
        }

        private async void btnPosZ_Click(object sender, EventArgs e)
        {
            await PosImpl(VsmdAxis.Z, txtPosZ);
        }
        private async Task<bool> PosImpl(VsmdAxis axis, TextBoxEx boxCtrl)
        {
            return await VsmdController.GetVsmdController().Pos(axis, int.Parse(boxCtrl.Text.Trim()));
        }

        private async void btnEnaX_Click(object sender, EventArgs e)
        {
            await EnaImpl(VsmdAxis.X);
        }

        private async void btnEnaY_Click(object sender, EventArgs e)
        {
            await EnaImpl(VsmdAxis.Y);
        }

        private async void btnEnaZ_Click(object sender, EventArgs e)
        {
            await EnaImpl(VsmdAxis.Z);
        }
        private async Task<bool> EnaImpl(VsmdAxis axis)
        {
            return await VsmdController.GetVsmdController().Ena(axis);
        }

        private async void btnOffX_Click(object sender, EventArgs e)
        {
            await OffImpl(VsmdAxis.X);
        }

        private async void btnOffY_Click(object sender, EventArgs e)
        {
            await OffImpl(VsmdAxis.Y);
        }

        private async void btnOffZ_Click(object sender, EventArgs e)
        {
            await OffImpl(VsmdAxis.Z);
        }
        private async Task<bool> OffImpl(VsmdAxis axis)
        {
            return await VsmdController.GetVsmdController().Off(axis);
        }

        private async void btnMoveX_Click(object sender, EventArgs e)
        {
            await MoveImpl(VsmdAxis.X, float.Parse(txtSpeedX.Text));
        }

        private async void btnMoveY_Click(object sender, EventArgs e)
        {
            await MoveImpl(VsmdAxis.Y, float.Parse(txtSpeedY.Text));
        }

        private async void btnMoveZ_Click(object sender, EventArgs e)
        {
            await MoveImpl(VsmdAxis.Z, float.Parse(txtSpeedZ.Text));
        }
        private async Task<bool> MoveImpl(VsmdAxis axis, float speed)
        {
            await VsmdController.GetVsmdController().SetSpeed(axis, speed);
            return await VsmdController.GetVsmdController().Move(axis);
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
        private async void StopImpl(VsmdAxis axis)
        {
            await VsmdController.GetVsmdController().Stop(axis);
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
        private async void OrgImpl(VsmdAxis axis)
        {
            await VsmdController.GetVsmdController().Org(axis);
        }

        private async void btnZeroStartX_Click(object sender, EventArgs e)
        {
            await ZeroStartImpl(VsmdAxis.X, float.Parse(txtZsdX.Text));
        }

        private async void btnZeroStartY_Click(object sender, EventArgs e)
        {
            await ZeroStartImpl(VsmdAxis.Y, float.Parse(txtZsdY.Text));
        }

        private async void btnZeroStartZ_Click(object sender, EventArgs e)
        {
            await ZeroStartImpl(VsmdAxis.Z, float.Parse(txtZsdZ.Text));
        }
        private async Task<bool> ZeroStartImpl(VsmdAxis axis, float speed)
        {
            await VsmdController.GetVsmdController().SetZsd(axis, speed);
            return await VsmdController.GetVsmdController().ZeroStart(axis);
        }

        private async void btnZeroStopX_Click(object sender, EventArgs e)
        {
            await ZeroStopImpl(VsmdAxis.X, float.Parse(txtZsdX.Text));
        }

        private async void btnZeroStopY_Click(object sender, EventArgs e)
        {
            await ZeroStopImpl(VsmdAxis.Y, float.Parse(txtZsdY.Text));
        }

        private async void btnZeroStopZ_Click(object sender, EventArgs e)
        {
            await ZeroStopImpl(VsmdAxis.Z, float.Parse(txtZsdZ.Text));
        }
        private async Task<bool> ZeroStopImpl(VsmdAxis axis, float speed)
        {
            return await VsmdController.GetVsmdController().ZeroStop(axis);
        }

        private async void btnStsX_Click(object sender, EventArgs e)
        {
            await StsImpl(VsmdAxis.X);
        }

        private async void btnStsY_Click(object sender, EventArgs e)
        {
            await StsImpl(VsmdAxis.Y);
        }

        private async void btnStsZ_Click(object sender, EventArgs e)
        {
            await StsImpl(VsmdAxis.Z);
        }
        private async Task<bool> StsImpl(VsmdAxis axis)
        {
            await VsmdController.GetVsmdController().Sts(axis);
            string newPos = VsmdController.GetVsmdController().GetAxis(axis).curPos.ToString();
            switch (axis)
            {
                case VsmdAxis.X:
                    txtPosX.Text = newPos;
                    break;
                case VsmdAxis.Y:
                    txtPosY.Text = newPos;
                    break;
                case VsmdAxis.Z:
                    txtPosZ.Text = newPos;
                    break;
                default:
                    break;
            }
            return true;
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

        private async void btnSaveX_Click(object sender, EventArgs e)
        {
            await SaveImpl(VsmdAxis.X);
        }
        private async void btnSaveY_Click(object sender, EventArgs e)
        {
            await SaveImpl(VsmdAxis.Y);
        }
        private async void btnSaveZ_Click(object sender, EventArgs e)
        {
            await SaveImpl(VsmdAxis.Z);
        }
        private async Task<bool> SaveImpl(VsmdAxis axis)
        {
            switch (axis)
            {
                case VsmdAxis.X:
                    if (!string.IsNullOrWhiteSpace(txtSpeedX.Text))
                    {
                        await VsmdController.GetVsmdController().SetSpeed(axis, float.Parse(txtSpeedX.Text));
                    }
                    if (!string.IsNullOrWhiteSpace(txtZsdX.Text))
                    {
                        await VsmdController.GetVsmdController().SetZsd(axis, float.Parse(txtZsdX.Text));
                    }
                    break;
                case VsmdAxis.Y:
                    if (!string.IsNullOrWhiteSpace(txtSpeedY.Text))
                    {
                        await VsmdController.GetVsmdController().SetSpeed(axis, float.Parse(txtSpeedY.Text));
                    }
                    if (!string.IsNullOrWhiteSpace(txtZsdY.Text))
                    {
                        await VsmdController.GetVsmdController().SetZsd(axis, float.Parse(txtZsdY.Text));
                    }
                    break;
                case VsmdAxis.Z:
                    if (!string.IsNullOrWhiteSpace(txtSpeedZ.Text))
                    {
                        await VsmdController.GetVsmdController().SetSpeed(axis, float.Parse(txtSpeedZ.Text));
                    }
                    if (!string.IsNullOrWhiteSpace(txtZsdX.Text))
                    {
                        await VsmdController.GetVsmdController().SetZsd(axis, float.Parse(txtZsdZ.Text));
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        private async void btnS3Input_Click(object sender, EventArgs e)
        {
            await VsmdController.GetVsmdController().SetS3Mode(VsmdAxis.Z, 0);
        }

        private async void btnS3Output_Click(object sender, EventArgs e)
        {
            await VsmdController.GetVsmdController().SetS3Mode(VsmdAxis.Z, 1);
        }

        private async void btnS3On_Click(object sender, EventArgs e)
        {
            await VsmdController.GetVsmdController().S3On(VsmdAxis.Z);
        }

        private async void btnS3Off_Click(object sender, EventArgs e)
        {
            await VsmdController.GetVsmdController().S3Off(VsmdAxis.Z);
        }
    }
}
