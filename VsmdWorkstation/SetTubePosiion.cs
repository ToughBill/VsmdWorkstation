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

namespace VsmdWorkstation
{
    public enum PostionType
    {
        FirstTubeX,
        FirstTubeY,
        TubeDistX,
        TubeDistY
    }
    public partial class SetTubePosiion : Form
    {
        private const float MOVE_SPEED = 500.0f;
        private ChromiumWebBrowser m_browser;
        private BridgeObject m_externalObj;
        private VsmdAxis m_axisType;
        private BoardMeta m_boardSetting;
        private float m_preAxisSpeed;
        private bool m_inMoving;
        public float PositionVal { get; set; }
        public SetTubePosiion(VsmdAxis type, BoardMeta boardSetting)
        {
            InitializeComponent();
            m_axisType = type;
            m_boardSetting = boardSetting;
            InitBrowser();
            m_inMoving = false;
        }
        private void InitBrowser()
        {
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            string url = Application.StartupPath + @"\..\..\..\html\tubeGrid.html";
            m_browser = new ChromiumWebBrowser(url);
            panel.Controls.Add(m_browser);
            m_browser.Dock = DockStyle.Fill;
            BindingOptions opt = new BindingOptions();
            opt.CamelCaseJavascriptNames = false;
            m_externalObj = new BridgeObject(m_browser);
            m_externalObj.onGridPageDomLoaded += OnGridPageDomLoaded;
            m_browser.RegisterJsObject("externalObj", m_externalObj, opt);
        }
        private void SetTubePosiion_Load(object sender, EventArgs e)
        {
            m_preAxisSpeed = VsmdController.GetVsmdController().GetSpeed(m_axisType);
            if(m_axisType == VsmdAxis.X)
            {
                lblTip.Text = "请通过键盘上\"←\"和、\"→\"来操作。";
            }
            else if(m_axisType == VsmdAxis.Y)
            {
                lblTip.Text = "请通过键盘上\"↑\"和、\"↓\"来操作。";
            }
            this.KeyPreview = true;
        }
        private void OnGridPageDomLoaded()
        {
            m_externalObj.BuildGrid(m_boardSetting);
        }
        private void SetTubePosiion_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_inMoving)
            {
                return;
            }
            if(m_axisType == VsmdAxis.X)
            {
                if (e.KeyCode == Keys.Right)
                {
                    VsmdController.GetVsmdController().SetSpeed(m_axisType, MOVE_SPEED);
                    VsmdController.GetVsmdController().Move(m_axisType);
                }
                else if (e.KeyCode == Keys.Left)
                {
                    VsmdController.GetVsmdController().SetSpeed(m_axisType, -MOVE_SPEED);
                    VsmdController.GetVsmdController().Move(m_axisType);
                }
            }
            else if(m_axisType == VsmdAxis.Y)
            {
                if (e.KeyCode == Keys.Up)
                {
                    VsmdController.GetVsmdController().SetSpeed(m_axisType, MOVE_SPEED);
                    VsmdController.GetVsmdController().Move(m_axisType);
                }
                else if(e.KeyCode == Keys.Down)
                {
                    VsmdController.GetVsmdController().SetSpeed(m_axisType, -MOVE_SPEED);
                    VsmdController.GetVsmdController().Move(m_axisType);
                }
            }
            
            
        }

        private void SetTubePosiion_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Down)
            if (m_axisType == VsmdAxis.X)
            {
                VsmdController.GetVsmdController().Stop(m_axisType);
                txtCurPos.Text = VsmdController.GetVsmdController().GetAxis(m_axisType).curPos.ToString();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //switch (keyData)
            //{
            //    case Keys.Tab:
            //        label1.Text = "1";
            //        break;
            //    case Keys.Left:
            //        label1.Text = "2";
            //        break;
            //    case Keys.Right:
            //        label1.Text = "3";

            //        break;
            //}
            if (keyData == Keys.Up || keyData == Keys.Down ||
                keyData == Keys.Left || keyData == Keys.Right)
                return false;
            else
                return base.ProcessDialogKey(keyData);

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtCurPos.Text.Trim()))
            {
                MessageBox.Show("值不能为空！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PositionVal = float.Parse(txtCurPos.Text);
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            VsmdController.GetVsmdController().ZeroStart(m_axisType);
        }

        private void btnZeroStop_Click(object sender, EventArgs e)
        {
            VsmdController.GetVsmdController().ZeroStop(m_axisType);
        }
    }
}
