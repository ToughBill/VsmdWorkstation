﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsmdWorkstation.Controls;

namespace VsmdWorkstation
{
    public class StatusBar
    {
        private static StatusBarEx m_StatusBar;
        public static void Init(StatusBarEx bar)
        {
            m_StatusBar = bar;
        }
        public static void DisplayMessage(MessageType msgType, string msg, int duringTime = 5)
        {
            m_StatusBar.DisplayMessage(msgType, msg, duringTime);
        }
    }
}
