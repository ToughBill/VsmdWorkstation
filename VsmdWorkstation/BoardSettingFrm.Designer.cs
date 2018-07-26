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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBlockCnt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRowCnt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtColCnt = new System.Windows.Forms.TextBox();
            this.txtFirstTubePosX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFirstTubePosY = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTubeDistY = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTubeDistX = new System.Windows.Forms.TextBox();
            this.btnSetFY = new System.Windows.Forms.Button();
            this.btnSetTY = new System.Windows.Forms.Button();
            this.btnSetFX = new System.Windows.Forms.Button();
            this.btnSetTX = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "载物架名称：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(102, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(291, 22);
            this.txtName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "规格：";
            // 
            // txtBlockCnt
            // 
            this.txtBlockCnt.Location = new System.Drawing.Point(102, 46);
            this.txtBlockCnt.Name = "txtBlockCnt";
            this.txtBlockCnt.Size = new System.Drawing.Size(65, 22);
            this.txtBlockCnt.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(173, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "组";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(272, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "行";
            // 
            // txtRowCnt
            // 
            this.txtRowCnt.Location = new System.Drawing.Point(201, 48);
            this.txtRowCnt.Name = "txtRowCnt";
            this.txtRowCnt.Size = new System.Drawing.Size(65, 22);
            this.txtRowCnt.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(371, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "列";
            // 
            // txtColCnt
            // 
            this.txtColCnt.Location = new System.Drawing.Point(300, 48);
            this.txtColCnt.Name = "txtColCnt";
            this.txtColCnt.Size = new System.Drawing.Size(65, 22);
            this.txtColCnt.TabIndex = 8;
            // 
            // txtFirstTubePosX
            // 
            this.txtFirstTubePosX.Location = new System.Drawing.Point(125, 81);
            this.txtFirstTubePosX.Name = "txtFirstTubePosX";
            this.txtFirstTubePosX.ReadOnly = true;
            this.txtFirstTubePosX.Size = new System.Drawing.Size(100, 22);
            this.txtFirstTubePosX.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "首孔位置：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(53, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "孔距：";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(105, 204);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(202, 204);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(102, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "X";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(102, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 17);
            this.label9.TabIndex = 18;
            this.label9.Text = "Y";
            // 
            // txtFirstTubePosY
            // 
            this.txtFirstTubePosY.Location = new System.Drawing.Point(125, 109);
            this.txtFirstTubePosY.Name = "txtFirstTubePosY";
            this.txtFirstTubePosY.ReadOnly = true;
            this.txtFirstTubePosY.Size = new System.Drawing.Size(100, 22);
            this.txtFirstTubePosY.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(102, 169);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 17);
            this.label10.TabIndex = 22;
            this.label10.Text = "Y";
            // 
            // txtTubeDistY
            // 
            this.txtTubeDistY.Location = new System.Drawing.Point(125, 166);
            this.txtTubeDistY.Name = "txtTubeDistY";
            this.txtTubeDistY.ReadOnly = true;
            this.txtTubeDistY.Size = new System.Drawing.Size(100, 22);
            this.txtTubeDistY.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(102, 141);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 17);
            this.label11.TabIndex = 20;
            this.label11.Text = "X";
            // 
            // txtTubeDistX
            // 
            this.txtTubeDistX.Location = new System.Drawing.Point(125, 138);
            this.txtTubeDistX.Name = "txtTubeDistX";
            this.txtTubeDistX.ReadOnly = true;
            this.txtTubeDistX.Size = new System.Drawing.Size(100, 22);
            this.txtTubeDistX.TabIndex = 19;
            // 
            // btnSetFY
            // 
            this.btnSetFY.Location = new System.Drawing.Point(231, 108);
            this.btnSetFY.Name = "btnSetFY";
            this.btnSetFY.Size = new System.Drawing.Size(46, 23);
            this.btnSetFY.TabIndex = 24;
            this.btnSetFY.Text = "设置";
            this.btnSetFY.UseVisualStyleBackColor = true;
            this.btnSetFY.Click += new System.EventHandler(this.btnSetFY_Click);
            // 
            // btnSetTY
            // 
            this.btnSetTY.Location = new System.Drawing.Point(231, 166);
            this.btnSetTY.Name = "btnSetTY";
            this.btnSetTY.Size = new System.Drawing.Size(46, 23);
            this.btnSetTY.TabIndex = 25;
            this.btnSetTY.Text = "设置";
            this.btnSetTY.UseVisualStyleBackColor = true;
            this.btnSetTY.Click += new System.EventHandler(this.btnSetTY_Click);
            // 
            // btnSetFX
            // 
            this.btnSetFX.Location = new System.Drawing.Point(231, 81);
            this.btnSetFX.Name = "btnSetFX";
            this.btnSetFX.Size = new System.Drawing.Size(46, 23);
            this.btnSetFX.TabIndex = 26;
            this.btnSetFX.Text = "设置";
            this.btnSetFX.UseVisualStyleBackColor = true;
            this.btnSetFX.Click += new System.EventHandler(this.btnSetFX_Click);
            // 
            // btnSetTX
            // 
            this.btnSetTX.Location = new System.Drawing.Point(231, 139);
            this.btnSetTX.Name = "btnSetTX";
            this.btnSetTX.Size = new System.Drawing.Size(46, 23);
            this.btnSetTX.TabIndex = 27;
            this.btnSetTX.Text = "设置";
            this.btnSetTX.UseVisualStyleBackColor = true;
            this.btnSetTX.Click += new System.EventHandler(this.btnSetTX_Click);
            // 
            // BoardSettingFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 241);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BoardSettingFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "载物架设置";
            this.Load += new System.EventHandler(this.BoardSettingFrm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BoardSettingFrm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBlockCnt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRowCnt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtColCnt;
        private System.Windows.Forms.TextBox txtFirstTubePosX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFirstTubePosY;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTubeDistY;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTubeDistX;
        private System.Windows.Forms.Button btnSetFY;
        private System.Windows.Forms.Button btnSetTY;
        private System.Windows.Forms.Button btnSetFX;
        private System.Windows.Forms.Button btnSetTX;
    }
}