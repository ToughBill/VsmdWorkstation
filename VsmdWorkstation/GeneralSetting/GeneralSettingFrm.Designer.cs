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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMoveSpd = new VsmdWorkstation.Controls.TextBoxEx();
            this.ckbEnableStsCmdLog = new System.Windows.Forms.CheckBox();
            this.ckbAutoConnect = new System.Windows.Forms.CheckBox();
            this.lvVolumeDelayDict = new System.Windows.Forms.ListView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblVolume = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVolumeML = new System.Windows.Forms.TextBox();
            this.txtDelayMs = new System.Windows.Forms.TextBox();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckbEnableCmdLog
            // 
            this.ckbEnableCmdLog.AutoSize = true;
            this.ckbEnableCmdLog.Location = new System.Drawing.Point(9, 370);
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
            this.btnOK.Location = new System.Drawing.Point(68, 413);
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
            this.btnCancel.Location = new System.Drawing.Point(160, 413);
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
            this.groupBox1.Controls.Add(this.txtDelayMs);
            this.groupBox1.Controls.Add(this.txtVolumeML);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblVolume);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.lvVolumeDelayDict);
            this.groupBox1.Location = new System.Drawing.Point(9, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(263, 273);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "滴液";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMoveSpd);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(9, 286);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(263, 55);
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
            this.ckbEnableStsCmdLog.Location = new System.Drawing.Point(29, 391);
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
            this.ckbAutoConnect.Location = new System.Drawing.Point(9, 349);
            this.ckbAutoConnect.Margin = new System.Windows.Forms.Padding(2);
            this.ckbAutoConnect.Name = "ckbAutoConnect";
            this.ckbAutoConnect.Size = new System.Drawing.Size(132, 16);
            this.ckbAutoConnect.TabIndex = 10;
            this.ckbAutoConnect.Text = "启动时自动连接设备";
            this.ckbAutoConnect.UseVisualStyleBackColor = true;
            // 
            // lvVolumeDelayDict
            // 
            this.lvVolumeDelayDict.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvVolumeDelayDict.Location = new System.Drawing.Point(5, 19);
            this.lvVolumeDelayDict.Name = "lvVolumeDelayDict";
            this.lvVolumeDelayDict.Size = new System.Drawing.Size(253, 192);
            this.lvVolumeDelayDict.TabIndex = 0;
            this.lvVolumeDelayDict.UseCompatibleStateImageBehavior = false;
            this.lvVolumeDelayDict.View = System.Windows.Forms.View.Details;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(202, 216);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 23);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(202, 243);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(56, 23);
            this.btnRemove.TabIndex = 12;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(5, 221);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(41, 12);
            this.lblVolume.TabIndex = 13;
            this.lblVolume.Text = "体积：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 248);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "延时(毫秒)：";
            // 
            // txtVolumeML
            // 
            this.txtVolumeML.Location = new System.Drawing.Point(80, 218);
            this.txtVolumeML.Name = "txtVolumeML";
            this.txtVolumeML.Size = new System.Drawing.Size(118, 21);
            this.txtVolumeML.TabIndex = 15;
            // 
            // txtDelayMs
            // 
            this.txtDelayMs.Location = new System.Drawing.Point(80, 245);
            this.txtDelayMs.Name = "txtDelayMs";
            this.txtDelayMs.Size = new System.Drawing.Size(117, 21);
            this.txtDelayMs.TabIndex = 16;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "加样体积";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "延时时间";
            // 
            // GeneralSettingFrm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(283, 506);
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
        private System.Windows.Forms.TextBox txtDelayMs;
        private System.Windows.Forms.TextBox txtVolumeML;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ListView lvVolumeDelayDict;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}