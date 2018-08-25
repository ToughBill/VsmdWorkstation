namespace VsmdWorkstation
{
    partial class BoardSettingFrm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSetFY = new System.Windows.Forms.Button();
            this.btnSetTY = new System.Windows.Forms.Button();
            this.btnSetFX = new System.Windows.Forms.Button();
            this.btnSetTX = new System.Windows.Forms.Button();
            this.btnChoose = new System.Windows.Forms.Button();
            this.btnSetBlockDist = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBlockDist = new VsmdWorkstation.Controls.TextBoxEx();
            this.txtTubeDistY = new VsmdWorkstation.Controls.TextBoxEx();
            this.txtTubeDistX = new VsmdWorkstation.Controls.TextBoxEx();
            this.txtFirstTubePosY = new VsmdWorkstation.Controls.TextBoxEx();
            this.txtFirstTubePosX = new VsmdWorkstation.Controls.TextBoxEx();
            this.txtColCnt = new VsmdWorkstation.Controls.TextBoxEx();
            this.txtRowCnt = new VsmdWorkstation.Controls.TextBoxEx();
            this.txtBlockCnt = new VsmdWorkstation.Controls.TextBoxEx();
            this.txtName = new VsmdWorkstation.Controls.TextBoxEx();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "载物架名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "规格：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 47);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "组";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "行";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 48);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "列";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 106);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "首孔位置：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 166);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "孔距：";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(71, 226);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(68, 25);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(184, 226);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 25);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(87, 106);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "X";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(87, 135);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "Y";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(87, 194);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 22;
            this.label10.Text = "Y";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(87, 165);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 20;
            this.label11.Text = "X";
            // 
            // btnSetFY
            // 
            this.btnSetFY.Location = new System.Drawing.Point(184, 132);
            this.btnSetFY.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetFY.Name = "btnSetFY";
            this.btnSetFY.Size = new System.Drawing.Size(50, 22);
            this.btnSetFY.TabIndex = 11;
            this.btnSetFY.Text = "设置";
            this.btnSetFY.UseVisualStyleBackColor = true;
            this.btnSetFY.Click += new System.EventHandler(this.btnSetFY_Click);
            // 
            // btnSetTY
            // 
            this.btnSetTY.Location = new System.Drawing.Point(184, 191);
            this.btnSetTY.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetTY.Name = "btnSetTY";
            this.btnSetTY.Size = new System.Drawing.Size(50, 21);
            this.btnSetTY.TabIndex = 15;
            this.btnSetTY.Text = "设置";
            this.btnSetTY.UseVisualStyleBackColor = true;
            this.btnSetTY.Click += new System.EventHandler(this.btnSetTY_Click);
            // 
            // btnSetFX
            // 
            this.btnSetFX.Location = new System.Drawing.Point(184, 104);
            this.btnSetFX.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetFX.Name = "btnSetFX";
            this.btnSetFX.Size = new System.Drawing.Size(50, 21);
            this.btnSetFX.TabIndex = 9;
            this.btnSetFX.Text = "设置";
            this.btnSetFX.UseVisualStyleBackColor = true;
            this.btnSetFX.Click += new System.EventHandler(this.btnSetFX_Click);
            // 
            // btnSetTX
            // 
            this.btnSetTX.Location = new System.Drawing.Point(184, 163);
            this.btnSetTX.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetTX.Name = "btnSetTX";
            this.btnSetTX.Size = new System.Drawing.Size(50, 21);
            this.btnSetTX.TabIndex = 13;
            this.btnSetTX.Text = "设置";
            this.btnSetTX.UseVisualStyleBackColor = true;
            this.btnSetTX.Click += new System.EventHandler(this.btnSetTX_Click);
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(273, 11);
            this.btnChoose.Margin = new System.Windows.Forms.Padding(0);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(36, 23);
            this.btnChoose.TabIndex = 2;
            this.btnChoose.Text = ".......";
            this.btnChoose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // btnSetBlockDist
            // 
            this.btnSetBlockDist.Location = new System.Drawing.Point(184, 74);
            this.btnSetBlockDist.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetBlockDist.Name = "btnSetBlockDist";
            this.btnSetBlockDist.Size = new System.Drawing.Size(50, 21);
            this.btnSetBlockDist.TabIndex = 7;
            this.btnSetBlockDist.Text = "设置";
            this.btnSetBlockDist.UseVisualStyleBackColor = true;
            this.btnSetBlockDist.Click += new System.EventHandler(this.btnSetBlockDist_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(50, 79);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 29;
            this.label14.Text = "组距：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBlockDist
            // 
            this.txtBlockDist.Location = new System.Drawing.Point(89, 74);
            this.txtBlockDist.Margin = new System.Windows.Forms.Padding(2);
            this.txtBlockDist.Name = "txtBlockDist";
            this.txtBlockDist.Size = new System.Drawing.Size(91, 21);
            this.txtBlockDist.TabIndex = 6;
            this.txtBlockDist.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.Interge;
            // 
            // txtTubeDistY
            // 
            this.txtTubeDistY.Location = new System.Drawing.Point(105, 191);
            this.txtTubeDistY.Margin = new System.Windows.Forms.Padding(2);
            this.txtTubeDistY.Name = "txtTubeDistY";
            this.txtTubeDistY.Size = new System.Drawing.Size(76, 21);
            this.txtTubeDistY.TabIndex = 14;
            this.txtTubeDistY.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.Interge;
            // 
            // txtTubeDistX
            // 
            this.txtTubeDistX.Location = new System.Drawing.Point(105, 163);
            this.txtTubeDistX.Margin = new System.Windows.Forms.Padding(2);
            this.txtTubeDistX.Name = "txtTubeDistX";
            this.txtTubeDistX.Size = new System.Drawing.Size(76, 21);
            this.txtTubeDistX.TabIndex = 12;
            this.txtTubeDistX.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.Interge;
            // 
            // txtFirstTubePosY
            // 
            this.txtFirstTubePosY.Location = new System.Drawing.Point(105, 133);
            this.txtFirstTubePosY.Margin = new System.Windows.Forms.Padding(2);
            this.txtFirstTubePosY.Name = "txtFirstTubePosY";
            this.txtFirstTubePosY.Size = new System.Drawing.Size(76, 21);
            this.txtFirstTubePosY.TabIndex = 10;
            this.txtFirstTubePosY.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.Interge;
            // 
            // txtFirstTubePosX
            // 
            this.txtFirstTubePosX.Location = new System.Drawing.Point(105, 104);
            this.txtFirstTubePosX.Margin = new System.Windows.Forms.Padding(2);
            this.txtFirstTubePosX.Name = "txtFirstTubePosX";
            this.txtFirstTubePosX.Size = new System.Drawing.Size(76, 21);
            this.txtFirstTubePosX.TabIndex = 8;
            this.txtFirstTubePosX.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.Interge;
            // 
            // txtColCnt
            // 
            this.txtColCnt.Location = new System.Drawing.Point(238, 45);
            this.txtColCnt.Margin = new System.Windows.Forms.Padding(2);
            this.txtColCnt.Name = "txtColCnt";
            this.txtColCnt.Size = new System.Drawing.Size(50, 21);
            this.txtColCnt.TabIndex = 5;
            this.txtColCnt.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.UnsignedInterge;
            // 
            // txtRowCnt
            // 
            this.txtRowCnt.Location = new System.Drawing.Point(164, 45);
            this.txtRowCnt.Margin = new System.Windows.Forms.Padding(2);
            this.txtRowCnt.Name = "txtRowCnt";
            this.txtRowCnt.Size = new System.Drawing.Size(50, 21);
            this.txtRowCnt.TabIndex = 4;
            this.txtRowCnt.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.UnsignedInterge;
            // 
            // txtBlockCnt
            // 
            this.txtBlockCnt.Location = new System.Drawing.Point(89, 43);
            this.txtBlockCnt.Margin = new System.Windows.Forms.Padding(2);
            this.txtBlockCnt.Name = "txtBlockCnt";
            this.txtBlockCnt.Size = new System.Drawing.Size(50, 21);
            this.txtBlockCnt.TabIndex = 3;
            this.txtBlockCnt.Text = "1";
            this.txtBlockCnt.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.UnsignedInterge;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(89, 12);
            this.txtName.Margin = new System.Windows.Forms.Padding(2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(182, 21);
            this.txtName.TabIndex = 1;
            this.txtName.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.String;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDelete});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 26);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(100, 22);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // BoardSettingFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(318, 261);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.btnSetBlockDist);
            this.Controls.Add(this.txtBlockDist);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.btnSetTX);
            this.Controls.Add(this.btnSetFX);
            this.Controls.Add(this.btnSetTY);
            this.Controls.Add(this.btnSetFY);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtTubeDistY);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtTubeDistX);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtFirstTubePosY);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtFirstTubePosX);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtColCnt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRowCnt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBlockCnt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BoardSettingFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "载物架设置";
            this.Load += new System.EventHandler(this.BoardSettingFrm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private VsmdWorkstation.Controls.TextBoxEx txtName;
        private System.Windows.Forms.Label label2;
        private VsmdWorkstation.Controls.TextBoxEx txtBlockCnt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private VsmdWorkstation.Controls.TextBoxEx txtRowCnt;
        private System.Windows.Forms.Label label5;
        private VsmdWorkstation.Controls.TextBoxEx txtColCnt;
        private VsmdWorkstation.Controls.TextBoxEx txtFirstTubePosX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private VsmdWorkstation.Controls.TextBoxEx txtFirstTubePosY;
        private System.Windows.Forms.Label label10;
        private VsmdWorkstation.Controls.TextBoxEx txtTubeDistY;
        private System.Windows.Forms.Label label11;
        private VsmdWorkstation.Controls.TextBoxEx txtTubeDistX;
        private System.Windows.Forms.Button btnSetFY;
        private System.Windows.Forms.Button btnSetTY;
        private System.Windows.Forms.Button btnSetFX;
        private System.Windows.Forms.Button btnSetTX;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Button btnSetBlockDist;
        private VsmdWorkstation.Controls.TextBoxEx txtBlockDist;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
    }
}