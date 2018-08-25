namespace VsmdWorkstation
{
    partial class MainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmConnectVsmd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVsmdSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBoardSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmGenaralSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarEx = new VsmdWorkstation.Controls.StatusBarEx();
            this.tsmDrip = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.tsmDrip});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(829, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmConnectVsmd,
            this.tsmVsmdSetting,
            this.tsmBoardSetting,
            this.tsmGenaralSetting});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.settingsToolStripMenuItem.Text = "设置";
            // 
            // tsmConnectVsmd
            // 
            this.tsmConnectVsmd.Name = "tsmConnectVsmd";
            this.tsmConnectVsmd.Size = new System.Drawing.Size(152, 22);
            this.tsmConnectVsmd.Text = "连接控制器";
            this.tsmConnectVsmd.Click += new System.EventHandler(this.tsmConnectVsmd_Click);
            // 
            // tsmVsmdSetting
            // 
            this.tsmVsmdSetting.Name = "tsmVsmdSetting";
            this.tsmVsmdSetting.Size = new System.Drawing.Size(152, 22);
            this.tsmVsmdSetting.Text = "控制器设置";
            this.tsmVsmdSetting.Click += new System.EventHandler(this.tsmVsmdSetting_Click);
            // 
            // tsmBoardSetting
            // 
            this.tsmBoardSetting.Name = "tsmBoardSetting";
            this.tsmBoardSetting.Size = new System.Drawing.Size(152, 22);
            this.tsmBoardSetting.Text = "载物架设置";
            this.tsmBoardSetting.Click += new System.EventHandler(this.tsmBoardSetting_Click);
            // 
            // tsmGenaralSetting
            // 
            this.tsmGenaralSetting.Name = "tsmGenaralSetting";
            this.tsmGenaralSetting.Size = new System.Drawing.Size(152, 22);
            this.tsmGenaralSetting.Text = "通用设置";
            this.tsmGenaralSetting.Click += new System.EventHandler(this.tsmGenaralSetting_Click);
            // 
            // statusBarEx
            // 
            this.statusBarEx.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBarEx.Location = new System.Drawing.Point(0, 645);
            this.statusBarEx.Name = "statusBarEx";
            this.statusBarEx.Size = new System.Drawing.Size(829, 24);
            this.statusBarEx.TabIndex = 4;
            this.statusBarEx.Text = "statusBarEx1";
            // 
            // tsmDrip
            // 
            this.tsmDrip.Name = "tsmDrip";
            this.tsmDrip.Size = new System.Drawing.Size(44, 21);
            this.tsmDrip.Text = "滴液";
            this.tsmDrip.Click += new System.EventHandler(this.tsmDrip_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 669);
            this.Controls.Add(this.statusBarEx);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VSMD工作站";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmVsmdSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmBoardSetting;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private Controls.StatusBarEx statusBarEx;
        private System.Windows.Forms.ToolStripMenuItem tsmConnectVsmd;
        private System.Windows.Forms.ToolStripMenuItem tsmGenaralSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmDrip;
    }
}