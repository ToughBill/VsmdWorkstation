using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VsmdWorkstation.Controls
{
    public enum MessageType
    {
        Info,
        Warning,
        Error
    }
    public partial class StatusBarEx : StatusStrip
    {
        private ToolStripStatusLabel m_msgLabel;
        public StatusBarEx()
        {
            InitializeComponent();
            m_msgLabel = new ToolStripStatusLabel();
            m_msgLabel.Size = new System.Drawing.Size(0, 17);
            m_msgLabel.AutoSize = false;
            m_msgLabel.TextAlign = ContentAlignment.MiddleLeft;
            m_msgLabel.Text = "";
            this.Items.Add(m_msgLabel);
        }

        public void DisplayMessage(MessageType msgType, string msg, int duringTime = 5)
        {
            m_msgLabel.BackColor = GetColor(msgType);
            m_msgLabel.Width = this.Width-20;
            m_msgLabel.Text = msg;
            SetTimeout(duringTime*1000, new Action(delegate()
                {
                    Action action = delegate()
                    {
                        m_msgLabel.Width = 0;
                    };
                    this.Invoke(action);
                })
            );
        }
        private Color GetColor(MessageType msgType)
        {
            Color o = this.BackColor;
            switch (msgType)
            {
                case MessageType.Info:
                    o = Color.Green;
                    break;
                case MessageType.Warning:
                    o = Color.Yellow;
                    break;
                case MessageType.Error:
                    o = Color.Red;
                    break;
                default: break;
            }
            return o;
        }

        public void SetTimeout(double interval, Action action) 
        { 
            System.Timers.Timer timer = new System.Timers.Timer(interval); 
            timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e) 
            { 
                timer.Enabled = false; 
                action(); 
            }; 
            timer.Enabled = true; 
        } 
         public void SetInterval(double interval, Action<System.Timers.ElapsedEventArgs> action) 
         { 
             System.Timers.Timer timer = new System.Timers.Timer(interval); 
             timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e) 
             { 
                 action(e); 
             }; 
             timer.Enabled = true; 
         } 

    }
}
