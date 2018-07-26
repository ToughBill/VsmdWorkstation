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
        private const float MOVE_SPEED = 200.0f;
        private ChromiumWebBrowser m_browser;
        private BridgeObject m_externalObj;
        private VsmdAxis m_axisType;
        private BoardSetting m_boardSetting;
        private float m_preAxisSpeed;
        public float PositionVal { get; set; }
        public SetTubePosiion(VsmdAxis type, BoardSetting boardSetting)
        {
            InitializeComponent();
            m_axisType = type;
            m_boardSetting = boardSetting;
            InitBrowser();
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
        }
        private void OnGridPageDomLoaded()
        {
            m_externalObj.BuildGrid(m_boardSetting);
        }
        private void SetTubePosiion_KeyDown(object sender, KeyEventArgs e)
        {
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

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtCurPos.Text.Trim()))
            {
                MessageBox.Show("值不能为空！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            PositionVal = float.Parse(txtCurPos.Text);
        }
    }
}
