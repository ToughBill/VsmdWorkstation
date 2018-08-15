using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VsmdWorkstation.Controls
{
    public partial class ButtonEx : Button
    {
        [Browsable(true)]
        public string ToolTip { get; set; }

        public ButtonEx()
        {
            InitializeComponent();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(this, ToolTip);
        }
    }
}
