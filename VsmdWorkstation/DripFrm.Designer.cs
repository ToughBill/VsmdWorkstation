using VsmdWorkstation.Controls;

namespace VsmdWorkstation
{
    partial class DripFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelGrid = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBoards = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnRestGrid = new System.Windows.Forms.Button();
            this.btnDevTools = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelGrid
            // 
            this.panelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGrid.Location = new System.Drawing.Point(9, 38);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(2);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(863, 498);
            this.panelGrid.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "载物架类型：";
            // 
            // cmbBoards
            // 
            this.cmbBoards.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoards.FormattingEnabled = true;
            this.cmbBoards.Location = new System.Drawing.Point(87, 12);
            this.cmbBoards.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBoards.Name = "cmbBoards";
            this.cmbBoards.Size = new System.Drawing.Size(130, 20);
            this.cmbBoards.TabIndex = 6;
            this.cmbBoards.SelectedIndexChanged += new System.EventHandler(this.cmbBoards_SelectedIndexChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(221, 10);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "开始滴液";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(379, 10);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "停止滴液";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(300, 10);
            this.btnPause.Margin = new System.Windows.Forms.Padding(2);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 9;
            this.btnPause.Text = "暂停滴液";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnRestGrid
            // 
            this.btnRestGrid.Location = new System.Drawing.Point(459, 10);
            this.btnRestGrid.Name = "btnRestGrid";
            this.btnRestGrid.Size = new System.Drawing.Size(75, 23);
            this.btnRestGrid.TabIndex = 10;
            this.btnRestGrid.Text = "重置";
            this.btnRestGrid.UseVisualStyleBackColor = true;
            this.btnRestGrid.Click += new System.EventHandler(this.btnRestGrid_Click);
            // 
            // btnDevTools
            // 
            this.btnDevTools.Location = new System.Drawing.Point(540, 10);
            this.btnDevTools.Name = "btnDevTools";
            this.btnDevTools.Size = new System.Drawing.Size(101, 23);
            this.btnDevTools.TabIndex = 11;
            this.btnDevTools.Text = "Open Dev Tools";
            this.btnDevTools.UseVisualStyleBackColor = true;
            this.btnDevTools.Visible = false;
            this.btnDevTools.Click += new System.EventHandler(this.btnDevTools_Click);
            // 
            // DripFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 545);
            this.Controls.Add(this.btnDevTools);
            this.Controls.Add(this.btnRestGrid);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cmbBoards);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DripFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBoards;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnRestGrid;
        private System.Windows.Forms.Button btnDevTools;
    }
}

