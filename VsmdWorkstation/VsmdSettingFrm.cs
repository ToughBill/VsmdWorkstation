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

namespace VsmdWorkstation
{
    public partial class VsmdSettingFrm : Form
    {
        private ChromiumWebBrowser m_browser;
        private BridgeObject m_externalObj;

        public VsmdSettingFrm()
        {
            InitializeComponent();
            InitBrowser();
        }
        private void InitBrowser()
        {
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            //Cef.Initialize(new CefSettings());
            string url = Application.StartupPath + @"\..\..\..\html\vsmdSetting.html";
            m_browser = new ChromiumWebBrowser(url);
            gridContainer.Controls.Add(m_browser);
            m_browser.Dock = DockStyle.Fill;
            BindingOptions opt = new BindingOptions();
            opt.CamelCaseJavascriptNames = false;
            m_browser.RegisterJsObject("externalObj", m_externalObj, opt);
        }

        private void VsmdSettingFrm_Load(object sender, EventArgs e)
        {
            InitAxises();
        }
        private void InitAxises()
        {
            VsmdInfo axisX = VsmdController.GetVsmdController().GetAxis(VsmdAxis.X);
            txtCidX.Text = axisX.Cid.ToString();
            txtPosX.Text = axisX.curPos.ToString();
            txtSpeedX.Text = axisX.curSpd.ToString();
            ckbAutoUpdateX.Checked = axisX.flgAutoUpdate;


            VsmdInfo axisY = VsmdController.GetVsmdController().GetAxis(VsmdAxis.Y);
            txtCidY.Text = axisY.Cid.ToString();
            txtPosY.Text = axisY.curPos.ToString();
            txtSpeedY.Text = axisY.curSpd.ToString();
            ckbAutoUpdateY.Checked = axisY.flgAutoUpdate;

            VsmdInfo axisZ = VsmdController.GetVsmdController().GetAxis(VsmdAxis.Z);
            txtCidZ.Text = axisZ.Cid.ToString();
            txtPosZ.Text = axisZ.curPos.ToString();
            txtSpeedZ.Text = axisZ.curSpd.ToString();
            ckbAutoUpdateZ.Checked = axisZ.flgAutoUpdate;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            m_browser.ShowDevTools();
        }
    }
}
