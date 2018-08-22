namespace VsmdWorkstation
{
    partial class SetTubePositionFrm
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
            this.lblTip = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStartPos = new VsmdWorkstation.Controls.TextBoxEx();
            this.btnSetStart = new System.Windows.Forms.Button();
            this.txtEndPos = new VsmdWorkstation.Controls.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDist = new VsmdWorkstation.Controls.TextBoxEx();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(71, 28);
            this.lblTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(247, 17);
            this.lblTip.TabIndex = 0;
            this.lblTip.Text = "请通过键盘上\"←\"和\"→\"来移动机械轴。";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(232, 139);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(168, 31);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "位置：";
            // 
            // txtStartPos
            // 
            this.txtStartPos.Location = new System.Drawing.Point(73, 66);
            this.txtStartPos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtStartPos.Name = "txtStartPos";
            this.txtStartPos.Size = new System.Drawing.Size(149, 22);
            this.txtStartPos.TabIndex = 3;
            this.txtStartPos.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.Interge;
            // 
            // btnSetStart
            // 
            this.btnSetStart.Location = new System.Drawing.Point(232, 60);
            this.btnSetStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSetStart.Name = "btnSetStart";
            this.btnSetStart.Size = new System.Drawing.Size(168, 31);
            this.btnSetStart.TabIndex = 4;
            this.btnSetStart.Text = "将当前位置设为起点";
            this.btnSetStart.UseVisualStyleBackColor = true;
            this.btnSetStart.Click += new System.EventHandler(this.btnSetStart_Click);
            // 
            // txtEndPos
            // 
            this.txtEndPos.Location = new System.Drawing.Point(73, 106);
            this.txtEndPos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEndPos.Name = "txtEndPos";
            this.txtEndPos.Size = new System.Drawing.Size(149, 22);
            this.txtEndPos.TabIndex = 6;
            this.txtEndPos.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.Interge;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 112);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "位置：";
            // 
            // txtDist
            // 
            this.txtDist.Location = new System.Drawing.Point(73, 144);
            this.txtDist.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDist.Name = "txtDist";
            this.txtDist.Size = new System.Drawing.Size(149, 22);
            this.txtDist.TabIndex = 9;
            this.txtDist.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.Interge;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(57, 281);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 31);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SetTubePositionFrm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(425, 191);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtDist);
            this.Controls.Add(this.txtEndPos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSetStart);
            this.Controls.Add(this.txtStartPos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SetTubePositionFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetTubePositionFrm_FormClosing);
            this.Load += new System.EventHandler(this.SetTubePositionFrm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SetTubePositionFrm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SetTubePositionFrm_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private Controls.TextBoxEx txtStartPos;
        private System.Windows.Forms.Button btnSetStart;
        private Controls.TextBoxEx txtEndPos;
        private System.Windows.Forms.Label label2;
        private Controls.TextBoxEx txtDist;
        private System.Windows.Forms.Button btnCancel;
    }
}