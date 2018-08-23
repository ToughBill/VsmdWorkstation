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
            ((System.ComponentModel.ISupportInitialize)(this.numDripInter)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckbEnableCmdLog
            // 
            this.ckbEnableCmdLog.AutoSize = true;
            this.ckbEnableCmdLog.Location = new System.Drawing.Point(12, 201);
            this.ckbEnableCmdLog.Name = "ckbEnableCmdLog";
            this.ckbEnableCmdLog.Size = new System.Drawing.Size(153, 21);
            this.ckbEnableCmdLog.TabIndex = 0;
            this.ckbEnableCmdLog.Text = "输出VSMD命令日志";
            this.ckbEnableCmdLog.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 17);
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
            this.numDripInter.Location = new System.Drawing.Point(129, 27);
            this.numDripInter.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numDripInter.Name = "numDripInter";
            this.numDripInter.Size = new System.Drawing.Size(85, 22);
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
            this.label2.Location = new System.Drawing.Point(217, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "毫秒";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(90, 234);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 31);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(220, 234);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 31);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "移动速度：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numDripInter);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(351, 66);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "滴液";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMoveSpd);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(351, 73);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "载物架设置";
            // 
            // txtMoveSpd
            // 
            this.txtMoveSpd.Location = new System.Drawing.Point(94, 31);
            this.txtMoveSpd.Name = "txtMoveSpd";
            this.txtMoveSpd.Size = new System.Drawing.Size(124, 22);
            this.txtMoveSpd.TabIndex = 7;
            this.txtMoveSpd.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.String;
            // 
            // GeneralSettingFrm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(374, 277);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ckbEnableCmdLog);
            this.Name = "GeneralSettingFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
    }
}