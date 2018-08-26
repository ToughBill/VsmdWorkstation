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
            this.label1 = new System.Windows.Forms.Label();
            this.numDripInter = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMoveSpd = new VsmdWorkstation.Controls.TextBoxEx();
            this.textBoxEx1 = new VsmdWorkstation.Controls.TextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxEx2 = new VsmdWorkstation.Controls.TextBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numDripInter)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckbEnableCmdLog
            // 
            this.ckbEnableCmdLog.AutoSize = true;
            this.ckbEnableCmdLog.Location = new System.Drawing.Point(9, 188);
            this.ckbEnableCmdLog.Margin = new System.Windows.Forms.Padding(2);
            this.ckbEnableCmdLog.Name = "ckbEnableCmdLog";
            this.ckbEnableCmdLog.Size = new System.Drawing.Size(234, 16);
            this.ckbEnableCmdLog.TabIndex = 0;
            this.ckbEnableCmdLog.Text = "输出VSMD命令日志（使用DbgView查看）";
            this.ckbEnableCmdLog.UseVisualStyleBackColor = true;
            this.ckbEnableCmdLog.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "滴液间隔时间：";
            // 
            // numDripInter
            // 
            this.numDripInter.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numDripInter.Location = new System.Drawing.Point(103, 17);
            this.numDripInter.Margin = new System.Windows.Forms.Padding(2);
            this.numDripInter.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numDripInter.Name = "numDripInter";
            this.numDripInter.Size = new System.Drawing.Size(64, 21);
            this.numDripInter.TabIndex = 2;
            this.numDripInter.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "毫秒";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(68, 223);
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
            this.btnCancel.Location = new System.Drawing.Point(160, 223);
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
            this.groupBox1.Controls.Add(this.textBoxEx2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxEx1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numDripInter);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(9, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(263, 101);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "滴液";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMoveSpd);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(9, 118);
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
            // textBoxEx1
            // 
            this.textBoxEx1.Location = new System.Drawing.Point(103, 44);
            this.textBoxEx1.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxEx1.Name = "textBoxEx1";
            this.textBoxEx1.Size = new System.Drawing.Size(118, 21);
            this.textBoxEx1.TabIndex = 9;
            this.textBoxEx1.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.String;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "Z轴安全高度：";
            // 
            // textBoxEx2
            // 
            this.textBoxEx2.Location = new System.Drawing.Point(103, 70);
            this.textBoxEx2.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxEx2.Name = "textBoxEx2";
            this.textBoxEx2.Size = new System.Drawing.Size(118, 21);
            this.textBoxEx2.TabIndex = 11;
            this.textBoxEx2.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.String;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 73);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "Z轴滴液高度：";
            // 
            // GeneralSettingFrm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(280, 257);
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
            ((System.ComponentModel.ISupportInitialize)(this.numDripInter)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ckbEnableCmdLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numDripInter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Controls.TextBoxEx txtMoveSpd;
        private Controls.TextBoxEx textBoxEx2;
        private System.Windows.Forms.Label label5;
        private Controls.TextBoxEx textBoxEx1;
        private System.Windows.Forms.Label label4;
    }
}