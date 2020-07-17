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
            this.btnDevTools = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDelaySeconds = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSampleCnt = new VsmdWorkstation.Controls.TextBoxEx();
            this.btnWash = new System.Windows.Forms.Button();
            this.cmbProjectName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnMoveFront = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnMoveToBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelGrid
            // 
            this.panelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGrid.Location = new System.Drawing.Point(12, 106);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(1192, 616);
            this.panelGrid.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "载物架类型：";
            // 
            // cmbBoards
            // 
            this.cmbBoards.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoards.FormattingEnabled = true;
            this.cmbBoards.Location = new System.Drawing.Point(116, 16);
            this.cmbBoards.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbBoards.Name = "cmbBoards";
            this.cmbBoards.Size = new System.Drawing.Size(172, 23);
            this.cmbBoards.TabIndex = 6;
            this.cmbBoards.SelectedIndexChanged += new System.EventHandler(this.cmbBoards_SelectedIndexChanged);
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(847, 15);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 29);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "开始滴液";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(847, 49);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 29);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "停止滴液";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(741, 49);
            this.btnPause.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(100, 29);
            this.btnPause.TabIndex = 9;
            this.btnPause.Text = "暂停滴液";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnDevTools
            // 
            this.btnDevTools.Location = new System.Drawing.Point(564, 47);
            this.btnDevTools.Margin = new System.Windows.Forms.Padding(4);
            this.btnDevTools.Name = "btnDevTools";
            this.btnDevTools.Size = new System.Drawing.Size(135, 29);
            this.btnDevTools.TabIndex = 11;
            this.btnDevTools.Text = "Open Dev Tools";
            this.btnDevTools.UseVisualStyleBackColor = true;
            this.btnDevTools.Visible = false;
            this.btnDevTools.Click += new System.EventHandler(this.btnDevTools_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "两孔延时（秒）：";
            // 
            // txtDelaySeconds
            // 
            this.txtDelaySeconds.Location = new System.Drawing.Point(144, 51);
            this.txtDelaySeconds.Margin = new System.Windows.Forms.Padding(4);
            this.txtDelaySeconds.Name = "txtDelaySeconds";
            this.txtDelaySeconds.Size = new System.Drawing.Size(144, 25);
            this.txtDelaySeconds.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(296, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "样本数：";
            // 
            // txtSampleCnt
            // 
            this.txtSampleCnt.Location = new System.Drawing.Point(359, 16);
            this.txtSampleCnt.Margin = new System.Windows.Forms.Padding(4);
            this.txtSampleCnt.Name = "txtSampleCnt";
            this.txtSampleCnt.Size = new System.Drawing.Size(100, 25);
            this.txtSampleCnt.TabIndex = 17;
            this.txtSampleCnt.Text = "1";
            this.txtSampleCnt.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.String;
            this.txtSampleCnt.TextChanged += new System.EventHandler(this.txtSampleCnt_TextChanged);
            // 
            // btnWash
            // 
            this.btnWash.Enabled = false;
            this.btnWash.Location = new System.Drawing.Point(741, 15);
            this.btnWash.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnWash.Name = "btnWash";
            this.btnWash.Size = new System.Drawing.Size(100, 29);
            this.btnWash.TabIndex = 18;
            this.btnWash.Text = "冲洗";
            this.btnWash.UseVisualStyleBackColor = true;
            this.btnWash.Click += new System.EventHandler(this.btnWash_Click);
            // 
            // cmbProjectName
            // 
            this.cmbProjectName.FormattingEnabled = true;
            this.cmbProjectName.Location = new System.Drawing.Point(539, 18);
            this.cmbProjectName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbProjectName.Name = "cmbProjectName";
            this.cmbProjectName.Size = new System.Drawing.Size(160, 23);
            this.cmbProjectName.TabIndex = 19;
            this.cmbProjectName.SelectedIndexChanged += new System.EventHandler(this.cmbProjectName_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(468, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = "项目名：";
            // 
            // btnMoveFront
            // 
            this.btnMoveFront.Location = new System.Drawing.Point(1064, 13);
            this.btnMoveFront.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMoveFront.Name = "btnMoveFront";
            this.btnMoveFront.Size = new System.Drawing.Size(100, 29);
            this.btnMoveFront.TabIndex = 21;
            this.btnMoveFront.Text = "移动到前方";
            this.btnMoveFront.UseVisualStyleBackColor = true;
            this.btnMoveFront.Click += new System.EventHandler(this.btnMoveFront_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(958, 48);
            this.btnClearAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(100, 29);
            this.btnClearAll.TabIndex = 22;
            this.btnClearAll.Text = "反选";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(958, 14);
            this.btnSelectAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(100, 29);
            this.btnSelectAll.TabIndex = 23;
            this.btnSelectAll.Text = "全部选择";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnMoveToBack
            // 
            this.btnMoveToBack.Location = new System.Drawing.Point(1064, 47);
            this.btnMoveToBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMoveToBack.Name = "btnMoveToBack";
            this.btnMoveToBack.Size = new System.Drawing.Size(100, 29);
            this.btnMoveToBack.TabIndex = 24;
            this.btnMoveToBack.Text = "移动到后方";
            this.btnMoveToBack.UseVisualStyleBackColor = true;
            this.btnMoveToBack.Click += new System.EventHandler(this.btnMoveToBack_Click);
            // 
            // DripFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 734);
            this.Controls.Add(this.btnMoveToBack);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnMoveFront);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbProjectName);
            this.Controls.Add(this.btnWash);
            this.Controls.Add(this.txtSampleCnt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDelaySeconds);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDevTools);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cmbBoards);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelGrid);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DripFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "滴液";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.Load += new System.EventHandler(this.DripFrm_Load);
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
        private System.Windows.Forms.Button btnDevTools;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDelaySeconds;
        private System.Windows.Forms.Label label3;
        private TextBoxEx txtSampleCnt;
        private System.Windows.Forms.Button btnWash;
        private System.Windows.Forms.ComboBox cmbProjectName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnMoveFront;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnMoveToBack;
    }
}

