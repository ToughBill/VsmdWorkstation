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
    public partial class TextBoxEx : TextBox
    {
        public enum TextBoxValueType
        {
            String,
            Interge,
            UnsignedInterge
        }

        public TextBoxEx()
        {
            InitializeComponent();
            ValueType = TextBoxValueType.String;
        }

        [Browsable(true)]

        public TextBoxValueType ValueType { get; set; }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if(ValueType == TextBoxValueType.UnsignedInterge)
            {
                if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            else if(ValueType == TextBoxValueType.Interge)
            {
                if (e.KeyChar != '-' && e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            
        }
    }

    
}
