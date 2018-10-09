namespace VsmdWorkstation
{
    partial class GeneralSettingFrm
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
            this.ckbEnableCmdLog = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPipettingSpeed = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMoveSpd = new VsmdWorkstation.Controls.TextBoxEx();
            this.ckbEnableStsCmdLog = new System.Windows.Forms.CheckBox();
            this.ckbAutoConnect = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckbEnableCmdLog
            // 
            this.ckbEnableCmdLog.AutoSize = true;
            this.ckbEnableCmdLog.Location = new System.Drawing.Point(11, 157);
            this.ckbEnableCmdLog.Margin = new System.Windows.Forms.Padding(2);
            this.ckbEnableCmdLog.Name = "ckbEnableCmdLog";
            this.ckbEnableCmdLog.Size = new System.Drawing.Size(234, 16);
            this.ckbEnableCmdLog.TabIndex = 0;
            this.ckbEnableCmdLog.Text = "输出VSMD命令日志（使用DbgView查看）";
            this.ckbEnableCmdLog.UseVisualStyleBackColor = true;
            this.ckbEnableCmdLog.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(129, 200);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(56, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(189, 200);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "移动速度：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPipettingSpeed);
            this.groupBox1.Location = new System.Drawing.Point(11, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(234, 53);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "滴液速度";
            // 
            // txtPipettingSpeed
            // 
            this.txtPipettingSpeed.Location = new System.Drawing.Point(16, 19);
            this.txtPipettingSpeed.Name = "txtPipettingSpeed";
            this.txtPipettingSpeed.Size = new System.Drawing.Size(118, 21);
            this.txtPipettingSpeed.TabIndex = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMoveSpd);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(11, 66);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(234, 55);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "载物架设置";
            // 
            // txtMoveSpd
            // 
            this.txtMoveSpd.Location = new System.Drawing.Point(80, 23);
            this.txtMoveSpd.Margin = new System.Windows.Forms.Padding(2);
            this.txtMoveSpd.Name = "txtMoveSpd";
            this.txtMoveSpd.Size = new System.Drawing.Size(118, 21);
            this.txtMoveSpd.TabIndex = 7;
            this.txtMoveSpd.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.String;
            // 
            // ckbEnableStsCmdLog
            // 
            this.ckbEnableStsCmdLog.AutoSize = true;
            this.ckbEnableStsCmdLog.Location = new System.Drawing.Point(31, 178);
            this.ckbEnableStsCmdLog.Margin = new System.Windows.Forms.Padding(2);
            this.ckbEnableStsCmdLog.Name = "ckbEnableStsCmdLog";
            this.ckbEnableStsCmdLog.Size = new System.Drawing.Size(114, 16);
            this.ckbEnableStsCmdLog.TabIndex = 9;
            this.ckbEnableStsCmdLog.Text = "输出sts命令日志";
            this.ckbEnableStsCmdLog.UseVisualStyleBackColor = true;
            this.ckbEnableStsCmdLog.Visible = false;
            // 
            // ckbAutoConnect
            // 
            this.ckbAutoConnect.AutoSize = true;
            this.ckbAutoConnect.Location = new System.Drawing.Point(11, 136);
            this.ckbAutoConnect.Margin = new System.Windows.Forms.Padding(2);
            this.ckbAutoConnect.Name = "ckbAutoConnect";
            this.ckbAutoConnect.Size = new System.Drawing.Size(132, 16);
            this.ckbAutoConnect.TabIndex = 10;
            this.ckbAutoConnect.Text = "启动时自动连接设备";
            this.ckbAutoConnect.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "毫秒/毫升";
            // 
            // GeneralSettingFrm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(258, 239);
            this.Controls.Add(this.ckbAutoConnect);
            this.Controls.Add(this.ckbEnableStsCmdLog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ckbEnableCmdLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "GeneralSettingFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通用设置";
            this.Load += new System.EventHandler(this.GeneralSettingFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ckbEnableCmdLog;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Controls.TextBoxEx txtMoveSpd;
        private System.Windows.Forms.CheckBox ckbEnableStsCmdLog;
        private System.Windows.Forms.CheckBox ckbAutoConnect;
        private System.Windows.Forms.TextBox txtPipettingSpeed;
        private System.Windows.Forms.Label label1;
    }
}