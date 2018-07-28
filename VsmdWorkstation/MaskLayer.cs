using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VsmdWorkstation
{
    public partial class MaskLayer : UserControl
    {
        private Color BackGround_Color = Color.FromArgb(125, 255, 255, 255);
        public MaskLayer()
        {
            InitializeComponent();
            SetStyle(System.Windows.Forms.ControlStyles.Opaque, true);
            lblTip.BackColor = BackGround_Color;
            lblTip.Left = (this.Width - lblTip.Width) / 2;
            lblTip.Top = (this.Height - lblTip.Height) / 2;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;  // 开启 WS_EX_TRANSPARENT,使控件支持透明
                return cp;
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            float vlblControlWidth;
            float vlblControlHeight;

            Pen labelBorderPen;
            SolidBrush labelBackColorBrush;

            
            labelBorderPen = new Pen(Color.FromArgb(125, 255, 255, 255), 1);
            labelBackColorBrush = new SolidBrush(Color.FromArgb(125, 255, 255, 255));
            
            base.OnPaint(e);
            vlblControlWidth = this.Size.Width;
            vlblControlHeight = this.Size.Height;
            e.Graphics.DrawRectangle(labelBorderPen, 0, 0, vlblControlWidth, vlblControlHeight);
            e.Graphics.FillRectangle(labelBackColorBrush, 0, 0, vlblControlWidth, vlblControlHeight);
        }
    }
}
