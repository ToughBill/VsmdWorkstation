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
            this.SuspendLayout();
            // 
            // btnZero
            // 
            this.btnZero.Location = new System.Drawing.Point(12, 12);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(75, 23);
            this.btnZero.TabIndex = 0;
            this.btnZero.Text = "归零";
            this.btnZero.UseVisualStyleBackColor = true;
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(12, 62);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(770, 447);
            this.panel.TabIndex = 1;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(12, 42);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(46, 17);
            this.lblTip.TabIndex = 2;
            this.lblTip.Text = "label1";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(93, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtCurPos
            // 
            this.txtCurPos.Location = new System.Drawing.Point(269, 12);
            this.txtCurPos.Name = "txtCurPos";
            this.txtCurPos.Size = new System.Drawing.Size(173, 22);
            this.txtCurPos.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(192, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "当前位置：";
            // 
            // SetTubePosiion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 521);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCurPos);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.btnZero);
            this.Name = "SetTubePosiion";
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
    }
}