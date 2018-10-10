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
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnReverse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVolume = new VsmdWorkstation.Controls.TextBoxEx();
            this.grpBox = new System.Windows.Forms.GroupBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtExCount = new VsmdWorkstation.Controls.TextBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.grpBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGrid
            // 
            this.panelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGrid.Location = new System.Drawing.Point(9, 99);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(2);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(863, 437);
            this.panelGrid.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 23);
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
            this.cmbBoards.Location = new System.Drawing.Point(95, 17);
            this.cmbBoards.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBoards.Name = "cmbBoards";
            this.cmbBoards.Size = new System.Drawing.Size(130, 20);
            this.cmbBoards.TabIndex = 6;
            this.cmbBoards.SelectedIndexChanged += new System.EventHandler(this.cmbBoards_SelectedIndexChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(16, 48);
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
            this.btnStop.Location = new System.Drawing.Point(174, 48);
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
            this.btnPause.Location = new System.Drawing.Point(95, 48);
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
            this.btnRestGrid.Location = new System.Drawing.Point(372, 48);
            this.btnRestGrid.Name = "btnRestGrid";
            this.btnRestGrid.Size = new System.Drawing.Size(53, 23);
            this.btnRestGrid.TabIndex = 10;
            this.btnRestGrid.Text = "重置";
            this.btnRestGrid.UseVisualStyleBackColor = true;
            this.btnRestGrid.Click += new System.EventHandler(this.btnRestGrid_Click);
            // 
            // btnDevTools
            // 
            this.btnDevTools.Location = new System.Drawing.Point(431, 48);
            this.btnDevTools.Name = "btnDevTools";
            this.btnDevTools.Size = new System.Drawing.Size(101, 23);
            this.btnDevTools.TabIndex = 11;
            this.btnDevTools.Text = "Open Dev Tools";
            this.btnDevTools.UseVisualStyleBackColor = true;
            this.btnDevTools.Visible = false;
            this.btnDevTools.Click += new System.EventHandler(this.btnDevTools_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(254, 48);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(53, 23);
            this.btnSelectAll.TabIndex = 12;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnReverse
            // 
            this.btnReverse.Location = new System.Drawing.Point(313, 48);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(53, 23);
            this.btnReverse.TabIndex = 13;
            this.btnReverse.Text = "反选";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "体积：";
            // 
            // txtVolume
            // 
            this.txtVolume.Location = new System.Drawing.Point(284, 16);
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(96, 21);
            this.txtVolume.TabIndex = 15;
            this.txtVolume.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.UnsignedInterge;
            // 
            // grpBox
            // 
            this.grpBox.Controls.Add(this.btnSelect);
            this.grpBox.Controls.Add(this.label4);
            this.grpBox.Controls.Add(this.txtExCount);
            this.grpBox.Controls.Add(this.btnReverse);
            this.grpBox.Controls.Add(this.label3);
            this.grpBox.Controls.Add(this.btnSelectAll);
            this.grpBox.Controls.Add(this.label1);
            this.grpBox.Controls.Add(this.btnDevTools);
            this.grpBox.Controls.Add(this.txtVolume);
            this.grpBox.Controls.Add(this.btnRestGrid);
            this.grpBox.Controls.Add(this.cmbBoards);
            this.grpBox.Controls.Add(this.btnPause);
            this.grpBox.Controls.Add(this.label2);
            this.grpBox.Controls.Add(this.btnStop);
            this.grpBox.Controls.Add(this.btnStart);
            this.grpBox.Location = new System.Drawing.Point(9, 12);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(860, 82);
            this.grpBox.TabIndex = 16;
            this.grpBox.TabStop = false;
            this.grpBox.Text = "参数";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(621, 17);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(53, 23);
            this.btnSelect.TabIndex = 19;
            this.btnSelect.Text = "选择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(383, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "ml";
            // 
            // txtExCount
            // 
            this.txtExCount.Location = new System.Drawing.Point(495, 17);
            this.txtExCount.Name = "txtExCount";
            this.txtExCount.Size = new System.Drawing.Size(120, 21);
            this.txtExCount.TabIndex = 17;
            this.txtExCount.ValueType = VsmdWorkstation.Controls.TextBoxEx.TextBoxValueType.UnsignedInterge;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(424, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "样品数量：";
            // 
            // DripFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 545);
            this.Controls.Add(this.grpBox);
            this.Controls.Add(this.panelGrid);
            this.Name = "DripFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "滴液";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.Load += new System.EventHandler(this.DripFrm_Load);
            this.grpBox.ResumeLayout(false);
            this.grpBox.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.Label label2;
        private TextBoxEx txtVolume;
        private System.Windows.Forms.GroupBox grpBox;
        private TextBoxEx txtExCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelect;
    }
}

