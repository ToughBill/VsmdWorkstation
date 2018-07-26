namespace VsmdWorkstation
{
    partial class SetTubePosiion
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
            this.btnZero = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.lblTip = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtCurPos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnZeroStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnZero
            // 
            this.btnZero.Location = new System.Drawing.Point(9, 9);
            this.btnZero.Margin = new System.Windows.Forms.Padding(2);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(70, 28);
            this.btnZero.TabIndex = 0;
            this.btnZero.Text = "归零开始";
            this.btnZero.UseVisualStyleBackColor = true;
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(9, 73);
            this.panel.Margin = new System.Windows.Forms.Padding(2);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(578, 308);
            this.panel.TabIndex = 1;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(11, 48);
            this.lblTip.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(41, 12);
            this.lblTip.TabIndex = 2;
            this.lblTip.Text = "label1";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(172, 9);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 28);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtCurPos
            // 
            this.txtCurPos.Location = new System.Drawing.Point(334, 14);
            this.txtCurPos.Margin = new System.Windows.Forms.Padding(2);
            this.txtCurPos.Name = "txtCurPos";
            this.txtCurPos.Size = new System.Drawing.Size(163, 21);
            this.txtCurPos.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "当前位置：";
            // 
            // btnZeroStop
            // 
            this.btnZeroStop.Location = new System.Drawing.Point(83, 9);
            this.btnZeroStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnZeroStop.Name = "btnZeroStop";
            this.btnZeroStop.Size = new System.Drawing.Size(70, 28);
            this.btnZeroStop.TabIndex = 6;
            this.btnZeroStop.Text = "归零停止";
            this.btnZeroStop.UseVisualStyleBackColor = true;
            this.btnZeroStop.Click += new System.EventHandler(this.btnZeroStop_Click);
            // 
            // SetTubePosiion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 391);
            this.Controls.Add(this.btnZeroStop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCurPos);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.btnZero);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SetTubePosiion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SetTubePosiion";
            this.Load += new System.EventHandler(this.SetTubePosiion_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SetTubePosiion_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SetTubePosiion_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtCurPos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnZeroStop;
    }
}