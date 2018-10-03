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
            this.txtPos = new VsmdWorkstation.Controls.TextBoxEx();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(51, 21);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(221, 12);
            this.lblTip.TabIndex = 0;
            this.lblTip.Text = "请通过键盘上\"←\"和\"→\"来移动机械轴。";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(230, 46);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(54, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtPos
            // 
            this.txtPos.Location = new System.Drawing.Point(31, 48);
            this.txtPos.Name = "txtPos";
            this.txtPos.Size = new System.Drawing.Size(133, 21);
            this.txtPos.TabIndex = 9;
            this.txtPos.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.UnsignedInterge;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(43, 211);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMove
            // 
            this.btnMove.Location = new System.Drawing.Point(170, 46);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(54, 23);
            this.btnMove.TabIndex = 11;
            this.btnMove.Text = "移动到";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // SetTubePositionFrm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(315, 86);
            this.ControlBox = false;
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtPos);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
        private Controls.TextBoxEx txtPos;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMove;
    }
}